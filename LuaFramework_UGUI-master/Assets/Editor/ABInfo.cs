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

	[MenuItem("AB/Delete AB #%D")]
	static void DeleteAB()
	{
		FileUtil.DeleteFileOrDirectory ( "c:/" + "luaframework" + "/");
	}



	const string duplicatePostfix = "_copy";
	[MenuItem ("Assets/Transfer Clip Curves to Copy")]
	static void CopyCurvesToDuplicate () 
	{
		// Get selected AnimationClip
		AnimationClip imported = Selection.activeObject as AnimationClip;
		if (imported == null) {
			Debug.Log("Selected object is not an AnimationClip");
			return;
		}
		// Find path of copy
		string importedPath = AssetDatabase.GetAssetPath(imported);
		string copyPath = importedPath.Substring(0, importedPath.LastIndexOf("/"));
		copyPath += "/" + imported.name + duplicatePostfix + ".anim";
		// Get copy AnimationClip
		AnimationClip copy = AssetDatabase.LoadAssetAtPath(copyPath, typeof(AnimationClip)) as AnimationClip;
		if (copy == null) {
			Debug.Log("No copy found at "+copyPath);
			return;
		}
		// Copy curves from imported to copy
		AnimationClipCurveData[] curveDatas = AnimationUtility.GetAllCurves(imported, true);
		for (int i=0; i<curveDatas.Length; i++) {
			AnimationUtility.SetEditorCurve(
				copy,
				curveDatas[i].path,
				curveDatas[i].type,
				curveDatas[i].propertyName,
				curveDatas[i].curve
			);
		}
		Debug.Log("Copying curves into "+copy.name+" is done");
	}

}