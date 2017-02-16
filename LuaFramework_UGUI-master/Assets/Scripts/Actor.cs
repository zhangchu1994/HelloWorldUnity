using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaFramework;
using UnityEngine.AI;

namespace GlobalGame 
{
	public class Actor :MonoBehaviour
	{
		

		public GameObject m_CanvasParent;
		public GameObject m_TextPrefab;

		private GameObject m_ActorObject = null;
		private GameObject WeaponInstance = null;

		/// <summary>
		/// Equipment informations
		/// </summary>
		private string skeleton;
		private string equipment_head;
		private string equipment_chest;
		private string equipment_hand;
		private string equipment_feet;

		private int index;

		private bool rotate = false;
		private int animationState = 0;

		private Animation animationController = null;

		private readonly string[] m_Index = new string[]{ "004", "006", "008" };

		private const int DEFAULT_WEAPON = 0;
		private const int DEFAULT_HEAD = 0;
		private const int DEFAULT_CHEST = 0;
		private const int DEFAULT_HAND = 0;
		private const int DEFAULT_FEET = 0;
		private const bool DEFAULT_COMBINEMATERIAL = true;

		private NavMeshAgent m_agent = null;
		public bool shoudldGo = false;
		public Transform m_Targettransform = null;


		void Start()
		{
			
		}

		public void InitActor (GameObject obj) 
		{
			
			m_CanvasParent = GameObject.Find ("Canvas");
			int index = 1;
			string weapon = "ch_we_one_hou_" + m_Index [DEFAULT_WEAPON];
			string head = "ch_pc_hou_" + m_Index [DEFAULT_HEAD] + "_tou"; 
			string chest = "ch_pc_hou_" + m_Index [DEFAULT_CHEST] + "_shen"; 
			string hand = "ch_pc_hou_" + m_Index [DEFAULT_HAND] + "_shou";
			string feet = "ch_pc_hou_" + m_Index [DEFAULT_FEET] + "_jiao";
			bool combine = true;

			m_ActorObject = obj;
			m_ActorObject.name = "Ian1970";
			this.index = index;
			this.skeleton = skeleton;
			this.equipment_head = head;
			this.equipment_chest = chest;
			this.equipment_hand = hand;
			this.equipment_feet = feet;


			string[] equipments = new string[4];
			equipments [0] = head;
			equipments [1] = chest;
			equipments [2] = hand;
			equipments [3] = feet;

			// Create and collect other parts SkinnedMeshRednerer
			SkinnedMeshRenderer[] meshes = new SkinnedMeshRenderer[4];
			GameObject[] objects = new GameObject[4];
			for (int i = 0; i < equipments.Length; i++) {

				Object res = Resources.Load ("Actor/Actor1/" + equipments [i]);
				objects[i] = GameObject.Instantiate (res) as GameObject;
				meshes[i] = objects[i].GetComponentInChildren<SkinnedMeshRenderer> ();
			}

			// Combine meshes
			CombineSkinnedMgr.Instance.CombineObject (m_ActorObject, meshes, combine);

			// Delete temporal resources
			for (int i = 0; i < objects.Length; i++) {

				GameObject.DestroyImmediate (objects [i].gameObject);
			}

			// Create weapon
			Object res1 = Resources.Load ("Actor/Actor1/" + weapon);
			WeaponInstance = GameObject.Instantiate (res1) as GameObject;

			Transform[] transforms = m_ActorObject.GetComponentsInChildren<Transform>();
			foreach (Transform joint in transforms) {
				if (joint.name == "weapon_hand_r") {// find the joint (need the support of art designer)
					WeaponInstance.transform.parent = joint.gameObject.transform;
					break;
				}	
			}

			// Init weapon relative informations
			WeaponInstance.transform.localScale = Vector3.one;
			WeaponInstance.transform.localPosition = Vector3.zero;
			WeaponInstance.transform.localRotation = Quaternion.identity;

			// Only for display
			animationController = m_ActorObject.GetComponent<Animation>();
			PlayStand();
			InitActorBlood();

//			Util.CallMethod("FirstBattleScene", "ActorDone");
//			BattleScene.Active.RoleLoadDone();
		}

		public void ChangeHeadEquipment (string equipment,bool combine = false)
		{
			ChangeEquipment (0, equipment, combine);
		}

		public void ChangeChestEquipment (string equipment,bool combine = false)
		{
			ChangeEquipment (1, equipment, combine);
		}

		public void ChangeHandEquipment (string equipment,bool combine = false)
		{
			ChangeEquipment (2, equipment, combine);
		}

		public void ChangeFeetEquipment (string equipment,bool combine = false)
		{
			ChangeEquipment (3, equipment, combine);
		}

		public void ChangeWeapon (string weapon)
		{
			Object res = Resources.Load ("Prefab/" + weapon);
			GameObject oldWeapon = WeaponInstance;
			WeaponInstance = GameObject.Instantiate (res) as GameObject;
			WeaponInstance.transform.parent = oldWeapon.transform.parent;
			WeaponInstance.transform.localPosition = Vector3.zero;
			WeaponInstance.transform.localScale = Vector3.one;
			WeaponInstance.transform.localRotation = Quaternion.identity;

			GameObject.Destroy(oldWeapon);
		}

		public void ChangeEquipment (int index, string equipment,bool combine = false)
		{
			switch (index) {

			case 0:
				equipment_head = equipment;
				break;
			case 1:
				equipment_chest = equipment;
				break;
			case 2:
				equipment_hand = equipment;
				break;
			case 3:
				equipment_feet = equipment;
				break;
			}

			string[] equipments = new string[4];
			equipments [0] = equipment_head;
			equipments [1] = equipment_chest;
			equipments [2] = equipment_hand;
			equipments [3] = equipment_feet;

			Object res = null;
			SkinnedMeshRenderer[] meshes = new SkinnedMeshRenderer[4];
			GameObject[] objects = new GameObject[4];
			for (int i = 0; i < equipments.Length; i++) {

				res = Resources.Load ("Prefab/" + equipments [i]);
				objects[i] = GameObject.Instantiate (res) as GameObject;
				meshes[i] = objects[i].GetComponentInChildren<SkinnedMeshRenderer> ();
			}

			CombineSkinnedMgr.Instance.CombineObject (m_ActorObject, meshes, combine);

			for (int i = 0; i < objects.Length; i++) {

				GameObject.DestroyImmediate(objects[i].gameObject);
			}
		}


		public void PlayAnimation(Global.BattleAnimationType argType)
		{
			animationController.Play(Global.GetAnimation(argType));
		}

		public void PlayStand () {
			animationController.wrapMode = WrapMode.Loop;
			animationController.Play((Global.GetAnimation(Global.BattleAnimationType.Stand)));
			animationState = 0;
		}

		public void PlayAttack () 
		{
			animationController.wrapMode = WrapMode.Once;
			animationController.PlayQueued("attack1");
			animationController.PlayQueued("attack2");
			animationController.PlayQueued("attack3");
			animationController.PlayQueued("attack4");
			animationState = 1;
		}

		void InitActorBlood()
		{
			GameObject t = Instantiate(m_TextPrefab) as GameObject;
			t.transform.SetParent(m_CanvasParent.transform, false);

			//			RectTransform Rect = t.GetComponent<RectTransform> ();
			//		Text text = t.GetComponent<Text> ();

			//Create new text info to instatiate 
			//		bl_Text item = t.GetComponent<bl_Text>();
			//
			//		item.m_Color = color;
			//		item.m_Transform = trans;
			//		item.m_text = text;

			//		t.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}

		void UpdateBlood()
		{
			Transform trans = m_ActorObject.transform;
			GameObject t = GameObject.Find ("Canvas/Text(Clone)");
			if (t == null)
				return;
			RectTransform Rect = t.GetComponent<RectTransform> ();
			Vector3 position = trans.GetComponent<Collider>().bounds.center + (((Vector3.up * trans.GetComponent<Collider>().bounds.size.y) * 0.6f));
			Vector3 front = position - Camera.main.transform.position;
			//its in camera view
			if ((front.magnitude <= 75) && (Vector3.Angle(Camera.main.transform.forward, position - Camera.main.transform.position) <= 180))
			{
				Vector2 v = Camera.main.WorldToViewportPoint(position);                       
				//			text.fontSize = 100;
				//			text.text = "123";
				Rect.anchorMax = v;
				Rect.anchorMin = v;
				//			text.color = color;
			}

		}

		public void SetTarget(Transform transform)
		{
			m_Targettransform = transform;
			InitNavMesh ();
		}

		void InitNavMesh()
		{
			//		m_Role.transform.position = new Vector3 (m_StartPoint.transform.position.x,m_StartPoint.transform.position.y,m_StartPoint.transform.position.z);
			shoudldGo = true;
			m_agent = (NavMeshAgent)m_ActorObject.GetComponent("NavMeshAgent");
			m_agent.speed = 15;
			m_agent.stoppingDistance = 0.01f; 
			m_agent.radius = 0.5f;
			m_agent.acceleration = 15;
			m_agent.autoRepath = true;
			m_agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
			PlayAnimation (Global.BattleAnimationType.Run);
		}

		void UpdateMove()
		{
			if (shoudldGo == true) 
			{
				m_agent.SetDestination (m_Targettransform.position);
//				if (m_agent.remainingDistance <= 0.6f) 
//				{
//					shoudldGo = false;
//					PlayAnimation (Global.BattleAnimationType.Stand);
//				}
			}
		}

		void UpdateAnimation()
		{
			if (animationState == 1)
			{
				if (! animationController.isPlaying)
				{
					PlayAttack();
				}
			}
			if (rotate)
			{
				m_ActorObject.transform.Rotate(new Vector3(0,90 * Time.deltaTime,0));
			}
		}

		void UpdateAI()
		{
			List<GameObject> monsterList = BattleScene.Active.m_monsterList;
			for (int i = 0; i < monsterList.Count; i++) 
			{
				//			Object monsterRes = Resources.Load ("Actor/Actor1/" + skeleton);
				//			GameObject monsterObject = GameObject.Instantiate (res) as GameObject;
				//			monsterObject.transform = 
				GameObject monsterObject = monsterList[i];
				if (monsterObject == null)
					continue;
				Vector3 pos1 = new Vector3( monsterObject.transform.position.x, 0, monsterObject.transform.position.z );
				Vector3 pos2 = new Vector3( m_ActorObject.transform.position.x, 0, m_ActorObject.transform.position.z );
//				Debug.Log ("UpdateAI = "+Vector3.Distance(pos1,pos2));

				if (Vector3.Distance(pos1,pos2) <= 2)
				{
					DestroyImmediate (monsterObject);

					Object psObj = Resources.Load ("Effect/CircleFX_Dark");
					GameObject t = Instantiate(psObj) as GameObject;
					t.transform.position = pos1;

				}

//				t.transform.SetParent(m_CanvasParent.transform, false);


//				monsterList.Remove (monsterObject);
			}
		}

		public void Update () 
		{
			UpdateBlood ();
			UpdateAnimation ();
			UpdateMove ();
			UpdateAI ();
		}
	}
}
