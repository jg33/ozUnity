#pragma strict


var currentPanel:int;
var animator:Animator;

private var totalPanels = 4;

function Start () {
	currentPanel = 1;
}

function Update () {

}

function panelUp(){
	currentPanel++;
	animator.SetInteger("panel #", currentPanel);
	if (currentPanel > totalPanels) currentPanel = 1;
}

function panelDown(){
	currentPanel--;
	animator.SetInteger("panel #", currentPanel);
	if (currentPanel < 1) currentPanel = 1;

}