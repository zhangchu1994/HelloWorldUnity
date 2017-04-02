﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class LuaFramework_LuaHelperWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("LuaHelper");
		L.RegFunction("GetType", GetType);
		L.RegFunction("GetPanelManager", GetPanelManager);
		L.RegFunction("GetSceneManager", GetSceneManager);
		L.RegFunction("GetResManager", GetResManager);
		L.RegFunction("GetWebManager", GetWebManager);
		L.RegFunction("GetSoundManager", GetSoundManager);
		L.RegFunction("OnCallLuaFunc", OnCallLuaFunc);
		L.RegFunction("OnJsonCallFunc", OnJsonCallFunc);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			string arg0 = ToLua.CheckString(L, 1);
			System.Type o = LuaFramework.LuaHelper.GetType(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPanelManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			LuaFramework.PanelManager o = LuaFramework.LuaHelper.GetPanelManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSceneManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			LuaFramework.MySceneManager o = LuaFramework.LuaHelper.GetSceneManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetResManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			LuaFramework.ResourceManager o = LuaFramework.LuaHelper.GetResManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetWebManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			GlobalGame.WebManager o = LuaFramework.LuaHelper.GetWebManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSoundManager(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 0);
			LuaFramework.SoundManager o = LuaFramework.LuaHelper.GetSoundManager();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnCallLuaFunc(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			LuaByteBuffer arg0 = new LuaByteBuffer(ToLua.CheckByteBuffer(L, 1));
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 2);
			LuaFramework.LuaHelper.OnCallLuaFunc(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnJsonCallFunc(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			string arg0 = ToLua.CheckString(L, 1);
			LuaFunction arg1 = ToLua.CheckLuaFunction(L, 2);
			LuaFramework.LuaHelper.OnJsonCallFunc(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

