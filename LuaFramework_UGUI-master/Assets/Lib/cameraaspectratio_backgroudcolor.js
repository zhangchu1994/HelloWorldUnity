// CameraAspectRatio
// By Nicolas Varchavsky @ Interatica (www.interatica.com)
// Date: March 27th, 2009
// Version: 1.0

// Attach this script to all the cameras you want to modify the aspect ratio.


// here we setup the targeted aspect ratio for width and height
var targetAspectWidth : float = 16.0;
var targetAspectHeight : float = 9.0;
var disableOnMobilePlatforms : boolean = false;

var executed:boolean = false;

private var previousWidth : float;
private var previousHeight : float;

private var baseRect : Rect;

function updateAspectRatio(sw : float, sh : float) {
    
	
    // let's check if we should modify the height first...
    // calculate the targeted size height
    var th : float = sw * (targetAspectHeight / targetAspectWidth);
	
    // these variables will hold the percentage of height or width we need to
    // apply to the camera.rect property
    // by default, we set them up in 1.0
    var ptw : float = 1.0;
    var pth : float = 1.0;
	
    // these variables will help us adjust the margin to center the screen
    var tx : float = 0.0;
    var ty : float = 0.0;
    var half : float = 0.0;
    var tw : float = 0.0;
	
    // let's try the height...
    // to do this, we check how much the targeted height represents on the screen height
    // so, if the result is greater than one, it means the height should not be modified since
    // the width is the one needing to be adjusted
    pth = th / sh;
	
    // check if either the height or the width needs to be adjusted
    if (pth > 1.0)
    {
        // since the result was greater than 1.0, we'll work on the width
        // we do the same thing as above, but with the width
        tw = sh * (targetAspectWidth / targetAspectHeight);
        ptw = tw / sw;
		
        // get half of the percentage we're taking from the width
        half = (1.0 - ptw) / 2.0;
		
        // adjust the margin
        tx = half+(1.0-2.0*half)*baseRect.x;
        ty = ty + baseRect.y;
		
    }
    else
    {
        // get half of the percentage we're taking from the height
        half = (1.0 - pth) / 2.0;
		
        // adjust the margin
        ty = half+(1.0-2.0*half)*baseRect.y;
        tx = tx + baseRect.x;
    }
	
	
    // apply the camera.rect	
    var r : Rect;
    r.x = tx;
    r.y = ty;
    r.width = ptw * baseRect.width;
    r.height = pth * baseRect.height;
    GetComponent.<Camera>().rect = r;
    GetComponent.<Camera>().backgroundColor = new Color(0,0,0,1);
    
    previousWidth = sw;
	previousHeight = sh;
}

function Update() {
    if (disableOnMobilePlatforms && isOnMobile()) {
        return;
    }
    var sw : float = Screen.width;
    var sh : float = Screen.height;
//    previousWidth = sw;
    //    previousHeight = sh;
    if (sw != previousWidth || sh != previousHeight) {
        updateAspectRatio(sw, sh);
    }
}

function Awake () 
{
	if (executed) {
		return;
	}
	executed = true;
	// we'll try to calculate what's the biggest resolution we can accomplish
	// to work in the desired aspect ratio
	if (disableOnMobilePlatforms && isOnMobile()) {
		return;
	}

	var sw : float = Screen.width;
	var sh : float = Screen.height;
	previousWidth = sw;
	previousHeight = sh;
	
	baseRect = GetComponent.<Camera>().rect;

    updateAspectRatio(sw, sh);

    // get screen size

//	camera.aspect = targetAspectWidth/targetAspectHeight;
}

function isOnMobile():boolean {
	return true;
	return (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer);
}

// we require a Camera on this!
@script RequireComponent(Camera)


