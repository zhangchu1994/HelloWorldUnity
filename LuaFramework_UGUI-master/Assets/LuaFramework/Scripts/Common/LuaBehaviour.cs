using UnityEngine;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace LuaFramework {
    public class LuaBehaviour : View {
//        private string data = null;
        private Dictionary<string, LuaFunction> buttons = new Dictionary<string, LuaFunction>();
		public string abName = null;
		public string luaName;
		private string m_iTweenCallBackName;


        protected void Awake() {
			Util.CallMethod(luaName, "Awake", gameObject);
        }

        protected void Start() 
		{
//			Debug.Log ("LuaBehaviour_Start");
			Util.CallMethod(luaName, "Start");
//			StartCoroutine(AllLoadDone());
        }

//		private IEnumerator AllLoadDone() 
//		{
//			yield return new WaitForEndOfFrame();
//		}


        protected void OnClick() {
			Util.CallMethod(luaName, "OnClick");
        }

        protected void OnClickEvent(GameObject go) {
			Util.CallMethod(luaName, "OnClick", go);
        }

		protected void Update()
		{
			Util.CallMethod(luaName, "Update");
		}


		public void AddiTween(GameObject obj,string callBackName)
		{
			m_iTweenCallBackName = callBackName;
			Hashtable args = new Hashtable();
			args["amount"] =  new Vector3(0,400,0);
			args["time"] =  0.5f;
			args["easetype"] = iTween.EaseType.linear;
			args["oncomplete"] = "iTweenDone";
			args["oncompletetarget"] = gameObject;
			args["oncompleteparams"] = obj;
			iTween.MoveBy (obj, args);
		}

		void iTweenDone(GameObject obj)
		{
			Util.CallMethod(luaName, m_iTweenCallBackName,obj);
		}

        /// <summary>
        /// 添加单击事件
        /// </summary>
        public void AddClick(GameObject go, LuaFunction luafunc) 
		{
            if (go == null || luafunc == null) return;
            buttons.Add(go.name, luafunc);
            go.GetComponent<Button>().onClick.AddListener(
                delegate() {
                    luafunc.Call(go);
                }
            );
        }

        /// <summary>
        /// 删除单击事件
        /// </summary>
        /// <param name="go"></param>
        public void RemoveClick(GameObject go) {
            if (go == null) return;
            LuaFunction luafunc = null;
            if (buttons.TryGetValue(go.name, out luafunc)) {
                luafunc.Dispose();
                luafunc = null;
                buttons.Remove(go.name);
            }
        }

        /// <summary>
        /// 清除单击事件
        /// </summary>
        public void ClearClick() {
            foreach (var de in buttons) {
                if (de.Value != null) {
                    de.Value.Dispose();
                }
            }
            buttons.Clear();
        }

        //-----------------------------------------------------------------
        protected void OnDestroy() {
            ClearClick();
#if ASYNC_MODE
//            string abName = name.ToLower().Replace("panel", "");
//            ResManager.UnloadAssetBundle(abName + AppConst.ExtName);
			if(abName != null && abName != "")
				ResManager.UnloadAssetBundle(abName + AppConst.ExtName);
#endif
            Util.ClearMemory();
            Debug.Log("~" + name + " was destroy!");
        }
    }
}