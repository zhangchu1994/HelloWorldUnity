using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class MagicZone : MonoBehaviour 
	{
		private float m_curEffectTime;
		private Actor m_attacker;
		private float m_EffectTime;

		void Awake()
		{
			m_curEffectTime = -1;


		}

		void Start () 
		{
			m_EffectTime = 1.5f;
		}

		public void InitMagicZoom(GameObject attackerObj,SkillData skillData)
		{
			m_curEffectTime = 0;
			m_attacker = attackerObj.GetComponent<Actor> ();
		}
		
		void Update () 
		{
//			Debug.Log ("MagicZone Update m_curEffectTime = "+m_curEffectTime);
			if (m_curEffectTime == -1)
				return;
			m_curEffectTime += Time.deltaTime;
			if (m_curEffectTime >= m_EffectTime) 
			{
				isEffect ();
				m_curEffectTime = -1;
			}
		}

		void isEffect()
		{
			List<GameObject> monsters = BattleScene.Active.m_monsterObjList;
			for (int i = 0; i < monsters.Count; i++) 
			{
				GameObject obj = monsters [i];
				if (obj == null)
					continue;
				float dis = Vector3.Distance (obj.gameObject.transform.position, this.gameObject.transform.position);
				if (dis < 5) 
				{
					Monster monster = obj.gameObject.GetComponent<Monster> ();
					monster.LoseBlood (m_attacker,-2f);
				}
			}
		}
	}
}
