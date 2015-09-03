#pragma strict

public var buttonsDisabled:boolean;
private var previousDisabledState:boolean;

function Start () {
	buttonsDisabled = false;
	previousDisabledState = buttonsDisabled;
}

function Update () {
	
	if(buttonsDisabled != previousDisabledState){
		var buttons : UI.Button[] =  GameObject.Find("MenuContainer").GetComponentsInChildren(UI.Button);
		for(var butt in buttons){
			butt.interactable = buttonsDisabled;
		
		}
	}

}