#pragma strict

private var cam: GameObject;

function Start () {
	cam = GameObject.Find("UPFTHeadTracker");
}

function Update () {
	var x:float = 0;
	var y :float = (cam.transform.localRotation.eulerAngles.y+180)*0.017;
	var z:float = 0;
	var rot:Quaternion = Quaternion.identity;
	rot.SetEulerAngles(x,y,z);
	this.transform.localRotation= rot;
	
	Debug.Log(y);
}
