using UnityEngine;
using System.Collections;

public class cueSystem : MonoBehaviour{
	public int cueNumber = 0;
	public int tempEventTriggers = 0;

	public bool forcePassive = false;


	public void Update(){

		if (Network.isServer){
			if (Input.GetKeyDown (KeyCode.Space)   || Input.GetKeyDown (KeyCode.UpArrow) ) {
				cueNumber += 1;

			} else if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.PageDown) ){
				cueNumber -= 1;

			} else if (Input.GetKeyDown(KeyCode.Alpha1)){
				tempEventTriggers = 1;

			} else if (Input.GetKeyDown(KeyCode.Alpha2)){
				tempEventTriggers = 2;
				
			} else if (Input.GetKeyDown(KeyCode.Alpha3)){
				tempEventTriggers = 3;
			} else if (Input.GetKeyDown(KeyCode.Alpha0)){
				tempEventTriggers = 0;
			} else if (Input.GetKeyDown(KeyCode.P)){
				forcePassive = !forcePassive;
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

		} else {
			stream.Serialize (ref cueNumber);
			stream.Serialize (ref tempEventTriggers);
			stream.Serialize (ref forcePassive);

		}
	}

	[RPC] public void vibrate(){
		#if UNITY_IPHONE
		Handheld.Vibrate();
		#endif
	}
	
	[RPC] public void playMovie(string clipName){

		if(Network.isClient){

			if(clipName == "randomRainbow"){
				Debug.Log("played rainbow");
					int rando = Random.Range(1,10);
					string videoString = string.Format("Video/rainbow_{0:00}.mp4", rando );
					#if UNITY_IPHONE
					Handheld.PlayFullScreenMovie(videoString);
					#endif
			
			}
		
		}
	}
	
	[RPC] public void  stopMovie(){
		//GameObject.Find("IOSVideoPlayer").SendMessage(
		
		
	}
	
	[RPC] public void  playAudio(string clipName){
		if(Network.isClient){
			AudioSource source = (AudioSource)GameObject.Find("Camera").GetComponent<AudioSource>();

			if(clipName == "noPlace"){
				//AudioClip clip = AudioClip.Create
				source.Play();
			}

		}
		
	}
	
	[RPC] public void stopAudio(){

		
	}



}