#pragma strict

function Start () {

}

function Update () {
	var text:UnityEngine.UI.Text = gameObject.GetComponent("Text");
	text.text =Input.compass.rawVector.ToString();
}