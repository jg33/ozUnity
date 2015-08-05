#pragma strict

public var screenHeight:float;
public var screenWidth:float;

function Start () {
	GetComponent(RectTransform).sizeDelta.y = Screen.height;
	screenHeight = Screen.height;
}

function Update () {
	
}