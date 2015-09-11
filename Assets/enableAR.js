#pragma strict


public var AREnabled:boolean = true;
private var ARCam:Behaviour;

function Start () {

}

function Update () {
	if(!ARCam){
		ARCam = GameObject.Find("ARCamera").GetComponent("VuforiaBehaviour");
	}
	
	if(AREnabled && !ARCam.enabled){
		ARCam.enabled = true;
	} else if (!AREnabled && ARCam.enabled){
		ARCam.enabled = false;
	}
}