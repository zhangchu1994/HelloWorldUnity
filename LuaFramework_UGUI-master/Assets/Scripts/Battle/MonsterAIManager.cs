using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class MonsterAIManager : MonoBehaviour 
	{
		Monster m_mainMonster;

		void Awake()
		{
			m_mainMonster = this.transform.GetComponent<Monster> ();
		}

		void Start () 
		{
			
		}
		
		void Update () 
		{
			if (m_mainMonster.IsActorStatus (Actor.ActorStatus.Dead) == true)
				return;
			UpdateRotation ();

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

		void GetAttackTarget()
		{
			
		}
	}
}
