using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GlobalGame 
{
	public class Bullet : MonoBehaviour 
	{
		public enum Type
		{
			NO_MOTION,						//0 不运动（无目标）
			RECTILINEAR_MOTION,				//1 直线运动（无目标）
			TRACK_NO_INERTIA_MOTION,		//2 追踪目标（无惯性）
			TRACK_INERTIA_MOTION,			//3 追踪目标（有惯性）
			PARABOLA_MOTION_WITHTARGET,		//4 抛物线（有目标）
			PARABOLA_MOTION_NOTARGET,		//5 抛物线 (无目标)
			TARGET_RECTILINEAR_MOTION,		//6 直线运动（有目标）
			RAY_MOTION,						//7 射线（有目标）
			RAY_MOTION_NO_TARGET,			//8 射线（无目标）
			JUMP_MOTION,					//9 在目标间跳跃（不重复打击目标）
			TRACK_NO_INERTIA_MOTION_PLUS,	//10追踪目标（补充）
			ENEGY_BALL_FLY,					//11 士气能量球
			Spiral,                         //12 螺旋
		}

		float m_Speed = 0;
		Type m_type;
		GameObject m_attack;
		GameObject m_defender;
		Actor m_AttActor;
		Actor m_Defactor;
		Skill m_Skill;
		SkillData m_SkillData;

		//抛物线相关参数
		public float m_ParabolaGravity = 3.8f;  
		private float m_ParabolaFlyTime;  

		public float m_ParabolaSpeed = 5;  
		private float  m_ParabolaVerticalSpeed;  
		private Vector3 m_ParabolaMoveDirection;  

		private float m_ParabolaAngleSpeed;  
		private float m_ParabolaAngle;  

		void Start() 
		{
			
		}
		
		public void InitBullet(GameObject attack,GameObject defender)
		{
	//		this.transform.LookAt(defender.transform);
	//		this.transform.rotation = rotation;
			m_attack = attack;
			m_defender = defender;
			m_AttActor = attack.GetComponent<Actor>();
			m_Defactor = defender.GetComponent<Actor>();
			m_Speed = 10f;
			this.transform.rotation = attack.transform.rotation;
			m_Skill = m_AttActor.m_ActorSkillManager.GetCurrentSkill ();
			m_SkillData = m_Skill.m_SkillData;

			m_type = (Type)m_SkillData.m_SkillType2;//Type.TARGET_RECTILINEAR_MOTION;

			if (m_type == Type.NO_MOTION) 
			{
				
			} 
			else if (m_type == Type.TARGET_RECTILINEAR_MOTION) 
			{
				
			}
			else if (m_type == Type.PARABOLA_MOTION_WITHTARGET) 
			{
//				InitParabolaMotionWithTarget ();
			}
			else if (m_type == Type.PARABOLA_MOTION_NOTARGET) 
			{

			}
			else if (m_type == Type.Spiral) 
			{
//				InitSpiral ();
			}
		}

//		void InitSpiral()
//		{
//			
//		}

//		void InitParabolaMotionWithTarget()
//		{
//			float tmepDistance = Vector3.Distance(transform.position, m_defender.transform.position);  
//			float tempTime = tmepDistance / m_ParabolaSpeed;  
//			float riseTime, downTime;  
//			riseTime = downTime = tempTime / 2;  
//			m_ParabolaVerticalSpeed = m_ParabolaGravity * riseTime;  
//			transform.LookAt(m_defender.transform.position);  
//
//			float tempTan = m_ParabolaVerticalSpeed / m_ParabolaSpeed;  
//			double hu = Math.Atan(tempTan);  
//			m_ParabolaAngle = (float)(180 / Math.PI * hu);  
//			transform.eulerAngles = new Vector3(-m_ParabolaAngle, transform.eulerAngles.y, transform.eulerAngles.z);  
//			m_ParabolaAngleSpeed = m_ParabolaAngle / riseTime;  
//
//			m_ParabolaMoveDirection = m_defender.transform.position - transform.position;  
//		}

		void FixedUpdate () 
		{
			//			return;
			if (m_type == Type.NO_MOTION) 
			{

			} 
			else if (m_type == Type.TARGET_RECTILINEAR_MOTION) 
			{
				UpdateRectilinearMotion ();
			}
			else if (m_type == Type.PARABOLA_MOTION_WITHTARGET) 
			{
//				UpdateParabolaMotionWithTarget ();
			}
			else if (m_type == Type.PARABOLA_MOTION_NOTARGET) 
			{
//				UpdateParabolaMotionNoTarget ();
			}
			else if (m_type == Type.Spiral) 
			{
//				UpdateSpiral ();
			}
		}

//		void UpdateSpiral()//12.螺旋
//		{
//			this.gameObject.transform.Translate(Vector3.forward *5* Time.deltaTime);
//
//			GameObject dot = this.gameObject.transform.FindChild ("Dot").gameObject;
//			GameObject particle = this.gameObject.transform.FindChild ("Flash_Main").gameObject;
//			particle.transform.RotateAround(dot.transform.position,Vector3.up*10,300*Time.deltaTime);
//			particle.transform.Rotate(Vector3.up, 20*Time.deltaTime, Space.World);
//
//		}
//
//		void UpdateParabolaMotionNoTarget()//5.抛物线
//		{
//			if( GetComponent<Rigidbody>() )
//			{
//				Vector3 forward = transform.forward;
//				//			Vector3 forward = transform.TransformDirection(Vector3.forward);
//				GetComponent<Rigidbody>().AddForce(new Vector3(10,15,0));
//			}
//		}
//
//		void UpdateParabolaMotionWithTarget()//4.抛物线
//		{
//			if (m_defender == null)
//				return;
//			if (transform.position.y < m_defender.transform.position.y)  
//			{  
//				return;  
//			}  
//			m_ParabolaFlyTime += Time.deltaTime;  
//			float test = m_ParabolaVerticalSpeed - m_ParabolaGravity * m_ParabolaFlyTime;  
//			transform.Translate(m_ParabolaMoveDirection.normalized * m_ParabolaSpeed * Time.deltaTime, Space.World);  
//			transform.Translate(new Vector3(0f,1f,0f)* test * Time.deltaTime,Space.World);  
//			float testAngle = -m_ParabolaAngle + m_ParabolaAngleSpeed * m_ParabolaFlyTime;  
//			transform.eulerAngles = new Vector3(testAngle, transform.eulerAngles.y, transform.eulerAngles.z);  
////			Debug.Log ("position = "+transform.position.ToString()+" testAngle = "+testAngle);
//		}

		void UpdateRectilinearMotion()//6.直线运动
		{
			if( GetComponent<Rigidbody>() )
			{
//				Debug.Log (this.gameObject.transform.position.ToString());
				Vector3 forward = transform.forward;
//			Vector3 forward = transform.TransformDirection(Vector3.forward);
				GetComponent<Rigidbody>().MovePosition( GetComponent<Rigidbody>().position + forward * m_Speed * Time.fixedDeltaTime );
			}
		}

		void OnTriggerEnter(Collider other) 
		{
			if (other.gameObject != null && m_defender != null && other.gameObject.CompareTag(Global.TagName_Enemy)) //&& other.gameObject.name == m_defender.name
			{
//				Debug.Log ("OnTriggerEnter______________" + other.gameObject.name);
				Monster monster = other.gameObject.GetComponent<Monster> ();
				monster.LoseBlood (m_AttActor,-10f);
//				Destroy(this.gameObject);
			}
		}
	}
}
