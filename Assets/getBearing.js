#pragma strict

function Start () {
	Input.compass.enabled = true;
}

function Update () {
	var text:UnityEngine.UI.Text = gameObject.GetComponent("Text");
	text.text =Input.compass.rawVector.ToString();
}