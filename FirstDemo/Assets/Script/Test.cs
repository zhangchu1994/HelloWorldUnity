using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour 
{
	public GameObject test1Obj1;
	public GameObject test1Obj2;
	public GameObject test1Obj3;
	float x = 0;

	void Start () 
	{
	
	}
	
	void Update () 
	{

		if (x <= 0.2f) 
		{
			x += Time.deltaTime;
		} 
		else 
		{
			x = 0;
//			Hashtable args = new Hashtable(6);
//			args["rotation"] = new Vector3(0.0f, 0.0f,5.0f);
//			args["islocal"] = true;
//			args["easetype"] = iTween.EaseType.easeInOutQuad;
//			args["time"] = 0.1f;
//			
//			args["oncomplete"] = "ShowAnimation0";
//			args["oncompletetarget"] = gameObject;
			Hashtable args = iTween.Hash("rotation",new Vector3(0.0f, 0.0f,5.0f),"islocal",true,"easetype",iTween.EaseType.easeInOutQuad,"time",0.1f);//,"oncomplete","ShowAnimation0","oncompletetarget",gameObject
			iTween.RotateTo (test1Obj1, args);

		}

// Slowly rotate the object around its X axis at 1 degree/second.
		//沿着x轴每秒1度慢慢的旋转物体
//		test1Obj1.transform.Rotate(Vector3.right * Time.deltaTime);
//		Debug.Log (Vector3.right.ToString());


		// ... at the same time as spinning relative to the global
		// Y axis at the same speed.
		//相对于世界坐标沿着y轴每秒1度慢慢的旋转物体
//		test1Obj1.transform.Rotate(Vector3.up * 1, Space.World);
//		test1Obj1.transform.Rotate(Vector3.up * 1);


//		float smooth = 2.0f;
//		float tiltAngle = 100.0f;
//		float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
//		float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
//
//		test1Obj1.transform.eulerAngles = new Vector3(tiltAroundX, 0, tiltAroundZ);
//
//		Quaternion target = Quaternion.Euler (tiltAroundX, 0, tiltAroundZ);
//		test1Obj1.transform.rotation = target;
//
//		// 从当前角度旋转到目标角度 第3个参数是速度
//		test1Obj1.transform.rotation = Quaternion.Slerp(test1Obj1.transform.rotation, target,Time.deltaTime * smooth);
//
//		if ( (int)tiltAroundZ != 0 || (int)tiltAroundX != 0 )
//			Debug.Log ("tiltAroundZ = "+tiltAroundZ+" tiltAroundX = "+tiltAroundX+" target = "+target.ToString()+" transform.rotation = "+test1Obj1.transform.rotation);
	}
}
