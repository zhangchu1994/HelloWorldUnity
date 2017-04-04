﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class SimpleJson_JsonObjectWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(SimpleJson.JsonObject), typeof(System.Object));
		L.RegFunction(".geti", get_Item);
		L.RegFunction("get_Item", get_Item);
		L.RegFunction("Add", Add);
		L.RegFunction("ContainsKey", ContainsKey);
		L.RegFunction("Clone", Clone);
		L.RegFunction("Remove", Remove);
		L.RegFunction("TryGetValue", TryGetValue);
		L.RegFunction("set_Item", set_Item);
		L.RegFunction("TryGet", TryGet);
		L.RegFunction("TrySet", TrySet);
		L.RegFunction("Clear", Clear);
		L.RegFunction("Contains", Contains);
		L.RegFunction("CopyTo", CopyTo);
		L.RegFunction("GetEnumerator", GetEnumerator);
		L.RegFunction("ToString", ToString);
		L.RegFunction("New", _CreateSimpleJson_JsonObject);
		L.RegVar("this", _this, null);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Keys", get_Keys, null);
		L.RegVar("KeysArray", get_KeysArray, null);
		L.RegVar("Values", get_Values, null);
		L.RegVar("Count", get_Count, null);
		L.RegVar("IsReadOnly", get_IsReadOnly, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSimpleJson_JsonObject(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				SimpleJson.JsonObject obj = new SimpleJson.JsonObject();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(System.Collections.Generic.IEqualityComparer<string>)))
			{
				System.Collections.Generic.IEqualityComparer<string> arg0 = (System.Collections.Generic.IEqualityComparer<string>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.IEqualityComparer<string>));
				SimpleJson.JsonObject obj = new SimpleJson.JsonObject(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: SimpleJson.JsonObject.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _get_this(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(string)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				object o = obj[arg0];
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(int)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				object o = obj[arg0];
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to operator method: SimpleJson.JsonObject.this");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _set_this(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			string arg0 = ToLua.CheckString(L, 2);
			object arg1 = ToLua.ToVarObject(L, 3);
			obj[arg0] = arg1;
			return 0;

		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _this(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushvalue(L, 1);
			LuaDLL.tolua_bindthis(L, _get_this, _set_this);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(string)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				object o = obj[arg0];
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(int)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				object o = obj[arg0];
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: SimpleJson.JsonObject.get_Item");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Add(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(System.Collections.Generic.KeyValuePair<string,object>)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				System.Collections.Generic.KeyValuePair<string,object> arg0 = (System.Collections.Generic.KeyValuePair<string,object>)ToLua.ToObject(L, 2);
				obj.Add(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(string), typeof(object)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				object arg1 = ToLua.ToVarObject(L, 3);
				obj.Add(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: SimpleJson.JsonObject.Add");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ContainsKey(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			string arg0 = ToLua.CheckString(L, 2);
			bool o = obj.ContainsKey(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clone(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			SimpleJson.JsonObject o = obj.Clone();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(System.Collections.Generic.KeyValuePair<string,object>)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				System.Collections.Generic.KeyValuePair<string,object> arg0 = (System.Collections.Generic.KeyValuePair<string,object>)ToLua.ToObject(L, 2);
				bool o = obj.Remove(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(string)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				bool o = obj.Remove(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: SimpleJson.JsonObject.Remove");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TryGetValue(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			string arg0 = ToLua.CheckString(L, 2);
			object arg1 = null;
			bool o = obj.TryGetValue(arg0, out arg1);
			LuaDLL.lua_pushboolean(L, o);
			ToLua.Push(L, arg1);
			return 2;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Item(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			string arg0 = ToLua.CheckString(L, 2);
			object arg1 = ToLua.ToVarObject(L, 3);
			obj[arg0] = arg1;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TryGet(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(SimpleJson.JsonObject), typeof(string)))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				object o = obj.TryGet(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (TypeChecker.CheckParamsType(L, typeof(object), 2, count - 1))
			{
				SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.ToObject(L, 1);
				object[] arg0 = ToLua.ToParamsObject(L, 2, count - 1);
				object o = obj.TryGet(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: SimpleJson.JsonObject.TryGet");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TrySet(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			object[] arg0 = ToLua.ToParamsObject(L, 2, count - 1);
			bool o = obj.TrySet(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			obj.Clear();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Contains(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			System.Collections.Generic.KeyValuePair<string,object> arg0 = (System.Collections.Generic.KeyValuePair<string,object>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.KeyValuePair<string,object>));
			bool o = obj.Contains(arg0);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CopyTo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			System.Collections.Generic.KeyValuePair<string,object>[] arg0 = ToLua.CheckObjectArray<System.Collections.Generic.KeyValuePair<string,object>>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.CopyTo(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<string,object>> o = obj.GetEnumerator();
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)ToLua.CheckObject(L, 1, typeof(SimpleJson.JsonObject));
			string o = obj.ToString();
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keys(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)o;
			System.Collections.Generic.ICollection<string> ret = obj.Keys;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Keys on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_KeysArray(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)o;
			string[] ret = obj.KeysArray;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index KeysArray on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Values(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)o;
			System.Collections.Generic.ICollection<object> ret = obj.Values;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Values on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)o;
			int ret = obj.Count;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index Count on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsReadOnly(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SimpleJson.JsonObject obj = (SimpleJson.JsonObject)o;
			bool ret = obj.IsReadOnly;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index IsReadOnly on a nil value" : e.Message);
		}
	}
}

