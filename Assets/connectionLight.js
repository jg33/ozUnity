#pragma strict

public var connected:boolean = false;
private var prevConnectedState:boolean = false;
function Start () {

}

function Update () {
	if (connected != prevConnectedState){
		gameObject.GetComponent(Animator).SetBool("connected", connected);	
		prevConnectedState = connected;
	}	
}

function setConnected(b:boolean){
	connected = b;

}