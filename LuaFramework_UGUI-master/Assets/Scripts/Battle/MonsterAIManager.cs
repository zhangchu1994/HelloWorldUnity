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
		}
		
		void Update () 
		{
			if (m_mainMonster.IsActorStatus (Actor.ActorStatus.Dead) == true)
				return;
			UpdateRotation ();
			UpdataCD ();
//			UpdateAttck ();
		}

		void UpdataCD()
		{
			if (m_mainMonster.IsActorStatus (Actor.ActorStatus.Attack) == false)
				return;

			m_ActorData.m_CurCd += Time.deltaTime;
			//			if (this.name == "Monster1")
			//				Debug.Log ("Update____CurCd = "+m_ActorData.m_CurCd);
			if (m_ActorData.m_CurCd >= m_ActorData.m_Cd ) 
			{
				if (m_mainMonster.m_CurrentTargetActor.IsActorStatus(Actor.ActorStatus.Dead) == true )
					m_mainMonster.SetActorStatus (Actor.ActorStatus.Stand);
				else
					m_mainMonster.StartAttack();
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
			GameObject obj = BattleScene.Active.GetCloseMonsterObj (m_mainMonster.gameObject, Actor.ActorType.Actor);
			float dis = Vector3.Distance (obj.transform.position, m_mainMonster.gameObject.transform.position);
			if (dis < 2) 
			{
				m_mainMonster.transform.LookAt (obj.transform);
				m_mainMonster.SetCurrentTarget (obj);
				m_mainMonster.StartAttack ();
				m_mainMonster.SetActorStatus(Actor.ActorStatus.Attack);
			}
		}
	}
}
