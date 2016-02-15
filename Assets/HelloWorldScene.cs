using UnityEngine;
using System.Collections;

public class HelloWorldScene : MonoBehaviour 
{
	//int m_iTime = 0;
	// Use this for initialization
	void Start () 
	{
		Debug.Log("1233");

		//GUI.Button(Rect(100,100,100,50),"PLAY");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//m_iTime = m_iTime + 0.1;
		//Debug.Log("555");
		//GameObject root = GameObject.Find("Cube");
		//root.transform.Rotate(0, 100 * Time.deltaTime, 0);

		GameObject root = GameObject.Find("Main Camera");
		root.transform.Rotate(0, 10 * Time.deltaTime, 0);


	}
}
