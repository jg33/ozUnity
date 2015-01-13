#pragma strict

public var movieFileName:String;
public var playOnLoad:boolean;
public var desktopVideoPlane:GameObject;

private var hasPlayed:boolean;
private var isEnabled:boolean;
private var socket:socketCtrl;
private var startTime:float;

function Start () {
	hasPlayed= false;
	isEnabled = false;

	socket = GameObject.Find("SceneController").GetComponent(socketCtrl);
}

function Update () {

	if(Time.frameCount>100 && !hasPlayed){ //If it hasn't played, update times.
		var currentTime = socket.localTime; 
		isEnabled = gameObject.transform.parent.gameObject.activeInHierarchy;
	}

	if (playOnLoad && !hasPlayed && isEnabled){
		play();
		hasPlayed = true;
	} else if (!playOnLoad && currentTime > startTime && !hasPlayed){
		play();
		hasPlayed = true;
	}

}

function play(){
	//Handheld.PlayFullScreenMovie(movieFileName, Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
	var movTex:MovieTexture;
	movTex = desktopVideoPlane.GetComponent(Renderer).material.mainTexture;
	movTex.Play() ;


}

function playAt(time:float){
	startTime = time;
}

function stop(){

}
