using UnityEngine;
using System.Collections;

public class lightcontrol : MonoBehaviour {


public Light Lights;
public GameObject bookcaseholder;
	// Use this for initialization
	
	
	
	void Awake(){
	bookcaseholder =GameObject.Find("BookChooseScript");
	
	}
	
	void Start () {


	
	
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(bookcaseholder.GetComponent<Bookstore>().daylightson==true){
	Lights.enabled=false;
	
	}
	if(bookcaseholder.GetComponent<Bookstore>().nightallbooklighton==true){
	Lights.enabled=true;
	
	}
		if(bookcaseholder.GetComponent<Bookstore>().nighteachbooklighton==true){
		if(this.name ==bookcaseholder.GetComponent<Bookstore>().booknum.ToString()){
		Lights.enabled=true;
		}
		else{
		Lights.enabled=false;
		
		}
		
		}
	
	
	}
	
	
	 
}
