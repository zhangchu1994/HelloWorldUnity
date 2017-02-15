using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections; 
using UnityEngine.AI;
using GlobalGame;

public class TestClass : MonoBehaviour 
{
	private static NavMeshAgent m_agent = null;
	GameObject m_start;
	GameObject m_End;
	// Use this for initialization
	void Start () 
	{
		m_start = GameObject.Find ("Start");
		m_End = GameObject.Find ("End");
//		m_agent = (NavMeshAgent)m_start.GetComponent("NavMeshAgent");
//    	m_agent.speed = 1;
//   		m_agent.stoppingDistance = 0.0f; 
//    	m_agent.radius = 3;
//    	m_agent.acceleration = 1;

		GameObject go = new GameObject();
   		go.name = "Actor";
		go.AddComponent<Actor1>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_agent != null) 
		{
			m_agent.SetDestination (m_End.transform.localPosition);
		}
	}

	public static void Go()
	{
		GameObject actor = GameObject.Find ("Ian1970");
		m_agent = (NavMeshAgent)actor.GetComponent("NavMeshAgent");
	}
}
