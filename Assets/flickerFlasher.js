#pragma strict

var flasher:UI.Image;


function Start () {
	flasher= this.gameObject.GetComponent(UI.Image);

}

function Update () {
	flasher.color.a = Random.Range(0,1000)*0.001;
	
}