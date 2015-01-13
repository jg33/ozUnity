#pragma strict

public var skin:GUISkin;

function Start () {

}

function Update () {

}

function OnGUI(){

	GUI.skin = skin;
	
	if( GUI.Button(Rect(50,50,50,50),"Hiya")){
		ButtClick();
	
	}

		
	System.Console.Write("butthole");
	
}

function ButtClick(){
		Debug.Log("butts");

}