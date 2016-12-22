using UnityEngine;
using System.Collections;

public class ABLoader : MonoBehaviour 
{
	public GameObject m_Load;
	public GameObject m_Realse;
	public ArrayList m_abList;

	// Use this for initialization
	void Start () 
	{
		m_abList = new ArrayList ();
		UIEventListener.Get(m_Load).onClick = load;
		UIEventListener.Get(m_Realse).onClick = relese;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void load(GameObject obj)
	{
//		WWW www = new WWW ("file://D:\\pic1.jpg");
		WWW www = new WWW ("file://E:\\WorkSpace_Unity\\HelloWorldUnity\\trunk\\FirstDemo\\New Resource.assetbundle");
		AssetBundle ab = www.assetBundle;
		string[] ablist = ab.GetAllAssetNames ();

		for (int i = 0; i < ablist.Length; i++) 
		{
			Debug.Log (ablist[i]);
		}
		m_abList.Add (ab);

		Object[] abOgj = ab.LoadAllAssets ();
	}

	void relese(GameObject obj)
	{
		AssetBundle ab = (AssetBundle)m_abList [0];
		ab.Unload (true);
	}
}
