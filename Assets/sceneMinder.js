#pragma strict

var poppyScene: GameObject;

private var mindPoppies: boolean = false;



function Start () {

}

function Update () {

	if(poppyScene.active){
		mindPoppies = true;
	}

	if(!poppyScene.active && mindPoppies){
		var renderers:Component[];
		renderers = poppyScene.GetComponentsInChildren(Renderer, true);
		Debug.Log("Length "+renderers.Length);
		
		for( var poppy:Renderer in renderers){
			poppy.enabled = false;
			Debug.Log("Disabled Poppy Renderer");

		}
		mindPoppies = false;
	
	}

}