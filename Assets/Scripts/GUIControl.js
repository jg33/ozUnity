#pragma strict

import System.Collections.Generic;

public var scenes:GameObject;
var numScenes:int = 3; 

@HideInInspector
var sceneArray: List.<GameObject> ;
private var canvasObject:GameObject;


function Start () {
	scenes.SetActive(true);

	for (var i = 0; i  < numScenes+1 ; i++) {
		sceneArray.Add(GameObject.Find("Scene"+i)) ;
		if(sceneArray[i] != null){
			sceneArray[i].SetActive(false);
		}
	};

	canvasObject = sceneArray[0];

}

function Update () {

}

function setActiveScene(newScene:String){
	var i=parseInt(newScene);
	if(i>=0){
		canvasObject.GetComponent(Animation).Play("UIFadeOut");
		yield WaitForSeconds(canvasObject.GetComponent(Animation).clip.length);
		canvasObject.SetActive(false);
		canvasObject = sceneArray[i];
		Debug.Log(sceneArray[i]);
		canvasObject.SetActive(true);
		canvasObject.GetComponent(Animation).Play("UIFadeIn");
	} else if (i<0){
		canvasObject.SetActive(false);

	}

}