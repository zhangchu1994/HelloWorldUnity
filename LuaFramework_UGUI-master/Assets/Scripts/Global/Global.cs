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
		public static string TagName_Actor = "Actor";

//		public static string ActorName = "Ian1970";

		public enum BattleAnimationType
		{
			Stand = 0,
			Run = 1,
			Hurt = 2,
			Attack = 3,
			Dead = 10,
		}

		public static ArrayList m_ActorNameList = new ArrayList(){"Actor1","Actor2","Actor3","Actor4","Actor5"};
		public static ArrayList m_MonsterNameList = new ArrayList(){"Monster1","Monster2","Monster3","Monster4","Monster5"};
		public static ArrayList m_AnimationNameList = new ArrayList(){"stand","run","hurt","attack1","4","5","6","7","8","9","die"};

		public static List<BattleAnimationType> GetAnimRestoreList(BattleAnimationType argType)
		{
			List<BattleAnimationType> animationList = new List<BattleAnimationType> ();
			animationList.Add(argType);
			animationList.Add(BattleAnimationType.Stand);

			return animationList;
		}

		public static string GetActorNmae(int index)
		{
			return (string)m_ActorNameList[index];
		}

		public static string GetMonsterNmae(int index)
		{
			return (string)m_MonsterNameList[index];
		}

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

		public static void BattleLog(Actor actor,string argString)
		{
			if (actor != null && actor.name == "Actor1")
				Debug.Log ("Nmae = "+actor.name+" Info = "+argString);
		}

	}
}
