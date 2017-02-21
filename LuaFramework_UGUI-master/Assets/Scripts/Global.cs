using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GlobalGame 
{
	public class Global 
	{
		public enum BattleAnimationType
		{
			Stand = 0,
			Run = 1,
			Hurt = 2,
			Attack = 3,
		}

		public static ArrayList m_AnimationNameList = new ArrayList(){"stand","run","","Attack"};

		public static string GetAnimation(BattleAnimationType argType)
		{
			return (string)m_AnimationNameList[(int)argType];
		}


	}
}
