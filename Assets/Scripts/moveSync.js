#pragma strict

var socketControl:socketCtrl;

@HideInInspector
var localTime:float;

function Start () {
	socketControl= GameObject.Find("SceneController").GetComponent(socketCtrl);
}

function Update () {
	localTime = socketControl.getLocalTime() ;
	var width = Screen.width;
	this.gameObject.transform.position = 
	Vector3( (width/2)+Mathf.Sin(localTime * 0.001)*width/2,Screen.height/2,0);
}