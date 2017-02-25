using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LuaInterface;
using UnityEngine.SceneManagement;

namespace LuaFramework {
    public class PanelManager : Manager {
        private Transform parent;

        Transform Parent {
            get {
                if (parent == null) {
                    GameObject go = GameObject.FindWithTag("GuiCamera");
                    if (go != null) parent = go.transform;
                }
                return parent;
            }
        }


//		public void GoToScene(string abName, string assetname,string luaName = null,LuaFunction func = null)
//		{
//			Debug.Log ("GoToScene1_________________________________");
//			ResManager.LoadSecne(abName, assetname, delegate(UnityEngine.Object[] objs) {
//				Debug.Log ("GoToScene2_________________________________");
//				SceneManager.LoadScene(assetname); 
////				SceneManager.LoadSceneAsync
//
//				GameObject go = new GameObject();
//				go.name = luaName;
//				go.layer = LayerMask.NameToLayer("Default");
////				go.transform.SetParent(Parent);
//				go.transform.localScale = Vector3.one;
//				go.transform.localPosition = Vector3.zero;
//				LuaBehaviour luaBehaviour = go.AddComponent<LuaBehaviour>();
//				luaBehaviour.abName = abName;
//				luaBehaviour.luaName = luaName;
//
//				if (func != null) func.Call();
//			});
//		}
//
        /// <summary>
        /// ������壬������Դ������
        /// </summary>C
        /// <param name="type"></param>
		public void CreatePanel(string abName,string assetName,string luaName = null, LuaFunction func = null) 
		{
//			Debug.Log ("CreatePanel__________"+name);
//            string assetName = name + "Panel";
			string abFullName = abName.ToLower() + AppConst.ExtName;
			if (Parent.FindChild(abName) != null) return;
#if ASYNC_MODE
			ResManager.LoadPrefab(abFullName, assetName, delegate(UnityEngine.Object[] objs) {
                if (objs.Length == 0) return;
                GameObject prefab = objs[0] as GameObject;
                if (prefab == null) return;

                GameObject go = Instantiate(prefab) as GameObject;
				go.name = luaName;
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
#else
            GameObject prefab = ResManager.LoadAsset<GameObject>(name, assetName);
            if (prefab == null) return;

            GameObject go = Instantiate(prefab) as GameObject;
            go.name = assetName;
            go.layer = LayerMask.NameToLayer("Default");
            go.transform.SetParent(Parent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.AddComponent<LuaBehaviour>();

            if (func != null) func.Call(go);
            Debug.LogWarning("CreatePanel::>> " + name + " " + prefab);
#endif
        }



        /// <summary>
        /// �ر����
        /// </summary>
        /// <param name="name"></param>
        public void ClosePanel(string name) {
            var panelName = name + "Panel";
            var panelObj = Parent.FindChild(panelName);
            if (panelObj == null) return;
            Destroy(panelObj.gameObject);
        }
    }
}