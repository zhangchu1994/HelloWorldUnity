using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

namespace LuaFramework {
    public class PanelManager : Manager {
//        private Transform parent;
//
//        Transform Parent {
//            get {
//                if (parent == null) {
//                    GameObject go = GameObject.FindWithTag("GuiCamera");
//                    if (go != null) parent = go.transform;
//                }
//                return parent;
//            }
//        }

		public void CreatePanel(string parent,string name, LuaFunction func = null) {
            AssetBundle bundle = ResManager.LoadBundle(name);
			StartCoroutine(StartCreatePanel(parent, name, bundle, func));
            Debug.LogWarning("CreatePanel::>> " + name + " " + bundle);
        }

		IEnumerator StartCreatePanel(string parent,string name, AssetBundle bundle, LuaFunction func = null) 
		{
            name += "Panel";
            GameObject prefab = Util.LoadAsset(bundle, name);
            yield return new WaitForEndOfFrame();
			if (getParentByName(parent).FindChild(name) != null || prefab == null) {
                yield break;
            }
            GameObject go = Instantiate(prefab) as GameObject;
            go.name = name;
            go.layer = LayerMask.NameToLayer("Default");
			go.transform.parent = getParentByName(parent);//Parent;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;

            yield return new WaitForEndOfFrame();
            go.AddComponent<LuaBehaviour>().OnInit(bundle);

            if (func != null) func.Call(go);
            Debug.Log("StartCreatePanel------>>>>" + name);
        }

		public void CreatePanel1(string parent,string name, string luaFile,LuaFunction func = null) 
		{
			StartCoroutine(StartCreatePanel1(parent, name,luaFile, func));
		}
		
		IEnumerator StartCreatePanel1(string parent,string name, string luaFile,LuaFunction func = null) 
		{
			GameObject prefab = Resources.Load<GameObject>(name);//Util.LoadAsset(bundle, name);
			yield return new WaitForEndOfFrame();
			if (getParentByName(parent).FindChild(name) != null || prefab == null) {
				yield break;
			}
			GameObject go = Instantiate(prefab) as GameObject;
			go.name = luaFile;
			go.layer = LayerMask.NameToLayer("Default");
			go.transform.parent = getParentByName(parent);//Parent;
			go.transform.localScale = Vector3.one;
			go.transform.localPosition = Vector3.zero;
			
			yield return new WaitForEndOfFrame();
			go.AddComponent<LuaBehaviour>().OnInit(null);//bundle
			
			if (func != null) func.Call(go);
			Debug.Log("StartCreatePanel------>>>>" + name);
		}

		
		
		private Transform getParentByName(string parentname)
		{
			Transform parent = null;
			GameObject go = GameObject.FindWithTag(parentname);
			if (go != null) parent = go.transform;
			return parent;
	    }
	}
}