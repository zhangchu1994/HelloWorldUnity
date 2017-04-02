using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
//	public enum SkillType
//	{
//		CutDown,//近战
//		Shoot,//射击
//		Magic,//法术
//	}

//	public class SkillData
//	{
//		public int m_SkillId = 1;
//		public float m_Radius = 2;
//		public SkillType m_SkillType = SkillType.CutDown;
//		public List<GameObject> m_TargetObjList = new List<GameObject>();
//		public List<Actor> m_TargetActorList = new List<Actor>();
//	}

	public class Skill : MonoBehaviour
	{
		Actor m_MainActor;
		public SkillData m_SkillData;
		private float m_CurSkillTime;
		void Awake()
		{
			
		}

		void Start () 
		{
			
		}
		
		public void Update () 
		{
			if (m_CurSkillTime == -1) 
				return;
			m_CurSkillTime += Time.deltaTime;
//			Debug.Log ("m_SkillTime = "+m_SkillTime);
			if (m_CurSkillTime > m_SkillData.m_Delay) 
			{
				m_CurSkillTime = -1;
				SkillTakeEffect();
			}
		}

		public void InitSkill(Actor argActor,int argSkillId)
		{
			m_MainActor = argActor;
			m_SkillData = new SkillData ();

			m_SkillData = DataTables.GetSkillData (argSkillId);
		}

		public void StartSkill()
		{
			m_MainActor.m_ActorAnimationManager.PlayAnimations (Global.GetAnimRestoreList(Global.BattleAnimationType.Attack), WrapMode.Once);
//			Debug.Log ("StartSkill_________出手 = "+m_MainActor.gameObject.name+" m_Id = "+m_SkillData.m_Id);
			GetSkillTarget();
			StartSkillEffect();
		}

		public void StartSkillEffect()
		{
			m_CurSkillTime = 0;
		}

		public void SkillTakeEffect()
		{
			if (m_SkillData.m_HasBullet == 1) //远程根据是否打到计算伤害
			{ 
				//				Debug.Log ("SkillTakeEffect__________________________ count = "+m_SkillData.m_TargetObjList.Count);
				for (int i = 0; i < m_SkillData.m_TargetObjList.Count; i++) 
				{
					BulletData bulletData = DataTables.GetBulletData (m_SkillData.m_BulletId);
					GameObject obj = m_SkillData.m_TargetObjList [i];
					m_MainActor.ShootFront (obj,m_SkillData,bulletData);
				}
			} 
			if (m_SkillData.m_HasBullet == 0 && m_SkillData.m_SkillAttckType == 1) //m_SkillData.m_SkillType1 == (int)SkillType.Magic
			{
				//					GameObject obj = m_SkillData.m_TargetObjList [i];
				if (m_SkillData.m_TargetObjList.Count > 0)
					m_MainActor.MagicZone (m_SkillData.m_TargetObjList[0],m_SkillData);
			}
			else //近战直接计算伤害
			{
				for (int i = 0; i < m_SkillData.m_TargetObjList.Count; i++) 
				{
					//					GameObject monster = m_SkillData.m_TargetObjList [i];
					Actor actor = m_SkillData.m_TargetActorList [i];
					float damage = GlobalBattle.GetSkillDamage (m_MainActor,actor,m_SkillData);
					actor.LoseBlood (m_MainActor,damage);
				}
			}
		}

		public void GetSkillTarget()
		{
			m_SkillData.m_TargetActorList.Clear ();
			m_SkillData.m_TargetObjList.Clear ();


			List<GameObject> objList = GlobalBattle.GetSkillTarget (this.m_MainActor.gameObject,this.m_MainActor,m_SkillData);

//			GameObject obj = m_MainActor.m_CurrentTarget;
//			Actor monster = m_MainActor.m_CurrentTargetActor;
			for (int i = 0; i < objList.Count; i++) 
			{
				GameObject obj = objList [i];
				if (obj == null)
					continue;
//				if (m_SkillData.m_Id == 5 || m_SkillData.m_Id == 6)
//				Debug.Log ("GetSkillTarget_________出手 = "+m_MainActor.gameObject.name+" m_Id = "+m_SkillData.m_Id+ " 被打的人 = "+obj.name);
				m_SkillData.m_TargetObjList.Add (obj);
				m_SkillData.m_TargetActorList.Add(obj.GetComponent<Actor>());
			}
		}
	}
}