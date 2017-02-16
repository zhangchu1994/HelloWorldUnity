using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePS : MonoBehaviour 
{
	public float deadTime;

	void Awake () 
	{
		Destroy (gameObject, deadTime);
	}

}
