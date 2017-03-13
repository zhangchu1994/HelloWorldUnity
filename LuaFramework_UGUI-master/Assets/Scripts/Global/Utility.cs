//#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Security.Cryptography;
using UnityEngine;
//using Object = UnityEngine.Object;



namespace GlobalGame 
{
	public class Utility 
	{

	    private static MonoBehaviour _starter;

	    public static MonoBehaviour CoroutineContainer 
		{
	        get {
	            if (_starter == null) 
				{
	                _starter = new GameObject("starter").AddComponent<MonoBehaviour>(); // MonoBehaviour();
					UnityEngine.Object.DontDestroyOnLoad(_starter.gameObject);
	            }
	            return _starter;
	        }
	    }
	}
}
