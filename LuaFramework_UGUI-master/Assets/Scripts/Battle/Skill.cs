using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public enum SkillType
	{
		CutDown,//近战
		Shoot,//射击
		Magic,//法术
	}

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

		void Awake()
		{
			
		}

		void Start () 
		{
			
		}
		
		public void Update () 
		{
//			if (m_SkillTime == -1) 
//				return;
//			m_SkillTime += Time.deltaTime;
////			Debug.Log ("m_SkillTime = "+m_SkillTime);
//			if (m_SkillTime > m_SkillData.m_EffectTime) 
//			{
//				m_SkillTime = -1;
//				SkillTakeEffect();
//			}
		}

		public void InitSkill(Actor argActor,int argSkillId)
		{
			m_MainActor = argActor;
			m_SkillData = new SkillData ();

			m_SkillData = DataTables.GetSkillData (argSkillId);

//			if (m_MainActor.m_Index == 0) 
//			{
//				
//			} 
//			else if (m_MainActor.m_Index == 1) 
//			{
//				m_SkillData = DataTables.GetSkillData (2);
//			} 
//			else if (m_MainActor.m_Index == 2) 
//			{
//				m_SkillData = DataTables.GetSkillData (3);
//			} 
//			else 
//			{
//				m_SkillData = Global.Clone(DataTables.GetSkillData (1));
//			}
//			Debug.Log ("InitSkill______________________________");
		}

		public void StartSkill()
		{
			m_MainActor.m_ActorAnimationManager.PlayAnimations (Global.GetAnimRestoreList(Global.BattleAnimationType.Attack), WrapMode.Once);
			GetSkillTarget();
			SkillTakeEffect();
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
//			else if (m_SkillData.m_SkillType1 == (int)SkillType.Magic) 
//			{
//				for (int i = 0; i < m_SkillData.m_TargetObjList.Count; i++) 
//				{
//					GameObject obj = m_SkillData.m_TargetObjList [i];
//					m_MainActor.MagicZone (obj,m_SkillData);
//				}
//			}
			else //近战直接计算伤害
			{
				for (int i = 0; i < m_SkillData.m_TargetObjList.Count; i++) 
				{
					GameObject monster = m_SkillData.m_TargetObjList [i];
					Actor actor = m_SkillData.m_TargetActorList [i];
					float damage = GetSkillDamage (m_MainActor,actor);
					actor.LoseBlood (m_MainActor,damage);
				}
			}
		}

		public float GetSkillDamage(Actor attack,Actor denfence)
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
			return -attack.m_ActorData.m_Attack;
		}

		public void GetSkillTarget()
		{
			m_SkillData.m_TargetActorList.Clear ();
			m_SkillData.m_TargetObjList.Clear ();

			GameObject obj = m_MainActor.m_CurrentTarget;
			Actor monster = m_MainActor.m_CurrentTargetActor;
			m_SkillData.m_TargetActorList.Add(monster);
			m_SkillData.m_TargetObjList.Add(obj);
		}


		public void GetSectorDefence(GameObject attack,GameObject defence)
		{
			float distance = Vector3.Distance(attack.transform.position, defence.transform.position);//距离
			Vector3 norVec = attack.transform.rotation * Vector3.forward * 5;//此处*5只是为了画线更清楚,可以不要
			Vector3 temVec = defence.transform.position - attack.transform.position;
//			Debug.DrawLine(transform.position, norVec, Color.red);//画出技能释放者面对的方向向量
//			Debug.DrawLine(transform.position, Target.position, Color.green);//画出技能释放者与目标点的连线
			float jiajiao = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
			if (distance < 10)
			{
				if (jiajiao <= 60 * 0.5f)
				{
					Debug.Log("在扇形范围内");
				}
			}
		}

		public void GetCircular(GameObject attack,GameObject defence)
		{
			float distance = Vector3.Distance(attack.transform.position, defence.transform.position);//距离
			if (distance < 10)
			{
				Debug.Log("在圆形范围内");
			}
		}

		public void GetRectangular(GameObject attack,GameObject defence)
		{
			Bounds bounds = new Bounds(Vector3.zero, new Vector3(1, 2, 1));
			if (bounds.Contains (defence.transform.position)) 
			{
				Debug.Log("在矩形范围内");
			}
//			Destroy (bounds);
		}
	}
}