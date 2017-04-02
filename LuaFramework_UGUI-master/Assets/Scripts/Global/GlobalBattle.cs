using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GlobalGame 
{
	public class GlobalBattle 
	{
		public static GameObject GetCloseMonsterObj(GameObject selfObj,Actor.ActorType actorType)//目前用于主角寻路和Monster的选择目标
		{
			List<GameObject> objList = null;
			List<Actor> actorList = null;

			if (actorType == Actor.ActorType.Actor) 
			{
				objList = BattleScene.Active.m_actorObjList;
				actorList = BattleScene.Active.m_actorList;
			} 
			else if (actorType == Actor.ActorType.Monster) 
			{
				objList = BattleScene.Active.m_monsterObjList;
				actorList = BattleScene.Active.m_monsterList;
			}

			GameObject closeMonster = null;
			float closeDis = 100000;
			for (int i = 0; i < objList.Count; i++) //m_actorList.Count
			{
				GameObject monsterObj = objList [i];
				Actor monster = actorList [i];
				if (monsterObj == null || monster.IsActorStatus (Actor.ActorStatus.Dead) == true)
					continue;

				float dis = Vector3.Distance (selfObj.transform.position,monsterObj.transform.position);

				if (dis < closeDis) 
				{
					closeMonster = monsterObj;
					closeDis = dis;
				}

			}
			return closeMonster;
		}


		public static float GetSkillDamage(Actor attack,Actor denfence,SkillData skillData)//计算伤害
		{
			//			bool isShanbi = Global.IsRateTrigger (denfence.m_ActorData.m_AddDodge);
			//			if (isShanbi == true) 
			//			{
			//				return 0;
			//			}
			//			float damage = attack.m_ActorData.m_Attack - denfence.m_ActorData.m_Defence;
			//			bool isBaoji = Global.IsRateTrigger (denfence.m_ActorData.m_Crit);
			//			if (isBaoji == true)
			//				damage = damage * (attack.m_ActorData.m_AddCritHurt - denfence.m_ActorData.m_InjuryFree);
			//
			//			return damage;
			if (skillData.m_SkillType == 0)//伤害
				return -attack.m_ActorData.m_Attack;
			else if (skillData.m_SkillType == 1)//治疗
				return attack.m_ActorData.m_Attack;
			else if (skillData.m_SkillType == 2)//辅助
				return 0;
			return 0;
		}

		public static List<GameObject> GetSkillTarget(GameObject AttackObj,Actor attacker,SkillData skillData)
		{
			List<GameObject> objList = null;
			List<Actor> actorList = null;
			List<GameObject> targetList = new List<GameObject>();

			if (skillData.m_TargetType == 0) //敌方
			{
				if (AttackObj.CompareTag (Global.TagName_Actor)) 
				{
					objList = BattleScene.Active.m_monsterObjList;
					actorList = BattleScene.Active.m_monsterList;
				} 
				else 
				{
					objList = BattleScene.Active.m_actorObjList;
					actorList = BattleScene.Active.m_actorList;
				}
			} 
			else if (skillData.m_TargetType == 1) //本方
			{
				if (AttackObj.CompareTag (Global.TagName_Enemy)) 
				{
					objList = BattleScene.Active.m_monsterObjList;
					actorList = BattleScene.Active.m_monsterList;
				} 
				else 
				{
					objList = BattleScene.Active.m_actorObjList;
					actorList = BattleScene.Active.m_actorList;
				}
			} 
			else if (skillData.m_TargetType == 2) //自身
			{
				targetList.Add (AttackObj);
				return targetList; 
			}

			int vaule = skillData.m_TargetChooseConitionValue;
			if (skillData.m_TargetChooseType == 0) //随机目标
			{
				List<int> numList = Global.GetRandomNumber (objList.Count,vaule);
				for (int i = 0; i < numList.Count; i++) 
				{
					int num = numList [i];
					GameObject obj = objList [num];
					targetList.Add (obj);
				}
				return targetList;
			}
			else if (skillData.m_TargetChooseType == 1) //指定位置
			{
				GameObject obj = objList [vaule];
				targetList.Add (obj);
				return targetList;
			}
			else if (skillData.m_TargetChooseType == 2) //数值最高
			{
				GlobalBattle.GetTopVaule (actorList,vaule,targetList);
				return targetList;
			}
			else if (skillData.m_TargetChooseType == 3) //数值最低
			{
				GlobalBattle.GetLeastVaule (actorList,vaule,targetList);
				return targetList;
			}
			else if (skillData.m_TargetChooseType == 4) //自身位置
			{
				GlobalBattle.GetAroundActor (AttackObj,objList,skillData,targetList);
				return targetList;
			}
			return targetList;
		}

		private static void GetTopVaule(List<Actor> actorList,int vauleIndex,List<GameObject> targetList)
		{
			Actor top = actorList[0];
			for (int i = 1; i < actorList.Count; i++)
			{
				Actor temp = actorList [i];
				if (temp.m_AttributeList [vauleIndex] > top.m_AttributeList [vauleIndex])
					top = temp;
			}
			targetList.Add (top.gameObject);
		}

		private static void GetLeastVaule(List<Actor> actorList,int vauleIndex,List<GameObject> targetList)
		{
			Actor least = actorList[0];
			for (int i = 1; i < actorList.Count; i++)
			{
				Actor temp = actorList [i];
				if (temp.m_AttributeList [vauleIndex] < least.m_AttributeList [vauleIndex])
					least = temp;
			}
			targetList.Add (least.gameObject);
		}

		private static void GetAroundActor(GameObject attackObj,List<GameObject> defenceList,SkillData skillData,List<GameObject> targetList)
		{
			if (skillData.m_TargetEffectType == 0) 
			{
				GlobalBattle.GetSectorDefence (attackObj,defenceList,skillData,targetList);
			}
			else if (skillData.m_TargetEffectType == 1) 
			{
				GlobalBattle.GetCircular (attackObj,defenceList,skillData,targetList);
			}
			else if (skillData.m_TargetEffectType == 2) 
			{
				GlobalBattle.GetRectangular (attackObj,defenceList,skillData,targetList);
			} 

		}

		private static void GetSectorDefence(GameObject attack,List<GameObject> defenceList,SkillData skillData,List<GameObject> targetList)
		{
			float dis = skillData.m_TargetEffectArea;
			for (int i = 1; i < defenceList.Count; i++) 
			{
				GameObject defence = defenceList [i];
				float distance = Vector3.Distance(attack.transform.position, defence.transform.position);//距离
				Vector3 norVec = attack.transform.rotation * Vector3.forward * 5;//此处*5只是为了画线更清楚,可以不要
				Vector3 temVec = defence.transform.position - attack.transform.position;
				//			Debug.DrawLine(transform.position, norVec, Color.red);//画出技能释放者面对的方向向量
				//			Debug.DrawLine(transform.position, Target.position, Color.green);//画出技能释放者与目标点的连线
				float jiajiao = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
				if (distance < dis)
				{
					if (jiajiao <= 60 * 0.5f)
					{
						targetList.Add (defence);
//						Debug.Log("在扇形范围内");
					}
				}
			}

		}

		private static void GetCircular(GameObject attack,List<GameObject> defenceList,SkillData skillData,List<GameObject> targetList)
		{
			float dis = skillData.m_TargetEffectArea;
			for (int i = 1; i < defenceList.Count; i++) 
			{
				GameObject defence = defenceList [i];
				float distance = Vector3.Distance(attack.transform.position, defence.transform.position);//距离
				if (distance < dis)
				{
					targetList.Add (defence);
//					Debug.Log("在圆形范围内");
				}
			}
		}

		private static void GetRectangular(GameObject attack,List<GameObject> defenceList,SkillData skillData,List<GameObject> targetList)
		{
			float dis = skillData.m_TargetEffectArea;
			for (int i = 1; i < defenceList.Count; i++) 
			{
				GameObject defence = defenceList [i];
				Bounds bounds = new Bounds(Vector3.zero, new Vector3(1, 2, 1));
				if (bounds.Contains (defence.transform.position)) 
				{
					targetList.Add (defence);
//					Debug.Log("在矩形范围内");
				}
//			Destroy (bounds);
			}
		}



	}
}
