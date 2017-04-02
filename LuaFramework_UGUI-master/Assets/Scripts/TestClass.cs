using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LuaFramework;
using SimpleJson;

namespace GlobalGame {
public class TestClass : MonoBehaviour {

	public Button m_Button;

	// Use this for initialization
	void Start () 
	{
		m_Button.onClick.AddListener(delegate() 
			{

				LuaHelper.GetWebManager().AddCmdHandler("LC_RegisterUserMsg","123");
				JsonObject json = new JsonObject();
				json.TrySet("account","123321");
				json.TrySet("passWord","456dsad");
				LuaHelper.GetWebManager().CMD("CL_RegisterUserMsg", json);
			});
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
}
