#pragma strict

var poppyScene: GameObject;
var tornadoScene: GameObject;
var camObj: GameObject;

private var mindPoppies: boolean = false;
private var mindSepia: boolean = false;


function Start () {

}

function Update () {
	///SEPIA TONING
	
	if(tornadoScene.active && camObj.GetComponent(Renderer) == 1 && mindSepia){
		//enable sepia
		//camObj.GetComponent("CC_Vintage").range= 1;
		//camObj.GetComponent("CC_Hue Focus").amount=1 ;
		mindSepia = false;
	
	} else if(!tornadoScene.active && camObj.GetComponent(Renderer)  == 1 ){
		// disable filter
		//camObj.GetComponent("CC_Vintage").amount=0;
		//camObj.GetComponent("CC_Hue Focus").range= 111;

		mindSepia = true;
	}

	///CLEAR POPPIES
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