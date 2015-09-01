#pragma strict


public var AREnabled:boolean = true;
private var ARCam:GameObject;

function Start () {

}

function Update () {
	if(!ARCam){
		ARCam = GameObject.Find("ARCamera");
	}
	
	if(AREnabled && !ARCam.active){
		ARCam.active = true;
	} else if (!AREnabled && ARCam.active){
		ARCam.active = false;
	}
}