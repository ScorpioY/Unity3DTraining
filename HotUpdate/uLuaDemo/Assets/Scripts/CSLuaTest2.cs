﻿using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class CSLuaTest2 : MonoBehaviour {

    /// <summary>
    /// lua虚拟机
    /// </summary>
    private LuaState luaState;


    // Use this for initialization
    void Start () {

        //创建lua虚拟机
        luaState = new LuaState();
        DelegateFactory.Init();
        luaState.Start();

        //在lua虚拟机(全局)中注册自定义函数
        luaState.BeginModule(null);
        CSFunctionTestWrap.Register(luaState);
        luaState.EndModule();

        //加载lua脚本
        this.luaState.DoFile(Application.streamingAssetsPath + "/MyLua2.lua");

        //加载完lua以后，获取lua中的函数，并执行。（此lua脚本回调了C#中的方法）
        LuaFunction luaFunction = luaState.GetFunction("LuaFunction");
        luaFunction.BeginPCall();
        luaFunction.Push("张三");
        luaFunction.PCall();
        luaFunction.EndPCall();

        luaFunction = luaState.GetFunction("LuaAdd");
        luaFunction.BeginPCall();
        luaFunction.Push(100);
        luaFunction.PCall();
        luaFunction.EndPCall();

        luaFunction.Dispose();
        luaState.Dispose();
        luaFunction = null;
        luaState = null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
