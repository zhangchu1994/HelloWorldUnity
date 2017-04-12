using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LuaFramework;
using SimpleJson;

namespace GlobalGame 
{
	public class GuideClass : MonoBehaviour 
	{
		public Button m_Button;
		public Image m_Image;
		public Camera m_cam;

		void Start () 
		{
//			float normalizedTop = 0.7691406f;
//			float normalizedBottom = 0.6220818f;
//			float normalizedLeft = 0.04555054f;
//			float normalizedRight = 0.1288839f;
			float isRect = 0;
			float radius = 0.09f;

			Vector3 pos = m_Button.gameObject.transform.position;  // get the game object position
			Vector3 viewportPoint = m_cam.WorldToViewportPoint(pos);  //convert game object position to VievportPoint
			Debug.Log("TestClass = "+viewportPoint.ToString());

			Canvas canvas = m_Button.gameObject.AddComponent<Canvas> ();
			canvas.overrideSorting = true;
			canvas.sortingOrder = 10;
			m_Button.gameObject.AddComponent<GraphicRaycaster>();

			float normalizedTop = viewportPoint.y + radius;
			float normalizedBottom = viewportPoint.y - radius;
			float normalizedLeft = viewportPoint.x - radius;
			float normalizedRight = viewportPoint.x + radius;

			m_Image = GetComponent<Image>();
			Material mat = m_Image.material;
			mat.SetFloat("_Top", normalizedTop);
			mat.SetFloat("_Bottom", normalizedBottom);
			mat.SetFloat("_Left", normalizedLeft);
			mat.SetFloat("_Right", normalizedRight);
			mat.SetFloat("_IsRect", isRect);
			mat.SetFloat("_Ratio", (float)Screen.width/(float)Screen.height);
		}

		void Update () 
		{

		}
	}
}
