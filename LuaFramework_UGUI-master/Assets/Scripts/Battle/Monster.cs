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

		public void InitActor (GameObject obj) 
		{
			m_ActorObject = obj;
			m_ActorData = new ActorData ();

//			m_ActorAgentManager = m_ActorObject.AddComponent<ActorAgentManager> ();

//			m_ActorBodyManager = m_ActorObject.AddComponent<ActorBobyManager> ();
//			m_ActorBodyManager.InitBoby ();

			m_ActorUIManager = m_ActorObject.AddComponent<ActorUIManager> ();
			m_ActorUIManager.InitActorBlood();

//			m_ActorMeshManager = m_ActorObject.AddComponent<ActorMeshManager> ();
//
//			m_ActorAnimationManager = m_ActorObject.AddComponent<ActorAnimationManager> ();
//			m_ActorAnimationManager.InitAnimation ();
//
//			m_ActorAIManager = m_ActorObject.AddComponent<ActorAIManager> ();

			//			Util.CallMethod("FirstBattleScene", "ActorDone");
		}


		// Update is called once per frame
		void Update () 
		{

		}
	}
}

