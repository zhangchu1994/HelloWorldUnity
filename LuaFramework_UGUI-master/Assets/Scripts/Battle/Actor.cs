using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaFramework;
using UnityEngine.AI;

namespace GlobalGame 
{
	public class ActorData
	{
		public float m_Hp = 100f;
		public float m_MaxHp = 100f;
		public float m_Cd = 1f;
		public float m_CurCd = -1f;
	}


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
		}

		public int m_Index;
		public GameObject m_ActorObject = null;

		public ActorAgentManager m_ActorAgentManager;
		public ActorBobyManager m_ActorBodyManager;
		public ActorUIManager m_ActorUIManager;
		public ActorMeshManager m_ActorMeshManager;
		public ActorAnimationManager m_ActorAnimationManager;
		public ActorAIManager m_ActorAIManager;
		public ActorSkillManager m_ActorSkillManager;

		public ActorData m_ActorData;
		public ActorStatus m_ActorStatus;

		public GameObject m_CurrentTarget;

		void Start()
		{
			
		}

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

			Global.BattleLog (this, status.ToString());
			m_ActorStatus = status;
			if (needPlayAnimation == true) 
			{
				if (status == Actor.ActorStatus.Stand) 
				{
					m_ActorData.m_CurCd = -1f;
					m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Stand, WrapMode.Loop);// 改变动画
				}
				else if (status == Actor.ActorStatus.Agent || status == Actor.ActorStatus.Follow)
					m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Run, WrapMode.Loop);// 改变动画
			}
		}

		public void InitActor (GameObject obj,int argIndex) 
		{
			m_ActorStatus = ActorStatus.Stand;

			m_Index = argIndex;
			m_ActorObject = obj;

			m_ActorData = new ActorData ();

			m_ActorAgentManager = m_ActorObject.AddComponent<ActorAgentManager> ();

			m_ActorBodyManager = m_ActorObject.AddComponent<ActorBobyManager> ();
			m_ActorBodyManager.InitBoby ();

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

		public void StartAttack()
		{
			m_ActorData.m_CurCd = 0;
			m_ActorSkillManager.StartUseSkill ();
		}

		void Update () 
		{
			if (m_ActorData.m_CurCd == -1f)
				return;
			m_ActorData.m_CurCd += Time.deltaTime;
//			Debug.Log ("Update____CurCd = "+m_ActorData.m_CurCd);
			if (m_ActorData.m_CurCd >= m_ActorData.m_Cd && IsActorStatus(Actor.ActorStatus.Attack) == true) 
			{
				if (BattleScene.Active.GetCurrentMonsterObj () != m_CurrentTarget)
					SetActorStatus (Actor.ActorStatus.Stand);
				else
					StartAttack();
			}
		}

		public void AddHp(float arghp)
		{
			float hp = m_ActorData.m_Hp + arghp;
			if (hp > m_ActorData.m_MaxHp)
				hp = m_ActorData.m_MaxHp;
			else if (hp < 0)
				hp = 0;
			m_ActorData.m_Hp = hp;

			float percent = m_ActorData.m_Hp / m_ActorData.m_MaxHp;
//			Debug.Log ("percent = " + percent);
			m_ActorUIManager.UpdateBloodRatio (percent);


			if (hp <= 0) 
			{
				SetActorStatus(Actor.ActorStatus.Dead);
				BattleScene.Active.CurrentMonsterDie ();
			}
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
					m_ActorAgentManager.SetDestination (monsterObject.transform.position,Vector3.zero);
				} 
			}
		}

		public void LoseBlood(Actor attacker,float damage)//怪物掉血
		{
			m_ActorUIManager.InitLoseBlood(damage);
			AddHp (damage);
		}

		public void SetCurrentTarget(GameObject Obj)
		{
			m_CurrentTarget = Obj;
		}

		public void ShootFront(GameObject obj)//RaycastHit hit
		{
//			for (int j = 0; j < m_actorObjList.Count; j++) 
//			{
//				GameObject obj = m_actorObjList [j];
			Vector3 target = obj.transform.position;
			Actor actor = this;
			m_ActorObject.transform.LookAt (target);

			Object psObj = Resources.Load ("Effect/CloudFlashFX");
			GameObject t = Instantiate(psObj) as GameObject;
			Global.ChangeParticleScale (t,2.5f);
			t.transform.position = m_ActorObject.transform.position;
			Bullet bulletScripte = t.GetComponent<Bullet>();
//			GameObject monsterObject = m_actorObjList [0];
			bulletScripte.InitBullet (m_ActorObject, obj);
			actor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Attack, WrapMode.Loop, true);
		}
//		}
	}
}
