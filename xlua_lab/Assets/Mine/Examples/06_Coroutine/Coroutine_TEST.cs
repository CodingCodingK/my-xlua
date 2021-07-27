using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Mine
{
    public class Coroutine_TEST : MonoBehaviour
    {
        // // Start is called before the first frame update
        // void Start()
        // {
        //     // var a = WaitAndPrintA(3f,2f);
        //     var a = WaitAndPrintB(2f);
        //     this.StartCoroutine(a);
        // }
        //
        // // Update is called once per frame
        // void Update()
        // {

        //     
        // }
        //
        // private IEnumerator WaitAndPrintA(float waitTimeA,float waitTimeB)
        // {
        //     
        //     yield return new WaitForSeconds(waitTimeA);
        //     print("WaitAndPrint A:" + Time.time);
        //     this.StartCoroutine(WaitAndPrintB(waitTimeB));
        // }
        //
        // private IEnumerator WaitAndPrintB(float waitTime)
        // {
        //     
        //     yield return new WaitForSeconds(waitTime);
        //     print("WaitAndPrint B:" + Time.time);
        // }
        
        LuaEnv luaenv = null;
        // Use this for initialization
        
        void Start()
        {
            luaenv = new LuaEnv();
            luaenv.DoString("require 'coruntine_test'");
        }

        // Update is called once per frame
        void Update()
        {
            if (luaenv != null)
            {
                luaenv.Tick();
            }
        }

        void OnDestroy()
        {
            luaenv.Dispose();
        }
    }
}

