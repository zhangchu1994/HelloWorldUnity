using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class ActorBobyManager : MonoBehaviour 
	{

		/// <summary>
		/// Equipment informations
		/// </summary>
		private string skeleton;
		private string equipment_head;
		private string equipment_chest;
		private string equipment_hand;
		private string equipment_feet;

		private int index;
		private GameObject WeaponInstance = null;
		private readonly string[] m_Index = new string[]{ "004", "006", "008" };

		private const int DEFAULT_WEAPON = 0;
		private const int DEFAULT_HEAD = 0;
		private const int DEFAULT_CHEST = 0;
		private const int DEFAULT_HAND = 0;
		private const int DEFAULT_FEET = 0;
		private const bool DEFAULT_COMBINEMATERIAL = true;
		public Actor m_MainActor;

		void Awake()
		{
			m_MainActor = this.transform.GetComponent<Actor> ();
		}

		// Use this for initialization
		void Start () 
		{
			
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}

		public void InitBoby()
		{
			int index = m_MainActor.m_Index;
			this.index = index;
			this.skeleton = skeleton;
			bool combine = true;

			string weapon = "ch_we_one_hou_" + m_Index [DEFAULT_WEAPON];
			string head = "ch_pc_hou_" + m_Index [DEFAULT_HEAD] + "_tou"; 
			string chest = "ch_pc_hou_" + m_Index [DEFAULT_CHEST] + "_shen"; 
			string hand = "ch_pc_hou_" + m_Index [DEFAULT_HAND] + "_shou";
			string feet = "ch_pc_hou_" + m_Index [DEFAULT_FEET] + "_jiao";

			this.equipment_head = head;
			this.equipment_chest = chest;
			this.equipment_hand = hand;
			this.equipment_feet = feet;

			string[] equipments = new string[4];
			equipments [0] = head;
			equipments [1] = chest;
			equipments [2] = hand;
			equipments [3] = feet;

			SkinnedMeshRenderer[] meshes = new SkinnedMeshRenderer[4];
			GameObject[] objects = new GameObject[4];
			for (int i = 0; i < equipments.Length; i++) {

				Object res = Resources.Load ("ModelActor/Actor1/" + equipments [i]);
				objects[i] = GameObject.Instantiate (res) as GameObject;
				meshes[i] = objects[i].GetComponentInChildren<SkinnedMeshRenderer> ();
			}

			CombineSkinnedMgr.Instance.CombineObject (this.transform.gameObject, meshes, combine);

			for (int i = 0; i < objects.Length; i++) {

				GameObject.DestroyImmediate (objects [i].gameObject);
			}

			Object res1 = Resources.Load ("ModelActor/Actor1/" + weapon);
			WeaponInstance = GameObject.Instantiate (res1) as GameObject;

			Transform[] transforms = this.transform.gameObject.GetComponentsInChildren<Transform>();
			foreach (Transform joint in transforms) {
				if (joint.name == "weapon_hand_r") {// find the joint (need the support of art designer)
					WeaponInstance.transform.parent = joint.gameObject.transform;
					break;
				}	
			}

			WeaponInstance.transform.localScale = Vector3.one;
			WeaponInstance.transform.localPosition = Vector3.zero;
			WeaponInstance.transform.localRotation = Quaternion.identity;
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

			CombineSkinnedMgr.Instance.CombineObject (this.transform.gameObject, meshes, combine);

			for (int i = 0; i < objects.Length; i++) {

				GameObject.DestroyImmediate(objects[i].gameObject);
			}
		}

	}
}
