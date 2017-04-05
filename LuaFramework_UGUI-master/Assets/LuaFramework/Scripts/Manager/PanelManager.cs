using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LuaInterface;
using UnityEngine.SceneManagement;
using GlobalGame;

namespace LuaFramework {
    public class PanelManager : Manager 
	{
		public void CreatePanel(string abName,string parentName,string luaName = null,string name = null, LuaFunction func = null) 
		{
//			Debug.Log ("CreatePanel__________"+name);
			Transform Parent = GameObject.Find (parentName).transform;
			if (Parent.FindChild (name) != null) 
			{
				GameObject obj = GameObject.Find (parentName + "/"+name);
				if (obj.activeInHierarchy == false)
					obj.SetActive (true);
				if (func != null) func.Call(null);
				return;
			}
				

			Dictionary<string,string> info1 = Global.CreateABInfo(abName,Global.GetAssetName(abName),AssetType.Perfab,0);
			ResManager.LoadPrefabWithInfo(info1, delegate(UnityEngine.Object[] objs,Dictionary<string,string> info) 
			{
                GameObject prefab = objs[0] as GameObject;
                GameObject go = Instantiate(prefab) as GameObject;
				go.name = name;
                go.layer = LayerMask.NameToLayer("Default");
                go.transform.SetParent(Parent);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
				LuaBehaviour luaBehaviour = go.AddComponent<LuaBehaviour>();
				luaBehaviour.abName = abName;
				luaBehaviour.luaName = luaName;
               	if (func != null) func.Call(go);
                Debug.LogWarning("CreatePanel::>> " + name + " " + prefab);
            });
        }

        public void ClosePanel(string name) 
		{
//            var panelName = name + "Panel";
//            var panelObj = Parent.FindChild(panelName);
//            if (panelObj == null) return;
//            Destroy(panelObj.gameObject);
        }
    }
}