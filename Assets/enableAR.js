#pragma strict


public var AREnabled:boolean = true;
private var ARCam:Vuforia.QCARAbstractBehaviour;
private var webCamBehavior:Vuforia.WebCamAbstractBehaviour;

function Start () {

}

function Update () {
	if(!ARCam){
		ARCam = GameObject.Find("ARCamera").GetComponent("QCARBehaviour");
		webCamBehavior = ARCam.gameObject.GetComponent("WebCamBehavior");
	} else {
	
		if(AREnabled && !ARCam.enabled){
			ARCam.enabled = true;
			Debug.Log("Enabled AR");
			//yield WaitForSeconds(1);
		} else if (!AREnabled && ARCam.enabled){
			delayDisable(3);
			Debug.Log("Disabled AR");
		} 
	
	}
}

function delayDisable(secs:float){
	yield WaitForSeconds(secs);
	ARCam.enabled = false;

}