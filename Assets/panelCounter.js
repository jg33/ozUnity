#pragma strict


var currentPanel:int;
var animator:Animator;

private var totalPanels = 4;

function Start () {
	currentPanel = 0;
}

function Update () {

}

function panelUp(){
	currentPanel++;
	if (currentPanel > totalPanels) currentPanel = 0;
	animator.SetInteger("panel #", currentPanel);
}

function panelDown(){
	currentPanel--;
	if (currentPanel < 1) currentPanel = 0;
	animator.SetInteger("panel #", currentPanel);

}