using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Mine
{
    [GCOptimize(OptimizeFlag.PackAsTable)]
    public struct PushAsTableStruct
    {
        public int x;
        public int y;
    }
    
    public class ReImplementInLua : MonoBehaviour
    {
        LuaEnv luaenv = new LuaEnv();

        void Start()
        {
            //这两个例子都必须生成代码才能正常运行
            //例子1：改造Vector3
            //沿用Vector3原来的映射方案Vector3 -> userdata，但是把Vector3的方法实现改为lua实现，通过xlua.genaccessor实现不经过C#直接操作内存
            //改为不经过C#的好处是性能更高，而且你可以省掉相应的生成代码以达成省text段的效果
            //仍然沿用映射方案的好处是userdata比table更省内存，但操作字段比table性能稍低，当然，你也可以结合例子2的思路，把Vector3也改为映射到table
            
            // 方法1：改造Vector3
            luaenv.DoString("require 'ReImplementVector3'");

            Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            //　方法2：struct映射到table改造
            //PushAsTableStruct传送到lua侧将会是table，例子里头还为这个table添加了一个成员方法SwapXY，静态方法Print，打印格式化，以及构造函数
            luaenv.DoString("require 'ReImplementPushAsTableStruct'");
            
            PushAsTableStruct test;
            test.x = 100;
            test.y = 200;
            luaenv.Global.Set("from_cs", test);
            
            luaenv.DoString(@"
            print('--------------from csharp---------------------')
            assert(type(from_cs) == 'table','A is not a Table !')
            print(from_cs)
            CS.Mine.PushAsTableStruct.Print(from_cs)
            from_cs:SwapXY()
            print(from_cs)

            print('--------------from lua---------------------')
            local from_lua = CS.Mine.PushAsTableStruct(4, 5)
            assert(type(from_lua) == 'table','B is not a Table !')
            print(from_lua)
            CS.Mine.PushAsTableStruct.Print(from_lua)
            from_lua:SwapXY()
            print(from_lua)
        ");
            print(test);
            Debug.Log(test);
            luaenv.Dispose();
        }
        
    }
}

