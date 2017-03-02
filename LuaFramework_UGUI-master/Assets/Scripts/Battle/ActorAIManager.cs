using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame
{
	public class ActorAIManager : MonoBehaviour 
	{
		public Actor m_MainActor;

		void Awake()
		{
			m_MainActor = this.transform.GetComponent<Actor> ();
		}

		void Start () 
		{

		}

		void UpdateAI()
		{
//			List<GameObject> monsterList = BattleScene.Active.m_monsterList;
//			for (int i = 0; i < monsterList.Count; i++) 
//			{
//				GameObject monsterObject = monsterList[i];
//				if (monsterObject == null)
//					continue;
//				Vector3 pos1 = new Vector3( monsterObject.transform.position.x, 0, monsterObject.transform.position.z );
//				Vector3 pos2 = new Vector3( m_ActorObject.transform.position.x, 0, m_ActorObject.transform.position.z );
////				Debug.Log ("UpdateAI = "+Vector3.Distance(pos1,pos2));
//
//				if (Vector3.Distance(pos1,pos2) <= 2)
//				{
//					DestroyImmediate (monsterObject);
//
//					Object psObj = Resources.Load ("Effect/CircleFX_Dark");
//					GameObject t = Instantiate(psObj) as GameObject;
//					t.transform.position = pos1;
//					t.transform.localScale = new Vector3 (2f, 2f, 2f);
//				}
//			}


			if (m_MainActor.m_Index != 0) //目前Index1标记为主角
			{
				ShouldAttack ();
				FllowTarget ();
			}
			else if (m_MainActor.m_Index == 0)
			{
				if (m_MainActor.IsActorStatus (Actor.ActorStatus.Stand)) 
				{
					GameObject monster = BattleScene.Active.GetCurrentMonsterObj ();
					m_MainActor.SetCurrentTarget (monster);
					m_MainActor.MonsterToAttack (monster);
//				&& m_MainActor.m_ActorSkillManager.GetAvailableSkill () != null
				}
			}
		}

		void ShouldAttack()
		{
			if (m_MainActor.IsActorStatus (Actor.ActorStatus.Attack) == true)
				return;
			GameObject target = BattleScene.Active.GetCurrentMonsterObj();
			float distance = Vector3.Distance(transform.position, target.transform.position);
			Skill skill = m_MainActor.m_ActorSkillManager.GetAvailableSkill ();
//			Debug.Log ("________________________________ name = "+target.name+" dis = "+distance+" Radius = "+skill.m_SkillData.m_Radius+" status = "+m_MainActor.m_ActorStatus);
			if (distance <= skill.m_SkillData.m_Radius)  //  当和主人之间的距离超过技能距离时跟随
			{
				m_MainActor.SetCurrentTarget (target);
				m_MainActor.StartAttack ();
				m_MainActor.SetActorStatus(Actor.ActorStatus.Attack);

			}
		}

		void FllowTarget()
		{
			if (m_MainActor.IsActorStatus (Actor.ActorStatus.Attack) == true)
				return;
			GameObject target = BattleScene.Active.m_actorObjList [0];
			Actor actor = BattleScene.Active.m_actorList [0];
			if (target == null)
				return;
			var dir = target.transform.position - transform.position;
			float distance = Vector3.Distance(transform.position, target.transform.position);
			int skillRange = 3;
			if (distance >= skillRange) //  当和主人之间的距离超过技能距离时跟随
			{ 
				var destPos = transform.position + dir.normalized * (distance - skillRange + 1);
				transform.position = Vector3.MoveTowards (transform.position, destPos, Time.deltaTime * 10);
				m_MainActor.SetActorStatus (Actor.ActorStatus.Follow,true);
			} 
			else if (distance < skillRange && actor.IsActorStatus(Actor.ActorStatus.Stand) == true)//
			{
				m_MainActor.SetActorStatus (Actor.ActorStatus.Stand,true);
			}
			else if (distance < skillRange && actor.IsActorStatus(Actor.ActorStatus.Attack) == true)//
			{
				m_MainActor.SetActorStatus (Actor.ActorStatus.Stand,true);
			}

			var rotation = Quaternion.LookRotation(dir); //  获得 目标方向
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);  // 差值  宠物朝向趋向目标
		}


		// Update is called once per frame
		void Update () 
		{
			UpdateAI ();
		}
	}

}
