#pragma strict

var poppyScene: GameObject;
var tornadoScene: GameObject;
var mLandScene: GameObject;
var scene1: GameObject;
var camObj: GameObject;

private var mindPoppies: boolean = false;
private var sepiaOn: boolean = true;


function Start () {
	var ArEnabler:Behaviour = GameObject.Find("ARCamera").GetComponent("QCARBehaviour");
	ArEnabler.enabled= true;

}

function Update () {

	///SEPIA TONING
	if(tornadoScene.active){
		//enable sepia
		//camObj.GetComponent("CC_Vintage").range= 1;
		//camObj.GetComponent("CC_Hue Focus").amount=1 ;
		GameObject.Find("Camera").GetComponent.<Animator>().SetBool("isSepia", true);
		//Debug.Log("to bw!");
	
	} else if(mLandScene.active || scene1.active){
		//Don't control sepia in MLand!
		//Debug.Log("not controlling sepia.");
	
	} else if(!tornadoScene.active){
		// disable filter
		//camObj.GetComponent("CC_Vintage").amount=0;
		//camObj.GetComponent("CC_Hue Focus").range= 111;
		GameObject.Find("Camera").GetComponent.<Animator>().SetBool("isSepia", false);
		//Debug.Log("to color!");

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
			Debug.Log(poppy.gameObject.name);

			if(!poppy.gameObject.GetComponent(ParticleSystem)){
				poppy.enabled = false;
				Debug.Log("Disabled Poppy Renderer");
			
			} 

		}
		mindPoppies = false;
	
	}
	


}