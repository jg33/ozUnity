#pragma strict


var currentPanel:int;
var animator:Animator;

private var backButton:GameObject;
private var nextButton:GameObject;
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
	
	
	setButtons();

}

function panelDown(){
	currentPanel--;
	if (currentPanel < 1) currentPanel = 0;
	animator.SetInteger("panel #", currentPanel);
	
	setButtons();
}

function setButtons(){
	/// Button ///
	backButton = GameObject.Find("InstructionBackText");
	nextButton = GameObject.Find("InstructionNextText");

	if(currentPanel == 1){
		backButton.GetComponent(UI.Text).text = "CLOSE";
		nextButton.GetComponent(UI.Text).text = "NEXT";

		} else if(currentPanel == 4){
		nextButton.GetComponent(UI.Text).text = "DONE";
		backButton.GetComponent(UI.Text).text = "BACK";

		} else{
		backButton.GetComponent(UI.Text).text = "BACK";
		nextButton.GetComponent(UI.Text).text = "NEXT";

		}
}