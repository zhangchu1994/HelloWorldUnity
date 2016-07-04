using UnityEngine;
using System.Collections; 
using System.Collections.Generic;
using System.Xml; 
using System.IO;
using System;


public class TouchInfo
{
	public Vector2 touchPosition;
	public bool swipeComplete;
	public float timeSwipeStarted;
}


public class BookEdit : MonoBehaviour {
//Do not remove or change pages
public GameObject[] pages;
//number of book pages. Either set manually or will load dynamically depending on xml parameter selection (see bellow).Must be even. 
public int pagesnumber = 20;
//The array that the book pages are loaded
//Can be loaded manually from editor or dynamicaly from Web or Resources folder . Must be even. 
public Texture2D[] pagestextures; 
//Speed of page turning
public float timepagesturn = 0.2f;
//The current page of the book at runtime
public int currentpage = 0;
//Disables gui button during page turn
public bool nextbutton = false;
//Disables back button if current page =0
public bool backbutton = false;
//If set to true the pages are loaded from XML file Local or web 
public bool xml = false;
//Set Localxml to true if you want to read xml file from resources foldes. The name of the xml file must be images1.xml
public bool Localxml = false;
//Set to XMLURL the url of the XML if LocalXml = false to read from the Web. Cross Domain policy needs to be valid. 
public string XMLURL;
//The Xml File Name of the local XML
public string localXMLFile;
//Enable/Disable magnifier
public bool camzoom1 = false;
//Page number that the user jumps to
public string   gotopage;
//Current button click on runtime
public int toolbarInt = -1;
//Button names
public string[] toolbarStrings = new string[] {"", "", ""};
public List<Texture2D> img_list = new List<Texture2D>();	
//Determines if the size of the book will be dynamic
public bool EnableBookSize = false;

////////chapters

public string[] chapters;
public int[] chapterPages;
private bool chapterson=false;
public bool ChaptersFromXML=false;
//Saves and retrieves the current page of the user
public bool EnableCurrentPage = false;
public bool EnableBookstore = false;
public bool EnableChapters = false;
public bool EnableTopMenu = false;
public bool EnableDownloadIcon= false;
////////ios enable booleans
public bool portraitenable = false;
public bool iosenabled = false;
public bool iostouchenabled = false;
/////ios swipe
public int swipeLength;
public int swipeVariance;
public float timeToSwipe;
private GUIText swipeText;
private TouchInfo[] touchInfoArray;
private int activeTouch = -1;
///////skin
public GUISkin skin1;

///
public bool guimovement;
bool movedownmenuup;
bool moveupmenuup;
float downtoolbalheight;
float uptoolbalheight;
string booktitle;
bool searchopen;
bool enableinvisiblebuttons;
public Texture backsearchimage;
public Texture downloadicon;
bool downloadiconbool;

 bool lockuppermenubool;
bool lockdownmenubool;
Texture lockmenu;
Texture uplockmenu;
void Awake(){


booktitle = PlayerPrefs.GetString("BookName");

if (EnableBookSize){
GameObject.Find("Book").transform.localScale = new Vector3(float.Parse(PlayerPrefs.GetString("BookX")),1f,float.Parse(PlayerPrefs.GetString("BookY"))); 
 GameObject.FindWithTag("portrait").transform.localScale = new Vector3(float.Parse(PlayerPrefs.GetString("BookX")),1f,float.Parse(PlayerPrefs.GetString("BookY"))); 
}
	}

	// Use this for initialization
	void Start () {
		
			GameObject.FindWithTag("plane1").GetComponent<Renderer>().enabled=false;
			GameObject.FindWithTag("plane2").GetComponent<Renderer>().enabled=false;
		
		// startPoint = transform.position;
        // startTime = Time.time;
		//////// screen orientation
		//get a reference to the GUIText component
		swipeText = (GUIText) GetComponent(typeof(GUIText));
		touchInfoArray = new TouchInfo[5];
		
        StartCoroutine(LoadBookPages());
		
        if(xml==false){
		    GameObject.FindWithTag("back1").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage];
		    GameObject.FindWithTag("portrait2").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage];
		}
		    GameObject.FindWithTag("in2").GetComponent<Renderer>().enabled = false;
    	    for (int i = 0; i <= 22; i++){
		    pages[i].GetComponent<Renderer>().enabled = false;
			                             }
    	}
	
	// Update is called once per frame	
	void LateUpdate () {

				
				
				

			if(portraitenable==true){
				if(currentpage!=0){
					GameObject.FindWithTag("in2").GetComponent<Renderer>().enabled = true;
					
				//	GameObject.FindWithTag("back1").renderer.material.mainTexture = pagestextures[currentpage+1];
					
				//GameObject.FindWithTag("in2").renderer.materials[1].mainTexture = pagestextures[currentpage];
					
				}
		}
		
	if(currentpage!=0){	
		
		
 GameObject.FindWithTag("portrait2").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage-1];
	}
	else{
			if(currentpage>0){
		 GameObject.FindWithTag("portrait2").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage];
			}
		
		}


/*
if(iosenabled==true){

	if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft) && (iPhoneSettings.screenOrientation != iPhoneScreenOrientation.LandscapeLeft))
	{ 
		iPhoneSettings.screenOrientation = iPhoneScreenOrientation.LandscapeLeft;
		//StartCoroutine(changepagelandscape());
		GameObject.FindWithTag("MainCamera").camera.enabled = false;
		GameObject.FindWithTag("portraitcam").camera.enabled = false;
		GameObject.FindWithTag("cam3d").camera.enabled = true;
		camzoom1 = false;
		portraitenable = false;
		if (currentpage%2==1) currentpage-=1;
	}
	if ((Input.deviceOrientation == DeviceOrientation.LandscapeRight) && (iPhoneSettings.screenOrientation != iPhoneScreenOrientation.LandscapeRight))
	{ 
		
		iPhoneSettings.screenOrientation = iPhoneScreenOrientation.LandscapeRight; 
		//StartCoroutine(changepagelandscape());
//	GameObject.FindWithTag("camobjects").transform.position = new Vector3(42.99366f,23.80605f,-6.408535f);
	GameObject.FindWithTag("MainCamera").camera.enabled = false;
	GameObject.FindWithTag("portraitcam").camera.enabled = false;
	GameObject.FindWithTag("cam3d").camera.enabled = true;
	camzoom1 = false;
	portraitenable = false;
	if (currentpage%2==1) currentpage-=1;
	}
	
	if ((Input.deviceOrientation == DeviceOrientation.Portrait) && (iPhoneSettings.screenOrientation != iPhoneScreenOrientation.Portrait))
	{ 
		iPhoneSettings.screenOrientation = iPhoneScreenOrientation.Portrait;
		GameObject.FindWithTag("MainCamera").camera.enabled = false;
			GameObject.FindWithTag("portraitcam").camera.enabled = true;
			GameObject.FindWithTag("cam3d").camera.enabled = false;
			camzoom1 = false;
			portraitenable = true;
	//GameObject.FindWithTag("MainCamera").camera.orthographicSize = 1.3f;
	//	GameObject.FindWithTag("MainCamera").camera.enabled = true;
	
	}
	
	
}	*/
	
        //if it is the first page of the book disable the back button
				if(currentpage==0){
	            backbutton = false;
				}
				else
				{
				backbutton = true;
				}
			//touch count is a bit dodgy at the moment so add the extra check to see if there are no more than 5 touches
				
		if(iosenabled ==true && iostouchenabled ==true && portraitenable ==false){
		if(Input.touchCount > 0 && Input.touchCount < 6)
		{
			foreach(Touch touch in Input.touches)
			{
				if(touchInfoArray[touch.fingerId] == null)
						touchInfoArray[touch.fingerId] = new TouchInfo();
						
				if(touch.phase == TouchPhase.Began)
				{
					touchInfoArray[touch.fingerId].touchPosition = touch.position;
					touchInfoArray[touch.fingerId].timeSwipeStarted = Time.time;
				}
				//check if withing swipe variance		
				if(touch.position.y > (touchInfoArray[touch.fingerId].touchPosition.y + swipeVariance))
				{
					touchInfoArray[touch.fingerId].touchPosition = touch.position;
				}
				if(touch.position.y < (touchInfoArray[touch.fingerId].touchPosition.y - swipeVariance))
				{
					touchInfoArray[touch.fingerId].touchPosition = touch.position;
				}
				//swipe right
				if((touch.position.x > touchInfoArray[touch.fingerId].touchPosition.x + swipeLength) && !touchInfoArray[touch.fingerId].swipeComplete 
					&& activeTouch == -1) 
				{
					
					if(GameObject.FindWithTag("camzoom").GetComponent<Camera>().enabled==false)
					{
						
						if(portraitenable==false){
					
						
					Reset(touch);
					StartCoroutine(rightpage());
					//StartCoroutine(portraitnextpage());//
						}
						
						
						else {
							
							StartCoroutine(portraitbackpage());
							
							}
						
						
					}
					
				}
				//swipe left
				if((touch.position.x < touchInfoArray[touch.fingerId].touchPosition.x - swipeLength) && !touchInfoArray[touch.fingerId].swipeComplete 
					&& activeTouch == -1)
				{
					
					
					if(GameObject.FindWithTag("camzoom").GetComponent<Camera>().enabled==false){
						
						
							if(portraitenable==false){
					Reset(touch);
					StartCoroutine(leftpage());
					//StartCoroutine(portraitnextpage());//
						}
						
							else {
							
							StartCoroutine(portraitnextpage());
							
							}
						
						
					}
						
				}
				//when the touch has ended we can start accepting swipes again
				if(touch.fingerId == activeTouch && touch.phase == TouchPhase.Ended)
				{
					//Debug.Log("Ending " + touch.fingerId);
					//if more than one finger has swiped then reset the other fingers so
					//you do not get a double/triple etc. swipe
					foreach(Touch touchReset in Input.touches)
					{
						touchInfoArray[touch.fingerId].touchPosition = touchReset.position;	
					}
					touchInfoArray[touch.fingerId].swipeComplete = false;
					activeTouch = -1;
				}
			}
			}			
		}	
		
				
	}

	void SwipeComplete(string messageToShow, Touch touch)
	{
		//Debug.Log(Time.time - touchInfoArray[touch.fingerId].timeSwipeStarted);
		Reset(touch);
		if(timeToSwipe == 0.0f || (timeToSwipe > 0.0f && (Time.time - touchInfoArray[touch.fingerId].timeSwipeStarted) <= timeToSwipe))
		{
			swipeText.text = messageToShow;
			//Do something here
		}
	}
	
	void Reset(Touch touch)
	{
		activeTouch = touch.fingerId;
		touchInfoArray[touch.fingerId].swipeComplete = true;		
	}
    
	IEnumerator LoadBookPages() 
{
	downloadiconbool=true;
	//yield return new WaitForSeconds(0);
	
    //reads book pages from XML file (local or Web)
	if(xml==true){
	    	Array.Clear(pagestextures, 0, pagestextures.Length);
            Array.Resize(ref pagestextures, pagestextures.Length -pagesnumber);
		    pagesnumber = 0;
		    string URLString3  = "";
			
           int i =0;
	
		

		if (ChaptersFromXML==true)
			{
				Array.Clear(chapters, 0, chapters.Length);
				Array.Clear(chapterPages, 0, chapterPages.Length);
			}




		
        if(Localxml==true){
            //local XML File
				
            
            if (EnableBookstore)
            {
                URLString3 = "file://" + Application.dataPath + "/FlippingBook/Resources/" + PlayerPrefs.GetString("BookXML");
            }
            else
            {
                URLString3 = "file://" + Application.dataPath + "/FlippingBook/Resources/" + localXMLFile;
            }
				//need to bypass xml reading just do it manually for ios devices
				//loop thru the list
				Array.Resize(ref pagestextures,1);
				for(int count=0;count<img_list.Count;count++){
				Texture2D tex_local = new Texture2D(128, 128, TextureFormat.RGB24, false); 
	     //   w.LoadImageIntoTexture(tex_local);
	        pagestextures[i]=tex_local;
            //i++;
					//print(img_list.Count);
					
					pagestextures[count]=img_list[count];
				yield return img_list[i]; 
            pagesnumber++;
		    				
				
				}
				
				if(currentpage==0){
                GameObject.FindWithTag("back1").GetComponent<Renderer>().material.mainTexture = pagestextures[0];
		                      }

				
			}
			else
			{
            //Xml file from URL 
            if (EnableBookstore)
            {
               URLString3 = PlayerPrefs.GetString("BookXML");
				
            }
            else
            {
                URLString3 = "" + XMLURL + "";
            }


                 
			}	
			
            WWW ww3 = new WWW(URLString3); 
			Debug.Log(URLString3);
		    yield return ww3; 
            string XmlString = ww3.data; 
            XmlDocument XmlData = new XmlDocument(); 
            XmlData.LoadXml(XmlString);
            XmlElement root3 = XmlData.DocumentElement; 
         
            //resizes array of images to the XML page Nodes
            Array.Resize(ref pagestextures, pagestextures.Length + root3.ChildNodes.Count);

	
		
			//this is to redim chapter and chapterPages ARRAYS
			int records=0;
					
            foreach (XmlNode thisnode in root3.ChildNodes) { 
						
            WWW w = null;
				
		    if(Localxml==true){
                //Loads Images from resources folder
                w = new WWW("file://" + Application.dataPath+"/FlippingBook/Resources/"+ thisnode.Attributes["name"].Value);
                    		}
		    else{
                //Loads Images from the Web URL stated in the Xml name attribute
			w = new WWW(thisnode.Attributes["name"].Value);
			                }
			yield return w; 
		    Texture2D tex = new Texture2D(4,4); 
	        w.LoadImageIntoTexture(tex);
	        pagestextures[i]=tex;
		
			//Code for chapters
            if (ChaptersFromXML)
            {
                if (thisnode.Attributes["newchapter"].Value == 1.ToString())
                {
                    Array.Resize(ref chapters, records + 1);
                    Array.Resize(ref chapterPages, records + 1);
                    chapters[records] = thisnode.Attributes["chaptername"].Value;
                    chapterPages[records] = i;
                    records += 1;
                }
            }
				
            i++;
            pagesnumber++;
		    if(currentpage==0){
                GameObject.FindWithTag("back1").GetComponent<Renderer>().material.mainTexture = pagestextures[0];
                 GameObject.FindWithTag("portrait2").GetComponent<Renderer>().material.mainTexture = pagestextures[0];
		                      }
	        }
downloadiconbool=false;
}


        if (EnableCurrentPage){ FindAndGoCurrentPagePlayerPrefs();}
		
        //if (EnableCurrentPage) StartCoroutine(FindAndGoCurrentPagePlayerPrefs());

}

////////////////////Next page in portrait mode

IEnumerator portraitnextpage(){
	
	    yield return new WaitForSeconds(0);
		GameObject.FindWithTag("portrait2").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage];
		GameObject.FindWithTag("back1").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage];
		GameObject.FindWithTag("in2").GetComponent<Renderer>().materials[1].mainTexture = pagestextures[currentpage+1];
 currentpage +=1;
	}
	
////////////////////Previous  page in portrait mode
IEnumerator portraitbackpage(){
	    yield return new WaitForSeconds(0);
		GameObject.FindWithTag("portrait2").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage-1];
		GameObject.FindWithTag("back1").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage];
		GameObject.FindWithTag("in2").GetComponent<Renderer>().materials[1].mainTexture = pagestextures[currentpage+1];
 currentpage -=1;
		
	}

	/////////////////////////////////
    //Previous page Generator
	IEnumerator leftpage	(){
	    yield return new WaitForSeconds(0);
        
        for (int i = 0; i <= 22; i++){
				yield return new WaitForSeconds(timepagesturn);
				if(currentpage+2==pagesnumber){
			    GameObject.FindWithTag("back1").GetComponent<Renderer>().enabled = false;
		        }
                else
			    {
				GameObject.FindWithTag("back1").GetComponent<Renderer>().enabled = true;
				GameObject.FindWithTag("back1").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage+2];
				}
				
    			pages[i].GetComponent<Renderer>().materials[0].mainTexture =  pagestextures[currentpage];
				pages[i].GetComponent<Renderer>().materials[1].mainTexture =  pagestextures[currentpage+1];
                pages[i].GetComponent<Renderer>().enabled = true;
				if(i>=1){
			    pages[i-1].GetComponent<Renderer>().enabled = false;
				        }
			                        }
			currentpage +=2;
			
        //saves the cuurent page    
             
            if (EnableCurrentPage) SaveCurrentPagePlayerPrefs(currentpage);
            //if (EnableCurrentPage) StartCoroutine(SaveCurrentPagePlayerPrefs(currentpage));

			}
    
    //Next page Generator
   
    
    IEnumerator rightpage()
    {
        yield return new WaitForSeconds(0);
        for (int i = 22; i >= 0; i--)
        {
            yield return new WaitForSeconds(timepagesturn);
            if (currentpage - 2 <= 0)
            {
                GameObject.FindWithTag("in2").GetComponent<Renderer>().enabled = false;
            }
            else
            {
                GameObject.FindWithTag("in2").GetComponent<Renderer>().enabled = true;
                if (currentpage == pagesnumber)
                {
                    GameObject.FindWithTag("in2").GetComponent<Renderer>().materials[1].mainTexture = pagestextures[currentpage - 3];
                }
                else
                {
                    GameObject.FindWithTag("in2").GetComponent<Renderer>().materials[1].mainTexture = pagestextures[currentpage - 3];
                }
            }


           
            if (currentpage > 1)
            {
                pages[i].GetComponent<Renderer>().materials[0].mainTexture = pagestextures[currentpage - 2];
                pages[i].GetComponent<Renderer>().materials[1].mainTexture = pagestextures[currentpage - 1];
            }
            else
            {
                
                pages[i].GetComponent<Renderer>().materials[0].mainTexture = pagestextures[currentpage];
              
            }
           
        
            pages[i].GetComponent<Renderer>().enabled = true;
            if (i < 22)
            {
                pages[i + 1].GetComponent<Renderer>().enabled = false;
            }
        }


       
        if (currentpage > 1)
        {
            currentpage -= 2;
        }
                if (EnableCurrentPage) SaveCurrentPagePlayerPrefs(currentpage);
           }
							
    IEnumerator SaveCurrentPage(int currentbookpage)
{
    
    string URLString3 = "file://" + Application.dataPath + "/FlippingBook/Resources/settings.xml";
    WWW ww3 = new WWW(URLString3);
    yield return ww3;
    string XmlString = ww3.text;
    ww3.Dispose();
    XmlDocument XmlData = new XmlDocument();
    XmlData.LoadXml(XmlString);
    XmlElement root3 = XmlData.DocumentElement;
    foreach (XmlNode thisnode in root3.ChildNodes)
    {
    if (thisnode.Attributes["name"].Value == "currentPage")
        {
            thisnode.Attributes["Value"].Value = currentbookpage.ToString(); 
         }
    }
    XmlTextWriter writer = new XmlTextWriter(Application.dataPath + "/FlippingBook/Resources/settings.xml", null);
    writer.Formatting = Formatting.Indented;
    XmlData.Save(writer);
    writer.Close();
        }

    void SaveCurrentPagePlayerPrefs(int currentbookpage)
    {
        if (EnableBookstore)
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("BookName") + "currentpage", currentbookpage);
        }
        else
        {
            PlayerPrefs.SetInt("currentpage", currentbookpage); 
        }
	}

    IEnumerator FindAndGoCurrentPage()
    {
        string URLString3 = "file://" + Application.dataPath + "/FlippingBook/Resources/settings.xml";
        WWW ww3 = new WWW(URLString3);
        yield return ww3;
        string XmlString = ww3.text;
        ww3.Dispose();
        XmlDocument XmlData = new XmlDocument();
        XmlData.LoadXml(XmlString);
        XmlElement root3 = XmlData.DocumentElement;
        foreach (XmlNode thisnode in root3.ChildNodes)
        {
            if (thisnode.Attributes["name"].Value == "currentPage")
            {
                currentpage = System.Convert.ToInt16(thisnode.Attributes["Value"].Value);
            }
        }

        if (currentpage > 0)
        {
            currentpage = currentpage - 2;
            chapterson = false;
            StartCoroutine(leftpage());
            StartCoroutine(buttontime());
        }    
           }

    void FindAndGoCurrentPagePlayerPrefs()
    {
        if (EnableBookstore)
        {
            currentpage = PlayerPrefs.GetInt(PlayerPrefs.GetString("BookName") + "currentpage");
        }
        else
        {
            currentpage = PlayerPrefs.GetInt("currentpage");
        }
        
        
        if (currentpage > 0)
        {
            currentpage = currentpage - 2;
            chapterson = false;
            StartCoroutine(leftpage());
            StartCoroutine(buttontime());
        }
    }

    IEnumerator changepagelandscape(){
    	
		yield return new WaitForSeconds(0);
       
				if(System.Convert.ToInt32(currentpage) % 2 ==0){
					
					GameObject.FindWithTag("back1").GetComponent<Renderer>().material.mainTexture = pagestextures[currentpage];
					GameObject.FindWithTag("in2").GetComponent<Renderer>().materials[1].mainTexture = pagestextures[currentpage+1];
				}
						
						
					
					if(System.Convert.ToInt32(currentpage) % 2 ==1){
				GameObject.FindWithTag("in2").GetComponent<Renderer>().materials[1].mainTexture = pagestextures[currentpage];
				GameObject.FindWithTag("back1").GetComponent<Renderer>().materials[1].mainTexture = pagestextures[currentpage-1];
					}
		
			
			    }
	
    
	//Disables Next and previous button until page turn	
	IEnumerator buttontime(){
				nextbutton=false;
				yield return new WaitForSeconds(1.5f);
				nextbutton=true;
				}
        
	void OnGUI(){
		GUI.skin = skin1;

        	
			
		////////////////////////VERTICAL GUI	
		if(portraitenable){
		
		
	
			if(GUI.Button(new Rect(-10,800,80,50),"Back")){
				
					StartCoroutine(portraitbackpage());	
		
			}
			
				if(GUI.Button(new Rect(Screen.width-80,800,80,50),"Next")){
				
					StartCoroutine(portraitnextpage());	
		
			}
							
		}
			
		
	

//invisible buttons	
if(enableinvisiblebuttons){
if(GUI.Button(new Rect(0,40,Screen.width/2,Screen.height-90),"","invisiblebuttons")){
					if(currentpage<=pagesnumber){
			
				if(nextbutton==true){
				
				StartCoroutine(rightpage());
				StartCoroutine(buttontime());
		       
			                    }
		                            }
			}
if(GUI.Button(new Rect(Screen.width/2,40,Screen.width/2,Screen.height-90),"","invisiblebuttons")){
			if(nextbutton==true){
				
				StartCoroutine(leftpage());
				StartCoroutine(buttontime());
			                      }
			
			}
			
			
			}
			
			
			
					if(EnableTopMenu){
		////////////////////////////////////////////////////////////CHAPTER GUI
  			 for (int i = 0; i < chapters.Length; i++)
        {
		//	Debug.Log ("Chaters buttons I "+i);
			
			if(chapterson==true){
			
			if (GUI.Button(new Rect(-10,30+(i*50),Screen.width/2,50),chapters[i])){
				if(currentpage<=chapterPages[i]){ 
					if (chapterPages[i]%2==0) {
					currentpage=chapterPages[i]-2;
					chapterson=false;
					}
					else
					{
						if (chapterPages[i]<3){
						currentpage=0;
						chapterson=false;
						}
						else
						{
						currentpage=chapterPages[i]-3;
						}
						}
						chapterson=false;
				StartCoroutine(leftpage());
				StartCoroutine(buttontime());
					}
				else
				{
					chapterson=false;
						if (chapterPages[i]%2==0) {
					currentpage=chapterPages[i]+2;
					chapterson=false;
					}
					else
					{
						chapterson=false;
						if (chapterPages[i]==0){
						currentpage=0;
						}
						else
						{
						currentpage=chapterPages[i]+1;
						}
						chapterson=false;
												}
					StartCoroutine(rightpage());
				StartCoroutine(buttontime());
				}
			}
				
			}
		
		
        }
		
			
			
			//search function
			if(searchopen){
			enableinvisiblebuttons=false;
			moveupmenuup=true;
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),backimage);
			GUI.DrawTexture(new Rect(Screen.width/2-100,Screen.height/2-40,200,80),backsearchimage);
			
			if(GUI.Button(new Rect(Screen.width/2+30,Screen.height/2-20,50,40),"","searchbutton")){
			enableinvisiblebuttons=true;
			searchopen=false;
			
			if(pagesnumber>=System.Convert.ToInt32(gotopage) && System.Convert.ToInt32(gotopage) >=0)
            {
			if(System.Convert.ToInt32(gotopage) % 2 ==1){
					gotopage = (System.Convert.ToInt32(gotopage) -1).ToString();
					                					}
				
			if(currentpage<System.Convert.ToInt32(gotopage)){
			    currentpage = System.Convert.ToInt32(gotopage)-2;
			    StartCoroutine(leftpage());
			    StartCoroutine(buttontime());
			    gotopage = "0";
			                                                }
			else
			    {
			    currentpage = System.Convert.ToInt32(gotopage)+2;
			    StartCoroutine(rightpage());
			    StartCoroutine(buttontime());
			    gotopage = "0";
			    }
            }
			}
			
			GUI.Label(new Rect(Screen.width/2-75,Screen.height/2-65,150,30),"Go to page");
			
			gotopage = GUI.TextField(new Rect(Screen.width/2-60, Screen.height/2-20, 60,40), gotopage, 25);
			
			}
			else{
			enableinvisiblebuttons=true;
			}
				
				
				
				
				
			////////////////// upper gui
		GUI.Box(new Rect(0,uptoolbalheight,Screen.width+10,40),"");
		GUI.Label(new Rect(100,uptoolbalheight+10,Screen.width-200,40),booktitle + "   " + currentpage+ " / " +   pagesnumber);
		if(EnableDownloadIcon){
		if(downloadiconbool){
		GUI.Label(new Rect(Screen.width-220,uptoolbalheight+10,20,20),downloadicon);
		GUI.Label(new Rect(Screen.width-40,15,20,20),downloadicon);
		}
		}
		
		
		if(EnableChapters==true){
		if(GUI.Button(new Rect(0,uptoolbalheight,100,40),"Chapters")){	
				chapterson=true;
			}
		}
		
        	
			if(GUI.Button(new Rect(Screen.width-120,uptoolbalheight,130,40),"Back to library ")){
			  Application.LoadLevel("ExampleBookstore");
		
			}
			
			lockuppermenubool=GUI.Toggle(new Rect(120,uptoolbalheight,30,30),lockuppermenubool,"");
			
			
			
			
			if(GUI.Button(new Rect(Screen.width-180,uptoolbalheight,50,40),searchicon)){
			searchopen=!searchopen;
     		}
			
		}
			
if(guimovement==true){
if(lockuppermenubool==false){
if(Input.mousePosition.y> 550)
{
moveupmenuup=true;
if(moveupmenuup){
uptoolbalheight+= Time.deltaTime * 100;
if(uptoolbalheight>=0){
uptoolbalheight=0;
moveupmenuup=false;
}
}
}
else {
uptoolbalheight-= Time.deltaTime * 100;
if(uptoolbalheight<=-35){
moveupmenuup=false;
uptoolbalheight=-35;
}
}
}
else{
uptoolbalheight=0;
}


}



else{
uptoolbalheight=0;
}
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
				
			//////////GUI LANDSCAPPORTRAIT	
			
		
			if(portraitenable==false){
						lockdownmenubool=GUI.Toggle(new Rect(Screen.width-40,Screen.height-downtoolbalheight,30,30),lockdownmenubool,"","togglelock2");

			//toolbarInt = GUI.Toolbar(new Rect(0,Screen.height-50, Screen.width, 50), toolbarInt, toolbarStrings);
			toolbarInt = GUI.Toolbar(new Rect(0,Screen.height-downtoolbalheight, Screen.width-40, 50), toolbarInt, toolbarStrings);
			
//move down gui 			
//Vector2 offset = (Vector2)Input.mousePosition - new Vector2(Screen.width , Screen.height / 3);

if(guimovement==true){
if(lockdownmenubool==false){

if(Input.mousePosition.x <  Screen.width && Input.mousePosition.y< Screen.height / 5)
{

movedownmenuup=true;
if(movedownmenuup){
downtoolbalheight+= Time.deltaTime * 100;
if(downtoolbalheight>=50){
downtoolbalheight=50;
movedownmenuup=false;
}
}
}

else {
downtoolbalheight-= Time.deltaTime * 100;
if(downtoolbalheight<=10){
movedownmenuup=false;
downtoolbalheight=10;
}
}
}
else{
downtoolbalheight=50;
}
}
else{
downtoolbalheight=50;

}




	    switch(toolbarInt){
	
		case 0:
		if(backbutton==true){
		if(currentpage<=pagesnumber){
			
				if(nextbutton==true){
				
				StartCoroutine(rightpage());
				StartCoroutine(buttontime());
		        toolbarInt=-1;
			                    }
		                            }
	                        }
		break;
		case 1:
		if(currentpage<pagesnumber){
			
				if(nextbutton==true){
				
				StartCoroutine(leftpage());
				StartCoroutine(buttontime());
			                      }
                                    }
	    toolbarInt=-1;
	    break;
	    case 2:
		GameObject.FindWithTag("cam3d").GetComponent<Camera>().enabled=false;
		GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled=true;
		GameObject.FindWithTag("camzoom").GetComponent<Camera>().enabled=false;
			GameObject.FindWithTag("plane1").GetComponent<Renderer>().enabled=false;
			GameObject.FindWithTag("plane2").GetComponent<Renderer>().enabled=false;
		toolbarInt=-1;
		break;
		case 3:
			GameObject.FindWithTag("camzoom").GetComponent<Camera>().enabled=false;
			GameObject.FindWithTag("plane1").GetComponent<Renderer>().enabled=false;
			GameObject.FindWithTag("plane2").GetComponent<Renderer>().enabled=false;
			GameObject.FindWithTag("cam3d").GetComponent<Camera>().enabled=true;
		    GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled=false;
			toolbarInt=-1;
			break;
		case 4:
		GameObject.FindWithTag("cam3d").GetComponent<Camera>().enabled=false;
		GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled=true;
		
		if(camzoom1==true){
			GameObject.FindWithTag("camzoom").GetComponent<Camera>().enabled=false;
			GameObject.FindWithTag("plane1").GetComponent<Renderer>().enabled=false;
			GameObject.FindWithTag("plane2").GetComponent<Renderer>().enabled=false;
			toolbarInt=-1;
			camzoom1=false;
		}else{
			GameObject.FindWithTag("camzoom").GetComponent<Camera>().enabled=true;
			GameObject.FindWithTag("plane1").GetComponent<Renderer>().enabled=true;
			GameObject.FindWithTag("plane2").GetComponent<Renderer>().enabled=true;
			toolbarInt=-1;
			camzoom1=true;
			}
			break;
			case 5:
			searchopen=true;
			toolbarInt=-1;
			break;
			
			
		}
			}
			
		
			
			
			
	}
	

public Texture backimage;
public Texture searchicon;

}
