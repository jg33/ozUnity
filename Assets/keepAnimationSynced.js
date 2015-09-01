#pragma strict

enum Scene{tornado,munchkin,poppy,monkey};

public var scene:Scene;
private var cueCtrl:cueSystem;
private var currentState:int;
private var ani:Animator;

function Start () {
	ani = gameObject.GetComponent(Animator);
	
	cueCtrl = GameObject.Find("Network").GetComponent(cueSystem);
}

function Update () {
	switch(scene){
		case Scene.tornado:
			if(cueCtrl.tornadoState != currentState){
				currentState = cueCtrl.tornadoState;
				ani.SetInteger("state", currentState);
			}	
		break;
		
		case Scene.munchkin:
			if(cueCtrl.munchkinState != currentState){
				currentState = cueCtrl.munchkinState;
				ani.SetInteger("state", currentState);
				
				if(currentState == 1){
					for(var a:AudioSource in this.gameObject.GetComponentsInChildren(AudioSource)){
						a.Play();
					
					}
				} else {
					for(var a:AudioSource in this.gameObject.GetComponentsInChildren(AudioSource)){
						//a.Stop();
					
					}
				}
			}	
		break;
		
		case Scene.poppy:
			if(cueCtrl.poppyState != currentState){
				currentState = cueCtrl.poppyState;
				ani.SetInteger("state", currentState);
			}	
		break;
		
		case Scene.monkey:
			if(cueCtrl.monkeyState != currentState){
				currentState = cueCtrl.monkeyState;
				ani.SetInteger("state", currentState);
			}	
		break;
		
		
		
		default:
		break;
	
	
	}

}