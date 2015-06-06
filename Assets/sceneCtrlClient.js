#pragma strict

public var connectionIP:String = "127.0.0.1";
public var portNumber:int = 16261;
private var connected:boolean = false;
var timeout: int = 300;

public var cueComponent:cueSystem;
//public var scenes:GameObject;
var numScenes:int = 3;
public var currentCue:int=0;
private var prevCue:int =0;

private var currentEventCue:int = 0;

@HideInInspector
var sceneArray: List.<GameObject> ;
private var canvasObject:GameObject;
var timeoutCounter: int;

private var forcePassive: boolean;

function Awake(){
	DontDestroyOnLoad (this);
}


function Start () {
	Screen.sleepTimeout = SleepTimeout.NeverSleep;
	Network.Connect (connectionIP, portNumber);
	
	GameObject.Find("Scenes").SetActive(true);
	var sceneListComp: sceneList = GameObject.Find("Scenes").GetComponent.<sceneList>();
	
	

}

function Update () {
	if (connected){
		forcePassive = cueComponent.forcePassive;
		
		if(currentCue !=0 && Application.loadedLevel == 2){
			//GameObject.Find("Look Up").active =  true;
		
		} else{
			//GameObject.Find("Look Up").active =  false;

		}
	
		if (Application.loadedLevel != 2 && !forcePassive){
			Application.LoadLevel(2);
			Debug.Log("Connected! Loading Active Mode!");
		
		} if (forcePassive && Application.loadedLevel != 1){
			Application.LoadLevel(1);
		}

		if (cueComponent.cueNumber != currentCue){
			prevCue = currentCue;
			currentCue = cueComponent.cueNumber;
			setActiveScene(currentCue.ToString());
		
			#if UNITY_IPHONE
			if (currentCue != 0 ) Handheld.Vibrate();
			#endif
		
		} 
		
		if (cueComponent.tempEventTriggers != currentEventCue){
			Debug.Log("event trigger!");
			currentEventCue = cueComponent.tempEventTriggers;				
				switch(currentCue){
				
					case 1:
					
					switch( currentEventCue ){
						case 1:
	
						GameObject.Find("TrashCan").GetComponent.<Animator>().SetTrigger("Anim1");
						Debug.Log("ugh");

						break;
					
						case 2:
						
						GameObject.Find("TrashCan").GetComponent.<Animator>().SetTrigger("Anim2");

						break;
						
					}
					
					break;
					
					case 2:
					
					switch( currentEventCue ){
						case 1:
						
						GameObject.Find("FunkyCube").GetComponent.<Animator>().SetTrigger("Anim1_1");
						Debug.Log("ImNumba1");

						break;
					
						case 2:
						GameObject.Find("FunkyCube").GetComponent.<Animator>().SetTrigger("Anim2_1");
						Debug.Log("ImNumba2");

						break;
					}
					break;
					
					case 5:
					
					switch( currentEventCue ){
						case 1:
						GameObject.Find("GlindaPath").GetComponent.<Animator>().SetTrigger("Anim1_11");
						Debug.Log("ImNumba5");

						break;
					}
					break;
					
					case 9:
					
					switch( currentEventCue ){
						case 1:
						
						var go:GameObject = Instantiate (Resources.Load ("pp1")) as GameObject; 
						go.transform.parent = GameObject.Find("Scene9").transform;
						Debug.Log("poppyClump1");

						break;
					
						case 2:
						
						var go2:GameObject  = Instantiate (Resources.Load ("pp1")) as GameObject; 
						go2.transform.parent = GameObject.Find("Scene9").transform;
						Debug.Log("poppyClump2");
						
						break;

					}
					break;
					
					default:
						switch( currentEventCue ){
						case 1:
						cueComponent.playMovie("randomRainbow");
						Debug.Log("rainbow!");

						break;
				
						case 2:
						cueComponent.playAudio("noPlace");
						Debug.Log("no place!");

						break;
				
						case 3:
						cueComponent.vibrate();
						Debug.Log("buzz!");
						break;
				
						default:
						break;
					  
					
					}
			
				}
		}
		
		
	} else{ //not connected
	
		if (Application.loadedLevel != 1 ){
			Application.LoadLevel(1);
			Debug.Log("Disconnected! Loading Passive Mode! :'(");

		}
	
	
		if(timeoutCounter> timeout){
			Network.Connect (connectionIP, portNumber);
			Debug.Log("Not Connected! "+ Network.peerType );
			timeoutCounter =0;
 			
 		} else if (timeoutCounter <= timeout){
			timeoutCounter++;
		} 
	

	}
}


function OnConnectedToServer(){
		Debug.Log ("Connected To Server");
		connected = true;
	}

function OnDisconnectedFromServer(){
		Debug.Log ("Disconnected From Server");
		connected = false;
	}

function OnFailedToConnect(error: NetworkConnectionError){
		Debug.Log ("Failed to Connect: " + error);
		connected = false;
	}

function setActiveScene(newScene:String){
	var i=parseInt(newScene);
	
	sceneArray = GameObject.Find("Scenes").GetComponent.<sceneList>().sceneArray;
	
	if(i>=0){
		canvasObject = sceneArray[prevCue];
		canvasObject.GetComponent(Animation).Play("UIFadeOut");
		yield WaitForSeconds(canvasObject.GetComponent(Animation).clip.length);
		canvasObject.SetActive(false);
		
		canvasObject = sceneArray[i];
		Debug.Log(sceneArray[i]);
		canvasObject.SetActive(true);
		canvasObject.GetComponent(Animation).Play("UIFadeIn");
		
	} else if (i<0){
		canvasObject.SetActive(false);

	}

}


