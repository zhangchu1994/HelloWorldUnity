using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

    public Vector3 offset;
    private Transform playerBip;
    public float smoothing = 0;
	public bool first = true;

	// Use this for initialization
	void Start () {
//	    if (TranscriptManager._instance != null)
//	    {
//	        playerBip = TranscriptManager._instance.player.transform.Find("Bip01");
//	    }
//	    else
//	    {
//	        playerBip = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
//	    }
//		playerBip = TestClass.m_Role.transform;
		offset = new Vector3(3.2f,23,-32.5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.position = playerBip.position + offset;
//		if (TestClass.m_Role != null && first == true) 
//		{
//			first = false;
//			transform.position = new Vector3 (TestClass.m_Role.transform.position.x,TestClass.m_Role.transform.position.y+10,TestClass.m_Role.transform.position.z);
//		}

		if (BattleScene.Active.m_RoleObject != null) 
		{
			playerBip = BattleScene.Active.m_RoleObject.transform;
			Vector3 targetPos = playerBip.position + offset;
			transform.position = Vector3.Lerp(transform.position, targetPos, smoothing * Time.deltaTime);
		}
	}
}
