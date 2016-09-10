using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour 
{
	//int m_iTime = 0;
	// Use this for initialization

	public GameObject m_meshObject;

	void Start () 
	{
		Debug.Log("1233");
//		UIEventListener.Get(m_Button).onClick = TapRefreshButton;
//		m_Actor.GetComponent<NavMeshAgent>().destination = m_pos1.transform.position;
//		m_Cube.GetComponent<NavMeshAgent>().destination = m_pos1.transform.position;
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

	void TapRefreshButton(GameObject pauseBtn)
	{
//		StartCoroutine( InitBattleScene());
//		for (int i = 0; i < 800; i++) 
//		{
//			loadUser();
//		}
	}

	void loadUser()
	{
		GameObject itemPrefab = (GameObject)Resources.Load("Actor/ActorPrefab/Actor/Actor101");
//		Yield(itemPrefab);
		GameObject role = GameObject.Instantiate(itemPrefab);//newObject(itemPrefab);
//		Yield(role);
		role.name = "Role";
		role.transform.parent = this.transform;
		role.transform.localScale = new Vector3(200,200,200);//--Vector3.one
		role.transform.localPosition = new Vector3(30,-80,-200);//--Vector3.zero
	}
}
