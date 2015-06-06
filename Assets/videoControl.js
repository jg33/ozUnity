#pragma strict

function Start () {

}

function Update () {

	if (Input.GetMouseButtonDown(0)  ){
		Debug.Log("Click!");
		this.SendMessage("gotoPosition",0.5);
	}

}