#pragma strict

private var currentPos: float;
private var cueComponent: cueSystem;
private var movieComponent: VideoTexture_Material;

function Start () {
	cueComponent = GameObject.Find("Network").GetComponent(cueSystem);
	movieComponent = this.GetComponent(VideoTexture_Material);
}

function Update () {
	
	if(movieComponent.CurrentPosition() !=currentPos){
		currentPos = movieComponent.CurrentPosition();
		cueComponent.moviePosition = currentPos;
		
	}
	
	if(Input.GetKeyDown(KeyCode.M)){
		SendMessage("gotoPosition", Random.Range(0,1000)/1000f) ;
	}
	
}
