using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using XLua;

namespace Mine
{
    public class InvokeLua : MonoBehaviour
    {
        public TextAsset luaScript;
        public class PropertyChangedEventArgs : EventArgs
        {
            public string name;
            public object value;
        }
        
        /// <summary>
        /// 下面的属性都会在Namespace_Class_InterfaceNameBridge文件中体现
        /// </summary>
        [CSharpCallLua]
        public interface ICalc
        {
            event EventHandler<PropertyChangedEventArgs> PropertyChanged;

            int Add(int a, int b);
            int Mult { get; set; }

            object this[int index] { get; set; }
            
        }
        
        /// <summary>
        /// DelegatesGensBridges文件中会统一生成代码获取
        /// </summary>
        [CSharpCallLua]
        public delegate ICalc CalcNew(int mult, params string[] args);
        
        
        void Start()
        {
            var luaenv = new LuaEnv();
            luaenv.DoString(luaScript.text);
            CalcNew calc_new = luaenv.Global.GetInPath<CalcNew>("Calc.New");
            ICalc calc = calc_new(10,"1","2");
            Debug.Log("-----改写后的索引器，会去调用get_item方法,不会空指针----");
            Debug.Log(calc[0]);
            calc[0] = "calc[0] changed";
            Debug.Log(calc[0]);
            Debug.Log(calc[2]);
            Debug.Log(calc[100]);
            Debug.Log("-----改写后的属性，对应同名lua中的key----");
            Debug.Log(calc.Mult);
            Debug.Log("-----改写后的方法，对应同名lua中的key----");
            Debug.Log("(5+5)*10="+calc.Add(5,5));
            calc.Mult = 20;
            Debug.Log("(5+5)*20="+calc.Add(5,5));
            Debug.Log("-----改写后的委托事件，会生成Bridge代码，重写其add和remove为反射调用lua的方法----");
            calc.PropertyChanged += Notify;
            calc[0] = 333;
            Debug.Log(calc[0]);
            calc.PropertyChanged -= Notify;
        }
        
        void Notify(object sender, PropertyChangedEventArgs e)
        {
            Debug.Log(string.Format("修改了数组属性，{0} = {1}", e.name, e.value));
        }

    }

}
