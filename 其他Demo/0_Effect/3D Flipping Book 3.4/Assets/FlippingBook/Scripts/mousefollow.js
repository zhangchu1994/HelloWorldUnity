var depth = 10.0; 

function Start () 
{ 
 //    Screen.showCursor = false; 
} 

function Update () 
{ 
	if(GameObject.FindWithTag("cam3d").GetComponent.<Camera>().enabled == false){
     var mousePos = Input.mousePosition; 
     var wantedPos = Camera.main.ScreenToWorldPoint (Vector3 (mousePos.x, mousePos.y, depth)); 
     transform.position = wantedPos; 
	}
}