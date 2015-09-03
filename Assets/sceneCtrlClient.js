#pragma strict

public var connectionIP:String = "127.0.0.1";
public var portNumber:int = 16261;
private var connected:boolean = false;
var timeout: int = 300;

public var cueComponent:cueSystem;
//public var scenes:GameObject;
var numScenes:int = 3;
public var currentCue:int = 0;
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

private var transitionSpeed : int =1;

// one-shot events that fire on cue or never //
enum TriggeredEvents{ MOE_VIDEO, RANDOM_RAINBOW, TORNADO_ALERT, APPLAUSE, AWW, NO_PLACE };

function Awake(){
	DontDestroyOnLoad (this);
}


function Start () {

	Screen.sleepTimeout = SleepTimeout.NeverSleep;
	Network.Connect (connectionIP, portNumber);

	
	setupText();
	


}

function Update () {
	if (connected){	
		
		forcePassive = cueComponent.forcePassive;
				
		  if (Application.loadedLevel != 2 && !forcePassive){ //if connected and in the show, jump on in!
			//GameObject.Destroy(GameObject.Find("Camera Container"));
			//yield WaitForSeconds(1);
			GameObject.Find("Camera Container").SendMessage("setTightTracking", false);
			GameObject.Find("Look Up").GetComponent(Renderer).enabled = true;

			Debug.Log("Connected! Loading Active Mode!");
			Application.LoadLevel(2);
			currentCue = -1; //force scene refresh
			camObj = GameObject.Find("Camera");
			GameObject.Find("RealImageTarget").SendMessage("updateTargetPos");

		
		} else if(Application.loadedLevel == 1 && forcePassive){ //if connected and in passive mode show connected text
			var alert:GameObject = GameObject.Find("InstructionAlertText");
			alert.GetComponent(UI.Text).text = "You are now connected. Enjoy the Show!";
			
		} else if (forcePassive && Application.loadedLevel != 1){
			//GameObject.Destroy(GameObject.Find("Camera Container"));
			//yield WaitForSeconds(1);
			GameObject.Find("Camera Container").SendMessage("setTightTracking", true);			
			GameObject.Find("Look Up").GetComponent(Renderer).enabled = false;

			Application.LoadLevel(1);

		} 

		/// cue control ///
		if (cueComponent.cueNumber != currentCue && Application.loadedLevel == 2){
			transitionSpeed = cueComponent.transitionSpeed;
			setActiveScene(cueComponent.cueNumber.ToString());
			GameObject.Find("RealImageTarget").SendMessage("updateTargetPos");

		} else if (cueComponent.cueNumber == 1 && Application.loadedLevel == 2 && !GameObject.Find("Scene1")){
			setActiveScene("1"); // emergency backup goto 1
		}



		/// Event Cueing ///
		if (cueComponent.tempEventTriggers != currentEventCue){
			currentEventCue = cueComponent.tempEventTriggers;
		
				switch( currentEventCue ){
						case 1:
						cueComponent.playMovie("MoeTest");
						Debug.Log("MoeTest!");

						break;
				
						case 2:
							cueComponent.playMovie("NoPlaceLikeMoe");
							Debug.Log("no place!");

						break;
				
						case 3:
						cueComponent.stopMovie();

						break;
						
						case 4:
						
						cueComponent.playAudio("noPlace");
						Debug.Log("no place audio!");

						break;
						
						case 5:
						
						cueComponent.playMovie("randomRainbow");
						Debug.Log("random rainbow!");

						break;
						
						case 6:
							camObj = GameObject.Find("Camera");
							var clip :AudioClip= Resources.Load("Audience_Applause_1");
							camObj.GetComponent(AudioSource).clip = clip;
							camObj.GetComponent(AudioSource).Play(); 
							Debug.Log("APPLAUSE!");
						break;
						
						case 7:
							cueComponent.playMovie("NoPlaceLikeMoe");
							Debug.Log("no place!");

						break;
						
						case 8:
							cueComponent.playMovie("tvStatic");
							Debug.Log("Static!");
							break;
						
						case 9:
							cueComponent.playMovie("scratches");
							Debug.Log("scratches!");
							break;

						case 10:
							setCompletedShow();
						break;
						
						default: 
						break;
						

					  
					
					}

		}
		
		
		/// Text Cueing ///
		if (cueComponent.textSelection != currentTextSelection && Application.loadedLevel == 2){
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
		
		/// Movie Sync ///
		if(cueComponent.moviePosition != moviePosition && camObj != null){
			moviePosition = cueComponent.moviePosition;
			camObj.SendMessage("syncToPosition", moviePosition, SendMessageOptions.DontRequireReceiver);	
			
			//Debug.Log("sync to: " + moviePosition);
		} else if (camObj == null){
			camObj = GameObject.Find("Camera");

		}

		
		
	} else{ //not connected
	
		if (Application.loadedLevel != 1 ){
			
			//GameObject.Destroy(GameObject.Find("Camera Container"));
			//yield WaitForSeconds(1);
			Debug.Log("Disconnected! Loading Passive Mode! :'(");
			GameObject.Find("Camera Container").SendMessage("setTightTracking", true);
			GameObject.Find("Look Up").GetComponent(Renderer).enabled = false;
			Application.LoadLevel(1);
			

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
		var alert:GameObject = GameObject.Find("InstructionAlertText");
		alert.GetComponent(UI.Text).text = "You are now connected. Enjoy the Show!";
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

	Debug.Log("SETTING ACTIVE SCENE: " + newScene);
	prevCue = currentCue;
	currentCue = cueComponent.cueNumber;
	
	#if UNITY_IPHONE || UNITY_ANDROID
	if (currentCue != 0 &&  currentCue != 1) Handheld.Vibrate();
	#endif
	
	var i=parseInt(newScene);
	
	sceneArray = GameObject.Find("Scenes").GetComponent.<sceneList>().sceneArray;
	
	if(i == 1){
		canvasObject = sceneArray[i];
		Debug.Log(sceneArray[i]);
		canvasObject.SetActive(true);
//		animation["UIFadeIn"].speed = transitionSpeed;
		canvasObject.GetComponent(Animation).Play("UIFadeIn");
	
		yield WaitForSeconds(canvasObject.GetComponent(Animation).clip.length+3);

		for (var j = 0; j< sceneArray.Count  ;j++){ //turn off the rest
			if(j!=i){
				canvasObject = sceneArray[j];
				canvasObject.SetActive(false);
			}
		}
		
	} else if(i>1){
		canvasObject = sceneArray[i];
		Debug.Log(sceneArray[i]);
		canvasObject.SetActive(true);
		
		canvasObject = sceneArray[prevCue];
//		animation["UIFadeOut"].speed = transitionSpeed;
		canvasObject.GetComponent(Animation).Play("UIFadeOut");
		yield WaitForSeconds(canvasObject.GetComponent(Animation).clip.length+3);
		for (j = 0; j< sceneArray.Count  ;j++){ //turn off the rest
			if(j!=i){
				canvasObject = sceneArray[j];
				canvasObject.SetActive(false);
			}
		}
		
	
	} else if (i==0){
		for (j = 0; j< sceneArray.Count  ;j++){
			canvasObject = sceneArray[j];
//			canvasObject.GetComponent(Animation).Play("UIFadeOut");
			canvasObject.SetActive(false);
		}
		canvasObject = sceneArray[0];
		canvasObject.SetActive(true);

	}

}


function handleEvent(state:TriggeredEvents){
	switch (state){
		case (TriggeredEvents.APPLAUSE):
		
		break;
		
		
		default:
		Debug.Log("unknown state: "+ state);
		break;

	}

}

function setupText(){

	messageText[0]= "There's only one way to succeed in this business. Step on those guys. Gouge their eyes out. Trample on them. Kick them in the balls. You'll be a smash.";
	messageText[1]= "App ready... Please wait.";
	messageText[2]= " ";
	messageText[3]= "'Ding Dong' reached number 2 in the UK Singles Chart following the death of Margaret Thatcher on 8 April 2013.";
	messageText[4]= "Which";
	messageText[5]= "Golden Snitch";
	messageText[6]= "Scratch an itch";
	messageText[7]= "In economics, bimetallism is a monetary standard in which the value of the monetary unit is defined as equivalent both[1] to a certain quantity of gold and to a certain quantity of silver; such a system establishes a fixed rate of exchange between the two metals. The defining characteristics of bimetallism are[2] Both gold and silver money are legal tender in unlimited amounts. The government will convert both gold and silver into legal tender coins at a fixed rate for individuals in unlimited quantities. This is called free coinage because the quantity is unlimited, even if a fee is charged.The combination of these conditions distinguishes bimetallism from a limping standard, where both gold and silver are legal tender but only one is freely coined (example: France, Germany, or the United States after 1873), or trade money where both metals are freely coined but only one is legal tender and the other is trade money (example: most of the coinage of western Europe from the 1200s to 1700s.) Economists also distinguish legal bimetallism, where the law guarantees these conditions, and de facto bimetallism where both gold and silver coins actually circulate at a fixed rate.";
	messageText[8]= "There's only one way to succeed in this business. Step on those guys. Gouge their eyes out. Trample on them. Kick them in the balls. You'll be a smash.";
	messageText[9]= "Scratch an itch";

}




function setCompletedShow(){
	
	PlayerPrefs.SetInt("CompletedShow",1);
	PlayerPrefs.Save();

}
