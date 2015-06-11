#pragma strict

function Start () {
	
}

function Update () {
	if(this.GetComponent(Renderer).enabled){
		this.GetComponent(Renderer).enabled = false;
	
	}
}