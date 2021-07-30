using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Mine
{
    public class ReImplementInLua : MonoBehaviour
    {
        LuaEnv luaenv = new LuaEnv();

        void Start()
        {
            luaenv.DoString("");
        }
    }
}

