#pragma strict


public var AREnabled:boolean = true;
private var ARCam:Vuforia.VuforiaAbstractBehaviour;
private var webCamBehavior:Vuforia.WebCamAbstractBehaviour;

function Start () {

}

function Update () {
	if(!ARCam){
		ARCam = GameObject.Find("ARCamera").GetComponent("VuforiaBehaviour");
		webCamBehavior = ARCam.gameObject.GetComponent("WebCamBehavior");
	}
	
	if(AREnabled && !ARCam.enabled){
		ARCam.enabled = true;
		Debug.Log("Enabled AR");
	} else if (!AREnabled && ARCam.enabled){
		delayDisable(3);
		Debug.Log("Disabled AR");
	} 
}

function delayDisable(secs:float){
	yield WaitForSeconds(secs);
	ARCam.enabled = false;

}