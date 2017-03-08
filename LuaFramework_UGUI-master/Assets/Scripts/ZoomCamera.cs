using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalGame 
{
	public class ZoomCamera : MonoBehaviour 
	{
		private Camera m_cam;
		public float minFov = 15f;
		public float maxFov = 90f;
		public float sensitivity = 10f;

		void Awake()
		{
			m_cam = this.GetComponent<Camera> ();
		}

		void Update()
		{
			float fov = Camera.main.fieldOfView;
			fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
			fov = Mathf.Clamp(fov, minFov, maxFov);
			Camera.main.fieldOfView = fov;

			JudgeFinger ();
		}

		private Vector3 startFingerPos;  
	    private Vector3 nowFingerPos;  
	    private float xMoveDistance;  
	    private float yMoveDistance;  
	    private int backValue = 0;  
//	    public GameObject my_Cube;  
  
		public void JudgeFinger ()  
	    {  
	        //没有触摸  
	        if (Input.touchCount <= 0) 
			{ 
//				Debug.Log("======没有触摸====="+Input.touchCount);  
	            return;  
	        }  
	  
	        if (Input.GetTouch (0).phase == TouchPhase.Began ) 
			{  
	            Debug.Log("======开始触摸=====");  
	            startFingerPos = Input.GetTouch (0).position;  
	        }  
	  
	        nowFingerPos = Input.GetTouch (0).position;  
	  
	        if ((Input.GetTouch (0).phase == TouchPhase.Stationary) || (Input.GetTouch(0).phase == TouchPhase.Ended)) 
			{  
	            startFingerPos = nowFingerPos;  
	            //Debug.Log("======释放触摸=====");  
	            return;  
	        }  
	        //          if (Input.GetTouch(0).phase == TouchPhase.Ended) {  
	        //                
	        //          }  
	        if (startFingerPos == nowFingerPos) 
			{  
	            return;  
	        }  
	        xMoveDistance = Mathf.Abs (nowFingerPos.x - startFingerPos.x);  
	  
	        yMoveDistance = Mathf.Abs (nowFingerPos.y - startFingerPos.y);  
	  
	        if (xMoveDistance > yMoveDistance) {  
	  
	            if (nowFingerPos.x - startFingerPos.x > 0) {  
	  
	                //Debug.Log("=======沿着X轴负方向移动=====");  
	  
	                backValue = -1; //沿着X轴负方向移动  
	  
	            } else {  
	  
	                //Debug.Log("=======沿着X轴正方向移动=====");  
	  
	                backValue = 1; //沿着X轴正方向移动  
	  
	            }  
	  
	        } else {  
	  
	            if (nowFingerPos.y - startFingerPos.y > 0) {  
	  
	                //Debug.Log("=======沿着Y轴正方向移动=====");  
	  
	                backValue = 2; //沿着Y轴正方向移动  
	  
	            } else {  
	  
	                //Debug.Log("=======沿着Y轴负方向移动=====");  
	  
	                backValue = -2; //沿着Y轴负方向移动  
	  
	            }  
	  
	        }  


			float y = m_cam.transform.position.y;
			float z = m_cam.transform.position.z;
//			if (x < 30)
//				return;
//			if (z < 13)
//				return;




	        if (backValue == -1) 
			{  
				float x = m_cam.transform.position.x + 30 * Time.deltaTime;

//	            my_Cube.transform.Rotate (Vector3.back * Time.deltaTime * 300, Space.World);     
				m_cam.transform.position = new Vector3 (x, y, z);
//				(Vector3.back * Time.deltaTime * 300, Space.World);     
	        } else if (backValue == 1) {
				float x = m_cam.transform.position.x - 30 * Time.deltaTime;
				m_cam.transform.position = new Vector3 (x, y, z);
//	            my_Cube.transform.Rotate (Vector3.back * -1 * Time.deltaTime * 300, Space.World);   
//				m_cam.transform.Rotate (Vector3.back * -1 * Time.deltaTime * 300, Space.World);     
	        }   
	        //      else if (backValue == 2) {  
	        //          my_Cube.transform.Rotate(Vector3.right  * Time.deltaTime * 200 , Space.World);   
	        //      } else if (backValue == -2) {  
	        //          my_Cube.transform.Rotate(Vector3.right  * -1  * Time.deltaTime * 200 , Space.World);   
	        //      }  
	  
	    }  
	}
}