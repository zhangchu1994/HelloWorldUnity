using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class TestClass : MonoBehaviour 
	{
		public GameObject m_Start;
		public GameObject m_End;
		public static TestClass Active = null;

		void Awake()
		{
			Active = this;
		}

		// Use this for initialization
		void Start () 
		{
//			Global.ChangeParticleScale (m_PS, 15);

		}
		
		// Update is called once per frame
		void Update () 
		{
			if(Input.GetButtonDown("Jump"))
			{
//				Jump ();
				Object psObj = Resources.Load ("Effect/CloudFlashFX");
				GameObject t = Instantiate(psObj) as GameObject;
				Global.ChangeParticleScale (t,1.5f);
				t.transform.position = m_Start.transform.position;
				Bullet bulletScripte = t.GetComponent<Bullet>();
				//			GameObject monsterObject = m_actorObjList [0];
				bulletScripte.InitBullet (m_Start, m_End,null);
			}

		}

//		void Jump ()
//		{
//			//			if(grounded == true)
//			//			{
//			Debug.Log("Jump____________________");
//			Rigidbody rigidbody = m_Start.GetComponent<Rigidbody> ();
//			rigidbody.AddForce(Vector3.up * 800);
//			//				grounded = false;
//			//			}
//		}
	}
}
