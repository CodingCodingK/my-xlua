using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Mine
{
    public class LuaCallCSMethod : MonoBehaviour
    {
        private const string script = @"
        local father = CS.Mine.MyTestClass()
        local child1 = CS.Mine.MyTestClass1()
        local child2 = CS.Mine.MyTestClass2()

        father:Method_NoInput()
        child1:Method_NoInput()
        child1:GenericMethod_OneInput()
        child1:GenericMethod_TwoInput(child2)
        -- invalid below
        -- child1:GenericMethod_TwoInput(father)
        -- child1:UnsupportedMethod2()
";
        private LuaEnv luaenv = new LuaEnv();
    
        void Start()
        {
            luaenv.DoString(script);

            // var x = new Foo1Child();
            // x.PlainExtension();
        }

        private void Update()
        {
            if (luaenv != null)
                luaenv.Tick();
        }

        private void OnDestroy()
        {
            luaenv.Dispose();
        }
    }

}
