using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalGame 
{
	public class ActorSkillManager : MonoBehaviour 
	{
		private Actor m_MainActor;
		private List<Skill> m_SkillList;
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
				Skill skill =m_SkillList[i];
				skill.Update ();
			}
		}

		public void InitSkillManager()
		{
			for (int i = 0; i < 1; i++) 
			{
				Skill skill = new Skill();
				skill.InitSkill (m_MainActor);
				m_SkillList.Add (skill);
			}
		}

		public Skill GetAvailableSkill()
		{
//			BattleScene.Active.m_monsterList;
			for (int i = 0; i < m_SkillList.Count; i++) 
			{
				Skill skill = m_SkillList[i];
//				skill.Update ();
				return skill;
			}
			return null;
		}

		public void StartUseSkill()
		{
			m_SkillList [0].StartSkill ();
		}
	}
}
