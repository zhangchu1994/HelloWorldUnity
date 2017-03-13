using UnityEngine;
using System.Collections;

public class CameraKeyBoardControl : MonoBehaviour 
{
    [SerializeField]
    float speed = 60f;

    [SerializeField]
    string forward_backward_axis = "Vertical";

    [SerializeField]
    string left_right_axis = "Horizontal";
	// Update is called once per frame
	void Update () 
    {
		
		float for_back = Input.GetAxis(forward_backward_axis);
        float left_right = Input.GetAxis(left_right_axis);

//        Vector3 for_back_vector = transform.forward * for_back;
//
//        Vector3 left_right_vector = transform.right * left_right;
//		Debug.Log (for_back_vector+"____"+left_right_vector);
//        transform.position += (for_back_vector + left_right_vector) * speed * Time.deltaTime;
		float x = transform.position.x + left_right * speed * Time.deltaTime;
		float y = transform.position.y;
		float z = transform.position.z + for_back * speed * Time.deltaTime;
//		if (x < 30 || x > 980)
//			return;
//		if (z < 13 || x > 1013)
//			return;


		transform.position = new Vector3 (x, y, z);
	}
}
