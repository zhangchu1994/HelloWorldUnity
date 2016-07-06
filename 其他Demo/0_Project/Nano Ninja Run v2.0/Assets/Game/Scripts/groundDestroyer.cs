using UnityEngine;
using System.Collections;

public class groundDestroyer : MonoBehaviour
{

		// Use this for initialization

		public bool canBeDestroyed = false ;
		Transform thisTrans;
		void Start ()
		{
				thisTrans = transform;
				InvokeRepeating ("CheckStatus", 2, 1.5f);
		}
	
		float lastUpdateTime;
		void CheckStatus ()
		{

	 
				if (canBeDestroyed) {
						if (Time.timeSinceLevelLoad - lastUpdateTime > 0.5f && PlayerController.thisPosition.z > thisTrans.localPosition.z + 40) {

								Destroy (gameObject);
								canBeDestroyed = false;
						}
				}
				 
		}


}
