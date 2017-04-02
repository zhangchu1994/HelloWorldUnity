using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class Monster : Actor 
	{

		public bool m_isHightLight = false;

		// Use this for initialization
		void Start () 
		{

		}

		public void InitActor (GameObject obj,ActorData actorData) 
		{
			m_ActorObject = obj;
			m_ActorData = actorData;//DataTables.GetUserData (1);
			m_ActorType = ActorType.Monster;
			if (BattleScene.Active.m_FightIndex == BattleScene.Active.m_BossPos) 
			{
				m_ActorData.m_CurHp = 500;
				m_ActorData.m_MaxHp = 500;
				m_ActorType = ActorType.Boss;
			}
				
//			m_ActorAgentManager = m_ActorObject.AddComponent<ActorAgentManager> ();

//			m_ActorBodyManager = m_ActorObject.AddComponent<ActorBobyManager> ();
//			m_ActorBodyManager.InitBoby ();

			m_ActorUIManager = m_ActorObject.AddComponent<ActorUIManager> ();
			m_ActorUIManager.InitActorBlood();

//			m_ActorMeshManager = m_ActorObject.AddComponent<ActorMeshManager> ();
//
			m_ActorAnimationManager = m_ActorObject.AddComponent<ActorAnimationManager> ();
			m_ActorAnimationManager.InitAnimation ();
//
			m_MonsterAIManager = m_ActorObject.AddComponent<MonsterAIManager> ();

//			m_ActorSkillManager = m_ActorObject.AddComponent<ActorSkillManager> ();
//			m_ActorSkillManager.InitSkillManager ();

//			Util.CallMethod("FirstBattleScene", "ActorDone");
		}


		// Update is called once per frame
		void Update () 
		{

		}
	}
}

