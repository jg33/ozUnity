#pragma strict

public var connectionIP:String = "127.0.0.1";
public var portNumber:int = 16261;
private var connected:boolean = false;
var timeout: int = 300;

public var cueComponent:cueSystem;
public var scenes:GameObject;
var numScenes:int = 3;
public var currentCue:int=0;

private var currentEventCue:int = 0;

@HideInInspector
var sceneArray: List.<GameObject> ;
private var canvasObject:GameObject;
var timeoutCounter: int;

public var Trashcan : GameObject;
public var FunkyCube : GameObject;
public var NewCameraPath : GameObject;




function Awake(){
	DontDestroyOnLoad (this);
	}

function Start () {
	Screen.sleepTimeout = SleepTimeout.NeverSleep;
	Network.Connect (connectionIP, portNumber);
	
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
	if (connected){
	
		if (cueComponent.cueNumber != currentCue){
			currentCue = cueComponent.cueNumber;
			setActiveScene(currentCue.ToString());
		
		}  
		
		if (cueComponent.tempEventTriggers != currentEventCue){
			Debug.Log("event trigger!");
			currentEventCue = cueComponent.tempEventTriggers;				
				switch(currentCue){
				
					case 1:
					
					switch( currentEventCue ){
						case 1:
	
						Trashcan.GetComponent.<Animator>().SetTrigger("Anim1");
																	Debug.Log("ugh");

						break;
					
						case 2:
						
						Trashcan.GetComponent.<Animator>().SetTrigger("Anim2");

						break;
						
					}
					
					break;
					
					case 2:
					
					switch( currentEventCue ){
						case 1:
						
						FunkyCube.GetComponent.<Animator>().SetTrigger("Anim1_1");
											Debug.Log("ImNumba1");

						break;
					
						case 2:
						FunkyCube.GetComponent.<Animator>().SetTrigger("Anim2_1");
							Debug.Log("ImNumba2");

						break;
					}
					break;
					
					case 5:
					
					switch( currentEventCue ){
						case 1:
						NewCameraPath.GetComponent.<Animator>().SetTrigger("Anim1_11");
											Debug.Log("ImNumba5");

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
		
		
	} else if(timeoutCounter> timeout){
		
		Network.Connect (connectionIP, portNumber);
		Debug.Log("Not Connected! "+ Network.peerType );
		timeoutCounter =0;

		
	} else if (timeoutCounter <= timeout){
		timeoutCounter++;
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
	if(i>=0){
		//Application.LoadLevel(i);
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


