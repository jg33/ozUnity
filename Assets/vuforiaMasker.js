#pragma strict

public var tornadoScene:GameObject;
public var munchkinScene:GameObject;
public var poppyScene:GameObject;
public var fireScene:GameObject;
public var monkeyScene:GameObject;


private var renderComponent:Canvas;


function Start () {
	renderComponent  = this.gameObject.GetComponent(Canvas);
}

function Update () {

	if(tornadoScene.active || munchkinScene.active || poppyScene.active || fireScene.active || monkeyScene.active){
		renderComponent.enabled = true;

	} else {
		renderComponent.enabled = false;
	}

}