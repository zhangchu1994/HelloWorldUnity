using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class MonsterAIManager : MonoBehaviour 
	{
		Monster m_mainMonster;
		ActorData m_ActorData;

		void Awake()
		{
			m_mainMonster = this.transform.GetComponent<Monster> ();
			m_ActorData = m_mainMonster.m_ActorData;
		}

		void Start () 
		{
			m_mainMonster.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Stand, WrapMode.Loop);
			m_mainMonster.SetActorStatus (Actor.ActorStatus.Stand);
		}
		
		void Update () 
		{
			if (m_mainMonster.IsActorStatus (Actor.ActorStatus.Dead) == true)
				return;
			UpdateRotation ();
			return;
			UpdataCD ();
			UpdateAttck ();
		}

		void UpdataCD()
		{
//			if (this.name == "Monster1") 
//			{
//				Debug.Log ("Update____CurCd = "+m_ActorData.m_CurCd);
//				Debug.Log ("UpdateAttck___"+ m_mainMonster.name+" status = "+m_mainMonster.m_ActorStatus);
//			}

			if (m_mainMonster.IsActorStatus (Actor.ActorStatus.Attack) == false)
				return;
			if (m_mainMonster.IsActorStatus (Actor.ActorStatus.Hurt) == true)
				return;

//			if (this.name == "Monster1") 
//			{
//				Debug.Log ("Update____CurCd = "+m_ActorData.m_CurCd+" status = "+m_mainMonster.m_ActorStatus);
//			}


			m_ActorData.m_CurCd += Time.deltaTime;
//			if (m_mainMonster.name == "Monster1")
//				Debug.Log ("UpdataCD__________________________"+this.name+" m_ActorData.m_CurCd = "+m_ActorData.m_CurCd);
			Skill curSkill = m_mainMonster.m_ActorSkillManager.GetCurrentSkill();
			if (m_ActorData.m_CurCd >= curSkill.m_SkillData.m_CdTime )//m_ActorData.m_Cd 
			{
				if (m_mainMonster.m_CurrentTargetActor.IsActorStatus (Actor.ActorStatus.Dead) == true)
					m_mainMonster.SetActorStatus (Actor.ActorStatus.Stand);
				else {
					m_mainMonster.StartAttack();
				}
			}
		}

		void UpdateRotation()
		{
			GameObject actorObj = BattleScene.Active.m_actorObjList [0]; 
			float distance = Vector3.Distance(transform.position, actorObj.transform.position);
			if (distance < 15) 
			{
				var dir = actorObj.transform.position - transform.position;
				var rotation = Quaternion.LookRotation(dir); //  获得 目标方向
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);  // 差值  宠物朝向趋向目标
			}
		}

		void UpdateAttck()
		{
//			Debug.Log ("UpdateAttck___"+ m_mainMonster.name+" status = "+m_mainMonster.m_ActorStatus);
			GameObject obj = GlobalBattle.GetCloseMonsterObj (m_mainMonster.gameObject, Actor.ActorType.Actor);
			float dis = Vector3.Distance (obj.transform.position, m_mainMonster.gameObject.transform.position);
			if (dis < 2.5f) 
			{
				m_mainMonster.SetActorStatus (Actor.ActorStatus.Attack);
//				m_mainMonster.transform.LookAt (obj.transform);
				m_mainMonster.SetCurrentTarget (obj);
//				m_mainMonster.StartAttack ();
//				m_mainMonster.SetActorStatus(Actor.ActorStatus.Attack);
			}
		}
	}
}
