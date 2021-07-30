using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;

namespace Mine
{
    [LuaCallCSharp]
    public class MyTestClass
    {
        
    }
    
    [LuaCallCSharp]
    public class MyTestClass1 : MyTestClass
    {
        
    }
    
    [LuaCallCSharp]
    public class MyTestClass2 : MyTestClass
    {
        
    }

    // public class TestClass
    // {
    //
    // }
    
    [LuaCallCSharp]
    public static class MyExtension
    {
        public static void Method_NoInput(this MyTestClass x)
        {
            Debug.Log("Method_NoInput : " + x.GetType());
        }
        
        public static void GenericMethod_OneInput<T>(this T x) where T:MyTestClass
        {
            Debug.Log("GenericMethod_OneInput : " + typeof(T));
        }
        
        public static void GenericMethod_TwoInput<T1,T2>(this T1 x1,T2 x2) where T1:MyTestClass1 where T2:MyTestClass2
        {
            Debug.Log("GenericMethod_TwoInput : " + typeof(T1) + "," + typeof(T2));
        }
        
        #region Unsupported methods

        /// <summary>
        /// 不支持生成lua的泛型方法（没有泛型约束）
        /// </summary>
        public static void UnsupportedMethod1<T>(T a)
        {
            Debug.Log("UnsupportedMethod1");
        }

        /// <summary>
        /// 不支持生成lua的泛型方法（缺少带约束的泛型参数）
        /// </summary>
        public static void UnsupportedMethod2<T>() where T : MyTestClass
        {
            Debug.Log(string.Format("UnsupportedMethod2<{0}>", typeof(T)));
        }

        /// <summary>
        /// 不支持生成lua的泛型方法（泛型约束必须为class）
        /// </summary>
        public static void UnsupportedMethod3<T>(T a) where T : IDisposable
        {
            Debug.Log(string.Format("UnsupportedMethod3<{0}>", typeof(T)));
        }

        #endregion
    }
}
