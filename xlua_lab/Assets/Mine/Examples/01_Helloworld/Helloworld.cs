using UnityEngine;
using XLua;

namespace Mine
{
    public class Helloworld : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var luaEnv = new LuaEnv();
            luaEnv.DoString("CS.UnityEngine.Debug.Log('Test !')");
            luaEnv.Dispose();
        }
        
    }
}


