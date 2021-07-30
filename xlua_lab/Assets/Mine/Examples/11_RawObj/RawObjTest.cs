using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Mine
{
    public class RawObjTest : MonoBehaviour
    {
        private LuaEnv luaenv = new LuaEnv();
        
        public static void PrintType(object o)
        {
            Debug.Log("type:" + o.GetType() + ", value:" + o);
        }

        // Use this for initialization
        void Start()
        {
            //直接传1234到一个object参数，xLua将选择能保留最大精度的long来传递
            luaenv.DoString("CS.XLuaTest.RawObjectTest.PrintType(1234)");
            
            //通过拆装箱实现的保留数据类型，需要再Cast里加接口。
            luaenv.DoString("CS.XLuaTest.RawObjectTest.PrintType(CS.XLua.Cast.Int32(1234))");
            
            luaenv.Dispose();
        }
    }
}

