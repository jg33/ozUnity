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

//tolerance is how far it can vary before it ignores the target
public var maxDistanceTolerance:float = 1;
public var maxAngleTolerance:float = 1;

//threshold is how far away it can be for [timeout] frames before updating
public var minDistanceThreshold:float = 10; 
public var minAngleThreshold:float = 10;
public var timeout:int = 20;
private var errorFrames:int = 0;

private var backgroundPlane:GameObject;
private var virtualBackgroundPlane:GameObject;

private var calibrationTimeout:int = 1800;
public var calibrationTimer:int = 0;
private var calibrationMsg:GameObject;

private var gotProjMatrix:boolean = false;

function Start () {
	ARCam = GameObject.Find("ARCamera");
	
	targetPosition = this.transform.GetChild(0);
	
	targetPositionArray = new Array();
	targetRotationArray = new Array();
	targetGyroCorrectionArray = new Array();
	
}

function Update () {
	
	if(!backgroundPlane){
		backgroundPlane = GameObject.Find("BackgroundPlane");
	}
	
	if(!virtualBackgroundPlane){
		virtualBackgroundPlane = GameObject.Find("Virtual BackgroundPlane");
	
	}

	if(!gotProjMatrix && ARCam.GetComponent(Vuforia.WebCamAbstractBehaviour).IsPlaying){ //is camera on? grab projection matrix
			Invoke ("setProjectionMatrix",0.1);
			
	}
	
	if (GameObject.Find("TextureBufferCamera")){
		cameraCam = GameObject.Find("TextureBufferCamera");
		cameraCam.transform.localPosition.x = 1000; //hide camera mirroring cam
	}
	
		//Calculate when to track
	if(foundTarget){
			if (targetPositionArray.length == 0 ){
				updateTarget();
				isTracking = true;
				errorFrames = 0; //depreciated
				calibrationTimer = 0; //reset calibration timer
			} else if ( targetPositionArray.length > 0 && targetPositionArray.length < 20 ){ //initial calibration
		    	updateTarget();
		    	isTracking = true;
		    	calibrationTimer = 0; //reset calibration timer

		    	Debug.Log("intial tracking....");
		    } else if ( Vector3.Distance(targetPosition.localPosition,ARCam.transform.localPosition) >= 0.01 && 
		    	Vector3.Distance(targetPosition.localPosition,ARCam.transform.localPosition) <= maxDistanceTolerance &&
		    	Quaternion.Angle(targetPosition.localRotation, ARCam.transform.localRotation) <= maxAngleTolerance
		    	){ 		//used to be 0.01, 1, 1.

		    	updateTarget();
		    	isTracking = true;
		    	Debug.Log("tracking.... Dist: " + Vector3.Distance(targetPositionArray[targetPositionArray.length-1],ARCam.transform.localPosition) );
				errorFrames=0;	   
		     }else {
		    	isTracking = false;
		    }
		    
		    gyroResetter.SendMessage("setTightTracking",false);

		    if(isTracking && Time.frameCount%1 == 0 ){
		    	GameObject.Find("GyroResetter").SendMessage("resetGyro");
		    	Debug.Log("Gyro Reset!");
		    };
		    
		    // send a poke-to-calibrate message, only in active mode //
		    if(calibrationTimer>calibrationTimeout && Application.loadedLevel == 2){
		    	if(!calibrationMsg){
		    		calibrationMsg = GameObject.Find("Tap Screen Msg");
		    	} else{
		    		calibrationMsg.GetComponent(Animator).SetBool("showRecalMsg", true);
		    	}
		    
		    }
	    
	    } else { //if lost target, add time to uncalibrated timer, but don't show the message!
			calibrationTimer++;
			if(!calibrationMsg){
			    		calibrationMsg = GameObject.Find("Tap Screen Msg");
			    	} else{
			    		calibrationMsg.GetComponent(Animator).SetBool("showRecalMsg", false);
			    	}
	
		}
		
		smoothToTarget(targetPosition, smoothing);
		
		if(Input.GetButton("Fire1") && foundTarget ){
		
				resetTracking();
				updateTarget();
				
		}

	
	
	if(tightTracking){ //Override cam position if tight tracking
		this.gameObject.transform.localPosition = ARCam.transform.localPosition;
		this.gameObject.transform.localRotation = ARCam.transform.localRotation;
		
		var cam:Camera = ARCam.GetComponentsInChildren(Camera)[0];
		projMatrix = cam.get_projectionMatrix();
	
		backgroundPlane.transform.localScale = virtualBackgroundPlane.transform.localScale;
		virtualBackgroundPlane.GetComponent.<Renderer>().enabled = false;
	
		cam = this.GetComponentsInChildren(Camera)[0];
		cam.set_projectionMatrix(projMatrix);
		
		gyroResetter.SendMessage("setTightTracking",true);
		gyroResetter.SendMessage("resetGyro");
	
	} 
	
}

function smoothToTarget(target:Transform, smooth:float){
	this.gameObject.transform.localPosition = Vector3.Lerp(this.gameObject.transform.localPosition, targetPosition.localPosition, smooth);
	this.gameObject.transform.localRotation = Quaternion.Lerp(this.gameObject.transform.localRotation, targetPosition.localRotation, smooth);
	//GameObject.Find("GyroResetter").transform.localRotation = Quaternion.Lerp(GameObject.Find("GyroResetter").transform.localRotation, targetGyroCorrection, smooth);
}

function hopToTarget(){
	this.gameObject.transform.localPosition = targetPosition.localPosition;
	this.gameObject.transform.localRotation = targetPosition.localRotation;

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
	
	backgroundPlane.transform.localScale = virtualBackgroundPlane.transform.localScale;
	//GameObject.Find("BackgroundPlane").transform.localScale = Vector3(2368,1,1435);
	virtualBackgroundPlane.GetComponent.<Renderer>().enabled = false;
	
	cam = this.GetComponentsInChildren(Camera)[0];
	cam.set_projectionMatrix(projMatrix);
	
	//also zero out gyro control
	//GameObject.Find("GyroResetter").SendMessage("resetGyro");
	
}


public function setProjectionMatrix(){
		var cam:Camera = ARCam.GetComponentsInChildren(Camera)[0];
		projMatrix = cam.get_projectionMatrix();
	
		backgroundPlane.transform.localScale = virtualBackgroundPlane.transform.localScale;
		virtualBackgroundPlane.GetComponent.<Renderer>().enabled = false;
	
		cam = this.GetComponentsInChildren(Camera)[0];
		cam.set_projectionMatrix(projMatrix);
		
		
		gotProjMatrix = true;


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
	foundTarget = false;
}

public function setTightTracking(b:boolean){
	tightTracking = b;

}

public function resetTracking(){
	Debug.Log("Reset Tracking!");
	targetPositionArray.Clear();
	targetRotationArray.Clear();
}

public function setFoundTarget(b:boolean){
	foundTarget = b;
}
