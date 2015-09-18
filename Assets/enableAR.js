#pragma strict


public var AREnabled:boolean = true;
private var ARCam:Vuforia.VuforiaAbstractBehaviour;
private var webCamBehavior:Vuforia.WebCamAbstractBehaviour;

private var isDelaying:boolean = false;

function Start () {

}

function Update () {
	if(!ARCam){
		ARCam = GameObject.Find("ARCamera").GetComponent("VuforiaBehaviour");
		webCamBehavior = ARCam.gameObject.GetComponent("WebCamBehavior");
	} else {
	
		if(AREnabled && !ARCam.enabled && !isDelaying){
			isDelaying = true;
			Debug.Log("Enabled AR");
			delayEnable(1);
			
		} else if (!AREnabled && ARCam.enabled && !isDelaying){
			delayDisable(3);
			isDelaying = true;
			Debug.Log("Disabled AR");
		} 
	
	}
}

function delayDisable(secs:float){
	yield WaitForSeconds(secs);
	ARCam.enabled = false;
	isDelaying = false;
}

function delayEnable(secs:float){
	ARCam.enabled = true;
	yield WaitForSeconds(1);
	isDelaying = false;
}