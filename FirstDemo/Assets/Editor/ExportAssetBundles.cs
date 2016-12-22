using UnityEditor;
using UnityEngine;

public class ExportAssetBundles 
{
	//在Unity编辑器中添加菜单  
	[MenuItem("AB/Build AssetBundle From Selection")]  
	static void ExportResourceRGB2()  
	{  
		//1.按照面板选择打包
//		string path = EditorUtility.SaveFilePanel("Save Resource", "Assets/ab", "New Resource", "assetbundle");  
//		
//		if (path.Length != 0)  
//		{  
//			Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);  
////			BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows);  
//			BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.StandaloneWindows);  
//		}  

		//2.按照下角标设置打包
		string path = Application.dataPath + "/ab";
		Debug.Log ("__________________path = "+path);
		BuildPipeline.BuildAssetBundles (path,BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows);
	}

	[MenuItem("AB/Save Scene")]  
	static void ExportScene()  
	{  
		// 打开保存面板，获得用户选择的路径  
		string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");  
		
		if (path.Length != 0)  
		{  
			// 选择的要保存的对象  
			Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);  
			string[] scenes = {"Assets/scene1.unity"};  
			//打包  
			BuildPipeline.BuildPlayer(scenes,path,BuildTarget.StandaloneWindows,BuildOptions.BuildAdditionalStreamedScenes);  
		}  
	}  


}

