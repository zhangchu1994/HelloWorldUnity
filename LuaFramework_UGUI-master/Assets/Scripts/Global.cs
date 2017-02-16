using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Global 
{
	public enum BattleAnimationType
	{
		Stand = 0,
		Run = 1,
		Hurt = 2,
	}

	public static ArrayList m_AnimationNameList = new ArrayList(){"stand","run",""};

	public static string GetAnimation(BattleAnimationType argType)
	{
		return (string)m_AnimationNameList[(int)argType];
	}
}
