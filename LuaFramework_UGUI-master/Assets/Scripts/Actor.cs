using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaFramework;
using UnityEngine.AI;

namespace GlobalGame 
{
	public class Actor :MonoBehaviour
	{

		public GameObject m_ActorObject = null;



		public Animation animationController = null;
		public ActorAgentManager m_ActorAgentManager;
		public ActorBobyManager m_ActorBodyManager;
		public ActorUIManager m_ActorUIManager;
		public ActorMeshManager m_ActorMeshManager;

		void Start()
		{
			
		}

		public void InitActor (GameObject obj) 
		{
			m_ActorObject = obj;
			m_ActorObject.name = "Ian1970";


			// Only for display
			animationController = m_ActorObject.GetComponent<Animation>();
//			PlayStand();
			PlayAnimation (Global.BattleAnimationType.Stand,WrapMode.Loop);
			m_ActorAgentManager = m_ActorObject.AddComponent<ActorAgentManager> ();
			m_ActorBodyManager = m_ActorObject.AddComponent<ActorBobyManager> ();
			m_ActorBodyManager.InitBoby ();
			m_ActorUIManager = m_ActorObject.AddComponent<ActorUIManager> ();
			m_ActorUIManager.InitActorBlood();
			m_ActorMeshManager = m_ActorObject.AddComponent<ActorMeshManager> ();

//			InitNavMesh ();
//			Util.CallMethod("FirstBattleScene", "ActorDone");
//			BattleScene.Active.RoleLoadDone();
		}



		public void PlayAnimation(Global.BattleAnimationType argType,WrapMode mode,bool isStop=false)
		{
			if (animationController.IsPlaying (Global.GetAnimation (argType)) == true)
				return;
			animationController.Stop ();
			animationController.wrapMode = mode;
			animationController.Play(Global.GetAnimation(argType));
		}

//		public void PlayStand () {
//			animationController.wrapMode = WrapMode.Loop;
//			animationController.Play((Global.GetAnimation(Global.BattleAnimationType.Stand)));
//			animationState = 0;
//		}

//		public void PlayAttack () 
//		{
//			animationController.wrapMode = WrapMode.Once;
//			animationController.PlayQueued("attack1");
//			animationController.PlayQueued("attack2");
//			animationController.PlayQueued("attack3");
//			animationController.PlayQueued("attack4");
//			animationState = 1;
//		}


		void UpdateAnimation()
		{
			
		}

		void UpdateAI()
		{
//			List<GameObject> monsterList = BattleScene.Active.m_monsterList;
//			for (int i = 0; i < monsterList.Count; i++) 
//			{
//				GameObject monsterObject = monsterList[i];
//				if (monsterObject == null)
//					continue;
//				Vector3 pos1 = new Vector3( monsterObject.transform.position.x, 0, monsterObject.transform.position.z );
//				Vector3 pos2 = new Vector3( m_ActorObject.transform.position.x, 0, m_ActorObject.transform.position.z );
////				Debug.Log ("UpdateAI = "+Vector3.Distance(pos1,pos2));
//
//				if (Vector3.Distance(pos1,pos2) <= 2)
//				{
//					DestroyImmediate (monsterObject);
//
//					Object psObj = Resources.Load ("Effect/CircleFX_Dark");
//					GameObject t = Instantiate(psObj) as GameObject;
//					t.transform.position = pos1;
//					t.transform.localScale = new Vector3 (2f, 2f, 2f);
//				}
//			}
		}

		public void Update () 
		{
			UpdateAnimation ();
			UpdateAI ();
		}
	}
}
