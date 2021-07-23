//public class backup_LuaBehaviour : MonoBehaviour
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mine
{
    /// <summary>
    /// 序列化这个类，是为了实现在unity面板上编辑
    /// </summary>
    [System.Serializable]
    public class Injection_backup
    {
        public string name;
        public GameObject value;
    }
    
    public class backup_LuaBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 把lua.txt放到这里，被脚本执行
        /// </summary>
        public TextAsset luaScript;
    
        /// <summary>
        /// 把游戏物体拖拽到这里，注入
        /// </summary>
        public Injection_backup[] injections;

        private Light lightCpnt;

        // Start is called before the first frame update
        void Start()
        {
            lightCpnt = injections.FirstOrDefault(o => o.name.Equals("my_light"))?.value.GetComponent<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * 10);
            lightCpnt.color = new Color(Mathf.Sin(Time.time) / 2f + 0.5f, 0, 0, 1);
        }
    }
}

