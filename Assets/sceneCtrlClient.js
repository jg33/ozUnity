#pragma strict

public var connectionIP:String = "127.0.0.1";
public var portNumber:int = 16261;
private var connected:boolean = false;
var timeout: int = 300;

public var cueComponent:cueSystem;
//public var scenes:GameObject;
var numScenes:int = 3;
public var currentCue:int=0;
@HideInInspector public var prevCue:int =0;

private var moviePosition:float = 0f;
private var currentEventCue:int = 0;

@HideInInspector
var sceneArray: List.<GameObject> ;
private var canvasObject:GameObject;
var timeoutCounter: int;

private var forcePassive: boolean;
private var camObj: GameObject;

private var messageText:String[] = new String[10];
private var currentTextSelection: int = 0;

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
	
	
	setupText();

	

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
		
		if (cueComponent.textSelection != currentTextSelection){
			var msg:GameObject = GameObject.Find("Message");
			var msgTxt: UI.Text = msg.GetComponent(UI.Text);
			currentTextSelection = cueComponent.textSelection;
			msgTxt.text = messageText[currentTextSelection];
			if (messageText[currentTextSelection].Length>140){
				//msg.transform.GetComponent(RectTransform).anchorMin = Vector2(0,-400);
				//msg.transform.GetComponent(RectTransform).anchorMax = Vector2(0,800);
			} else {
				//msg.transform.GetComponent(RectTransform).anchorMin = Vector2(0,-150);
				//msg.transform.GetComponent(RectTransform).anchorMax = Vector2(0,300);
			}
		}
		
		if (cueComponent.tempEventTriggers != currentEventCue){
			Debug.Log("event trigger!");
			currentEventCue = cueComponent.tempEventTriggers;
			
				switch(currentCue){
					case 1:
						switch( currentEventCue ){
							case 1:
							cueComponent.playMovie("MoeTest");
							Debug.Log("MoeTest!");
							break;
						
							case 2:
							cueComponent.stopMovie();
							break;
						
							case 3:
							camObj = GameObject.Find("Camera");
							var clip :AudioClip= Resources.Load("Audience_Applause_1");
							camObj.GetComponent(AudioSource).clip = clip;
							camObj.GetComponent(AudioSource).Play();
							break;
						
							case 4:
							break;
						
							case 5:
							break;
						
							case 6:
							break;
						
							default:
						
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
						//TORNADO ComeIn
						GameObject.Find("Tornado").GetComponent.<Animator>().SetTrigger("TornadoTrigger1");
						
						break;
						
						case 3:
						///TORNADO Move To Loop2
						GameObject.Find("Tornado").GetComponent.<Animator>().SetTrigger("TornadoTrigger2");
						break;
						
						case 4:
						///TORNADO OUT
						GameObject.Find("Tornado").GetComponent.<Animator>().SetTrigger("TornadoTrigger3");

						break;
						
						default:
						
						break;
						
					}
					break;
					
					
					case 5:
					
					switch( currentEventCue ){
						case 1:
						GameObject.Find("GlindaPath").GetComponent.<Animator>().SetTrigger("Anim1_11");

						break;
						
						case 2:
						GameObject.Find("Camera").GetComponent.<Animator>().SetTrigger("Anim1_M");

						break;
					}
					break;
					
					case 9:
					
					switch( currentEventCue ){
						case 1:
						
						var go:GameObject = Instantiate (Resources.Load ("pp")) as GameObject; 
						go.transform.parent = GameObject.Find("Scene9").transform;
						Debug.Log("poppyClump1");

						break;
					
						case 2:
						
						var go2:GameObject  = Instantiate (Resources.Load ("pp")) as GameObject; 
						go2.transform.parent = GameObject.Find("Scene9").transform;
						Debug.Log("poppyClump2");
						
						break;

					}
					break;
					
					case 0:
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
			
			//Debug.Log("sync to: " + moviePosition);
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


function setupText(){

	messageText[0]= "There's only one way to succeed in this business. Step on those guys. Gouge their eyes out. Trample on them. Kick them in the balls. You'll be a smash.";
	messageText[1]= "I want to make beautiful pictures about beautiful people.";
	messageText[2]= "The most expensive movie ever made today is 'Pirates of the Caribbean: On Stranger Tides' ";
	messageText[3]= "'Ding Dong' reached number 2 in the UK Singles Chart following the death of Margaret Thatcher on 8 April 2013.";
	messageText[4]= "Which";
	messageText[5]= "Golden Snitch";
	messageText[6]= "Scratch an itch";
	messageText[7]= "In economics, bimetallism is a monetary standard in which the value of the monetary unit is defined as equivalent both[1] to a certain quantity of gold and to a certain quantity of silver; such a system establishes a fixed rate of exchange between the two metals. The defining characteristics of bimetallism are[2] Both gold and silver money are legal tender in unlimited amounts. The government will convert both gold and silver into legal tender coins at a fixed rate for individuals in unlimited quantities. This is called free coinage because the quantity is unlimited, even if a fee is charged.The combination of these conditions distinguishes bimetallism from a limping standard, where both gold and silver are legal tender but only one is freely coined (example: France, Germany, or the United States after 1873), or trade money where both metals are freely coined but only one is legal tender and the other is trade money (example: most of the coinage of western Europe from the 1200s to 1700s.) Economists also distinguish legal bimetallism, where the law guarantees these conditions, and de facto bimetallism where both gold and silver coins actually circulate at a fixed rate.";
	messageText[8]= "There's only one way to succeed in this business. Step on those guys. Gouge their eyes out. Trample on them. Kick them in the balls. You'll be a smash.";
	messageText[9]= "Scratch an itch";
	messageText[10]= "Which";
	messageText[11]= "Golden Snitch";
	messageText[12]= "Scratch an itch";

}


