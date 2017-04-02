using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class MagicZone : MonoBehaviour 
	{
		private float m_curEffectTime;
		private Actor m_attacker;
//		private Actor m_defenser;
		private SkillData m_SkillData;

		void Awake()
		{
			m_curEffectTime = -1;


		}

		void Start () 
		{
			
		}

		public void InitMagicZoom(GameObject attackerObj,SkillData skillData)
		{
			m_curEffectTime = 0;
			m_attacker = attackerObj.GetComponent<Actor> ();
//			m_defenser = defenceObj.GetComponent<Actor> ();
			m_SkillData = skillData;
		}
		
		void Update () 
		{
//			Debug.Log ("MagicZone Update m_curEffectTime = "+m_curEffectTime);
			if (m_curEffectTime == -1)
				return;
			m_curEffectTime += Time.deltaTime;
			if (m_curEffectTime >= m_SkillData.m_Delay) 
			{
				isEffect ();
				m_curEffectTime = -1;
			}
		}

		void isEffect()
		{
			for (int i = 0; i < m_SkillData.m_TargetObjList.Count; i++) 
			{
				GameObject obj = m_SkillData.m_TargetObjList [i];
//				Debug.Log ("MagicZone isEffect = "+obj.name);
				Actor defenser = obj.gameObject.GetComponent<Actor> ();
				float damage = GlobalBattle.GetSkillDamage (m_attacker,defenser,m_SkillData);
				defenser.LoseBlood (m_attacker,damage);
			}

//			List<GameObject> monsters = BattleScene.Active.m_monsterObjList;
//			for (int i = 0; i < monsters.Count; i++) 
//			{
//				GameObject obj = monsters [i];
//				if (obj == null)
//					continue;
//				float dis = Vector3.Distance (obj.gameObject.transform.position, this.gameObject.transform.position);
//				if (dis < 5) 
//				{
//					Monster monster = obj.gameObject.GetComponent<Monster> ();
//					float damage = GlobalBattle.GetSkillDamage (m_attacker,m_defenser,m_SkillData);
//					monster.LoseBlood (m_attacker,damage);
//				}
//			}
		}
	}
}
