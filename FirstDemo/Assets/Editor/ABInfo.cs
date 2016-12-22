using UnityEngine;
using System.Collections;
using UnityEditor;

public class ABInfo 
{
	[MenuItem("AB/LoadAB")]
	static void load()
	{
		//获取在Project视图中选择的所有游戏对象
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);


		//遍历所有的游戏对象
		foreach (Object obj in SelectedAsset) 
		{
			string str1 = Application.dataPath;
			str1 = str1.Replace ("Assets","");
			string filePath = str1 + AssetDatabase.GetAssetPath(obj);
			filePath = filePath.Replace ("/","\\");
			filePath = "file://" + filePath;
			Debug.Log ("filePath = "+filePath);

			WWW www = new WWW (filePath);
			AssetBundle ab =  www.assetBundle;
			string[] ablist = ab.GetAllAssetNames ();

			for (int i = 0; i < ablist.Length; i++) 
			{
				Debug.Log (ablist[i]);
			}
		}
//		WWW www = new WWW ("file://E:\\WorkSpace_Unity\\HelloWorldUnity\\trunk\\FirstDemo\\Assets\\ab\\New Resource.assetbundle");
						   		
//		AssetBundle ab = www.assetBundle;
//		string[] ablist = ab.GetAllAssetNames ();
//
//		for (int i = 0; i < ablist.Length; i++) 
//		{
//			Debug.Log (ablist[i]);
//		}
	}

}
