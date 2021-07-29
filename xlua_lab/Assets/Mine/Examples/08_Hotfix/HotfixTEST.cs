using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Mine
{
    [LuaCallCSharp]
    [Hotfix]
    public class HotfixTEST : MonoBehaviour
    {
        private LuaEnv luaenv = new LuaEnv();

        private int counter = 0;

        void Start()
        {
            luaenv.DoString("require 'HotfixTest'");
        }
        
        void Update()
        {
            if (++counter % 100 == 0)
            {
                Debug.Log("From C# " + counter + ",hotfixCounter = " );
            }
        }
    }
}

