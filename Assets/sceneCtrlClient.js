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

private var moviePosition:float = 0f;
private var currentEventCue:int = 0;

@HideInInspector
var sceneArray: List.<GameObject> ;
private var canvasObject:GameObject;
var timeoutCounter: int;

private var forcePassive: boolean;
private var camObj: GameObject;


function Awake(){
	DontDestroyOnLoad (this);
}


function Start () {

	#if UNITY_IPHONE
	iOS.NotificationServices.RegisterForNotifications(iOS.NotificationType.Alert);
	#endif
	Screen.sleepTimeout = SleepTimeout.NeverSleep;
	Network.Connect (connectionIP, portNumber);
	
	//GameObject.Find("Scenes").SetActive(true);
	//var sceneListComp: sceneList = GameObject.Find("Scenes").GetComponent.<sceneList>();
	
	

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
			
			camObj = GameObject.Find("Camera");
		
		} if (forcePassive && Application.loadedLevel != 1){
			Application.LoadLevel(1);
			
			
		}

		if (cueComponent.cueNumber != currentCue && Application.loadedLevel == 2){
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
					
					//Removed Sassy Cylinder. Sorry.
										
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
					
					case 4: //'Nado
					
					switch( currentEventCue ){
						case 1:
						///ALERT
						Debug.Log("ALERT!!!");
						#if UNITY_IPHONE
						var tornadoAlert: iOS.LocalNotification = new iOS.LocalNotification();
						tornadoAlert.alertAction = "Butts.";
						tornadoAlert.alertBody = "ALERT: FLASH FLOOD WARNING IN YOUR AREA";
						tornadoAlert.soundName = "cbs_alert_us";
						tornadoAlert.fireDate = Date.Now.AddSeconds(2);
						iOS.NotificationServices.ScheduleLocalNotification(tornadoAlert);
						#endif
						
						break;
						
						case 2:
						///TORNADO IN
						break;
						
						case 3:
						///TORNADO GROW
						break;
						
						case 4:
						///TORNADO OUT
						break;
						
					}
					break;
					
					
					case 5:
					
					switch( currentEventCue ){
						case 1:
						GameObject.Find("GlindaPath").GetComponent.<Animator>().SetTrigger("Anim1_11");
						Debug.Log("ImNumba5");

						break;
						
						case 2:
						GameObject.Find("Camera").GetComponent.<Animator>().SetTrigger("Anim1_M");
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
						cueComponent.playMovie("MoeTest");
						Debug.Log("MoeTest!");

						break;
				
						case 2:
						cueComponent.playMovie("kazoo");
						Debug.Log("kazoo!");

						break;
				
						case 3:
						cueComponent.stopMovie();

						break;
						
						case 4:
						
						cueComponent.playAudio("noPlace");
						Debug.Log("no place!");

						break;
						
						case 5:
						
						cueComponent.playMovie("randomRainbow");
						Debug.Log("no place!");

						break;
				
				
						default:
						break;
					  
					
					}
			
				}
		}
		
		
		if(cueComponent.moviePosition != moviePosition && camObj != null){
			moviePosition = cueComponent.moviePosition;
			camObj.SendMessage("syncToPosition", moviePosition, SendMessageOptions.DontRequireReceiver);	
			
			Debug.Log("sync to: " + moviePosition);
		} else if (camObj == null){
			camObj = GameObject.Find("Camera");

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
		canvasObject = sceneArray[i];
		yield WaitForSeconds(canvasObject.GetComponent(Animation).clip.length);
		Debug.Log(sceneArray[i]);
		canvasObject.SetActive(true);
		canvasObject.GetComponent(Animation).Play("UIFadeIn");
		
		canvasObject = sceneArray[prevCue];
		canvasObject.GetComponent(Animation).Play("UIFadeOut");
		canvasObject.SetActive(false);
	
	} else if (i<0){
		canvasObject.SetActive(false);

	}

}


