#pragma strict
var poppyColor: UI.Image;

function Start () {

	poppyColor = this.gameObject.GetComponent(UI.Image);

}

function Update () {
	var frameCount:float;
	frameCount= Time.frameCount;
	poppyColor.color.a = 0.25+Mathf.Abs(Mathf.Sin( frameCount * 0.01));

}