#pragma strict


var currentPanel:int;
var animator:Animator;

function Start () {
	currentPanel = 1;
}

function Update () {

}

function panelUp(){
	currentPanel++;
	animator.SetInteger("panel #", currentPanel);
}

function panelDown(){
	currentPanel--;
	animator.SetInteger("panel #", currentPanel);
}