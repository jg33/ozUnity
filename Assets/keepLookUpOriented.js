#pragma strict

private var cam: GameObject;

function Start () {
	cam = GameObject.Find("UPFTHeadTracker");
}

function Update () {
	var x:float = 0;
	var y:float = cam.transform.localRotation.eulerAngles.y;
	var z:float = 0;
	var rot:Quaternion = Quaternion.identity;
	rot.SetEulerAngles(x,y,z);
	this.transform.localRotation.eulerAngles.y = cam.transform.localRotation.eulerAngles.y;
	
	Debug.Log(y);
}
