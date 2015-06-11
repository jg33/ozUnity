#pragma strict

var poppyScene: GameObject;
var tornadoScene: GameObject;
var camObj: GameObject;

private var mindPoppies: boolean = false;
private var sepiaOn: boolean = true;


function Start () {

}

function Update () {
	///SEPIA TONING
	
	if(tornadoScene.active && !sepiaOn){
		//enable sepia
		//camObj.GetComponent("CC_Vintage").range= 1;
		//camObj.GetComponent("CC_Hue Focus").amount=1 ;
		GameObject.Find("Camera").GetComponent.<Animator>().SetTrigger("to_bw");
		sepiaOn = true;
		Debug.Log("to bw!");
	
	} else if(!tornadoScene.active && sepiaOn ){
		// disable filter
		//camObj.GetComponent("CC_Vintage").amount=0;
		//camObj.GetComponent("CC_Hue Focus").range= 111;
		GameObject.Find("Camera").GetComponent.<Animator>().SetTrigger("Anim1_M");
		sepiaOn = false;
		Debug.Log("to color!");

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