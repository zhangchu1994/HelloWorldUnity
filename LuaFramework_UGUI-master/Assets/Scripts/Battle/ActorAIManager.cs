using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame
{
	public class ActorAIManager : MonoBehaviour 
	{
		public Actor m_MainActor;
		public ActorData m_ActorData;
//		bool grounded = true;

		#region Init
		void Awake()
		{
			m_MainActor = this.transform.GetComponent<Actor> ();
			m_ActorData = m_MainActor.m_ActorData;
		}

		void Start () 
		{

		}
		#endregion



		#region Update
		void Update () 
		{
//			return;
			UpdataCD ();
			UpdateAI ();
		}

		void UpdataCD()
		{
			if (m_MainActor.IsActorStatus (Actor.ActorStatus.Attack) == false)
				return;
			
			m_ActorData.m_CurCd += Time.deltaTime;
			Skill curSkill = m_MainActor.m_ActorSkillManager.GetCurrentSkill ();
//			if (this.name == "Actor1")
//				Debug.Log ("Update____CurCd = "+m_ActorData.m_CurCd+" status = "+m_MainActor.m_CurrentTargetActor.m_ActorStatus+" cd = "+curSkill.m_SkillData.m_CdTime);
			
			if (m_ActorData.m_CurCd >= curSkill.m_SkillData.m_CdTime) 
			{
				if (m_MainActor.m_CurrentTargetActor.IsActorStatus(Actor.ActorStatus.Dead) == true )
					m_MainActor.SetActorStatus (Actor.ActorStatus.Stand);
				else
					m_MainActor.StartAttack();
			}
		}

		void UpdateAI()
		{
//			if(Input.GetButtonDown("Jump"))
//				Jump ();
//			return;
			if (m_MainActor.m_ActorType == Actor.ActorType.Actor)
			{
				if (m_MainActor.IsActorStatus (Actor.ActorStatus.Stand)) //当处于站立状态时 由主角来找要打的怪
				{
//					if (m_MainActor.gameObject.name == "Actor1")
//						Debug.Log ("UpdateAI = "+m_MainActor.m_ActorStatus+" name = "+m_MainActor.gameObject.name);
					
					GameObject monster = GlobalBattle.GetCloseMonsterObj (m_MainActor.gameObject,Actor.ActorType.Monster);
					m_MainActor.SetCurrentTarget (monster);
					m_MainActor.MonsterToAttack (monster);
//					Global.BattleLog (m_MainActor, "MonsterToAttack = " + monster.name);
				}
				ShouldAttack ();
			}
			else if (m_MainActor.m_ActorType != Actor.ActorType.Actor ) //目前Index1标记为主角
			{
				ShouldAttack ();
				FllowTarget ();//宝宝跟随主角
			}
		}

//		void Jump ()
//		{
//			if(m_MainActor.IsActorStatus(Actor.ActorStatus.Jump) == false)
//			{
//				m_MainActor.SetActorStatus (Actor.ActorStatus.Jump);
//				m_MainActor.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
////				Debug.Log("Jump____________________");
//				Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody> ();
//				rigidbody.isKinematic = false;
//				rigidbody.useGravity = true;
//				rigidbody.AddForce(Vector3.up * 500);
//			}
//		}

		void OnCollisionEnter(Collision hit)
		{
//			m_MainActor.SetActorStatus (Actor.ActorStatus.Stand);
//			m_MainActor.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
//			Debug.Log("I'm colliding with something!");
		}


		void ShouldAttack()
		{
			bool isRun = (m_MainActor.IsActorStatus (Actor.ActorStatus.Agent) == true || m_MainActor.IsActorStatus (Actor.ActorStatus.AgentToAttack) == true 
				|| m_MainActor.IsActorStatus (Actor.ActorStatus.Follow) == true);

			if (isRun == false)
				return;
			Actor actor = BattleScene.Active.m_actorList [0];
			GameObject target = actor.m_CurrentTarget;
			if (target == null)
				return;
			float distance = Vector3.Distance (transform.position, target.transform.position);
			Skill skill = m_MainActor.m_ActorSkillManager.GetCurrentSkill ();
//				Global.BattleLog (m_MainActor, "distance = " + distance);
			if (distance <= m_ActorData.m_Range)  //  当和主人之间的距离超过技能距离时跟随
			{ 
//				if (m_MainActor.gameObject.name == "Actor2")
//					Debug.Log ("________________________________ name = " + target.name + " dis = " + distance + " Radius = " + m_ActorData.m_Range + " status = " + m_MainActor.m_ActorStatus);
				m_MainActor.m_ActorAgentManager.StopAgent ();
				m_MainActor.gameObject.transform.LookAt (target.transform);
				m_MainActor.SetCurrentTarget (target);
				m_MainActor.StartAttack ();
				m_MainActor.SetActorStatus (Actor.ActorStatus.Attack);
			}
		}

		void FllowTarget()
		{
			if (m_MainActor.IsActorStatus (Actor.ActorStatus.Attack) == true || m_MainActor.IsActorStatus (Actor.ActorStatus.Hurt) == true)
				return;
			
			Actor actor = BattleScene.Active.m_actorList [0];
			GameObject target = actor.gameObject;// = BattleScene.Active.m_actorObjList [0];
//			if (m_MainActor.m_ActorType == Actor.ActorType.Partner1)
//				target = actor.m_Fllow1;
//			else if (m_MainActor.m_ActorType == Actor.ActorType.Partner2)	
//				target = actor.m_Fllow2;
			
			if (target == null)
				return;
			var dir = target.transform.position - transform.position;
			float distance = Vector3.Distance(transform.position, target.transform.position);
			int skillRange = 0;
			if (distance >= skillRange) //  当和主人之间的距离超过技能距离时跟随
			{ 
				var destPos = transform.position + dir.normalized * (distance - skillRange + 1);
				m_MainActor.m_ActorAgentManager.SetDestinationParent (destPos);
//				transform.position = Vector3.MoveTowards (transform.position, destPos, Time.deltaTime * 10);
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


		#endregion
	}
}
