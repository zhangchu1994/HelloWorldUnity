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
    }
}