using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class DebugLineData{
	public List<Vector3> point = new List<Vector3>();
}
public class DebugLine :MonoBehaviour{
	public static DebugLine Instance;
	public List<DebugLineData> _data = new List<DebugLineData> ();
	public List<Vector3> _autoLine = new List<Vector3> ();
	public bool AutoDraw;
	private Vector3 lastPos;
	public void Awake(){
		Instance = this;
	}
	public void Add(Vector3 p1,Vector3 p2){
		DebugLineData data = new DebugLineData ();
		data.point.Add (p1);
		data.point.Add (p2);
		_data.Add (data);
	}
	public void AddLine(Vector3 pos){
		_autoLine.Add (transform.position);
	}
	public void Update(){
		if (AutoDraw) {
			if(lastPos != transform.position)
			_autoLine.Add (transform.position);
			lastPos = transform.position;
		}
	}
	void OnDrawGizmos(){
		for (int i = 0; i < _data.Count; i++) {
			for (int x = 0; x < _data [i].point.Count-1; x++) {
				Gizmos.DrawLine (_data [i].point [x], _data [i].point [x + 1]);
			} 
			Gizmos.color = Color.green;
			Gizmos.DrawSphere (_data [i].point[0], 0.1f);
			Gizmos.color = Color.red;
			Gizmos.DrawSphere (_data [i].point[_data [i].point.Count - 1], 0.1f);
		}
		for (int i = 0; i < _autoLine.Count - 1; i++) {
			Gizmos.DrawLine (_autoLine[i],_autoLine[i+1]);
		}
	}
}


