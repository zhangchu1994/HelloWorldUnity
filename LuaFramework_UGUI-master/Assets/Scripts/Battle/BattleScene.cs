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
		public GameObject m_ActorObject;
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
//			m_EndPonit = GameObject.Find ("EndPoint");

			InitBattleRole ();

			StartCoroutine(StartBattle());  
		}

		void InitBattleRole()
		{
	//		m_Role = GameObject.Find ("Ian1994");
	//		SetNavMesh ();

			string skeleton = "ch_pc_hou";
			Object res = Resources.Load ("Actor/Actor1/" + skeleton);
			GameObject ActorObject = GameObject.Instantiate (res) as GameObject;
			Actor actor = ActorObject.GetComponent<Actor>();
			actor.InitActor (ActorObject);
			m_ActorObject = GameObject.Find (Global.ActorName);
			m_ActorObject.transform.position = new Vector3 (m_StartPoint.transform.position.x, m_StartPoint.transform.position.y, m_StartPoint.transform.position.z);
			m_Actor = actor;

			for (int i = 1; i <= 3; i++) 
			{
//			Object monsterRes = Resources.Load ("Actor/Actor1/" + skeleton);
//			GameObject monsterObject = GameObject.Instantiate (res) as GameObject;
				GameObject monsterObject = GameObject.Find("Monster"+i.ToString());
				Monster monster = monsterObject.GetComponent<Monster>();
				monster.InitActor (monsterObject);
				m_monsterList.Add (monsterObject);
			}
		}

		IEnumerator StartBattle()  
	    {  
	    	yield return new WaitForSeconds(2);
	    }  

		void UpdateClick()
		{
			if (Input.GetMouseButtonDown (0)) 
			{  
				HandleLeftClick();
			} 
			else if (Input.GetMouseButtonDown (1)) 
			{
				HandleRightClick ();
			}
		}

		void HandleRightClick()
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  
			if (Physics.Raycast (ray)) 
			{  
				RaycastHit[] hits = Physics.RaycastAll (ray);
				for (int i = 0; i < hits.Length; i++) 
				{
					RaycastHit hit = hits [i];
					m_ActorObject.transform.LookAt (hit.point);
				}
			}
			Object psObj = Resources.Load ("Effect/CloudFlashFX");
			GameObject t = Instantiate(psObj) as GameObject;
			Global.ChangeParticleScale (t,2.5f);
			t.transform.position = m_ActorObject.transform.position;
			Bullet bulletScripte = t.GetComponent<Bullet>();
			GameObject monsterObject = m_monsterList [0];
			bulletScripte.InitBullet (m_ActorObject, monsterObject);
			m_Actor.m_ActorAnimationManager.PlayAnimation (Global.BattleAnimationType.Attack, WrapMode.Loop, true);
		}

		void HandleLeftClick()//鼠标左键
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  
			if (Physics.Raycast (ray)) {  
				RaycastHit[] hits = Physics.RaycastAll (ray);

				int monsterHitIndex = -1;
				int groundHitIndex = -1;
				for (int i = 0; i < hits.Length; i++) 
				{
					RaycastHit hit = hits [i];
					GameObject obj = hit.collider.gameObject;
					if (obj.CompareTag (Global.TagName_Enemy))
						monsterHitIndex = i;
					if (obj.CompareTag (Global.TagName_Ground))
						groundHitIndex = i;
				}

				if (monsterHitIndex != -1) 
				{
					RaycastHit monsterHit = hits [monsterHitIndex];
					GameObject obj =  monsterHit.collider.gameObject;
					AttackMonster (obj);
					ChangeHeightLightMonster (obj);
				}

				if (groundHitIndex != -1 && monsterHitIndex == -1) 
				{
					RaycastHit groundHit = hits [groundHitIndex];
					GameObject obj = groundHit.collider.gameObject;
					m_Actor.m_ActorAgentManager.SetDestination (groundHit.point, groundHit.normal, false);
					ChangeHeightLightMonster (null);
				}
			}  
		}

		void AttackMonster(GameObject obj)
		{
			for (int i = 0; i < m_monsterList.Count; i++) 
			{
				GameObject monsterObject = m_monsterList [i];
				if (monsterObject == null)
					continue;
				if (monsterObject.name == obj.name) 
				{
					m_Actor.m_ActorAgentManager.SetDestination (monsterObject.transform.position,Vector3.zero,true);
				} 
			}
		}


		void ChangeHeightLightMonster(GameObject obj)
		{
			for (int i = 0; i < m_monsterList.Count; i++) 
			{
				GameObject monsterObject = m_monsterList [i];
				if (monsterObject == null)
					continue;
				Renderer render = monsterObject.GetComponentInChildren<Renderer> ();
				Monster monster = monsterObject.GetComponent<Monster> ();
				Debug.Log (render.material.name + "i = "+i);

				if (obj != null && monsterObject.name == obj.name && monster.m_isHightLight == false) 
				{
					Material mater = new Material (Shader.Find ("Toon/Basic Outline"));
					mater.CopyPropertiesFromMaterial (BasicOutLine);
					render.material = mater;
					monster.m_isHightLight = true;
				} 
				else if ( monster.m_isHightLight == true )
				{
					Material mater = new Material (Shader.Find ("Legacy Shaders/VertexLit"));
					mater.CopyPropertiesFromMaterial (NormalMaterial);
					render.material = mater;
					monster.m_isHightLight = false;
				}
			}
		}

		public void MonsterLoseBlood()
		{
			for (int i = 0; i < m_monsterList.Count; i++) 
			{
				GameObject monsterObject = m_monsterList [i];
				if (monsterObject == null)
					continue;
				Monster monster = monsterObject.GetComponent<Monster> ();
				if (monster.m_isHightLight == true) 
				{
					monster.m_ActorUIManager.InitLoseBlood();
				}
			}
		}

		void Update () 
		{
			UpdateClick ();
		}
	}
}
	