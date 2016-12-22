using UnityEngine;
using System.Collections;
using UnityEditor;

public class ABInfo 
{
	[MenuItem("AB/LoadAB")]
	static void load()
	{
		//		WWW www = new WWW ("file://D:\\pic1.jpg");
		WWW www = new WWW ("file://E:\\WorkSpace_Unity\\HelloWorldUnity\\trunk\\FirstDemo\\Assets\\ab\\New Resource.assetbundle");
		AssetBundle ab = www.assetBundle;
		string[] ablist = ab.GetAllAssetNames ();

		for (int i = 0; i < ablist.Length; i++) 
		{
			Debug.Log (ablist[i]);
		}
	}

}
