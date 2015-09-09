using UnityEngine;
using System.Collections;

public class cueSystem : MonoBehaviour{
	public int cueNumber = 0;
	public int tempEventTriggers = 0;
	public float moviePosition = 0;
	public int textSelection = 0;
	public float transitionSpeed = 1;

	public int tornadoState;
	public int poppyState;
	public int munchkinState;
	public int monkeyState;
	public int fireState;

	public bool forcePassive = false;

	NetworkView nv;

	private GameObject seqPlayer;

	public void Start(){

		nv = this.GetComponent<NetworkView>();
	}

	public void Update(){

		if (Network.isServer){
			if (Input.GetKeyDown (KeyCode.Space)   || Input.GetKeyDown (KeyCode.UpArrow) ) {
				cueNumber += 1;

			} else if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.PageDown) ){
				cueNumber -= 1;

			} else if (Input.GetKeyDown(KeyCode.Alpha1)){
				cueNumber = 1;
			} else if (Input.GetKeyDown(KeyCode.Alpha2)){
				cueNumber = 2;
			} else if (Input.GetKeyDown(KeyCode.Alpha3)){
				cueNumber = 3;
			} else if (Input.GetKeyDown(KeyCode.Alpha4)){
				cueNumber = 4;
			} else if (Input.GetKeyDown(KeyCode.Alpha5)){
				cueNumber = 5;
			} else if (Input.GetKeyDown(KeyCode.Alpha6)){
				cueNumber = 6;
			} else if (Input.GetKeyDown(KeyCode.Alpha7)){
				cueNumber = 7;
			} else if (Input.GetKeyDown(KeyCode.Alpha8)){
				cueNumber = 8;
			} else if (Input.GetKeyDown(KeyCode.Alpha9)){
				cueNumber = 9;
			} else if (Input.GetKeyDown(KeyCode.Alpha0)){
				cueNumber = 0;
			} else if (Input.GetKeyDown(KeyCode.Q)){
				tempEventTriggers = 1;
			} else if (Input.GetKeyDown(KeyCode.W)){
				tempEventTriggers = 2;
			} else if (Input.GetKeyDown(KeyCode.E)){
				tempEventTriggers = 3;
			} else if (Input.GetKeyDown(KeyCode.R)){
				tempEventTriggers = 4;
			} else if (Input.GetKeyDown(KeyCode.T)){
				tempEventTriggers = 5;
			} else if (Input.GetKeyDown(KeyCode.Y)){
				tempEventTriggers = 6;
			} else if (Input.GetKeyDown(KeyCode.Tab)){
				tempEventTriggers = 0;
			} else if (Input.GetKeyDown(KeyCode.M)){
			
			}

			else if (Input.GetKeyDown(KeyCode.P)){
				forcePassive = !forcePassive;
			} else if( Input.GetKeyDown(KeyCode.A) ){

				nv.RPC("vibrate", RPCMode.All);

			}
		}	

		if(!seqPlayer && cueNumber ==1){
			seqPlayer = GameObject.Find ("TexturePlayer");
		}
	}

	public void GUI(){
		GUILayout.Label (cueNumber.ToString ());
	}

	private void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info){
		if (stream.isWriting) {
			stream.Serialize (ref cueNumber);
			stream.Serialize (ref tempEventTriggers);
			stream.Serialize (ref forcePassive);
			stream.Serialize (ref moviePosition);
			stream.Serialize (ref textSelection);
			stream.Serialize (ref tornadoState);
			stream.Serialize (ref munchkinState);
			stream.Serialize (ref poppyState);
			stream.Serialize (ref monkeyState);
			stream.Serialize (ref fireState);




		} else {
			stream.Serialize (ref cueNumber);
			stream.Serialize (ref tempEventTriggers);
			stream.Serialize (ref forcePassive);
			stream.Serialize (ref moviePosition);
			stream.Serialize (ref textSelection);
			stream.Serialize (ref tornadoState);
			stream.Serialize (ref munchkinState);
			stream.Serialize (ref poppyState);
			stream.Serialize (ref monkeyState);
			stream.Serialize (ref fireState);
		}
	}

	[RPC] public void vibrate(){
		#if UNITY_IPHONE || UNITY_ANDROID
		Handheld.Vibrate();
		#endif
	}
	
	[RPC] public void playMovie(string clipName){
		GameObject cam = GameObject.Find ("Camera");

		if(Network.isClient && cam.GetComponent<Camera>() ){

			if(clipName == "randomRainbow"){
				Debug.Log("randomRainbow");
				Random.seed = Time.frameCount;
				int rando = Random.Range(1,7);
				string videoString = string.Format("Video/rainbow_{0:00}.mp4", rando );
					#if UNITY_IPHONE
					Handheld.PlayFullScreenMovie(videoString);
					#elif UNITY_ANDROID
					string path = Application.persistentDataPath + "/" + videoString;
					Debug.LogError( "The video path: " + path );
					Handheld.PlayFullScreenMovie( Application.persistentDataPath + "/" + videoString);
					#endif
			
			} else if(clipName == "MoeTest"){
				cam.SendMessage("loadMovie", "MoeOzTest", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("setNumLoops", 1);
				cam.SendMessage("setLooping", false);
				cam.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				Debug.Log("MoeTest");

			} else if(clipName == "kazoo"){
				cam.SendMessage("loadMovie", "rainbowAudioTest", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("setNumLoops", 1);
				cam.SendMessage("setLooping", false);
				cam.SendMessage ("setLastFrame", 1968);
				cam.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("Play", SendMessageOptions.DontRequireReceiver);

				
				Debug.Log("kazooooooo");

			} else if(clipName == "NoPlaceLikeMoe"){
				cam.SendMessage("loadMovie", "NoPlaceLikeMoe", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("setNumLoops", 5);
				cam.SendMessage("setLooping", true);
				cam.SendMessage("setLastFrame",149);
				cam.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				Debug.Log("NoPlaceLikeMoe");
				
			} else if(clipName == "scratches"){
				seqPlayer.SendMessage("setFrames", 117);
				seqPlayer.SendMessage("loadMovie","scratchSm" );
				seqPlayer.SendMessage("play");
			} else if(clipName == "tvStatic"){
				seqPlayer.SendMessage("setFrames", 70);
				seqPlayer.SendMessage("loadMovie","tvStatic" );
				seqPlayer.SendMessage("play");
			}

		
		} else if (Network.isServer){

			GameObject video = GameObject.Find ("Video");

			if(clipName == "MoeTest"){
				video.SendMessage("loadMovie", "MoeOzTest", SendMessageOptions.DontRequireReceiver);
				video.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				video.SendMessage("setNumLoops", 1);
				video.SendMessage("setLooping", false);
				video.SendMessage("setLastFrame",299);

				video.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				//video.SendMessage("setNumLoops", 1);

				Debug.Log("moeTest");
				
			} else if(clipName == "kazoo"){
				video.SendMessage("loadMovie", "rainbowAudioTest", SendMessageOptions.DontRequireReceiver);
				video.SendMessage("setNumLoops", 1);
				video.SendMessage("setLooping", false);
				video.SendMessage("setLastFrame",1968);

				video.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
			
				video.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				//video.SendMessage("setNumLoops", 1);

			} else if(clipName == "NoPlaceLikeMoe"){
				video.SendMessage("loadMovie", "NoPlaceLikeMoe", SendMessageOptions.DontRequireReceiver);
				video.SendMessage("setNumLoops", 5);
				video.SendMessage("setLooping", true);
				video.SendMessage("setLastFrame",149);

				video.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				video.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				Debug.Log("NoPlaceLikeMoe");
				
			} else{
				Debug.Log("Server cannot play "+clipName);
			}
		}
	}
	
	[RPC] public void  stopMovie(){

		if (Network.isClient){
			GameObject.Find ("Camera").SendMessage("Stop");
			seqPlayer.SendMessage("stop");

		}else if (Network.isServer){
			GameObject.Find ("Video").SendMessage("Stop");
		}

		
	}
	
	[RPC] public void  playAudio(string clipName){

		if(Network.isClient){
			AudioSource source = (AudioSource)GameObject.Find("Camera").GetComponent<AudioSource>();
			AudioClip clip;

			source.Stop();

			switch (clipName){
			case "applause":
				clip = Resources.Load ("Audience_Applause_1") as AudioClip;
				break;
			case "noPlace":
				clip = Resources.Load ("no_place_like_home2") as AudioClip;
				break;
			case "cicada":
				clip = Resources.Load ("cicada") as AudioClip;
				break;
			case "cicada2":
				clip = Resources.Load ("cicada2") as AudioClip;
				break;
			case "frogs1":
				clip = Resources.Load ("frogs1") as AudioClip;				
				break;
			case "frogs2":
				clip = Resources.Load ("frogs2") as AudioClip;				
				break;
			case "drums1":
				clip = Resources.Load ("DRUMS-76") as AudioClip;				
				break;
			case "drums2":
				clip = Resources.Load ("riser drum open") as AudioClip;				
				break;
			case "wind":
				clip = Resources.Load ("wind") as AudioClip;	
				break;
			case "wind2":
				clip = Resources.Load ("wind2") as AudioClip;	
				break;
			case "wind3":
				clip = Resources.Load ("wind3") as AudioClip;	
				break;
			case "wind4":
				clip = Resources.Load ("wind4") as AudioClip;
				break;

			default:
				clip = Resources.Load ("0") as AudioClip;
				break;
			}
			source.clip = clip;
			source.Play();

		}
		
	}
	
	[RPC] public void stopAudio(){
		AudioSource source = (AudioSource)GameObject.Find("Camera").GetComponent<AudioSource>();
		source.Stop();
		
	}

	public void setTransitionSpeed(float f){
		transitionSpeed = f;
	}


	public void selectText( int i){
		textSelection = i;
	}

	public void setTornadoState(float i){
		tornadoState = (int)i;
	}

	public void setPoppyState(float i){
		poppyState = (int) i;
	}

	public void setMunchkinState(float i){
		munchkinState = (int) i;
	}

	public void setMonkeyState(float i){
		monkeyState = (int) i;
	}
	public void setFireState(float i){
		fireState = (int) i;
	}
}