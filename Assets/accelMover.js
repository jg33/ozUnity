#pragma strict

private var previousX;
private var previousY;

function Start () {

}

function Update () {
	var newX;
	var newY;

	//MOBILE
	/* 
	gameObject.transform.localPosition.x = Input.acceleration.x  ;
	gameObject.transform.localPosition.y = Input.acceleration.y  ;
	*/

	//DESKTOP


	gameObject.transform.localPosition.x = Input.mousePosition.x  ;
	gameObject.transform.localPosition.y = Input.mousePosition.y ;



}