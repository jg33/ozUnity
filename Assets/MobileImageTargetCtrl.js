#pragma strict

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