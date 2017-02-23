using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalGame 
{
	public class ActorUIManager : MonoBehaviour 
	{
		public GameObject m_CanvasParent;
//		public Object m_TextPrefab;
		private bool m_testBlood;

		// Use this for initialization
		void Start () 
		{
			m_testBlood = false;
		}
		
		// Update is called once per frame
		void Update () 
		{
			UpdatePosition ("Canvas/Blood" + this.name);
			UpdatePosition ("Canvas/BloodText" + this.name);
		}


		public void InitLoseBlood()
		{
			if (m_testBlood == false) 
			{
				Object m_TextPrefab = Resources.Load ("BloodText");
				GameObject t = Instantiate(m_TextPrefab) as GameObject;
				t.transform.SetParent(m_CanvasParent.transform, false);
				t.name = "BloodText" + this.name;
				m_testBlood = true;
				Text text = t.GetComponent<Text> ();
				text.text = "-20";


				Hashtable args = new Hashtable();
				args["amount"] =  new Vector3(0,10,0);
				args["time"] =  0.5f;
				args["easetype"] = iTween.EaseType.linear;
				args["oncomplete"] = "DamageLabelMove";
				args["oncompletetarget"] = gameObject;
				args["oncompleteparams"] = t;
				iTween.MoveBy (t, args);


//				iTween.MoveBy(t,new Vector3(0,10,0),0.3f);
				iTween.ScaleBy(t,new Vector3(2,2,2),0.5f);
			}
		}

		void DamageLabelMove(GameObject t)
		{
			m_testBlood = false;
			Destroy (t);
		}

		public void InitActorBlood()
		{
			m_CanvasParent = GameObject.Find ("Canvas");
			Object m_TextPrefab = Resources.Load ("Blood");
			GameObject t = Instantiate(m_TextPrefab) as GameObject;
			t.transform.SetParent(m_CanvasParent.transform, false);
			t.name = "Blood" + this.name;
		}

		void UpdatePosition(string argName)
		{
			Transform trans = this.transform;
			GameObject t = GameObject.Find (argName);
			if (t == null)
				return;
			if (argName == "Canvas/BloodText(Clone)") 
			{
				Debug.Log ("UpdatePosition__________________"+this.transform.name);
			}
			RectTransform Rect = t.GetComponent<RectTransform> ();
			Vector3 position = trans.GetComponent<Collider>().bounds.center + (((Vector3.up * trans.GetComponent<Collider>().bounds.size.y) * 0.6f));
			Vector3 front = position - Camera.main.transform.position;

			//its in camera view
//			if ((front.magnitude <= 75) && (Vector3.Angle(Camera.main.transform.forward, position - Camera.main.transform.position) <= 180))
//			{
				Vector2 v = Camera.main.WorldToViewportPoint(position);                       
				Rect.anchorMax = v;
				Rect.anchorMin = v;
//			}

		}
	}
}
