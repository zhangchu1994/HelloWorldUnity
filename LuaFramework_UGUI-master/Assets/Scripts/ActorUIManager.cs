using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame 
{
	public class ActorUIManager : MonoBehaviour 
	{
		public GameObject m_CanvasParent;
		public Object m_TextPrefab;

		// Use this for initialization
		void Start () 
		{

		}
		
		// Update is called once per frame
		void Update () 
		{
			UpdateBlood ();
		}

		public void InitActorBlood()
		{
			m_CanvasParent = GameObject.Find ("Canvas");
			m_TextPrefab = Resources.Load ("Text");
			GameObject t = Instantiate(m_TextPrefab) as GameObject;
			t.transform.SetParent(m_CanvasParent.transform, false);

			//			RectTransform Rect = t.GetComponent<RectTransform> ();
			//		Text text = t.GetComponent<Text> ();

			//Create new text info to instatiate 
			//		bl_Text item = t.GetComponent<bl_Text>();
			//
			//		item.m_Color = color;
			//		item.m_Transform = trans;
			//		item.m_text = text;

			//		t.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}

		void UpdateBlood()
		{
			Transform trans = this.transform;
			GameObject t = GameObject.Find ("Canvas/Text(Clone)");
			if (t == null)
				return;
			RectTransform Rect = t.GetComponent<RectTransform> ();
			Vector3 position = trans.GetComponent<Collider>().bounds.center + (((Vector3.up * trans.GetComponent<Collider>().bounds.size.y) * 0.6f));
			Vector3 front = position - Camera.main.transform.position;
			//its in camera view
			if ((front.magnitude <= 75) && (Vector3.Angle(Camera.main.transform.forward, position - Camera.main.transform.position) <= 180))
			{
				Vector2 v = Camera.main.WorldToViewportPoint(position);                       
				//			text.fontSize = 100;
				//			text.text = "123";
				Rect.anchorMax = v;
				Rect.anchorMin = v;
				//			text.color = color;
			}

		}
	}
}
