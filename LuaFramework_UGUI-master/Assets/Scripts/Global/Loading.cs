using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LuaFramework;
using UnityEngine.SceneManagement;

namespace GlobalGame 
{
	public class Loading : MonoBehaviour 
	{
//		public string m_ToSceneName;
		public Slider m_ProcessSlider;
		private AsyncOperation mAsyncOperation;
		private MySceneManager m_mySceneManager;

		void Start () 
		{
			GameObject gameManager = GameObject.Find ("GameManager");
			m_mySceneManager = gameManager.GetComponent<MySceneManager> ();
			ResourceManager myResManager = gameManager.GetComponent<ResourceManager> ();

			if (1==1)
			{
				
				mAsyncOperation = SceneManager.LoadSceneAsync(m_mySceneManager.m_assetName);
//				if (mySceneManager.m_func != null) mySceneManager.m_func.Call();

				return;
			}

//			myResManager.LoadSecne(mySceneManager.m_abName, mySceneManager.m_assetName, delegate(UnityEngine.Object[] objs) 
//			{
//				Debug.Log ("GoToScene2_________________________________");
////				SceneManager.LoadScene(assetname);
//				mAsyncOperation = SceneManager.LoadSceneAsync(mySceneManager.m_assetName);
//
//				if (mySceneManager.m_func != null) mySceneManager.m_func.Call();
//			});
		}

		void Update () 
		{
			if (mAsyncOperation != null) {
				Debug.Log ("mAsyncOperation.progress = "+mAsyncOperation.progress);
				if (mAsyncOperation.progress >= 0.9f) 
				{
//					if (m_mySceneManager.m_luaName != null && GameObject.Find (m_mySceneManager.m_luaName) != null)
//						return;
//					m_ProcessSlider.value = 1;
//					Debug.Log ("MySceneManager1111111111______________ = " + mAsyncOperation.progress);


					m_ProcessSlider.value = 0.9f;
					mAsyncOperation.allowSceneActivation = true;
//					GameObject go = new GameObject ();
//					go.name = m_mySceneManager.m_luaName;
//					go.layer = LayerMask.NameToLayer ("Default");
//					//					go.transform.SetParent(gameObject.transform);
//					go.transform.localScale = Vector3.one;
//					go.transform.localPosition = Vector3.zero;
//					LuaBehaviour luaBehaviour = go.AddComponent<LuaBehaviour> ();
//					luaBehaviour.abName = m_mySceneManager.m_abName;
//					luaBehaviour.luaName = m_mySceneManager.m_luaName;
				} 
				else 
				{
//					Debug.Log ("mAsyncOperation.progress = "+mAsyncOperation.progress);
					m_ProcessSlider.value = mAsyncOperation.progress;
				}
			}
		}
	}
}
