using UnityEngine;
using System.Collections;
using LuaInterface;
using System;

public class ScriptsFromFile_02 : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //LuaState l = new LuaState();
        //LuaDLL.luaopen_socket_core(l.L);
        //l.DoFile("C:/Users/Administrator/Documents/New Unity Project/Assets/uLua/Lua/main.lua");

        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.DoFile("main");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
