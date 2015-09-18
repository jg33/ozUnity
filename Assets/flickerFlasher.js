#pragma strict

var flasher:UI.Image;


function Start () {
	flasher= this.gameObject.GetComponent(UI.Image);

}

function Update () {
	flasher.color.a = Mathf.PerlinNoise(Time.frameCount*0.2,Time.frameCount*0.024);
	
}