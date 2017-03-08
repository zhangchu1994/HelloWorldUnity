using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessWord : MonoBehaviour 
{

//	public List<GameObject> m_Panes;

	public GameObject[] objs;
	private Camera cam;
	private Plane[] planes;
	int LogCount = 0;

	bool cacheInit;
	int m_ZoneCount;
	public Text m_Text;

	// Use this for initialization
	void Start () 
	{
		objs = GameObject.FindGameObjectsWithTag ("Ground");
		cam = Camera.main;
		cam.transform.position = new Vector3 (210,30,193);

//		cam.cameraToWorldMatrix
//		GameObject obj = objs [0];
//		List<string> names = getBorder (obj);
//
//		for (int i = 0; i < names.Count; i++) 
//		{
//			string str = names [i];
//			Debug.Log (" i = "+i+" name = "+str);
//		}
	}

//	void OnDrawGizmosSelected() {
//		Matrix4x4 m = cam.cameraToWorldMatrix;
//		Vector3 p = m.MultiplyPoint(new Vector3(0, 0, 10));
//		Gizmos.color = Color.yellow;
//		Gizmos.DrawSphere(p, 0.2F);
//	}


	
	// Update is called once per frame
	void Update () 
	{
//		LogCount++;
//		if (LogCount > 1)
//			return;
//		Matrix4x4 m = cam.cameraToWorldMatrix;
//		Vector3 p = m.MultiplyPoint(new Vector3(0, 0, 0));
//		Debug.Log ("p x = "+p.x+" p.y = "+p.y + " p.z = "+p.z);

//		check0 ();

//		
//		70显示8列
		objs = GameObject.FindGameObjectsWithTag ("Ground");
		int width = 10;
		int height = 10;
		int offetX = (int)(cam.transform.position.x - 30)/width;
		int offetZ = (int)(cam.transform.position.z - 13)/height;
		int offetX1 = (int)(cam.transform.position.x - 30);
		int offetZ1 = (int)(cam.transform.position.z - 13);

		m_Text.text = "X = " + offetX1 + " Y = " + offetZ1;
		for (int i = 0; i < 8; i++) 
		{
			int newX = i + offetX;
			for (int j = 0; j < 5; j++) 
			{
				int newY = j + offetZ;
				string name = "X" + newX.ToString () + "Z" + newY.ToString();
				if (IsExist (name) == false) 
				{
					Object res = Resources.Load ("DPanel");
					GameObject ActorObject = GameObject.Instantiate (res) as GameObject;
					ActorObject.name = name;
					ActorObject.tag = "Ground";
					ActorObject.transform.position = new Vector3 (newX * 10, 0, newY*10);
					TextMesh text = ActorObject.GetComponentInChildren<TextMesh> ();
					text.text = name;
				}

			}
		}
	}

	void check0()
	{
		
	}

	void check()
	{
		objs = GameObject.FindGameObjectsWithTag ("Ground");
		planes = GeometryUtility.CalculateFrustumPlanes(cam);
		for (int i = 0; i < objs.Length; i++) 
		{
			GameObject anObject = objs [i];
			if (GeometryUtility.TestPlanesAABB (planes, anObject.GetComponent<Collider> ().bounds)) 
			{
				List<string> nameList = getBorder (anObject);
//				for (int j = 0; j < nameList.Count; i++) 
//				{
//					string name = nameList [j];
//					Debug.Log (name);
//					bool isExist = IsExist (name);
//					if (isExist == false) 
//					{
//						Object res = Resources.Load ("DPanel");
//						GameObject ActorObject = GameObject.Instantiate (res) as GameObject;
//						ActorObject.name = name;
//						ActorObject.tag = "Ground";
//						TextMesh text = ActorObject.GetComponentInChildren<TextMesh> ();
//						text.text = name;
//						int x = 0;
//						int z = 0;
//						getXZNum (name,ref x,ref z);
//						ActorObject.transform.position = new Vector3 (x * 10, 0, z * 10);
//						check();
//						break;
//					}
//				}
				Debug.Log(anObject.name + " has been detected!");
			}
			else
				Debug.Log("Nothing has been detected = " + anObject.name);	
		}
//		Debug.Log("_________________________________________");	
	}


	void getXZNum(string name,ref int x,ref int z)
	{
		string[] names=name.Split('Z');
		x = int.Parse(names[0].Replace ("X", ""));
		z = int.Parse(names[1]);
	}


	List<string> getBorder(GameObject obj)
	{
		string name = obj.name;
		int x = 0;
		int z = 0;
		getXZNum (obj.name, ref x, ref z);
		List<string> strList = new List<string>();

		int[] zList = { -1, 0, 1 };
		int[] xList = { -1, 0, 1 };
		int index = 0;
		for (int i = 0; i < zList.Length; i++) 
		{
			int zTemp = zList [i];
			for (int j = 0; j < xList.Length; j++) 
			{
				int xTemp = xList [j];
				int newX = x+xTemp;
				int newZ = z+zTemp;
				if (x != newX || z != newZ) 
					strList.Add("X"+newX.ToString()+"Z"+newZ.ToString());
				index++;
			}
		}
		return strList;
	}

	bool IsExist(string name)
	{
		bool isExist = false;
		for (int i = 0; i < objs.Length; i++) 
		{
			GameObject anObject = objs [i];
			if (anObject.name == name)
				return true;
		}
		return isExist;
	}

	/*
	public void checkCache(int x, int y)
	{

		//      //////////////////
		//      1-4,1-3,1-2,1-1
		//      2-4,2-3,2-2,2-1 
		//      3-4,3-3,3-2,3-1
		//      4-4,4-3,4-2,4-1 
		//      ////////////////// 2,2
		cacheNewTemp.Clear();
		cacheNew.Clear ();
		int leftx = x;
		int lefty = y+1;
		int leftUpx = x-1;
		int leftUpy = y+1;
		int leftButtomx = x+1;
		int leftButtomy = y+1;
		int topx =  x-1;
		int topy =  y;
		int buttomx = x+1;
		int buttomy = y;
		int rightx = x;
		int righty = y-1;
		int rightUpx = x-1;
		int rightUpy = y-1;
		int rightButtomx = x+1;
		int rightButtomy = y-1;
		if (cacheInit) {

			if(leftx>=0&&lefty>=0  && leftx<m_ZoneCount && lefty<m_ZoneCount){
				getCahce(leftx,lefty);
			}
			if(leftUpx>=0&&leftUpy>=0  && leftUpx<m_ZoneCount && leftUpy<m_ZoneCount){
				getCahce(leftUpx,leftUpy);
			}
			if(leftButtomx>=0&&leftButtomy>=0 && leftButtomx<m_ZoneCount && leftButtomy<m_ZoneCount){
				getCahce(leftButtomx,leftButtomy);
			}
			if(topx>=0&&topy>=0 && topx<m_ZoneCount && topy<m_ZoneCount){
				getCahce(topx,topy);
			}
			if(buttomx>=0&&buttomy>=0  && buttomx<m_ZoneCount && buttomy<m_ZoneCount){
				getCahce(buttomx,buttomy);
			}
			if(rightx>=0&&righty>=0 && rightx<m_ZoneCount && righty<m_ZoneCount){
				getCahce(rightx,righty);
			}
			if(rightUpx>=0&&rightUpy>=0 && rightUpx<m_ZoneCount && rightUpy<m_ZoneCount){
				getCahce(rightUpx,rightUpy);
			}
			if(rightButtomx>=0&&rightButtomy>=0  && rightButtomx<m_ZoneCount && rightButtomy<m_ZoneCount){
				getCahce(rightButtomx,rightButtomy);
			}
			string key = x+","+y;
			cacheNewTemp.Add(key,key);

			getRemoveCahce();

		} else {
			if(leftx>=0&&lefty>=0 && leftx<m_ZoneCount && lefty<m_ZoneCount){
				addCache(leftx,lefty);
			}
			if(leftUpx>=0&&leftUpy>=0 && leftUpx<m_ZoneCount && leftUpy<m_ZoneCount){
				addCache(leftUpx,leftUpy);
			}
			if(leftButtomx>=0&&leftButtomy>=0 && leftButtomx<m_ZoneCount && leftButtomy<m_ZoneCount){
				addCache(leftButtomx,leftButtomy);
			}
			if(topx>=0&&topy>=0 && topx<m_ZoneCount && topy<m_ZoneCount){
				addCache(topx,topy);
			}
			if(buttomx>=0&&buttomy>=0 && buttomx<m_ZoneCount && buttomy<m_ZoneCount){
				addCache(buttomx,buttomy);
			}
			if(rightx>=0&&righty>=0 && rightx<m_ZoneCount && righty<m_ZoneCount){
				addCache(rightx,righty);
			}
			if(rightUpx>=0&&rightUpy>=0 && rightUpx<m_ZoneCount && rightUpy<m_ZoneCount ){
				addCache(rightUpx,rightUpy);
			}
			if(rightButtomx>=0&&rightButtomy>=0 && rightButtomx<m_ZoneCount && rightButtomy<m_ZoneCount){
				addCache(rightButtomx,rightButtomy);
			}
			string key = x+","+y;
			cache.Add(key,key);
			cacheInit =true;
		}

		getLoadNewCahce ();
	}

	public void getRemoveCahce(){
		Hashtable rtmp = new Hashtable(); 
		IDictionaryEnumerator enumerator = cache.GetEnumerator(); 
		while (enumerator.MoveNext()) 
		{          
			Debug.Log("cache key = "+enumerator.Key.ToString());
			if(!cacheNewTemp.Contains (enumerator.Key.ToString())){
				rtmp.Add(enumerator.Key.ToString(),enumerator.Key.ToString());
			} 
		} 
		IDictionaryEnumerator enumerator2 = rtmp.GetEnumerator(); 
		while (enumerator2.MoveNext()) 
		{ 
			Debug.Log("del cache key = "+enumerator2.Key.ToString());
			if(cache.Contains (enumerator2.Key.ToString())){
				cache.Remove(enumerator2.Key.ToString());
				string[] s=enumerator2.Key.ToString().Split(',');
				StartCoroutine(UnloadZone(int.Parse(s[0]), int.Parse(s[1])));
			}      
		} 
		rtmp.Clear ();
		rtmp = null;
	}
	public void getLoadNewCahce(){
		IDictionaryEnumerator enumerator = cacheNew.GetEnumerator(); 
		while (enumerator.MoveNext()) 
		{ 
			string[] s=enumerator.Key.ToString().Split(',');
			StartCoroutine(LoadZone(int.Parse(s[0]), int.Parse(s[1])));
		}
	}
	public void getCahce(int x, int y){
		string key = x+","+y;
		cacheNewTemp.Add(key, key);
		if (!cache.Contains (key)) {
			cache.Add(key,key);
			cacheNew.Add(key,key);         
		}  
	}
	public void addCache(int x, int y){
		string key = x+","+y;
		cache.Add(key,key);
		StartCoroutine(LoadZone(x, y));
	}
	public void removeCache(int x, int y){
		string key = x+","+y;     
		StartCoroutine(UnloadZone(x, y));
	}
	*/
}
