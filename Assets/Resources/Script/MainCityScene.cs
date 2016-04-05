using UnityEngine;
using System.Collections;

public class MainCityScene : MonoBehaviour 
{
	//int m_iTime = 0;
	// Use this for initialization

	public GameObject m_Plane;
	public GameObject m_Cube;
	public GameObject m_Camera;
	public GameObject m_Actor;
	public GameObject m_pos1;

	void Start () 
	{
		Debug.Log("1233");
		m_Actor.GetComponent<NavMeshAgent>().destination = m_pos1.transform.position;
		//GUI.Button(Rect(100,100,100,50),"PLAY");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//m_iTime = m_iTime + 0.1;
		//Debug.Log("555");
		//GameObject root = GameObject.Find("Cube");
		//root.transform.Rotate(0, 100 * Time.deltaTime, 0);

//		GameObject root = GameObject.Find("Main Camera");
//		root.transform.Rotate(0, 100 * Time.deltaTime, 0);
//		m_Cube.transform.Rotate(100 * Time.deltaTime, 100 * Time.deltaTime, 100 * Time.deltaTime);


	}
}
