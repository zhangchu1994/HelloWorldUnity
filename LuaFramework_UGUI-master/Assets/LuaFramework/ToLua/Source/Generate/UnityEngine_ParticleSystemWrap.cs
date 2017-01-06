﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class UnityEngine_ParticleSystemWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(UnityEngine.ParticleSystem), typeof(UnityEngine.Component));
		L.RegFunction("SetParticles", SetParticles);
		L.RegFunction("GetParticles", GetParticles);
		L.RegFunction("Simulate", Simulate);
		L.RegFunction("Play", Play);
		L.RegFunction("Stop", Stop);
		L.RegFunction("Pause", Pause);
		L.RegFunction("Clear", Clear);
		L.RegFunction("IsAlive", IsAlive);
		L.RegFunction("Emit", Emit);
		L.RegFunction("New", _CreateUnityEngine_ParticleSystem);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("startDelay", get_startDelay, set_startDelay);
		L.RegVar("isPlaying", get_isPlaying, null);
		L.RegVar("isStopped", get_isStopped, null);
		L.RegVar("isPaused", get_isPaused, null);
		L.RegVar("loop", get_loop, set_loop);
		L.RegVar("playOnAwake", get_playOnAwake, set_playOnAwake);
		L.RegVar("time", get_time, set_time);
		L.RegVar("duration", get_duration, null);
		L.RegVar("playbackSpeed", get_playbackSpeed, set_playbackSpeed);
		L.RegVar("particleCount", get_particleCount, null);
		L.RegVar("enableEmission", get_enableEmission, set_enableEmission);
		L.RegVar("emissionRate", get_emissionRate, set_emissionRate);
		L.RegVar("startSpeed", get_startSpeed, set_startSpeed);
		L.RegVar("startSize", get_startSize, set_startSize);
		L.RegVar("startColor", get_startColor, set_startColor);
		L.RegVar("startRotation", get_startRotation, set_startRotation);
		L.RegVar("startLifetime", get_startLifetime, set_startLifetime);
		L.RegVar("gravityModifier", get_gravityModifier, set_gravityModifier);
		L.RegVar("maxParticles", get_maxParticles, set_maxParticles);
		L.RegVar("simulationSpace", get_simulationSpace, set_simulationSpace);
		L.RegVar("randomSeed", get_randomSeed, set_randomSeed);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_ParticleSystem(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				UnityEngine.ParticleSystem obj = new UnityEngine.ParticleSystem();
				ToLua.Push(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: UnityEngine.ParticleSystem.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetParticles(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.CheckObject(L, 1, typeof(UnityEngine.ParticleSystem));
			UnityEngine.ParticleSystem.Particle[] arg0 = ToLua.CheckObjectArray<UnityEngine.ParticleSystem.Particle>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.SetParticles(arg0, arg1);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetParticles(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.CheckObject(L, 1, typeof(UnityEngine.ParticleSystem));
			UnityEngine.ParticleSystem.Particle[] arg0 = ToLua.CheckObjectArray<UnityEngine.ParticleSystem.Particle>(L, 2);
			int o = obj.GetParticles(arg0);
			LuaDLL.lua_pushinteger(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Simulate(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(float)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
				obj.Simulate(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(float), typeof(bool)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
				bool arg1 = LuaDLL.lua_toboolean(L, 3);
				obj.Simulate(arg0, arg1);
				return 0;
			}
			else if (count == 4 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(float), typeof(bool), typeof(bool)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				float arg0 = (float)LuaDLL.lua_tonumber(L, 2);
				bool arg1 = LuaDLL.lua_toboolean(L, 3);
				bool arg2 = LuaDLL.lua_toboolean(L, 4);
				obj.Simulate(arg0, arg1, arg2);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Simulate");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				obj.Play();
				return 0;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(bool)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				bool arg0 = LuaDLL.lua_toboolean(L, 2);
				obj.Play(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Play");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Stop(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				obj.Stop();
				return 0;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(bool)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				bool arg0 = LuaDLL.lua_toboolean(L, 2);
				obj.Stop(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Stop");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Pause(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				obj.Pause();
				return 0;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(bool)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				bool arg0 = LuaDLL.lua_toboolean(L, 2);
				obj.Pause(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Pause");
			}
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
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				obj.Clear();
				return 0;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(bool)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				bool arg0 = LuaDLL.lua_toboolean(L, 2);
				obj.Clear(arg0);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Clear");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsAlive(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				bool o = obj.IsAlive();
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(bool)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				bool arg0 = LuaDLL.lua_toboolean(L, 2);
				bool o = obj.IsAlive(arg0);
				LuaDLL.lua_pushboolean(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.IsAlive");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Emit(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(UnityEngine.ParticleSystem.Particle)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				UnityEngine.ParticleSystem.Particle arg0 = (UnityEngine.ParticleSystem.Particle)ToLua.ToObject(L, 2);
				obj.Emit(arg0);
				return 0;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(int)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				obj.Emit(arg0);
				return 0;
			}
			else if (count == 6 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.ParticleSystem), typeof(UnityEngine.Vector3), typeof(UnityEngine.Vector3), typeof(float), typeof(float), typeof(UnityEngine.Color32)))
			{
				UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)ToLua.ToObject(L, 1);
				UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
				UnityEngine.Vector3 arg1 = ToLua.ToVector3(L, 3);
				float arg2 = (float)LuaDLL.lua_tonumber(L, 4);
				float arg3 = (float)LuaDLL.lua_tonumber(L, 5);
				UnityEngine.Color32 arg4 = (UnityEngine.Color32)ToLua.ToObject(L, 6);
				obj.Emit(arg0, arg1, arg2, arg3, arg4);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: UnityEngine.ParticleSystem.Emit");
			}
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
	static int get_startDelay(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.startDelay;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startDelay on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPlaying(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool ret = obj.isPlaying;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isPlaying on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isStopped(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool ret = obj.isStopped;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isStopped on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPaused(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool ret = obj.isPaused;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isPaused on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_loop(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool ret = obj.loop;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index loop on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_playOnAwake(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool ret = obj.playOnAwake;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index playOnAwake on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_time(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.time;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index time on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_duration(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.duration;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index duration on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_playbackSpeed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.playbackSpeed;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index playbackSpeed on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_particleCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			int ret = obj.particleCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index particleCount on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enableEmission(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool ret = obj.enableEmission;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index enableEmission on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_emissionRate(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.emissionRate;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index emissionRate on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startSpeed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.startSpeed;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startSpeed on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.startSize;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startColor(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			UnityEngine.Color ret = obj.startColor;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startColor on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startRotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.startRotation;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startRotation on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_startLifetime(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.startLifetime;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startLifetime on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gravityModifier(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float ret = obj.gravityModifier;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index gravityModifier on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxParticles(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			int ret = obj.maxParticles;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxParticles on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_simulationSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			UnityEngine.ParticleSystemSimulationSpace ret = obj.simulationSpace;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index simulationSpace on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_randomSeed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			uint ret = obj.randomSeed;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index randomSeed on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startDelay(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.startDelay = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startDelay on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_loop(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.loop = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index loop on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_playOnAwake(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.playOnAwake = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index playOnAwake on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_time(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.time = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index time on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_playbackSpeed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.playbackSpeed = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index playbackSpeed on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_enableEmission(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.enableEmission = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index enableEmission on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_emissionRate(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.emissionRate = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index emissionRate on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startSpeed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.startSpeed = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startSpeed on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startSize(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.startSize = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startSize on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startColor(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			UnityEngine.Color arg0 = ToLua.ToColor(L, 2);
			obj.startColor = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startColor on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startRotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.startRotation = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startRotation on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_startLifetime(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.startLifetime = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index startLifetime on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gravityModifier(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.gravityModifier = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index gravityModifier on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxParticles(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.maxParticles = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index maxParticles on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_simulationSpace(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			UnityEngine.ParticleSystemSimulationSpace arg0 = (UnityEngine.ParticleSystemSimulationSpace)ToLua.CheckObject(L, 2, typeof(UnityEngine.ParticleSystemSimulationSpace));
			obj.simulationSpace = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index simulationSpace on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_randomSeed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			UnityEngine.ParticleSystem obj = (UnityEngine.ParticleSystem)o;
			uint arg0 = (uint)LuaDLL.luaL_checknumber(L, 2);
			obj.randomSeed = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index randomSeed on a nil value" : e.Message);
		}
	}
}

