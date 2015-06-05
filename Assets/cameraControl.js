#pragma strict


var smoothing: float = 0.1;


private var cameraCam: GameObject;
private var targetPosition: Transform;
private var ARCam: GameObject;

private var targetPositionArray: Array;
private var targetRotationArray: Array;

private var projMatrix: Matrix4x4;

function Start () {
	ARCam = GameObject.Find("ARCamera");
	targetPosition = this.transform.GetChild(0);

	targetPositionArray = new Array();
	targetRotationArray = new Array();
}

function Update () {
	if (GameObject.Find("TextureBufferCamera")){
		cameraCam = GameObject.Find("TextureBufferCamera");
		cameraCam.transform.localPosition.x = 1000; //hide camera mirroring cam
	}
	
	//Calculate when to track
	if (targetPositionArray.length == 0){
		updateTarget();

	} else if ( targetPositionArray.length > 0 &&
		 Vector3.Distance(targetPositionArray[targetPositionArray.length-1],ARCam.transform.localPosition) > 0.01
		 ){
    	updateTarget();
    }
	
	smoothToTarget(targetPosition, smoothing);
	
	
	
}

function smoothToTarget(target:Transform, smooth:float){
	this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, targetPosition.localPosition, smooth);
	this.gameObject.transform.localRotation = Quaternion.Lerp(this.gameObject.transform.localRotation, targetPosition.localRotation, smooth);

}

function updateTarget(){

	targetPositionArray.Push(ARCam.transform.localPosition);
	targetRotationArray.Push(ARCam.transform.localRotation);
	
	while(targetPositionArray.length > 40){
		targetPositionArray.RemoveAt(0);
	}
	
	while(targetRotationArray.length > 40){
		targetRotationArray.RemoveAt(0);
	}


	for(i in targetPositionArray){
		targetPosition.localPosition = Vector3.Lerp(targetPosition.localPosition,i,0.02);
	}
	for(i in targetRotationArray){
		targetPosition.localRotation = Quaternion.Lerp(targetPosition.localRotation,i,0.02);
	}

	/*
	targetPosition.localPosition = ARCam.transform.localPosition;
	targetPosition.localRotation = ARCam.transform.localRotation;
	*/

	var cam:Camera = ARCam.GetComponentsInChildren(Camera)[0];
	projMatrix = cam.get_projectionMatrix();
	
	GameObject.Find("BackgroundPlane").transform.localScale = GameObject.Find("Virtual BackgroundPlane").transform.localScale;
	GameObject.Find("Virtual BackgroundPlane").GetComponent.<Renderer>().enabled = false;
	
	cam = this.GetComponentsInChildren(Camera)[0];
	cam.set_projectionMatrix(projMatrix);
	
	//also zero out gyro control
	GameObject.Find("GyroResetter").SendMessage("resetGyro");
	
}
