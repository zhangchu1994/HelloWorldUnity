using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalGame 
{
	public class ActorUIManager : MonoBehaviour 
	{
		public GameObject m_CanvasParent;
		private Actor m_MainActor;
		GameObject m_Blood;//血条GameObject
		Slider m_Slider;//血条Slider
		List<GameObject> m_DamageText = new List<GameObject>();//掉的血

		void Awake()
		{
			m_MainActor = this.transform.GetComponent<Actor> ();
			m_CanvasParent = GameObject.Find ("Canvas");
		}

		void Start () 
		{
			
		}
		
		void Update () 
		{
			UpdatePosition (m_Blood);
			for (int i = 0; i < m_DamageText.Count; i++) 
			{
				GameObject text = m_DamageText [i];
				UpdatePosition (text);
			}
		}

//		int Rom()
//		{
//			
//		}


		public void InitLoseBlood(float blood)
		{
			Object m_TextPrefab = Resources.Load ("BloodText");
			GameObject t = Instantiate(m_TextPrefab) as GameObject;
			t.transform.SetParent(m_CanvasParent.transform, false);
//			t.name = "BloodText" + this.name+m_DamageText.Count.ToString();
			float number = Random.Range(-1.0f, 1.0f);
			t.name =  number.ToString();
			t.tag = "LoseBloodText";
			Text text = t.GetComponent<Text> ();
			text.text = blood.ToString();
			m_DamageText.Add (t);

			Hashtable args = new Hashtable();
			args["amount"] =  new Vector3(0,10,0);
			args["time"] =  0.5f;
			args["easetype"] = iTween.EaseType.linear;
			args["oncomplete"] = "DamageLabelMove";
			args["oncompletetarget"] = gameObject;
			args["oncompleteparams"] = t;
			iTween.MoveBy (t, args);

			iTween.ScaleBy(t,new Vector3(2.5f,2.5f,2.5f),0.5f);
		}

		void DamageLabelMove(GameObject t)
		{
			m_DamageText.Remove (t);
			Destroy (t);
		}

		public void InitActorBlood()
		{
			Object m_TextPrefab = Resources.Load ("Blood");
			m_Blood = Instantiate(m_TextPrefab) as GameObject;
			m_Blood.transform.SetParent(m_CanvasParent.transform, false);
			m_Blood.name = "Blood" + this.name;

			m_Slider = m_Blood.GetComponent<Slider>();
		}

		public void UpdateBloodRatio(float ratio)
		{
			m_Slider.value = ratio;
		}

		void UpdatePosition(GameObject obj)
		{
			Transform trans = this.transform;
			if (obj == null)
				return;
			
			RectTransform Rect = obj.GetComponent<RectTransform> ();
			Vector3 center0 = trans.GetComponent<Collider>().bounds.center;

			Vector3 center = new Vector3(center0.x,center0.y,center0.z);
			if (obj.CompareTag ("LoseBloodText")) 
			{
				float x = float.Parse (obj.name); 
//				if (m_MainActor.name == "Monster1" && obj != m_Blood)
//				Debug.Log ("UpdatePosition_______ = "+x);
				center = new Vector3(center0.x+x,center0.y,center0.z);
			}
			Vector3 position = center + (((Vector3.up * trans.GetComponent<Collider>().bounds.size.y) * 0.6f));
			Vector3 front = position - Camera.main.transform.position;

			//its in camera view
//			if ((front.magnitude <= 75) && (Vector3.Angle(Camera.main.transform.forward, position - Camera.main.transform.position) <= 180))
//			{
				Vector2 v = Camera.main.WorldToViewportPoint(position);                       
				Rect.anchorMax = v;
				Rect.anchorMin = v;
//			}

		}

		public void ActorDead()
		{
			Destroy (m_Blood);
		}
	}
}
