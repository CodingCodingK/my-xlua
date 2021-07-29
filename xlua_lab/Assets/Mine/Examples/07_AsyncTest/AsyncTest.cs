using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Mine
{
    public class AsyncTest : MonoBehaviour
    {
        private LuaEnv luaenv = new LuaEnv();
        // Start is called before the first frame update
        void Start()
        {
            luaenv.DoString("require 'async_test'");
        }

        // Update is called once per frame
        void Update()
        {
            luaenv?.Tick();
        }
    }

}
