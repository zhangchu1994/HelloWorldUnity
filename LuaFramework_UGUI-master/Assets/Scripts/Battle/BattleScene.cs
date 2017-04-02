using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGame;
using UnityEngine.UI;
using LuaFramework;

namespace GlobalGame 
{
	public class BattleScene : MonoBehaviour 
	{
		public static BattleScene Active = null;

		private int m_MonsterPlaceCount = 4;//有几处怪
//		private int m_MonsterCount = 5;


		public GameObject m_StartPoint;
		public List<GameObject> m_MonsterPoints;

		public List<GameObject> m_monsterObjList = new List<GameObject>{};
		public List<GameObject> m_actorObjList = new List<GameObject>{};
		public List<Actor> m_monsterList = new List<Actor>{};//Monster
		public List<Actor> m_actorList = new List<Actor>{};

		public Material BasicOutLine;
		public Material NormalMaterial;

		private Dictionary<string,GameObject> m_PreLoadActor = new Dictionary<string,GameObject>();
		private Dictionary<string,GameObject> m_PreLoadMonster = new Dictionary<string,GameObject>();

		public int m_FightIndex = 0;
		public int m_BossPos = 2;

		public int ProloadCount = 0;
		public PveData m_CurPveData;

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

		void Start()
		{
			ProloadCount = 0;
			m_Sequence = BattleSequence.ClockWise;
			StartCoroutine(InitLua());  
		}

		void Update () 
		{
			//			UpdateClick ();
			if (ProloadCount == -1)
				return;
			if (ProloadCount == 3) 
			{
				ProloadCount = -1;
				InitBattleData ();
				InitBattleRole ();
				InitMonster ();
			}
		}

		IEnumerator InitLua()
		{
			yield return new WaitForSeconds (0.5f);


			PreloadActor();
		}

		void InitBattleData()
		{
			m_CurPveData = DataTables.GetPveData (1);
		}



		void PreloadActor()
		{
			int[] Ids = {1,2,3};//2,
			for (int i = 0; i < Ids.Length; i++) 
			{
				ActorData actorData = DataTables.GetUserData (Ids[i]);
				Dictionary<string,string> info1 = Global.CreateABInfo(actorData.m_Model,Global.GetAssetName(actorData.m_Model),AssetType.Perfab,i);
				ResourceManager.Active.LoadPrefabWithInfo(info1, delegate(UnityEngine.Object[] objs,Dictionary<string,string> info) {
					ProloadCount++;
					string index = Global.getDicStrVaule(info1,Global.ABInfoKey_Index);

					GameObject prefab = objs[0] as GameObject;
					if (m_PreLoadActor.ContainsKey(Global.GetAssetName(actorData.m_Model)) == false)
						m_PreLoadActor.Add(Global.GetAssetName(actorData.m_Model),prefab);
				});
			}
//			InitBattleRole ();
		}

		void InitBattleRole()
		{
			float num = 1.5f;
			float[] xOffet = {0,-num,num };//,-num,num
			float[] zOffet = {0,-num,-num };//,num,num

			int[] Ids = {1,2,3};//2,
			for (int i = 0; i < Ids.Length; i++) 
			{
				ActorData actorData = DataTables.GetUserData (Ids[i]);

				GameObject prefab = Global.getDicObjVaule(m_PreLoadActor,Global.GetAssetName(actorData.m_Model));
				GameObject ActorObject = GameObject.Instantiate (prefab) as GameObject;
				Actor actor = ActorObject.GetComponent<Actor>();
				ActorObject.name = Global.GetActorNmae(i);
				actor.InitActor (ActorObject,i,actorData);
				m_actorObjList.Add(ActorObject);
				m_actorList.Add (actor);
				ActorObject.transform.position = new Vector3 (m_StartPoint.transform.position.x+xOffet[i], m_StartPoint.transform.position.y, m_StartPoint.transform.position.z+zOffet[i]);
			}
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
//			string name = nameList [m_FightIndex];
			GameObject startPoint = m_MonsterPoints[m_FightIndex];
			int monsterCount = GetMonsterCount ();
			for (int i = 0; i < monsterCount; i++) 
			{
//				if (m_FightIndex == m_BossPos)
//					name = "enemy/Boss";

				string abPath = "01-FlowerMonster-Blue";
				Dictionary<string,string> info1 = Global.CreateABInfo ("modelenemy/"+abPath,abPath,AssetType.Perfab,i);
				ResourceManager.Active.LoadPrefabWithInfo(info1, delegate(UnityEngine.Object[] objs,Dictionary<string,string> info) {
					GameObject prefab = objs[0] as GameObject;

					string indexStr = "";
					info.TryGetValue("Index",out indexStr);
					int index = int .Parse(indexStr);

					GameObject monsterObject = GameObject.Instantiate (prefab) as GameObject;
					ActorData actorData = DataTables.GetMonsterData (index);
					if (m_FightIndex == m_BossPos)
						monsterObject.transform.localScale = new Vector3 (3, 3, 3);
					Monster monster = monsterObject.GetComponent<Monster>();
//					Debug.Log("InitMonster  index = "+index+" name = ");//+Global.GetMonsterNmae (i)
					monsterObject.name = Global.GetMonsterNmae (index);
					monster.InitActor (monsterObject,actorData);
					m_monsterObjList.Add (monsterObject);
					m_monsterList.Add (monster);

					monsterObject.transform.position = new Vector3 (startPoint.transform.position.x+xOffet[index], startPoint.transform.position.y, startPoint.transform.position.z+zOffet[index]);
				});
			}
		}

		int GetMonsterCount()
		{
			if (m_FightIndex == m_BossPos)
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

		public GameObject GetFllowActor()
		{
			if (m_actorObjList != null && m_actorObjList.Count >= 1)
				return m_actorObjList [0];
			else
				return null;
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


	}
}
	