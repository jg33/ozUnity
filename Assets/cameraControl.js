#pragma strict


var smoothing: float = 0.1;


private var cameraCam: GameObject;
private var targetPosition: Transform;
private var targetGyroCorrection: Quaternion;
private var ARCam: GameObject;

private var targetPositionArray: Array;
private var targetRotationArray: Array;
private var targetGyroCorrectionArray: Array;

private var projMatrix: Matrix4x4;

private var isTracking: boolean;

public var tightTracking:boolean;

public var gyroResetter:GameObject;
public var foundTarget:boolean;

function Start () {
	ARCam = GameObject.Find("ARCamera");
	
	targetPosition = this.transform.GetChild(0);
	
	targetPositionArray = new Array();
	targetRotationArray = new Array();
	targetGyroCorrectionArray = new Array();
}

function Update () {
	if (GameObject.Find("TextureBufferCamera")){
		cameraCam = GameObject.Find("TextureBufferCamera");
		cameraCam.transform.localPosition.x = 1000; //hide camera mirroring cam
	}
	
	
	if(tightTracking){
		this.gameObject.transform.localPosition = ARCam.transform.localPosition;
		this.gameObject.transform.localRotation = ARCam.transform.localRotation;
		
		var cam:Camera = ARCam.GetComponentsInChildren(Camera)[0];
		projMatrix = cam.get_projectionMatrix();
	
		GameObject.Find("BackgroundPlane").transform.localScale = GameObject.Find("Virtual BackgroundPlane").transform.localScale;
		GameObject.Find("Virtual BackgroundPlane").GetComponent.<Renderer>().enabled = false;
	
		cam = this.GetComponentsInChildren(Camera)[0];
		cam.set_projectionMatrix(projMatrix);
		
		var motionAmp:Behaviour = cam.gameObject.GetComponent("Amplify Motion Effect");
		motionAmp.enabled = false;
		
		gyroResetter.SendMessage("setTightTracking",true);
		gyroResetter.SendMessage("resetGyro");
	
	} else {
	
		//Calculate when to track
		if (targetPositionArray.length == 0  ){
			//updateTarget();
			//isTracking = true;

		} else if ( targetPositionArray.length > 0 && targetPositionArray.length < 20){ //initial calibration
	    	updateTarget();
	    	isTracking = true;
	    	Debug.Log("intial tracking....");
		
	    } else if ( Vector3.Distance(targetPosition.localPosition,ARCam.transform.localPosition) >= 0.01 &&
	    	Vector3.Distance(targetPosition.localPosition,ARCam.transform.localPosition) <= 1 &&
	    	Quaternion.Angle(targetPosition.localRotation, ARCam.transform.localRotation) <= 1
	    	){ 
	    	updateTarget();
	    	isTracking = true;
	    	Debug.Log("tracking.... Dist: " + Vector3.Distance(targetPositionArray[targetPositionArray.length-1],ARCam.transform.localPosition) );

	    } else {
	    	isTracking = false;
	    }
	    
	    if(isTracking && Time.frameCount%1 == 0 ){
	    	gyroResetter.SendMessage("setTightTracking",false);
	    	GameObject.Find("GyroResetter").SendMessage("resetGyro");
	    	Debug.Log("Gyro Reset!");
	    };
		
		smoothToTarget(targetPosition, smoothing);
		
		if(Input.GetButton("Fire1") && foundTarget ){
		
				targetPositionArray.Clear();
				targetRotationArray.Clear();
				updateTarget();
				
		}
		
		motionAmp  = this.gameObject.GetComponentsInChildren(Camera)[0].gameObject.GetComponent("Amplify Motion Effect");
		motionAmp.enabled = true;

	}
	
}

function smoothToTarget(target:Transform, smooth:float){
	this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, targetPosition.localPosition, smooth);
	this.gameObject.transform.localRotation = Quaternion.Lerp(this.gameObject.transform.localRotation, targetPosition.localRotation, smooth);
	//GameObject.Find("GyroResetter").transform.localRotation = Quaternion.Lerp(GameObject.Find("GyroResetter").transform.localRotation, targetGyroCorrection, smooth);
}

function updateTarget(){

	targetPositionArray.Push(ARCam.transform.localPosition);
	targetRotationArray.Push(ARCam.transform.localRotation);
	//targetGyroCorrectionArray.Push(getInvertedGyro());
	
	while(targetPositionArray.length > 40){
		targetPositionArray.RemoveAt(0);
	}
	
	while(targetRotationArray.length > 40){
		targetRotationArray.RemoveAt(0);
	}
	
	while(targetGyroCorrectionArray.length > 40){
		//targetGyroCorrectionArray.RemoveAt(0);
	}


	for(i in targetPositionArray){
		targetPosition.localPosition = Vector3.Lerp(targetPosition.localPosition,i,0.02);
	}
	for(i in targetRotationArray){
		targetPosition.localRotation = Quaternion.Lerp(targetPosition.localRotation,i,0.02);
	}
	for(i in targetGyroCorrectionArray){
		//targetGyroCorrection = Quaternion.Lerp(targetGyroCorrection,i,0.02);
	}

	/*
	targetPosition.localPosition = ARCam.transform.localPosition;
	targetPosition.localRotation = ARCam.transform.localRotation;
	*/

	var cam:Camera = ARCam.GetComponentsInChildren(Camera)[0];
	projMatrix = cam.get_projectionMatrix();
	
	GameObject.Find("BackgroundPlane").transform.localScale = GameObject.Find("Virtual BackgroundPlane").transform.localScale;
	//GameObject.Find("BackgroundPlane").transform.localScale = Vector3(2368,1,1435);
	GameObject.Find("Virtual BackgroundPlane").GetComponent.<Renderer>().enabled = false;
	
	cam = this.GetComponentsInChildren(Camera)[0];
	cam.set_projectionMatrix(projMatrix);
	
	//also zero out gyro control
	//GameObject.Find("GyroResetter").SendMessage("resetGyro");
	
}


public function getInvertedGyro(){
    		var  invertedOrientation:Quaternion;
    		invertedOrientation = Quaternion.Inverse(GameObject.Find("UPFTHeadTracker").transform.localRotation);
    		var invertedEulers = invertedOrientation.eulerAngles;
    		invertedOrientation = Quaternion.Euler(invertedEulers.x, invertedEulers.y, invertedEulers.z   );
    		return invertedOrientation;
}

public function lostTarget(){
	isTracking = false;

}

public function setTightTracking(b:boolean){
	tightTracking = b;

}

public function resetTracking(){
	targetPositionArray.Clear();
	targetRotationArray.Clear();
}

public function setFoundTarget(b:boolean){
	foundTarget = b;
}
