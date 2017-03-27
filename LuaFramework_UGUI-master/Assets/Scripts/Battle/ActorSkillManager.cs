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
			m_SkillList = new List<Skill> ();
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
//			string[] ids = m_MainActor.m_ActorData.m_SkillIds.Split(',');  
//			for (int i = 0; i < ids.Length; i++) 
//			{
//				int id = int.Parse (ids [i]);
//				Skill skill = new Skill();
//				skill.InitSkill (m_MainActor,id);
//				m_SkillList.Add (skill);
//			}
			SortSkillList();
			for (int i = 0; i < m_SkillList.Count; i++) 
			{
				Skill skill = m_SkillList [i];
				Debug.Log ("_____________________________ Id = "+skill.m_SkillData.m_Id);
			}
			Debug.Log ("InitSkillManager__________________________________");
		}

//		List<ClientDefine.S_Item> SortByFightValue(List<ClientDefine.S_Item> m_EquipList)
//		{
//			List<int> m_FightValue = new List<int>();
//			for(int i = 0; i < m_EquipList.Count; i++)
//			{
//				m_FightValue.Add(RefreshEquipFightValue(m_EquipList[i]));
//			}
//			for(int i = 0; i < m_EquipList.Count; i++)
//			{
//				ClientDefine.S_Item TempItem = new ClientDefine.S_Item();
//				for (int j = i+1; j < m_EquipList.Count; j++)
//				{
//					if (m_FightValue[i] < m_FightValue[j])
//					{
//						TempItem = m_EquipList[i];
//						m_EquipList[i] = m_EquipList[j];
//						m_EquipList[j] = TempItem;
//					}
//				}
//			}
//			return m_EquipList;
//		}

		void SortSkillList()
		{
			string[] ids = m_MainActor.m_ActorData.m_SkillIds.Split(',');

			int[] skillIdList = Global.StringArrayToIntArray (ids);
			for(int i = 0; i < ids.Length; i++)
			{
				SkillData TempItem;
				for (int j = i+1; j < ids.Length; j++)
				{
					int skillId0 = int.Parse(ids[i]);
					int skillId1 = int.Parse(ids[j]);
					SkillData skill0 = DataTables.GetSkillData(skillId0);
					SkillData skill1 = DataTables.GetSkillData(skillId1);
					if (skill0 == null || skill1 == null)
						continue;
					if (skill0.m_Priority < skill1.m_Priority)
					{
						TempItem = skill0;
						skillIdList[i] = skill1.m_Id;
						skillIdList[j] = TempItem.m_Id;
					}
				}
			}

			for (int i = 0; i < skillIdList.Length; i++) 
			{
				int id = skillIdList [i];
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
