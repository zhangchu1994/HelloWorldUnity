using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalGame 
{
	public class ActorSkillManager : MonoBehaviour 
	{
		private Actor m_MainActor;
		private List<Skill> m_SkillList;
		private int m_SkillIndex;

		void Awake()
		{
			m_MainActor = this.transform.GetComponent<Actor> ();
			m_SkillList = new  List<Skill> ();
		}

		// Use this for initialization
		void Start () 
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			for (int i = 0; i < m_SkillList.Count; i++) 
			{
				Skill skill = m_SkillList[i];
				skill.Update ();
			}
		}

		public void InitSkillManager()
		{
			string[] ids = m_MainActor.m_ActorData.m_SkillIds.Split(',');  
			for (int i = 0; i < ids.Length; i++) 
			{
				int id = int.Parse (ids [i]);
				Skill skill = new Skill();
				skill.InitSkill (m_MainActor,id);
				m_SkillList.Add (skill);
			}
		}

//		public Skill GetAvailableSkill()
//		{
////			BattleScene.Active.m_monsterList;
//			for (int i = 0; i < m_SkillList.Count; i++) 
//			{
//				Skill skill = m_SkillList[i];
////				skill.Update ();
//				return skill;
//			}
//			return null;
//		}

		public void StartUseSkill()
		{
			m_SkillIndex++;
			if (m_SkillIndex >= m_SkillList.Count)
				m_SkillIndex = 0;
			m_SkillList [m_SkillIndex].StartSkill ();
		}

		public Skill GetCurrentSkill()
		{
			return m_SkillList [m_SkillIndex];
		}
	}
}
