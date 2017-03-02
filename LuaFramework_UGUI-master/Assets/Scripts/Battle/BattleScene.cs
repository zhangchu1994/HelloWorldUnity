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


		public List<GameObject> m_StartPoints;
		public List<GameObject> m_MonsterPoints;

//		public GameObject m_ActorObject;
//		public Actor m_Actor;
//		public GameObject m_ActorObject1;
//		public Actor m_Actor1;

		public List<GameObject> m_monsterObjList = new List<GameObject>{};
		public List<GameObject> m_actorObjList = new List<GameObject>{};
		public List<Monster> m_monsterList = new List<Monster>{};
		public List<Actor> m_actorList = new List<Actor>{};

		public Material BasicOutLine;
		public Material NormalMaterial;

		public int m_FightIndex = 0;

		void Awake()
		{
			if (Active == null)
				Active = this;
		}

		void Start () 
		{

			InitBattleRole ();

			StartCoroutine(StartBattle());  
		}

		public GameObject GetFllowActor()
		{
			return m_actorObjList [0];
		}

		void InitBattleRole()
		{
			for (int i = 0; i < 2; i++) 
			{
				string skeleton = "ch_pc_hou";
				Object res = Resources.Load ("Actor/Actor1/" + skeleton);
				GameObject ActorObject = GameObject.Instantiate (res) as GameObject;
				Actor actor = ActorObject.GetComponent<Actor>();
				ActorObject.name = Global.GetActorNmae(i);
				actor.InitActor (ActorObject,i);
				m_actorObjList.Add(ActorObject);
				m_actorList.Add (actor);

				GameObject startPoint = m_StartPoints [i];
				ActorObject.transform.position = new Vector3 (startPoint.transform.position.x, startPoint.transform.position.y, startPoint.transform.position.z);
			}


			for (int i = 0; i < 4; i++) 
			{
				Object monsterRes = Resources.Load ("enemy/01-FlowerMonster-Blue");
				GameObject monsterObject = GameObject.Instantiate (monsterRes) as GameObject;
//				GameObject monsterObject = GameObject.Find("Monster"+i.ToString());
				Monster monster = monsterObject.GetComponent<Monster>();
				monster.name = Global.GetMonsterNmae (i);
				monster.InitActor (monsterObject);
				m_monsterObjList.Add (monsterObject);
				m_monsterList.Add (monster);

				GameObject startPoint = m_MonsterPoints [i];
				monsterObject.transform.position = new Vector3 (startPoint.transform.position.x, startPoint.transform.position.y, startPoint.transform.position.z);
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
//					ShootFront (hit.point);
				}
			}
		}


		void HandleLeftClick()//鼠标左键
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  
			if (Physics.Raycast (ray)) 
			{  
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

//				if (monsterHitIndex != -1) //点击到怪物
//				{
//					RaycastHit monsterHit = hits [monsterHitIndex];
//					GameObject obj =  monsterHit.collider.gameObject;
//					Actor actor = m_actorList [0];
//					actor.MonsterToAttack (obj);
//					ChangeHeightLightMonster (obj);
//				}
//
//				if (groundHitIndex != -1 && monsterHitIndex == -1) //点击到地面
//				{
//					RaycastHit groundHit = hits [groundHitIndex];
//					GameObject obj = groundHit.collider.gameObject;
//					MoveToPoint (groundHit);
//					ChangeHeightLightMonster (null);
//				}
			}  
		}

		void MoveToPoint(RaycastHit groundHit)
		{
			for (int i = 0; i < 1; i++) //m_actorList.Count
			{
				GameObject actorObj = m_actorObjList [i];
				Actor actor = m_actorList [i];
				actor.SetActorStatus (Actor.ActorStatus.Agent);
				actor.m_ActorAgentManager.SetDestination (groundHit.point, groundHit.normal);
			}
		}

		public GameObject GetCurrentMonsterObj()
		{
			return m_monsterObjList [m_FightIndex];
		}

		public Monster GetCurrentMonster()
		{
			return m_monsterList [m_FightIndex];
		}

		public void CurrentMonsterDie()
		{
			m_FightIndex++;
			if (m_FightIndex >= 4)
				m_FightIndex = 0;
		}

		void ChangeHeightLightMonster(GameObject obj)//怪物的选中状态
		{
			for (int i = 0; i < m_monsterObjList.Count; i++) 
			{
				GameObject monsterObject = m_monsterObjList [i];
				if (monsterObject == null)
					continue;
				Renderer render = monsterObject.GetComponentInChildren<Renderer> ();
				Monster monster = monsterObject.GetComponent<Monster> ();
//				Debug.Log (render.material.name + "i = "+i);

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

		void Update () 
		{
			UpdateClick ();
		}
	}
}
	