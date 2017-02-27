using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class TestClass : MonoBehaviour 
	{
		public GameObject m_PS;

		// Use this for initialization
		void Start () 
		{
			Global.ChangeParticleScale (m_PS, 15);
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}
	}
}
