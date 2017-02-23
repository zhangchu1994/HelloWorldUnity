using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGame
{
	public class ActorAIManager : MonoBehaviour 
	{
		void Start () 
		{

		}

		void UpdateAI()
		{
//			List<GameObject> monsterList = BattleScene.Active.m_monsterList;
//			for (int i = 0; i < monsterList.Count; i++) 
//			{
//				GameObject monsterObject = monsterList[i];
//				if (monsterObject == null)
//					continue;
//				Vector3 pos1 = new Vector3( monsterObject.transform.position.x, 0, monsterObject.transform.position.z );
//				Vector3 pos2 = new Vector3( m_ActorObject.transform.position.x, 0, m_ActorObject.transform.position.z );
////				Debug.Log ("UpdateAI = "+Vector3.Distance(pos1,pos2));
//
//				if (Vector3.Distance(pos1,pos2) <= 2)
//				{
//					DestroyImmediate (monsterObject);
//
//					Object psObj = Resources.Load ("Effect/CircleFX_Dark");
//					GameObject t = Instantiate(psObj) as GameObject;
//					t.transform.position = pos1;
//					t.transform.localScale = new Vector3 (2f, 2f, 2f);
//				}
//			}
		}


		// Update is called once per frame
		void Update () 
		{
			UpdateAI ();
		}
	}

}
