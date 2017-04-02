using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using LuaFramework;
//using SceneAssetProcesser;

public class Packager {
    public static string platform = string.Empty;
    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();
    static List<AssetBundleBuild> maps = new List<AssetBundleBuild>();

    ///-----------------------------------------------------------
    static string[] exts = { ".txt", ".xml", ".lua", ".assetbundle", ".json" };
    static bool CanCopy(string ext) {   //能不能复制
        foreach (string e in exts) {
            if (ext.Equals(e)) return true;
        }
        return false;
    }

    /// <summary>
    /// 载入素材
    /// </summary>
    static UnityEngine.Object LoadAsset(string file) {
        if (file.EndsWith(".lua")) file += ".txt";
        return AssetDatabase.LoadMainAssetAtPath("Assets/LuaFramework/Examples/Builds/" + file);
    }

    [MenuItem("LuaFramework/Build iPhone Resource", false, 100)]
    public static void BuildiPhoneResource() {
        BuildTarget target;
#if UNITY_5
		target = BuildTarget.iOS;//iOS
#else
        target = BuildTarget.iPhone;
#endif
        BuildAssetResource(target);
    }

    [MenuItem("LuaFramework/Build Android Resource", false, 101)]
    public static void BuildAndroidResource() {
        BuildAssetResource(BuildTarget.Android);
    }

    [MenuItem("LuaFramework/Build Windows Resource", false, 102)]
    public static void BuildWindowsResource() {
        BuildAssetResource(BuildTarget.StandaloneWindows);
    }

	[MenuItem("LuaFramework/Build Mac Resource", false, 104)]
	public static void BuildMacResource() {
		BuildAssetResource(BuildTarget.StandaloneOSXUniversal);
	}

    /// <summary>
    /// 生成绑定素材
    /// </summary>
    public static void BuildAssetResource(BuildTarget target) {
        if (Directory.Exists(Util.DataPath)) {
            Directory.Delete(Util.DataPath, true);
        }
        string streamPath = Application.streamingAssetsPath;
        if (Directory.Exists(streamPath)) {
            Directory.Delete(streamPath, true);
        }
        Directory.CreateDirectory(streamPath);
        AssetDatabase.Refresh();


        maps.Clear();
        if (AppConst.LuaBundleMode) {
            HandleLuaBundle();
        } else {
            HandleLuaFile();
        }
        if (AppConst.ExampleMode) {
            HandleExampleBundle();
        }
        string resPath = "Assets/" + AppConst.AssetDir;
        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle | 
                                          BuildAssetBundleOptions.UncompressedAssetBundle;
		BuildPipeline.BuildAssetBundles(resPath, maps.ToArray(), options, BuildTarget.Android);
//		Packager.BuildSceneBundle(target);
        BuildFileIndex();

        string streamDir = Application.dataPath + "/" + AppConst.LuaTempDir;
        if (Directory.Exists(streamDir)) Directory.Delete(streamDir, true);
		Util.Log("_______________________________Util.DataPath = "+Util.DataPath+" streamPath = "+streamPath+" streamDir = "+streamDir);

		AssetDatabase.Refresh();
    }

	static void AddBuildMap(string bundleName, string pattern, string path)//1.ab名字 2.什么类型文件会被打爆 3.要打包文件的文件夹路径(把文件夹下每个指定后缀名的文件单独打成AB)
	{
		string[] files = Directory.GetFiles(path, pattern);
		if (files.Length == 0) return;

		for (int i = 0; i < files.Length; i++) 
		{
			string name = Path.GetFileNameWithoutExtension (files[i]);

			files[i] = files[i].Replace('\\', '/');
			AssetBundleBuild build = new AssetBundleBuild();
			build.assetBundleName = bundleName + name + AppConst.ExtName;
			string[] files1 = { files [i] };
			build.assetNames = files1;
			maps.Add(build);
		}
	}

	static void AddBuildMapFolder(string bundleName, string pattern, string path)//1.ab名字 2.什么类型文件会被打爆 3.要打包文件的文件夹路径(把文件夹下所有后缀文件打成一个AB)
	{
        string[] files = Directory.GetFiles(path, pattern);
        if (files.Length == 0) return;

        for (int i = 0; i < files.Length; i++) {
            files[i] = files[i].Replace('\\', '/');
        }
        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = bundleName;
        build.assetNames = files;
        maps.Add(build);
    }

	static void getBuildMap(string bundleName, string pattern, string path) 
	{
		List<AssetBundleBuild> tempMaps = new List<AssetBundleBuild>();

		string[] files = Directory.GetFiles(path, pattern);
		if (files.Length == 0) return;

		for (int i = 0; i < files.Length; i++) {
			files[i] = files[i].Replace('\\', '/');
		}
		AssetBundleBuild build = new AssetBundleBuild();
		build.assetBundleName = bundleName;
		build.assetNames = files;
		tempMaps.Add(build);
	}


    /// <summary>
    /// 处理Lua代码包
    /// </summary>
    static void HandleLuaBundle() {
        string streamDir = Application.dataPath + "/" + AppConst.LuaTempDir;
        if (!Directory.Exists(streamDir)) Directory.CreateDirectory(streamDir);

		string[] srcDirs = { CustomSettings.luaDir, CustomSettings.FrameworkPath + "/ToLua/Lua",AppDataPath + "/ScriptsLua/" };
        for (int i = 0; i < srcDirs.Length; i++) {
            if (AppConst.LuaByteMode) {
                string sourceDir = srcDirs[i];
                string[] files = Directory.GetFiles(sourceDir, "*.lua", SearchOption.AllDirectories);
                int len = sourceDir.Length;

                if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\') {
                    --len;
                }
                for (int j = 0; j < files.Length; j++) {
                    string str = files[j].Remove(0, len);
                    string dest = streamDir + str + ".bytes";
                    string dir = Path.GetDirectoryName(dest);
                    Directory.CreateDirectory(dir);
                    EncodeLuaFile(files[j], dest);
                }    
            } else {
                ToLuaMenu.CopyLuaBytesFiles(srcDirs[i], streamDir);
            }
        }
        string[] dirs = Directory.GetDirectories(streamDir, "*", SearchOption.AllDirectories);
        for (int i = 0; i < dirs.Length; i++) {
            string name = dirs[i].Replace(streamDir, string.Empty);
            name = name.Replace('\\', '_').Replace('/', '_');
            name = "lua/lua_" + name.ToLower() + AppConst.ExtName;

            string path = "Assets" + dirs[i].Replace(Application.dataPath, "");
			AddBuildMapFolder(name, "*.bytes", path);
        }
		AddBuildMapFolder("lua/lua" + AppConst.ExtName, "*.bytes", "Assets/" + AppConst.LuaTempDir);

        //-------------------------------处理非Lua文件----------------------------------
        string luaPath = AppDataPath + "/StreamingAssets/lua/";
        for (int i = 0; i < srcDirs.Length; i++) {
            paths.Clear(); files.Clear();
            string luaDataPath = srcDirs[i].ToLower();
            Recursive(luaDataPath);
            foreach (string f in files) {
                if (f.EndsWith(".meta") || f.EndsWith(".lua")) continue;
                string newfile = f.Replace(luaDataPath, "");
                string path = Path.GetDirectoryName(luaPath + newfile);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string destfile = path + "/" + Path.GetFileName(f);
                File.Copy(f, destfile, true);
            }
        }
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 处理框架实例包
    /// </summary>
    static void HandleExampleBundle() 
	{
        string resPath = AppDataPath + "/" + AppConst.AssetDir + "/";
//		Util.Log ("HandleExampleBundle_______________________"+resPath);
        if (!Directory.Exists(resPath)) Directory.CreateDirectory(resPath);

//		string uiPerfab = "Assets/Perfab/UI/";
//		AddBuildMapFolder("LoginPerfab" + AppConst.ExtName, "*.prefab", uiPerfab + "Login");
//		AddBuildMapFolder("MainCityPerfab" + AppConst.ExtName, "*.prefab", uiPerfab + "MainCity");
//		AddBuildMapFolder("BattleFieldPerfab" + AppConst.ExtName, "*.prefab", uiPerfab + "BattleField");

		string actorPerfab = "Assets/Resources/ModelActor/";
		AddBuildMap("ModelActor/", "*.prefab", actorPerfab);

		string actor1Perfab = "Assets/Resources/ModelActor/Actor1/";
		AddBuildMap("ModelActor/Actor1/", "*.prefab", actor1Perfab);

		string modelPerfab = "Assets/Resources/ModelEnemy/";
		AddBuildMap("ModelEnemy/", "*.prefab", modelPerfab);

		string effectPerfab = "Assets/Resources/Effect/";
		AddBuildMap("Effect/", "*.prefab", effectPerfab);

		string uIEffectPerfab = "Assets/Resources/UIEffect/";
		AddBuildMap("UIEffect/", "*.prefab", uIEffectPerfab);

		string uIPerfab = "Assets/Resources/UI/";
		AddBuildMap("UI/", "*.prefab", uIPerfab);

//		string scene = "Assets/Scene/";
//		AddBuildMapFolder("MainCityScene" + AppConst.ExtName, "*.unity", scene + "MainCity");
//		AddBuildMapFolder("FirstBattleScene" + AppConst.ExtName, "*.unity", scene + "FirstBattle");

//		AddBuildMap("prompt_asset" + AppConst.ExtName, "*.png", "Assets/LuaFramework/Examples/Textures/Prompt");
//        AddBuildMap("shared_asset" + AppConst.ExtName, "*.png", "Assets/LuaFramework/Examples/Textures/Shared");
    }

//	[MenuItem("LuaFramework/Build Scene Resource", false, 101)]
//	static void BuildSceneBundle(BuildTarget target)
//	{
//		// 打开保存面板，获得用户选择的路径  
////		string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");  
//		string path = AppDataPath + "/" + AppConst.AssetDir + "/";
//		if (path.Length != 0)  
//		{  
//			// 选择的要保存的对象  
////			Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);  
//			string[] scenes = {"Assets/Scene/MainCity.unity"};  
//			//打包  
//			BuildPipeline.BuildPlayer(scenes,path+"/zhangchu"+AppConst.ExtName,target,BuildOptions.BuildAdditionalStreamedScenes);  
//			Util.Log ("BuildSceneBundle_________Done");
//		} 
//	}

//	[MenuItem("LuaFramework/Build Select Resource", false, 101)]
//	static void BuildSelectObjectBundle()
//	{
//		//1.按照面板选择打包
//		string path = "Assets/" + AppConst.AssetDir;//EditorUtility.SaveFilePanel("Save Resource", "Assets/ab", "New Resource", "assetbundle");  
//				
//		if (path.Length != 0)  
//		{  
//			Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);  
//			path = path + "/" + selection[0].name;
////			BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows);  
//			BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows);  
//		} 
//
////		AddBuildMap("Login" + AppConst.ExtName, "*.prefab", "Assets/LuaFramework/Examples/Builds/Message");
//
////		string resPath = "Assets/" + AppConst.AssetDir;
////		BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle | 
////			BuildAssetBundleOptions.UncompressedAssetBundle;
////		BuildPipeline.BuildAssetBundles(resPath, Packager.getBuildMap("Login" + AppConst.ExtName, "*.prefab", "Assets/LuaFramework/Examples/Builds/Message"), options, BuildTarget.StandaloneWindows);
//	}

    /// <summary>
    /// 处理Lua文件
    /// </summary>
    static void HandleLuaFile() {
        string resPath = AppDataPath + "/StreamingAssets/";
        string luaPath = resPath + "/lua/";

        //----------复制Lua文件----------------
        if (!Directory.Exists(luaPath)) {
            Directory.CreateDirectory(luaPath); 
        }
        string[] luaPaths = { AppDataPath + "/LuaFramework/lua/", 
                              AppDataPath + "/LuaFramework/Tolua/Lua/",
							  AppDataPath + "/ScriptsLua/"	};

        for (int i = 0; i < luaPaths.Length; i++) {
            paths.Clear(); files.Clear();
            string luaDataPath = luaPaths[i].ToLower();
            Recursive(luaDataPath);
            int n = 0;
            foreach (string f in files) {
                if (f.EndsWith(".meta")) continue;
                string newfile = f.Replace(luaDataPath, "");
                string newpath = luaPath + newfile;
                string path = Path.GetDirectoryName(newpath);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                if (File.Exists(newpath)) {
                    File.Delete(newpath);
                }
                if (AppConst.LuaByteMode) {
                    EncodeLuaFile(f, newpath);
                } else {
                    File.Copy(f, newpath, true);
                }
                UpdateProgress(n++, files.Count, newpath);
            } 
        }
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
    }

    static void BuildFileIndex() {
        string resPath = AppDataPath + "/StreamingAssets/";
        ///----------------------创建文件列表-----------------------
        string newFilePath = resPath + "/files.txt";
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        paths.Clear(); files.Clear();
        Recursive(resPath);

        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < files.Count; i++) {
            string file = files[i];
            string ext = Path.GetExtension(file);
            if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

            string md5 = Util.md5file(file);
            string value = file.Replace(resPath, string.Empty);
            sw.WriteLine(value + "|" + md5);
        }
        sw.Close(); fs.Close();
    }

    /// <summary>
    /// 数据目录
    /// </summary>
    static string AppDataPath {
        get { return Application.dataPath.ToLower(); }
    }

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void Recursive(string path) {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names) {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs) {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }

    static void UpdateProgress(int progress, int progressMax, string desc) {
        string title = "Processing...[" + progress + " - " + progressMax + "]";
        float value = (float)progress / (float)progressMax;
        EditorUtility.DisplayProgressBar(title, desc, value);
    }

    public static void EncodeLuaFile(string srcFile, string outFile) {
        if (!srcFile.ToLower().EndsWith(".lua")) {
            File.Copy(srcFile, outFile, true);
            return;
        }
        bool isWin = true;
        string luaexe = string.Empty;
        string args = string.Empty;
        string exedir = string.Empty;
        string currDir = Directory.GetCurrentDirectory();
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            isWin = true;
            luaexe = "luajit.exe";
            args = "-b " + srcFile + " " + outFile;
            exedir = AppDataPath.Replace("assets", "") + "LuaEncoder/luajit/";
        } else if (Application.platform == RuntimePlatform.OSXEditor) {
            isWin = false;
            luaexe = "./luac";
            args = "-o " + outFile + " " + srcFile;
            exedir = AppDataPath.Replace("assets", "") + "LuaEncoder/luavm/";
        }
        Directory.SetCurrentDirectory(exedir);
        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = luaexe;
        info.Arguments = args;
        info.WindowStyle = ProcessWindowStyle.Hidden;
        info.ErrorDialog = true;
        info.UseShellExecute = isWin;
        Util.Log(info.FileName + " " + info.Arguments);

        Process pro = Process.Start(info);
        pro.WaitForExit();
        Directory.SetCurrentDirectory(currDir);
    }

    [MenuItem("LuaFramework/Build Protobuf-lua-gen File")]
    public static void BuildProtobufFile() {
        if (!AppConst.ExampleMode) {
            UnityEngine.Debug.LogError("若使用编码Protobuf-lua-gen功能，需要自己配置外部环境！！");
            return;
        }
        string dir = AppDataPath + "/Lua/3rd/pblua";
        paths.Clear(); files.Clear(); Recursive(dir);

        string protoc = "d:/protobuf-2.4.1/src/protoc.exe";
        string protoc_gen_dir = "\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";

        foreach (string f in files) {
            string name = Path.GetFileName(f);
            string ext = Path.GetExtension(f);
            if (!ext.Equals(".proto")) continue;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = protoc;
            info.Arguments = " --lua_out=./ --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + name;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = true;
            info.WorkingDirectory = dir;
            info.ErrorDialog = true;
            Util.Log(info.FileName + " " + info.Arguments);

            Process pro = Process.Start(info);
            pro.WaitForExit();
        }
        AssetDatabase.Refresh();
    }
}