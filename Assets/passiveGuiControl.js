#pragma strict

enum state {AR_CAM, MAIN_MENU, LEARN_MORE, BONUS_MENU };
var bIsConnected:boolean;

var menuContainer: GameObject;
var connectedButton: GameObject;
var instructionModal: GameObject;
var instructionAlert: GameObject;

function Start () {
	menuContainer = GameObject.Find("MenuContainer");
	connectedButton = GameObject.Find("Connected Button");
	instructionModal = GameObject.Find("Instruction Modal");
	instructionAlert = GameObject.Find("Instruction Alert");

}

function Update () {

}

function setConnected(connectedState){
	bIsConnected = connectedState;
}