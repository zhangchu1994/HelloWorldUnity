﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGame;
using UnityEngine.UI;

namespace GlobalGame 
{
	public class BattleScene : MonoBehaviour 
	{
		public static BattleScene Active = null;

		private int m_MonsterPlaceCount = 4;//有几处怪
//		private int m_MonsterCount = 5;


		public GameObject m_StartPoint;
		public List<GameObject> m_MonsterPoints;

//		public GameObject m_ActorObject;
//		public Actor m_Actor;
//		public GameObject m_ActorObject1;
//		public Actor m_Actor1;

		public List<GameObject> m_monsterObjList = new List<GameObject>{};
		public List<GameObject> m_actorObjList = new List<GameObject>{};
		public List<Actor> m_monsterList = new List<Actor>{};//Monster
		public List<Actor> m_actorList = new List<Actor>{};

		public Material BasicOutLine;
		public Material NormalMaterial;

		public int m_FightIndex = 0;

		public enum BattleSequence
		{
			ClockWise,
			AntiClockWise
		}

		public BattleSequence m_Sequence;

		void Awake()
		{
			if (Active == null)
				Active = this;
		}

		void Start () 
		{
			m_Sequence = BattleSequence.ClockWise;

			StartCoroutine(StartBattle());  
		}

		public GameObject GetFllowActor()
		{
			if (m_actorObjList != null && m_actorObjList.Count >= 1)
				return m_actorObjList [0];
			else
				return null;
		}

		void InitBattleRole()
		{
			string[] names = {"Actor/Actor1/ch_pc_hou","Actor/Actor2","Actor/Actor2"};
			for (int i = 0; i < 3; i++) 
			{
				Object res = Resources.Load (names[i]);
				GameObject ActorObject = GameObject.Instantiate (res) as GameObject;
				Actor actor = ActorObject.GetComponent<Actor>();
				ActorObject.name = Global.GetActorNmae(i);
				actor.InitActor (ActorObject,i);
				m_actorObjList.Add(ActorObject);
				m_actorList.Add (actor);
				ActorObject.transform.rotation = m_StartPoint.transform.rotation;

				if (i == 0) 
				{
					ActorObject.transform.position = new Vector3 (m_StartPoint.transform.position.x, m_StartPoint.transform.position.y, m_StartPoint.transform.position.z);
//					ActorObject.transform.rotation = new Vector3 (m_StartPoint.transform.Rotate, m_StartPoint.transform.position.y, m_StartPoint.transform.position.z);
				}
				else if (i == 1) 
				{
					GameObject ActorObject0 = m_actorObjList[0];
					Actor actor0 = ActorObject0.GetComponent<Actor>();
					ActorObject.transform.position = new Vector3 (actor0.m_Fllow1.transform.position.x, actor0.m_Fllow1.transform.position.y, actor0.m_Fllow1.transform.position.z);
				} 
				else if (i == 2) 
				{
					GameObject ActorObject0 = m_actorObjList[0];
					Actor actor0 = ActorObject0.GetComponent<Actor>();
					ActorObject.transform.position = new Vector3 (actor0.m_Fllow2.transform.position.x, actor0.m_Fllow2.transform.position.y, actor0.m_Fllow2.transform.position.z);
				}
			}

			InitMonster();
		}

		void InitMonster()
		{
//			DestoryMonster ();
			m_monsterObjList.Clear ();
			m_monsterList.Clear ();
			string[] nameList = { "enemy/01-FlowerMonster-Blue", "enemy/03-MaskedOrc-Grey","enemy/01-FlowerMonster-Blue","enemy/03-MaskedOrc-Grey" };
			float num = 1.5f;
			float[] xOffet = {0,-num,num,-num,num };
			float[] zOffet = {0,-num,-num,num,num };
			string name = nameList [m_FightIndex];
			GameObject startPoint = m_MonsterPoints [m_FightIndex];
			int monsterCount = GetMonsterCount ();
			for (int i = 0; i < monsterCount; i++) 
			{
				if (m_FightIndex == 3)
					name = "enemy/Boss";
				Object monsterRes = Resources.Load (name);
				GameObject monsterObject = GameObject.Instantiate (monsterRes) as GameObject;
				//				GameObject monsterObject = GameObject.Find("Monster"+i.ToString());
				if (m_FightIndex == 3)
					monsterObject.transform.localScale = new Vector3 (3, 3, 3);
				Monster monster = monsterObject.GetComponent<Monster>();
				monster.name = Global.GetMonsterNmae (i);
				monster.InitActor (monsterObject);
				m_monsterObjList.Add (monsterObject);
				m_monsterList.Add (monster);

				monsterObject.transform.position = new Vector3 (startPoint.transform.position.x+xOffet[i], startPoint.transform.position.y, startPoint.transform.position.z+zOffet[i]);
			}
		}

		int GetMonsterCount()
		{
			if (m_FightIndex == 3)
				return 1;
			else
				return 5;
		}

		public void DestoryMonster(GameObject monsterObj,Actor monster)
		{
//			Debug.Log ("DestoryMonster = "+monsterObj.name);
			Destroy (monsterObj);
		}

		public void DestoryMonsters()
		{
			if (m_monsterObjList.Count <= 0)
				return;
			for (int i = 0; i < m_monsterObjList.Count; i++) 
			{
				GameObject obj = m_monsterObjList [i];
				Destroy (obj);
			}
		}

		IEnumerator StartBattle()  
	    {  
	    	yield return new WaitForSeconds(0.2f);
			InitBattleRole ();
	    }  

//		void UpdateClick()
//		{
//			if (Input.GetMouseButtonDown (0)) 
//			{  
//				HandleLeftClick();
//			} 
//			else if (Input.GetMouseButtonDown (1)) 
//			{
//				HandleRightClick ();
//			}
//		}

//		void HandleRightClick()
//		{
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  
//			if (Physics.Raycast (ray)) 
//			{  
//				RaycastHit[] hits = Physics.RaycastAll (ray);
//				for (int i = 0; i < hits.Length; i++) 
//				{
//					RaycastHit hit = hits [i];
////					ShootFront (hit.point);
//				}
//			}
//		}


//		void HandleLeftClick()//鼠标左键
//		{
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  
//			if (Physics.Raycast (ray)) 
//			{  
//				RaycastHit[] hits = Physics.RaycastAll (ray);
//
//				int monsterHitIndex = -1;
//				int groundHitIndex = -1;
//				for (int i = 0; i < hits.Length; i++) 
//				{
//					RaycastHit hit = hits [i];
//					GameObject obj = hit.collider.gameObject;
//					if (obj.CompareTag (Global.TagName_Enemy))
//						monsterHitIndex = i;
//					if (obj.CompareTag (Global.TagName_Ground))
//						groundHitIndex = i;
//				}
//
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
//			}  
//		}

//		void MoveToPoint(RaycastHit groundHit)
//		{
//			for (int i = 0; i < 1; i++) //m_actorList.Count
//			{
//				GameObject actorObj = m_actorObjList [i];
//				Actor actor = m_actorList [i];
//				actor.SetActorStatus (Actor.ActorStatus.Agent);
//				actor.m_ActorAgentManager.SetDestination (groundHit.point, groundHit.normal);
//			}
//		}

		public GameObject GetCloseMonsterObj(GameObject selfObj,Actor.ActorType actorType)
		{
			List<GameObject> objList = null;
			List<Actor> actorList = null;

			if (actorType == Actor.ActorType.Actor) 
			{
				objList = m_actorObjList;
				actorList = m_actorList;
			} 
			else if (actorType == Actor.ActorType.Monster) 
			{
				objList = m_monsterObjList;
				actorList = m_monsterList;
			}

			GameObject closeMonster = null;
			float closeDis = 100000;
			for (int i = 0; i < objList.Count; i++) //m_actorList.Count
			{
				GameObject monsterObj = objList [i];
				Actor monster = actorList [i];
				if (monsterObj == null || monster.IsActorStatus (Actor.ActorStatus.Dead) == true)
					continue;
				
				float dis = Vector3.Distance (selfObj.transform.position,monsterObj.transform.position);

				if (dis < closeDis) 
				{
					closeMonster = monsterObj;
					closeDis = dis;
				}
					
			}
			return closeMonster;
		}

		public void CurrentMonsterDie()
		{
			if (IsCurrentMonsterHasAlive () == true)
				return;
				

			if (m_FightIndex == m_MonsterPlaceCount-1) 
			{
				m_Sequence = BattleSequence.AntiClockWise;
			}
			else if (m_FightIndex == 0)
			{
				m_Sequence = BattleSequence.ClockWise;
			}

			if (m_Sequence == BattleSequence.ClockWise)
				m_FightIndex++;
			else if (m_Sequence == BattleSequence.AntiClockWise)
				m_FightIndex--;

			InitMonster();
		}

		bool IsCurrentMonsterHasAlive()
		{
			bool hasAlive = false;
			for (int i = 0; i < m_monsterList.Count; i++) 
			{
				Actor monster = (Actor)m_monsterList [i];
				if (monster.IsActorStatus (Actor.ActorStatus.Dead) == false)
					return true;
			}
			return hasAlive;
		}

//		void ChangeHeightLightMonster(GameObject obj)//怪物的选中状态
//		{
//			for (int i = 0; i < m_monsterObjList.Count; i++) 
//			{
//				GameObject monsterObject = m_monsterObjList [i];
//				if (monsterObject == null)
//					continue;
//				Renderer render = monsterObject.GetComponentInChildren<Renderer> ();
//				Monster monster = monsterObject.GetComponent<Monster> ();
////				Debug.Log (render.material.name + "i = "+i);
//
//				if (obj != null && monsterObject.name == obj.name && monster.m_isHightLight == false) 
//				{
//					Material mater = new Material (Shader.Find ("Toon/Basic Outline"));
//					mater.CopyPropertiesFromMaterial (BasicOutLine);
//					render.material = mater;
//					monster.m_isHightLight = true;
//				} 
//				else if ( monster.m_isHightLight == true )
//				{
//					Material mater = new Material (Shader.Find ("Legacy Shaders/VertexLit"));
//					mater.CopyPropertiesFromMaterial (NormalMaterial);
//					render.material = mater;
//					monster.m_isHightLight = false;
//				}
//			}
//		}

		void Update () 
		{
//			UpdateClick ();
		}
	}
}
	