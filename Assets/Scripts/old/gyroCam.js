// iPhone gyroscope-controlled camera demo v0.3 8/8/11
// Perry Hoberman <hoberman@bway.net>
// Directions: Attach this script to main camera.
// Note: Unity Remote does not currently support gyroscope. 

private var gyroBool : boolean;
private var gyro : Gyroscope;
private var rotFix : Quaternion;
public var fpc : GameObject;

function Start() {	

	Screen.orientation  = ScreenOrientation.LandscapeLeft;
        
        
	var originalParent = transform.parent; // check if this transform has a parent
	var camParent = new GameObject ("camParent"); // make a new parent
	camParent.transform.position = transform.position; // move the new parent to this transform position
	transform.parent = camParent.transform; // make this transform a child of the new parent
	camParent.transform.parent = originalParent; // make the new parent a child of the original parent
	
	gyroBool = Input.isGyroAvailable;
	
	if (gyroBool) {

		gyro = Input.gyro;
		gyro.enabled = true;
		
		camParent.transform.eulerAngles = Vector3(90,90,0); //hard fix for orientation

		/*
		if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
			camParent.transform.eulerAngles = Vector3(90,90,0);
		} else if (Screen.orientation == ScreenOrientation.Portrait) {
			camParent.transform.eulerAngles = Vector3(90,180,0);
		}
		*/

		if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
			rotFix = Quaternion(0,0,0.7071,0.7071);
		} else if (Screen.orientation == ScreenOrientation.Portrait) {
			rotFix = Quaternion(0,0,1,0);
		}	
		

	} else {
		print("NO GYRO");
	}
}

function Update () {
	if (gyroBool) {
		var camRot : Quaternion = gyro.attitude * rotFix;
		transform.localRotation = camRot;
		var direction : Vector3 = camRot * Vector3.up;
		direction.y = 0;
		//gameObject.Find("print").guiText.text = direction.ToString();
		//fpc.transform.rotation = Quaternion.LookRotation(direction);
		}
}