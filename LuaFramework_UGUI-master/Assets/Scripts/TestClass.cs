using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections; 
using UnityEngine.AI;
using GlobalGame;
using UnityEngine.UI;

public class TestClass : MonoBehaviour 
{
	private static NavMeshAgent m_agent = null;
	public static GameObject m_StartPoint;
	public static GameObject m_EndPonit;
	public static GameObject m_Role;
	public static bool shoudldGo = false;
	public static Transform m_Targettransform = null;
	public RectTransform CanvasParent;
	public GameObject TextPrefab;

	// Use this for initialization
	void Start () 
	{
		m_StartPoint = GameObject.Find ("StartPoint");
		m_EndPonit = GameObject.Find ("EndPoint");

//		m_Role = GameObject.Find ("Ian1994");
//		TestClass.SetNavMesh ();

		GameObject go = new GameObject();
   		go.name = "Actor";
		go.AddComponent<Actor1>();

		StartCoroutine(GetResult());  
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (shoudldGo == true) 
		{
			m_agent.SetDestination (m_Targettransform.position);
		}
		updateText ();
	}

	public static void Go()
	{
		m_Role = GameObject.Find ("Ian1970");
		m_Role.transform.position = new Vector3 (m_StartPoint.transform.position.x, m_StartPoint.transform.position.y, m_StartPoint.transform.position.z);
	}

	IEnumerator GetResult()  
    {  
    	yield return new WaitForSeconds(2);
		NewText ("123",m_Role.transform,Color.white);
//		TestClass.SetNavMesh ();
    }  

	static void SetNavMesh()
	{
//		m_Role.transform.position = new Vector3 (m_StartPoint.transform.position.x,m_StartPoint.transform.position.y,m_StartPoint.transform.position.z);
		shoudldGo = true;
		m_agent = (NavMeshAgent)m_Role.GetComponent("NavMeshAgent");
		m_agent.speed = 15;
		m_agent.stoppingDistance = 0.01f; 
		m_agent.radius = 2;
		m_agent.acceleration = 15;
		m_agent.autoRepath = true;
		m_agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
	}

	public static void SetTarget(Transform transform)
	{
		m_Targettransform = transform;
		TestClass.SetNavMesh ();
	}


	public void NewText(string text1, Transform trans, Color color)
	{
		GameObject t = Instantiate(TextPrefab) as GameObject;
		RectTransform Rect = t.GetComponent<RectTransform> ();
//		Text text = t.GetComponent<Text> ();

		//Create new text info to instatiate 
//		bl_Text item = t.GetComponent<bl_Text>();
//
//		item.m_Color = color;
//		item.m_Transform = trans;
//		item.m_text = text;

		t.transform.SetParent(CanvasParent, false);
//		t.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	void updateText()
	{
		Transform trans = m_Role.transform;
		GameObject t = GameObject.Find ("Canvas/Text(Clone)");
		if (t == null)
			return;
		RectTransform Rect = t.GetComponent<RectTransform> ();
		Vector3 position = trans.GetComponent<Collider>().bounds.center + (((Vector3.up * trans.GetComponent<Collider>().bounds.size.y) * 0.5f));
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
	