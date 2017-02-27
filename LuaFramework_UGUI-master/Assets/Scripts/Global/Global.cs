using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GlobalGame 
{
	public class Global 
	{
		public static string TagName_Enemy = "Enemy";
		public static string TagName_Ground = "Ground";
		public static string ActorName = "Ian1970";

		public enum BattleAnimationType
		{
			Stand = 0,
			Run = 1,
			Hurt = 2,
			Attack = 3,
		}

		public static ArrayList m_AnimationNameList = new ArrayList(){"stand","run","","attack1"};

		public static string GetAnimation(BattleAnimationType argType)
		{
			return (string)m_AnimationNameList[(int)argType];
		}

		//缩放粒子及模型
		public static void ChangeParticleScale(GameObject Obj,float SetScale)
		{
			if (SetScale > 0) 
			{
				ParticleSystem[] systems = Obj.GetComponentsInChildren<ParticleSystem> ();

				for (int i = 0; i < systems.Length; i++ )
				{
					systems[i].startSize = systems[i].startSize * SetScale;
					systems[i].startSpeed = systems[i].startSpeed * SetScale;
				}

				Obj.transform.localScale *= SetScale;
			}
		}

	}
}
