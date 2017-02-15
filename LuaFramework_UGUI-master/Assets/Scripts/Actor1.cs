using UnityEngine;
using System.Collections;
using LuaFramework;

namespace GlobalGame 
{
	public class Actor1 :MonoBehaviour
	{

		/// <summary>
		/// GameObject reference
		/// </summary>
		public GameObject Instance = null;
		public GameObject WeaponInstance = null;

		/// <summary>
		/// Equipment informations
		/// </summary>
		public string skeleton;
		public string equipment_head;
		public string equipment_chest;
		public string equipment_hand;
		public string equipment_feet;

		/// <summary>
		/// The unique id in the scene
		/// </summary>
		public int index;

		/// <summary>
		/// Other vars
		/// </summary>
		public bool rotate = false;
		public int animationState = 0;

		private Animation animationController = null;

		private readonly string[] m_Index = new string[]{ "004", "006", "008" };
		/// <summary>
		/// Config default equipment informations.
		/// </summary>
		private const int DEFAULT_WEAPON = 0;
		private const int DEFAULT_HEAD = 0;
		private const int DEFAULT_CHEST = 0;
		private const int DEFAULT_HAND = 0;
		private const int DEFAULT_FEET = 0;
		private const bool DEFAULT_COMBINEMATERIAL = true;

		/// <summary>
		/// Use this for GUI display.
		/// </summary>
	//	private bool combine = DEFAULT_COMBINEMATERIAL;
	//	private bool[] weapon_list = new bool[3];
	//	private bool[] head_list = new bool[3];
	//	private bool[] chest_list = new bool[3];
	//	private bool[] hand_list = new bool[3];
	//	private bool[] feet_list = new bool[3];


		void Start()
		{
			CreatActor (1,
				"ch_pc_hou", 
				"ch_we_one_hou_" + m_Index[DEFAULT_WEAPON],
				"ch_pc_hou_" + m_Index[DEFAULT_HEAD] + "_tou", 
				"ch_pc_hou_" + m_Index[DEFAULT_CHEST] + "_shen", 
				"ch_pc_hou_" + m_Index[DEFAULT_HAND] + "_shou", 
				"ch_pc_hou_" + m_Index[DEFAULT_FEET] + "_jiao",
				true
			);
		}

		public void CreatActor (int index,string skeleton, string weapon, string head, string chest, string hand, string feet, bool combine = false) {

			//Creates the skeleton object
			Object res = Resources.Load ("Actor/Actor1/" + skeleton);
			this.Instance = GameObject.Instantiate (res) as GameObject;
			this.Instance.name = "Ian1970";
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

				res = Resources.Load ("Actor/Actor1/" + equipments [i]);
				objects[i] = GameObject.Instantiate (res) as GameObject;
				meshes[i] = objects[i].GetComponentInChildren<SkinnedMeshRenderer> ();
			}

			// Combine meshes
			CombineSkinnedMgr.Instance.CombineObject (Instance, meshes, combine);

			// Delete temporal resources
			for (int i = 0; i < objects.Length; i++) {

				GameObject.DestroyImmediate (objects [i].gameObject);
			}

			// Create weapon
			res = Resources.Load ("Actor/Actor1/" + weapon);
			WeaponInstance = GameObject.Instantiate (res) as GameObject;

			Transform[] transforms = Instance.GetComponentsInChildren<Transform>();
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
			animationController = Instance.GetComponent<Animation>();
			PlayStand();
//			Util.CallMethod("FirstBattleScene", "ActorDone");
			TestClass.Go();
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

			CombineSkinnedMgr.Instance.CombineObject (Instance, meshes, combine);

			for (int i = 0; i < objects.Length; i++) {

				GameObject.DestroyImmediate(objects[i].gameObject);
			}
		}


		public void PlayStand () {

			animationController.wrapMode = WrapMode.Loop;
			animationController.Play("breath");
			animationState = 0;
		}

		public void PlayAttack () {

			animationController.wrapMode = WrapMode.Once;
			animationController.PlayQueued("attack1");
			animationController.PlayQueued("attack2");
			animationController.PlayQueued("attack3");
			animationController.PlayQueued("attack4");
			animationState = 1;
		}



		// Update is called once per frame
		public void Update () {

			if (animationState == 1)
			{
				if (! animationController.isPlaying)
				{
					PlayAttack();
				}
			}
			if (rotate)
			{
				Instance.transform.Rotate(new Vector3(0,90 * Time.deltaTime,0));
			}
		}
	}
}
