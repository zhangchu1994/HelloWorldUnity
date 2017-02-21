using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class RemovePS : MonoBehaviour 
	{
		public float deadTime;

		void Awake () 
		{
			Destroy (gameObject, deadTime);
		}

	}
}