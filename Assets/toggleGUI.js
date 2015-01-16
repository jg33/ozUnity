#pragma strict

public var objects:GameObject[];

function Start () {

}

function Update () {

}

function guiOn( toggle){
	for(var obj in objects){
		obj.SetActive(toggle) ;

	}


}