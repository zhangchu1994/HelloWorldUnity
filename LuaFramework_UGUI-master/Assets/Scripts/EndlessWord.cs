using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessWord : MonoBehaviour 
{

	public List<GameObject> m_Panes;

	public GameObject[] objs;
	private Camera cam;
	private Plane[] planes;
	int LogCount = 0;

	bool cacheInit;
	int m_ZoneCount;

	// Use this for initialization
	void Start () 
	{
		objs = GameObject.FindGameObjectsWithTag ("Ground");
		cam = Camera.main;

	}
	
	// Update is called once per frame
	void Update () 
	{
		LogCount++;
		if (LogCount > 1)
			return;
		planes = GeometryUtility.CalculateFrustumPlanes(cam);
		for (int i = 0; i < objs.Length; i++) 
		{
			GameObject anObject = objs [i];
			if (GeometryUtility.TestPlanesAABB (planes, anObject.GetComponent<Collider> ().bounds)) 
			{
//				List<string> nameList = getBorder (anObject);
//				for (int j = 0; j < nameList.Count; i++) 
//				{
//					string name = nameList [j];
//					Debug.Log (name);
////					bool isExist = IsExist (name);
////					if (isExist == false) 
////					{
////						Object res = Resources.Load ("DPanel");
////						GameObject ActorObject = GameObject.Instantiate (res) as GameObject;
////						ActorObject.name = name;
////						TextMesh text = ActorObject.GetComponentInChildren<TextMesh> ();
////						text.text = name;
////					}
//				}

				Debug.Log(anObject.name + " has been detected!");
			}
			else
				Debug.Log("Nothing has been detected = " + anObject.name);	
		}
		Debug.Log("_________________________________________");	
	}



	List<string> getBorder(GameObject obj)
	{
		string name = obj.name;
//		string[] names = name.Split (
		string[] names=name.Split('Z');
		int x = int.Parse(names [0].Replace ("X", ""));
		int z = int.Parse(names[1]);
		List<string> strList = new List<string>();

		int[] zList = { -1, 0, 1 };
		int[] xList = { -1, 0, 1 };
		int index = 0;
		for (int i = 0; i < xList.Length; i++) 
		{
			for (int j = 0; j < zList.Length; j++) 
			{
				int newX = x;
				int newY = z;
				strList.Add("X"+newX.ToString()+"Z"+newY.ToString());
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
