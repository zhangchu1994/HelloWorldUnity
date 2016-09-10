using UnityEngine;
using System.Collections;

public class MainCityScene : MonoBehaviour 
{
	//int m_iTime = 0;
	// Use this for initialization
	public RectTransform rectTransform;
	public GameObject m_Plane;
	public GameObject m_Cube;
	public GameObject m_Camera;
	public GameObject m_Actor;
	public GameObject m_pos1;
	Transform standardPos;			// the usual position for the camera, specified by a transform in the game

	void Start () 
	{
		Debug.Log("1233");
//		m_Actor.GetComponent<NavMeshAgent>().destination = m_pos1.transform.position;
//		m_Cube.GetComponent<NavMeshAgent>().destination = m_pos1.transform.position;
		//GUI.Button(Rect(100,100,100,50),"PLAY");

		standardPos = GameObject.Find ("CamPos").transform;
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

	public void MoveStart()
	{

	}
	
	public void Move()
	{
		//		anim.SetBool("Move",true);

		m_Camera.transform.position = standardPos.position;	
		m_Camera.transform.forward = standardPos.forward;

		Debug.Log (rectTransform.anchoredPosition3D.x+"________"+rectTransform.anchoredPosition3D.y);
		GameObject obj = m_Actor.GetComponent<Actor>().m_meshObject;
		Animation animation = obj.GetComponent<Animation>();
//		animation.wrapMode = WrapMode.Loop;
		animation.Play("Run",PlayMode.StopAll);
	}
	
	public void MoveStop()
	{
//		anim.SetBool("Move",false);
	}
}
