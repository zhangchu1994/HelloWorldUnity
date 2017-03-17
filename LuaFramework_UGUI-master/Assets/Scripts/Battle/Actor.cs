using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaFramework;
using UnityEngine.AI;

namespace GlobalGame 
{
	public class Actor : MonoBehaviour
	{
		public enum ActorStatus
		{
			Stand,
			Agent,
			Attack,
			Hurt,
			Follow,
			Dead,
			AgentToAttack,
			Jump,
		}

		public enum ActorType
		{
			Actor,
			Partner1,
			Partner2,
			Monster,
		}

		public int m_Index;
		public GameObject m_ActorObject = null;

		public ActorAgentManager m_ActorAgentManager;
		public ActorBobyManager m_ActorBodyManager;
		public ActorUIManager m_ActorUIManager;
		public ActorMeshManager m_ActorMeshManager;
		public ActorAnimationManager m_ActorAnimationManager;
		public ActorAIManager m_ActorAIManager;
		public MonsterAIManager m_MonsterAIManager;
		public ActorSkillManager m_ActorSkillManager;
		public GameObject m_Fllow1;
		public GameObject m_Fllow2;

		public ActorData m_ActorData;
		public ActorStatus m_ActorStatus;
		public ActorType m_ActorType;

		public GameObject m_CurrentTarget;
		public Actor m_CurrentTargetActor;

		#region Init
		void Start()
		{

		}

		public void InitActor (GameObject obj,int argIndex) 
		{
			m_ActorStatus = ActorStatus.Stand;

			if (argIndex == 0)
				m_ActorType = ActorType.Actor;
			else if (argIndex == 1)
				m_ActorType = ActorType.Partner1;
			else if (argIndex == 2)
				m_ActorType = ActorType.Partner2;
			
			m_Index = argIndex;
			m_ActorObject = obj;

			m_ActorData = DataTables.GetUserData (m_Index+1);

			m_ActorAgentManager = m_ActorObject.AddComponent<ActorAgentManager> ();
			if (argIndex == 0) 
			{
				m_ActorBodyManager = m_ActorObject.AddComponent<ActorBobyManager> ();
				m_ActorBodyManager.InitBoby ();
			}

			m_ActorUIManager = m_ActorObject.AddComponent<ActorUIManager> ();
			m_ActorUIManager.InitActorBlood();

			m_ActorMeshManager = m_ActorObject.AddComponent<ActorMeshManager> ();

			m_ActorAnimationManager = m_ActorObject.AddComponent<ActorAnimationManager> ();
			m_ActorAnimationManager.InitAnimation ();

			m_ActorAIManager = m_ActorObject.AddComponent<ActorAIManager> ();

			m_ActorSkillManager = m_ActorObject.AddComponent<ActorSkillManager> ();
			m_ActorSkillManager.InitSkillManager ();
//			Util.CallMethod("FirstBattleScene", "ActorDone");
		}
		#endregion

		#region Update
		void Update () 
		{
			
		}
		#endregion

		#region Status
		public bool IsActorStatus(ActorStatus status)
		{
			if (m_ActorStatus == status) 
				return true;
			else
				return false;
		}


		public void SetActorStatus(ActorStatus status,bool needPlayAnimation = false)
		{	
			if (m_ActorStatus == status)
				return;

			//			Global.BattleLog (this, status.ToString());
			m_ActorStatus = status;

			if (needPlayAnimation == true) 
			{
				if (status == Actor.ActorStatus.Stand) 
				{
					m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Stand, WrapMode.Loop);
				} 
				else if (status == Actor.ActorStatus.Agent || status == Actor.ActorStatus.Follow) 
				{
					m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Run, WrapMode.Loop);
				} 
				else if (status == Actor.ActorStatus.Dead ) 
				{
					m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Dead, WrapMode.Once);
				}
			}
		}
		#endregion


		#region HP
		public void AddHp(float arghp)
		{
			if (m_ActorData.m_CurHp <= 0)
				return;
			m_ActorUIManager.InitLoseBlood(arghp);
			float hp = m_ActorData.m_CurHp + arghp;
			if (hp > m_ActorData.m_MaxHp)
				hp = m_ActorData.m_MaxHp;
			else if (hp < 0)
				hp = 0;
			m_ActorData.m_CurHp = hp;

			float percent = m_ActorData.m_CurHp / m_ActorData.m_MaxHp;
//			Debug.Log ("percent = " + percent+" name = "+this.name+" m_CurHp = "+m_ActorData.m_CurHp+" arghp = "+arghp);
			m_ActorUIManager.UpdateBloodRatio (percent);


			if (hp <= 0 && IsActorStatus (Actor.ActorStatus.Dead) == false) 
			{
				SetActorStatus (Actor.ActorStatus.Dead, true);
				BattleScene.Active.CurrentMonsterDie ();
			} 
			else 
			{
				m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Hurt,WrapMode.Once);
			}
		}

		public void LoseBlood(Actor attacker,float damage)//怪物掉血
		{
			AddHp (damage);
		}
		#endregion

		#region Attack
		public void SetCurrentTarget(GameObject Obj)
		{
			m_CurrentTarget = Obj;
			m_CurrentTargetActor = m_CurrentTarget.GetComponent<Actor>();
		}

		public void MonsterToAttack(GameObject obj)
		{
			List<GameObject> monsterList = BattleScene.Active.m_monsterObjList;
			for (int i = 0; i < monsterList.Count; i++) 
			{
				GameObject monsterObject = monsterList [i];
				if (monsterObject == null)
					continue;
				if (monsterObject.name == obj.name) 
				{
					//					Actor actor = m_actorList[0];
					SetActorStatus(ActorStatus.AgentToAttack);
					Collider collider = obj.GetComponent<Collider> ();//.bounds.size.y
					m_ActorAgentManager.SetDestination (monsterObject.transform.position,Vector3.zero,collider.bounds.size.x/2);
				} 
			}
		}

		public void ShootFront(GameObject obj,SkillData data)//RaycastHit hit
		{
//			for (int j = 0; j < m_actorObjList.Count; j++) 
//			{
//				GameObject obj = m_actorObjList [j];
			Vector3 target = obj.transform.position;
			Actor actor = this;
			m_ActorObject.transform.LookAt (target);

			Object psObj = Resources.Load (data.m_EffectPath);
			GameObject t = Instantiate(psObj) as GameObject;
			Global.ChangeParticleScale (t,data.m_Scale);
			t.transform.position = m_ActorObject.transform.position;
			Bullet bulletScripte = t.GetComponent<Bullet>();
//			GameObject monsterObject = m_actorObjList [0];
			bulletScripte.InitBullet (m_ActorObject, obj);
//			actor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Attack, WrapMode.Loop, true);
		}

		public void MagicZone(GameObject obj,SkillData data)
		{
			Vector3 target = obj.transform.position;
			Actor actor = this;
			m_ActorObject.transform.LookAt (target);

			Object psObj = Resources.Load (data.m_EffectPath);
			GameObject t = Instantiate(psObj) as GameObject;
			Global.ChangeParticleScale (t,data.m_Scale);
			t.transform.position = obj.transform.position;
//			Bullet bulletScripte = t.GetComponent<Bullet>();
			//			GameObject monsterObject = m_actorObjList [0];
//			bulletScripte.InitBullet (m_ActorObject, obj);
//			actor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Attack, WrapMode.Loop, true);
		}

		public void StartAttack()
		{
			m_ActorData.m_CurCd = 0;
			m_ActorSkillManager.StartUseSkill ();
		}

		public void AgentDone()
		{
			Global.BattleLog (this,"AgentDone");

//			if (IsActorStatus(Actor.ActorStatus.AgentToAttack) == true) 
//			{
//				SetActorStatus(Actor.ActorStatus.Attack);
//				//				m_MainActor.StartAttack ();
//			} 
//			else if (IsActorStatus(Actor.ActorStatus.Agent) == true) 
//			{
//				SetActorStatus(Actor.ActorStatus.Stand,true);
//			}
		}


		#endregion

		public void ActorDeadAnimationDone()
		{
			BattleScene.Active.DestoryMonster (this.gameObject,this);
		}
	}
}
