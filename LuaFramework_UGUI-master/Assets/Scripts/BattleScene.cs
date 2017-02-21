using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGame;
using UnityEngine.UI;

namespace GlobalGame 
{
	public class BattleScene : MonoBehaviour 
	{
		public static BattleScene Active = null;


		public GameObject m_StartPoint;
		public GameObject m_EndPonit;
		public GameObject m_RoleObject;
		public Actor m_Actor;
		public List<GameObject> m_monsterList = new List<GameObject>{};
		public Material BasicOutLine;
		public Material NormalMaterial;

		void Awake()
		{
			if (Active == null)
				Active = this;
		}

		// Use this for initialization
		void Start () 
		{
			m_StartPoint = GameObject.Find ("StartPoint");
			m_EndPonit = GameObject.Find ("EndPoint");

			InitBattleRole ();

			StartCoroutine(StartBattle());  
		}

		void InitBattleRole()
		{
	//		m_Role = GameObject.Find ("Ian1994");
	//		SetNavMesh ();

			string skeleton = "ch_pc_hou";
			//Creates the skeleton object
			Object res = Resources.Load ("Actor/Actor1/" + skeleton);
			GameObject ActorObject = GameObject.Instantiate (res) as GameObject;
			Actor actor = ActorObject.GetComponent<Actor>();
			actor.InitActor (ActorObject);
			m_RoleObject = GameObject.Find ("Ian1970");
			m_RoleObject.transform.position = new Vector3 (m_StartPoint.transform.position.x, m_StartPoint.transform.position.y, m_StartPoint.transform.position.z);
			m_Actor = actor;



			for (int i = 1; i <= 3; i++) 
			{
	//			Object monsterRes = Resources.Load ("Actor/Actor1/" + skeleton);
	//			GameObject monsterObject = GameObject.Instantiate (res) as GameObject;
	//			monsterObject.transform = 
				GameObject monsterObject = GameObject.Find("Monster"+i.ToString());
				m_monsterList.Add (monsterObject);
			}
		}

		IEnumerator StartBattle()  
	    {  
	    	yield return new WaitForSeconds(2);
	//		TestClass.SetNavMesh ();
	    }  

		void updateClick()
		{
			if(Input.GetMouseButtonDown(0))  
	       {  
	           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
	           if (Physics.Raycast(ray))  
	           {  
	//               	Debug.Log("okok");  
					RaycastHit[] hits = Physics.RaycastAll(ray);
					foreach (RaycastHit hit in hits) 
					{
						GameObject obj =  hit.collider.gameObject;
						Debug.Log ("updateClick________________"+obj.name);

//						for (int i = 0; i < m_monsterList.Count; i++) 
//						{
//							GameObject monsterObject = m_monsterList [i];
//							if (monsterObject == null)
//								continue;
//							Renderer render = monsterObject.GetComponentInChildren<Renderer> ();
//							Debug.Log (render.material.name + "i = "+i);
//
//							if (monsterObject.name == obj.name) {
//								Material mater = new Material (Shader.Find ("Toon/Basic Outline"));
//								mater.CopyPropertiesFromMaterial (BasicOutLine);
//								render.material = mater;
//								m_Actor.m_ActorAgentManager.SetDestination (monsterObject.transform.position,Vector3.zero,true);
//							} 
//							else 
//							{
//								Material mater = new Material (Shader.Find ("Legacy Shaders/VertexLit"));
//								mater.CopyPropertiesFromMaterial (NormalMaterial);
//								render.material = mater;
//							}
//						}

					}
	           }  
	       }
		}




		void Update () 
		{
			updateClick ();
		}

	}
}
	