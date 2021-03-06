﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class GlobalGame_WebManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(GlobalGame.WebManager), typeof(Manager));
		L.RegFunction("InitProtocol", InitProtocol);
		L.RegFunction("ReconnectServer", ReconnectServer);
		L.RegFunction("Send", Send);
		L.RegFunction("CMD", CMD);
		L.RegFunction("Connect", Connect);
		L.RegFunction("Disconnect", Disconnect);
		L.RegFunction("CloseWebSocket", CloseWebSocket);
		L.RegFunction("ShowBackToLoginConfirm", ShowBackToLoginConfirm);
		L.RegFunction("NeedLoading", NeedLoading);
		L.RegFunction("LoadingCheck", LoadingCheck);
		L.RegFunction("SelectUrl", SelectUrl);
		L.RegFunction("AddCmdHandler", AddCmdHandler);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("ws", get_ws, set_ws);
		L.RegVar("_OnCmds", get__OnCmds, set__OnCmds);
		L.RegVar("_toDoMessage", get__toDoMessage, set__toDoMessage);
		L.RegVar("_toDoOpen", get__toDoOpen, set__toDoOpen);
		L.RegVar("root", get_root, set_root);
		L.RegVar("cmdInfo", get_cmdInfo, set_cmdInfo);
		L.RegVar("reconnect", get_reconnect, set_reconnect);
		L.RegVar("disconnect", get_disconnect, set_disconnect);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitProtocol(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			string arg0 = ToLua.CheckString(L, 2);
			obj.InitProtocol(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ReconnectServer(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			obj.ReconnectServer();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Send(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			string arg0 = ToLua.CheckString(L, 2);
			obj.Send(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CMD(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			string arg0 = ToLua.CheckString(L, 2);
			SimpleJson.JsonObject arg1 = (SimpleJson.JsonObject)ToLua.CheckObject(L, 3, typeof(SimpleJson.JsonObject));
			obj.CMD(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Connect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 4);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			string arg0 = ToLua.CheckString(L, 2);
			object arg1 = ToLua.ToVarObject(L, 3);
			bool arg2 = LuaDLL.luaL_checkboolean(L, 4);
			obj.Connect(arg0, arg1, arg2);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Disconnect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			ushort arg0 = (ushort)LuaDLL.luaL_checknumber(L, 2);
			obj.Disconnect(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CloseWebSocket(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			ushort arg0 = (ushort)LuaDLL.luaL_checknumber(L, 2);
			obj.CloseWebSocket(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ShowBackToLoginConfirm(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			obj.ShowBackToLoginConfirm();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int NeedLoading(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			obj.NeedLoading();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadingCheck(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			obj.LoadingCheck();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SelectUrl(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			string o = obj.SelectUrl();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddCmdHandler(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)ToLua.CheckObject(L, 1, typeof(GlobalGame.WebManager));
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj.AddCmdHandler(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ws(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			WebSocketSharp.WebSocket ret = obj.ws;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ws on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__OnCmds(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			System.Collections.Generic.Dictionary<string,string> ret = obj._OnCmds;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _OnCmds on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__toDoMessage(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			System.Collections.Generic.Queue<GlobalGame.MessageInfo> ret = obj._toDoMessage;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _toDoMessage on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__toDoOpen(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			GlobalGame.OpenInfo ret = obj._toDoOpen;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _toDoOpen on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_root(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			System.Xml.XmlElement ret = obj.root;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index root on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cmdInfo(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			GlobalGame.MessageInfo ret = obj.cmdInfo;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cmdInfo on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_reconnect(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			bool ret = obj.reconnect;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index reconnect on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_disconnect(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			bool ret = obj.disconnect;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index disconnect on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ws(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			WebSocketSharp.WebSocket arg0 = (WebSocketSharp.WebSocket)ToLua.CheckObject(L, 2, typeof(WebSocketSharp.WebSocket));
			obj.ws = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index ws on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__OnCmds(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			System.Collections.Generic.Dictionary<string,string> arg0 = (System.Collections.Generic.Dictionary<string,string>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Dictionary<string,string>));
			obj._OnCmds = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _OnCmds on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__toDoMessage(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			System.Collections.Generic.Queue<GlobalGame.MessageInfo> arg0 = (System.Collections.Generic.Queue<GlobalGame.MessageInfo>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.Queue<GlobalGame.MessageInfo>));
			obj._toDoMessage = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _toDoMessage on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__toDoOpen(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			GlobalGame.OpenInfo arg0 = (GlobalGame.OpenInfo)ToLua.CheckObject(L, 2, typeof(GlobalGame.OpenInfo));
			obj._toDoOpen = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index _toDoOpen on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_root(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			System.Xml.XmlElement arg0 = (System.Xml.XmlElement)ToLua.CheckObject(L, 2, typeof(System.Xml.XmlElement));
			obj.root = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index root on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cmdInfo(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			GlobalGame.MessageInfo arg0 = (GlobalGame.MessageInfo)ToLua.CheckObject(L, 2, typeof(GlobalGame.MessageInfo));
			obj.cmdInfo = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cmdInfo on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_reconnect(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.reconnect = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index reconnect on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_disconnect(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			GlobalGame.WebManager obj = (GlobalGame.WebManager)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.disconnect = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index disconnect on a nil value" : e.Message);
		}
	}
}

