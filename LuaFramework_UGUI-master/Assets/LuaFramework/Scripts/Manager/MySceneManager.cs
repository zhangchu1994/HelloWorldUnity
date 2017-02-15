using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using LuaInterface;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


namespace LuaFramework {
	public class MySceneManager : Manager 
	{
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
		public string m_abName;
		public string m_assetName;
		public string m_luaName;

		private AsyncOperation mAsyncOperation;
//		private int mCurProgress = 0;

		public void GoToScene(string abName, string assetname,string luaName = null,LuaFunction func = null)
		{
			Debug.Log ("GoToScene1_________________________________");
			m_abName = abName;
			m_assetName = assetname;
			m_luaName = luaName;

			if (1==1)
			{
				mAsyncOperation = SceneManager.LoadSceneAsync(assetname);
				if (func != null) func.Call();
				return;
			}

			ResManager.LoadSecne(abName, assetname, delegate(UnityEngine.Object[] objs) {
				Debug.Log ("GoToScene2_________________________________");
//				SceneManager.LoadScene(assetname);
				mAsyncOperation = SceneManager.LoadSceneAsync(assetname);

				if (func != null) func.Call();
			});
		}

		void Update () 
		{
			if (mAsyncOperation != null) 
			{
				if (mAsyncOperation.progress == 1) 
				{
					if (GameObject.Find(m_luaName) != null) 
						return;

//					GameObject role = GameObject.Find (m_luaName);
//					role.transform.Translate (Vector3 (0, 1, 0));
//					role.transform.Translate(Vector3 (0, 1, 0));
//					NavMeshAgent nav;
//					nav.speed = 

					Debug.Log ("MySceneManager______________ = "+mAsyncOperation.progress);
					mAsyncOperation.allowSceneActivation = true;
					GameObject go = new GameObject();
					go.name = m_luaName;
					go.layer = LayerMask.NameToLayer("Default");
//					go.transform.SetParent(gameObject.transform);
					go.transform.localScale = Vector3.one;
					go.transform.localPosition = Vector3.zero;
					LuaBehaviour luaBehaviour = go.AddComponent<LuaBehaviour>();
					luaBehaviour.abName = m_abName;
					luaBehaviour.luaName = m_luaName;
				
				}
			}

//			// 以下都是为实现加载进度条的
//			int progressBar = 0;
//			if (mAsyncOperation.progress < 0.8) 
//				progressBar = (int)(mAsyncOperation.progress * 100);
//			else 
//				progressBar = 100;
//			if (mCurProgress <= progressBar)
//			{
//				mCurProgress++;
//				// 进度条ui显示（本文不讨论） 
//				((Win_Loading)UIWindowCtrl.GetInstance().GetCurrentWindow()).loadingView.SetLoadSceneInfo(mCurProgress * 0.01f);
//			}
//			else
//			{
//				// 必须等进度条跑到100%才允许切换到下一场景
//				if (progressBar == 100) mAsyncOperation.allowSceneActivation = true;
//			}
		}
    }
}