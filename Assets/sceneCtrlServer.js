#pragma strict

public var portNumber:int = 16261;
private var connected:boolean = false;

public var cueComponent:cueSystem;
public var scenes:GameObject;
var numScenes:int = 3;
public var currentCue:int=0;
public var currentEvent: int = 0;

@HideInInspector
var sceneArray: List.<GameObject> ;
private var canvasObject:GameObject;

function Start () {
	Network.InitializeServer(500,portNumber,true);
	Debug.Log("Initializing Server");
	
	scenes.SetActive(true);

	for (var i = 0; i  < numScenes+1 ; i++) {
		sceneArray.Add(GameObject.Find("Scene"+i)) ;
		if(sceneArray[i] != null){
			sceneArray[i].SetActive(false);
		}
	};

	canvasObject = sceneArray[0];
	

}

function Update () {
	
	currentCue = cueComponent.cueNumber;
	
	var net:NetworkView = this.gameObject.GetComponent.<NetworkView>();
	
	if(currentEvent != cueComponent.tempEventTriggers  ){
		currentEvent = cueComponent.tempEventTriggers ;
		switch( currentEvent ){
						case 1:
						cueComponent.playMovie("MoeTest");
						Debug.Log("MoeTest!");

						break;
				
						case 2:
						cueComponent.playMovie("kazoo");
						Debug.Log("kazoo!");

						break;
						
						
						default:
						break;
						}
						
	}

}

function OnGUI()
	{
		GUILayout.Label ("Connections: " + Network.connections.Length.ToString());
		GUILayout.Label ("Current Scene: " +  currentCue);
		GUILayout.Label ("Force Passive Mode: " +  cueComponent.forcePassive);

		
	}
	
	
function setActiveScene(newScene:String){
	var i=parseInt(newScene);
	if(i>=0){
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