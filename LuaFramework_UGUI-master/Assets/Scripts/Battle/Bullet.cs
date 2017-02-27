using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public enum Type
	{
		NO_MOTION,						//0 不运动（无目标）
		RECTILINEAR_MOTION,				//1 直线运动（无目标）
		TRACK_NO_INERTIA_MOTION,		//2 追踪目标（无惯性）
		TRACK_INERTIA_MOTION,			//3 追踪目标（有惯性）
		PARABOLA_MOTION,				//4 抛物线（有目标）
		TARGET_RECTILINEAR_MOTION,		//5 直线运动（有目标）
		PARABOLA_MOTION_NO_TARGET,		//6 抛物线 (无目标)
		RAY_MOTION,						//7 射线（有目标）
		RAY_MOTION_NO_TARGET,			//8 射线（无目标）
		JUMP_MOTION,					//9 在目标间跳跃（不重复打击目标）
		TRACK_NO_INERTIA_MOTION_PLUS,	//10追踪目标（补充）
		ENEGY_BALL_FLY,					//11 士气能量球	
	}

	float m_Speed = 0;
	Type m_type;
	GameObject m_attack;
	GameObject m_defender;

	// Use this for initialization
	void Start() 
	{
		
	}
	
	public void InitBullet(GameObject attack,GameObject defender)
	{
//		this.transform.LookAt(defender.transform);
//		this.transform.rotation = rotation;
		m_Speed = 10f;
		this.transform.rotation = attack.transform.rotation;
		m_type = Type.TARGET_RECTILINEAR_MOTION;

		if (m_type == Type.NO_MOTION) 
		{
			
		} 
		else if (m_type == Type.TARGET_RECTILINEAR_MOTION) 
		{
			
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (m_type == Type.NO_MOTION) 
		{

		} 
		else if (m_type == Type.TARGET_RECTILINEAR_MOTION) 
		{
			UpdateRectilinearMotion ();
		}
	}

	void UpdateRectilinearMotion()//直线运动
	{
		if( GetComponent<Rigidbody>() )
		{
			Vector3 forward = transform.forward;
//			Vector3 forward = transform.TransformDirection(Vector3.forward);
			GetComponent<Rigidbody>().MovePosition( GetComponent<Rigidbody>().position + forward * m_Speed * Time.fixedDeltaTime );
		}
	}

}
