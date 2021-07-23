using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XLua;

namespace Mine
{
    /// <summary>
    /// 序列化这个类，是为了实现在unity面板上编辑
    /// </summary>
    [System.Serializable]
    public class Injection
    {
        public string name;
        public GameObject value;
    }
    
    public class LuaBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 把lua.txt放到这里，被脚本执行
        /// </summary>
        public TextAsset luaScript;
    
        /// <summary>
        /// 把游戏物体拖拽到这里，注入
        /// </summary>
        public Injection[] injections;
        
        private static LuaEnv luaEnv = new LuaEnv();
        internal static float lastGCTime = 0;
        internal const float GCInterval = 1;//1 second 

        private Action luaAwake;
        private Action luaStart;
        private Action luaUpdate;
        private Action luaOnDestroy;
        
        private LuaTable scriptEnv;

        //private Light lightCpnt;

        void Awake()
        {
            // TODO 需要进一步理解
            scriptEnv = luaEnv.NewTable();

            // 1.为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();
            
            scriptEnv.Set("self",this);
            // 2.注入GameObject到Lua可调用栈中
            //lightCpnt = injections.FirstOrDefault(o => o.name.Equals("my_light"))?.value.GetComponent<Light>();
            foreach (var injection in injections)
            {
                scriptEnv.Set(injection.name,injection.value);
            }
            
            // 3.脚本解析执行lua
            luaEnv.DoString(luaScript.text, "LuaBehaviourScript", scriptEnv);
            
            scriptEnv.Get("awake",out luaAwake);
            scriptEnv.Get("start", out luaStart);
            scriptEnv.Get("update", out luaUpdate);
            scriptEnv.Get("ondestroy", out luaOnDestroy);

        }
        
        void Start()
        {
            if (luaStart != null)
            {
                luaStart();
            }
        }
        
        void Update()
        {
            //this.transform.Rotate(Vector3.up * Time.deltaTime * 10);
            //lightCpnt.color = new Color(Mathf.Sin(Time.time) / 2f + 0.5f, 0, 0, 1);
            
            if (luaUpdate != null)
            {
                luaUpdate();
            }
            // TODO 垃圾回收
            if (Time.time - LuaBehaviour.lastGCTime > GCInterval)
            {
                luaEnv.Tick();
                LuaBehaviour.lastGCTime = Time.time;
            }
        }

        void OnDestroy()
        {
            if (luaOnDestroy != null)
            {
                luaOnDestroy();
            }
            luaOnDestroy = null;
            luaUpdate = null;
            luaStart = null;
            scriptEnv.Dispose();
            injections = null;
        }
    }
}

