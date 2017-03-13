using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalGame 
{
	public class CameraControl : MonoBehaviour 
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

			if (Global.isMobile () == true) 
			{
				FingerZoom ();
				FingerMove ();
			} 
			else 
			{
				MouseZoom ();
				MouseMove ();
			}
		}

		void MouseZoom()
		{
			float fov = Camera.main.fieldOfView;
			fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
			fov = Mathf.Clamp(fov, minFov, maxFov);
			Camera.main.fieldOfView = fov;
		}

		bool m_isLeftMouseDown = false;
		void MouseMove()
		{
			if (Input.GetMouseButtonDown (0)) 
				m_isLeftMouseDown = true;

			if (Input.GetMouseButtonUp (0)) 
				m_isLeftMouseDown = false;

		
			if (m_isLeftMouseDown == true) 
			{
				if(Input.GetAxis("Mouse X")<0)
				{
					m_cam.transform.position = new Vector3 (m_cam.transform.position.x-30 * Time.deltaTime, m_cam.transform.position.y, m_cam.transform.position.z);
					//Code for action on mouse moving left
//					print("Mouse moved left");
				}
				if(Input.GetAxis("Mouse X")>0)
				{
					m_cam.transform.position = new Vector3 (m_cam.transform.position.x+30 * Time.deltaTime, m_cam.transform.position.y, m_cam.transform.position.z);
					//Code for action on mouse moving right
//					print("Mouse moved right");
				}
			}
		}

		public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
		public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
		void FingerZoom()
		{
			// If there are two touches on the device...
			if (Input.touchCount == 2)
			{
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				if (m_cam.orthographic)
				{
					m_cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
					m_cam.orthographicSize = Mathf.Max(m_cam.orthographicSize, 0.1f);
				}
				else
				{
					// Otherwise change the field of view based on the change in distance between the touches.
					m_cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

					// Clamp the field of view to make sure it's between 0 and 180.
					m_cam.fieldOfView = Mathf.Clamp(m_cam.fieldOfView, 0.1f, 179.9f);
				}
			}
		}

		private Vector3 startFingerPos;  
		private Vector3 nowFingerPos;  
		private float xMoveDistance;  
		private float yMoveDistance;  
		private int backValue = 0;  
		void FingerMove ()  
	    {  
			if (Input.touchCount != 1 ) 
	            return;  

			Vector2 direction = new Vector2(0,0);
			float speed = 0;

			if(Input.touches[0].phase == TouchPhase.Moved)//Check if Touch has moved.
			{
				direction = Input.touches[0].deltaPosition.normalized;  //Unit Vector of change in position
				speed = Input.touches[0].deltaPosition.magnitude / Input.touches[0].deltaTime; //distance traveled divided by time elapsed
			}
			float x = m_cam.transform.position.x;
			float y = m_cam.transform.position.y;
			float z = m_cam.transform.position.z;
			m_cam.transform.position = new Vector3 (x+direction.x, y, z+direction.y);
	    }  
	}
}