using UnityEngine;
using System.Collections;

public class cueSystem : MonoBehaviour{
	public int cueNumber = 0;
	public int tempEventTriggers = 0;
	public float moviePosition = 0;
	public int textSelection = 0;

	public int tornadoState;
	public int poppyState;
	public int munchkinState;
	public int monkeyState;

	public bool forcePassive = false;

	NetworkView nv;

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

		}
	}

	[RPC] public void vibrate(){
		#if UNITY_IPHONE
		Handheld.Vibrate();
		#endif
	}
	
	[RPC] public void playMovie(string clipName){
		GameObject cam = GameObject.Find ("Camera");

		if(Network.isClient && cam.GetComponent<Camera>() ){

			if(clipName == "randomRainbow"){
				Debug.Log("randomRainbow");
				int rando = Random.Range(1,7);
				string videoString = string.Format("Video/rainbow_{0:00}.mov", rando );
					#if UNITY_IPHONE
					Handheld.PlayFullScreenMovie(videoString);
					#endif
			
			} else if(clipName == "MoeTest"){
				cam.SendMessage("loadMovie", "MoeOzTest", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("setNumLoops", 1);
				cam.SendMessage("setLooping", false);
				cam.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				Debug.Log("MoeTest");

			} else if(clipName == "kazoo"){
				cam.SendMessage("loadMovie", "rainbow04_", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("setNumLoops", 1);

				Debug.Log("kazooooooo");

			} else if(clipName == "NoPlaceLikeMoe"){
				cam.SendMessage("loadMovie", "NoPlaceLikeMoe", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("setNumLoops", 5);
				cam.SendMessage("setLooping", true);
				cam.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("Play", SendMessageOptions.DontRequireReceiver);

				Debug.Log("NoPlaceLikeMoe");
				
			} else if (clipName == "Scratches"){
				cam.SendMessage("loadMovie", "Scratches", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				cam.SendMessage("setNumLoops", 1);
				
				Debug.Log("Scratches");
			}

		
		} else if (Network.isServer){

			GameObject video = GameObject.Find ("Video");

			if(clipName == "MoeTest"){
				video.SendMessage("loadMovie", "MoeOzTest", SendMessageOptions.DontRequireReceiver);
				video.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				video.SendMessage("setNumLoops", 1);
				video.SendMessage("setLooping", false);
				video.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				//video.SendMessage("setNumLoops", 1);

				Debug.Log("moeTest");
				
			} else if(clipName == "kazoo"){
				video.SendMessage("loadMovie", "rainbow04_", SendMessageOptions.DontRequireReceiver);
				video.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				video.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				//video.SendMessage("setNumLoops", 1);

			} else if(clipName == "NoPlaceLikeMoe"){
				video.SendMessage("loadMovie", "NoPlaceLikeMoe", SendMessageOptions.DontRequireReceiver);
				video.SendMessage("setNumLoops", 5);
				video.SendMessage("setLooping", true);
				video.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				video.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				Debug.Log("NoPlaceLikeMoe");
				
			} else if (clipName == "Scratches"){
				video.SendMessage("loadMovie", "Scratches", SendMessageOptions.DontRequireReceiver);
				video.SendMessage("gotoPosition", 0.01f, SendMessageOptions.DontRequireReceiver);
				video.SendMessage("Play", SendMessageOptions.DontRequireReceiver);
				video.SendMessage("setNumLoops", 1);
				
				Debug.Log("Server Scratch");
			}

		}
	}
	
	[RPC] public void  stopMovie(){

		if (Network.isClient){
			GameObject.Find ("Camera").SendMessage("Stop");

		}else if (Network.isServer){
			GameObject.Find ("Video").SendMessage("Stop");
		}

		
	}
	
	[RPC] public void  playAudio(string clipName){
		if(Network.isClient){
			AudioSource source = (AudioSource)GameObject.Find("Camera").GetComponent<AudioSource>();

			if(clipName == "noPlace"){
				AudioClip clip = Resources.Load ("no_place_like_home2") as AudioClip;
				source.Play();
			}

		}
		
	}
	
	[RPC] public void stopAudio(){

		
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

}