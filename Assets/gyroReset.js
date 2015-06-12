#pragma strict

var targetRotation: Quaternion;

var tightTracking: boolean;


function Start () {
	targetRotation = Quaternion.identity;
}

function Update () {
	if(tightTracking){
		transform.localRotation = targetRotation;
	} else{
		transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation,0.04  );
	}

}

public function resetGyro(){
    		var  invertedOrientation:Quaternion;
    		invertedOrientation = Quaternion.Inverse(GameObject.Find("UPFTHeadTracker").transform.localRotation);
    		var invertedEulers = invertedOrientation.eulerAngles;
    		invertedOrientation = Quaternion.Euler(invertedEulers.x, invertedEulers.y, invertedEulers.z   );
    		targetRotation = invertedOrientation;

   		 }
   		 
public function setTightTracking(t:boolean){

	tightTracking = t;
}