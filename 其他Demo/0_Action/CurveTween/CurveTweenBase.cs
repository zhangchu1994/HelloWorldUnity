using UnityEngine;
using System.Collections;

public class CurveTweenBase : MonoBehaviour {
	public float _time = 5;
	public WrapMode _wrapMode = WrapMode.PingPong;
	private float _saveTime;

	// Use this for initialization
	void Start () {
		_saveTime = Time.time;
	}
	protected float nowTime{
		get{
			float t1 = Time.time - _saveTime;
			float t2 = 0;
			switch ((int)_wrapMode) {
			case (int)WrapMode.Default:
			case (int)WrapMode.Once:
				t2 = t1 / _time;
				break;
			case (int)WrapMode.Loop:
				t2 = (t1 % _time)/_time;
				break;
			case (int)WrapMode.PingPong:
				t2 = Mathf.PingPong (t1, _time)/_time;
				break;
			}
			return t2; 
		}
	}

}
