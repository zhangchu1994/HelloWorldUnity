using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaFramework;
using UnityEngine.AI;

namespace GlobalGame 
{
	public class Actor : MonoBehaviour
	{

		public GameObject m_ActorObject = null;

		public ActorAgentManager m_ActorAgentManager;
		public ActorBobyManager m_ActorBodyManager;
		public ActorUIManager m_ActorUIManager;
		public ActorMeshManager m_ActorMeshManager;
		public ActorAnimationManager m_ActorAnimationManager;
		public ActorAIManager m_ActorAIManager;

		void Start()
		{
			
		}

		public void InitActor (GameObject obj) 
		{
			m_ActorObject = obj;
			m_ActorObject.name = Global.ActorName;


			m_ActorAgentManager = m_ActorObject.AddComponent<ActorAgentManager> ();

			m_ActorBodyManager = m_ActorObject.AddComponent<ActorBobyManager> ();
			m_ActorBodyManager.InitBoby ();

			m_ActorUIManager = m_ActorObject.AddComponent<ActorUIManager> ();
			m_ActorUIManager.InitActorBlood();

			m_ActorMeshManager = m_ActorObject.AddComponent<ActorMeshManager> ();

			m_ActorAnimationManager = m_ActorObject.AddComponent<ActorAnimationManager> ();
			m_ActorAnimationManager.InitAnimation ();

			m_ActorAIManager = m_ActorObject.AddComponent<ActorAIManager> ();

//			Util.CallMethod("FirstBattleScene", "ActorDone");
		}

		void Update () 
		{
			
		}
	}
}
