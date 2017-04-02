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
			None,
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
			Boss,
		}

		public enum ActorAttributeType
		{
			Hp = 0,
			Attack = 0,
			Defence = 1,
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

		public ActorData m_ActorData;
		public ActorStatus m_ActorStatus;
		public ActorType m_ActorType;

		public GameObject m_CurrentTarget;
		public Actor m_CurrentTargetActor;
		public Actor.ActorStatus m_lastActorStatus = Actor.ActorStatus.None;


		public List<float> m_AttributeList = new List<float>();

		#region Init
		void Start()
		{

		}

		public void InitActor (GameObject obj,int argIndex,ActorData actorData) 
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

			m_ActorData = actorData;

			m_ActorAgentManager = m_ActorObject.AddComponent<ActorAgentManager> ();
//			if (argIndex == 0) 
//			{
//				m_ActorBodyManager = m_ActorObject.AddComponent<ActorBobyManager> ();
//				m_ActorBodyManager.InitBoby ();
//			}

			m_ActorUIManager = m_ActorObject.AddComponent<ActorUIManager> ();
			m_ActorUIManager.InitActorBlood();

			m_ActorMeshManager = m_ActorObject.AddComponent<ActorMeshManager> ();

			m_ActorAnimationManager = m_ActorObject.AddComponent<ActorAnimationManager> ();
			m_ActorAnimationManager.InitAnimation ();

			m_ActorAIManager = m_ActorObject.AddComponent<ActorAIManager> ();

			m_ActorSkillManager = m_ActorObject.AddComponent<ActorSkillManager> ();
			m_ActorSkillManager.InitSkillManager ();
//			Util.CallMethod("FirstBattleScene", "ActorDone");


			m_AttributeList.Add (m_ActorData.m_MaxHp);
			m_AttributeList.Add (m_ActorData.m_Attack);
			m_AttributeList.Add (m_ActorData.m_Defence);
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
//			if (this.gameObject.name == "Actor1" && status == ActorStatus.Stand)
//				Debug.Log ("SetActorStatus___________________________");
								
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
				else if (status == Actor.ActorStatus.Dead) 
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
			else if (IsActorStatus (Actor.ActorStatus.Dead) == false)
			{
//				if (this.name == "Monster1")
//					Debug.Log ("AddHp_________________________________________");
				m_lastActorStatus = m_ActorStatus;
				if (arghp < 0) 
				{
					SetActorStatus (Actor.ActorStatus.Hurt, false);
					m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Hurt,WrapMode.Once);
				}
			}
		}


		public void RestoreActorStatus()
		{
//			Debug.Log ("RestoreActorStatus m_ActorStatus = "+m_ActorStatus);
			if (m_ActorStatus == Actor.ActorStatus.Dead)
				return;
			m_ActorStatus = m_lastActorStatus;
			m_lastActorStatus = Actor.ActorStatus.None;
		}

		public void LoseBlood(Actor attacker,float damage)//怪物掉血
		{
			AddHp (damage);
		}
		#endregion

		#region Attack
		public void SetCurrentTarget(GameObject Obj)
		{
//			if (this.gameObject.CompareTag(Global.TagName_Actor))
//				Debug.Log ("SetCurrentTarget________________________________ name = "+Obj.name);
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

		#region 3种攻击方式
		public void ShootFront(GameObject obj,SkillData data,BulletData bulletData)//RaycastHit hit
		{
//			for (int j = 0; j < m_actorObjList.Count; j++) 
//			{
//				GameObject obj = m_actorObjList [j];
			if (obj == null)
				return;
			Vector3 target = obj.transform.position;
			Actor actor = this;
			m_ActorObject.transform.LookAt (target);

			Dictionary<string,string> info1 = Global.CreateABInfo(bulletData.m_EffectRoute,Global.GetAssetName(bulletData.m_EffectRoute),AssetType.Perfab,-1);
			ResourceManager.Active.LoadPrefabWithInfo (info1, delegate(UnityEngine.Object[] objs, Dictionary<string,string> info) {
				int index = int.Parse (Global.getDicStrVaule (info1, Global.ABInfoKey_Index));
				GameObject prefab = objs [0] as GameObject;

				GameObject t = Instantiate(prefab) as GameObject;
				//			Global.ChangeParticleScale (t,data.m_Scale);
				t.transform.position = m_ActorObject.transform.position;
				Bullet bulletScripte = t.GetComponent<Bullet>();
				//			GameObject monsterObject = m_actorObjList [0];
				bulletScripte.InitBullet (m_ActorObject, obj,bulletData);
				//			actor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Attack, WrapMode.Loop, true);
			});


		}

		public void MagicZone(GameObject obj,SkillData data)
		{
			if (obj == null)
				return;
			
			Vector3 target = obj.transform.position;
			Actor actor = this;
			m_ActorObject.transform.LookAt (target);

			Dictionary<string,string> info1 = Global.CreateABInfo(data.m_EffectPrefab,Global.GetAssetName(data.m_EffectPrefab),AssetType.Perfab,-1);
			ResourceManager.Active.LoadPrefabWithInfo (info1, delegate(UnityEngine.Object[] objs, Dictionary<string,string> info) {
				int index = int.Parse (Global.getDicStrVaule (info1, Global.ABInfoKey_Index));
				GameObject prefab = objs [0] as GameObject;

				GameObject t = Instantiate(prefab) as GameObject;
//			Global.ChangeParticleScale (t,data.m_Scale);
				t.transform.position = obj.transform.position;
				MagicZone magicZone = t.GetComponent<MagicZone>();
//			GameObject monsterObject = m_actorObjList [0];
				magicZone.InitMagicZoom(m_ActorObject,data);

			});
			//			actor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Attack, WrapMode.Loop, true);
		}

		public void MeeleAttack(GameObject obj,SkillData data)
		{
			if (obj == null)
				return;



		}
		#endregion

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
