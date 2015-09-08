﻿#pragma strict

private var targetPos:GameObject;

private var cueComponent:cueSystem;


function Start () {
	cueComponent = GameObject.Find("Network").GetComponent(cueSystem);
}

function Update () {

	if(targetPos && this.gameObject.transform.localPosition != targetPos.transform.localPosition ){
			this.gameObject.transform.localPosition = targetPos.transform.localPosition;
			Debug.Log("Moved Image Target!");
			GameObject.Find("Camera Container").SendMessage("resetTracking");

	}

}

function updateTargetPos(){

	targetPos = GameObject.Find("ImageTargetLocation"+cueComponent.cueNumber.ToString());

}

function overrideTargetPos(x:float,y:float,z:float){

	targetPos.transform.localPosition.x=x;
	targetPos.transform.localPosition.y=y;
	targetPos.transform.localPosition.z=z;


}