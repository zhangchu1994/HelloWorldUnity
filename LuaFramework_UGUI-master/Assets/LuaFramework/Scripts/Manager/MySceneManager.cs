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
		public string m_abName;
		public string m_assetName;
		public string m_luaName;
		public LuaFunction m_func;

//		private int mCurProgress = 0;

		public void GoToScene(string abName, string assetname,string luaName = null,LuaFunction func = null)
		{
			Debug.Log ("MySceneManager_GoToScene_________________________________SceneName = "+assetname);
			m_abName = abName;
			m_assetName = assetname;
			m_luaName = luaName;
			m_func = func;

			SceneManager.LoadScene("Loading");
		}

//		void Update () 
//		{
//			
//		}

//			// ���¶���Ϊʵ�ּ��ؽ�������
//			int progressBar = 0;
//			if (mAsyncOperation.progress < 0.8) 
//				progressBar = (int)(mAsyncOperation.progress * 100);
//			else 
//				progressBar = 100;
//			if (mCurProgress <= progressBar)
//			{
//				mCurProgress++;
//				// ������ui��ʾ�����Ĳ����ۣ� 
//				((Win_Loading)UIWindowCtrl.GetInstance().GetCurrentWindow()).loadingView.SetLoadSceneInfo(mCurProgress * 0.01f);
//			}
//			else
//			{
//				// ����Ƚ������ܵ�100%�������л�����һ����
//				if (progressBar == 100) mAsyncOperation.allowSceneActivation = true;
//			}
//		}
    }
}