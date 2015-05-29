#pragma strict


var smoothing: float = 0.1;


private var cameraCam: GameObject;
private var targetPosition: Transform;
private var ARCam: GameObject;

private var projMatrix: Matrix4x4;

function Start () {
	ARCam = GameObject.Find("ARCamera");
	targetPosition = this.transform.GetChild(0);

}

function Update () {
	if (GameObject.Find("TextureBufferCamera")){
		cameraCam = GameObject.Find("TextureBufferCamera");
		cameraCam.transform.localPosition.x = 1000; //hide camera mirroring cam
	}
	
	//check difference to AR Camera
	if(Vector3.Distance(targetPosition.localPosition, ARCam.transform.localPosition) > 1){
		updateTarget();
	}
	
	
	smoothToTarget(targetPosition, smoothing);
	
	
	
}

function smoothToTarget(target:Transform, smooth:float){
	this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, targetPosition.localPosition, smooth);
	this.gameObject.transform.localRotation = Quaternion.Lerp(this.gameObject.transform.localRotation, targetPosition.localRotation, smooth);

}

function updateTarget(){
	targetPosition.localPosition = ARCam.transform.localPosition;
	targetPosition.localRotation = ARCam.transform.localRotation;

	var cam:Camera = ARCam.GetComponentsInChildren(Camera)[0];
	projMatrix = cam.get_projectionMatrix();
	
	cam = this.GetComponentsInChildren(Camera)[0];
	cam.set_projectionMatrix(projMatrix);
	
	//also zero out gyro control
	GameObject.Find("Camera").SendMessage("resetGyro");
}
