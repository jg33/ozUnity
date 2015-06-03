using UnityEngine;
using System.Collections;

public class cueSystem : MonoBehaviour{
	public int cueNumber = 0;
	public int tempEventTriggers = 0;


	public void Update(){

		if (Network.isServer){
			if (Input.GetKeyDown (KeyCode.Space)) {
				cueNumber += 1;

			} else if (Input.GetKeyDown (KeyCode.PageDown)){
				cueNumber -= 1;

			} else if (Input.GetKeyDown(KeyCode.Alpha1)){
				tempEventTriggers = 1;

			} else if (Input.GetKeyDown(KeyCode.Alpha2)){
				tempEventTriggers = 2;
				
			} else if (Input.GetKeyDown(KeyCode.Alpha3)){
				tempEventTriggers = 3;
			} else if (Input.GetKeyDown(KeyCode.Alpha0)){
				tempEventTriggers = 0;
			}
		}	
	}

	public void GUI(){
		GUILayout.Label (cueNumber.ToString ());
	}

	private void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting) {
			stream.Serialize (ref cueNumber);
			stream.Serialize (ref tempEventTriggers);
		} else {
			stream.Serialize (ref cueNumber);
			stream.Serialize (ref tempEventTriggers);
		}
	}

	[RPC] public void vibrate(){
		Handheld.Vibrate();
	}
	
	[RPC] public void playMovie(string clipName){

		if(Network.isClient){

			if(clipName == "randomRainbow"){
				Debug.Log("played rainbow");
				//if(DeviceType.Handheld){
					//GameObject.Find("IOSVideoPlayer").SendMessage("ShouldPauseUnity", false);
					//GameObject.Find("IOSVideoPlayer").SendMessage("PlayVideo" , "Teaser_Final" );
					
					int rando = Random.Range(1,10);
					string videoString = string.Format("Video/rainbow_{0:00}.mp4", rando );
					Handheld.PlayFullScreenMovie(videoString);
				//}
			
			}
		
		}
	}
	
	[RPC] public void  stopMovie(){
		//GameObject.Find("IOSVideoPlayer").SendMessage(
		
		
	}
	
	[RPC] public void  playAudio(string clipName){
		if(Network.isClient){
			AudioSource source = (AudioSource)GameObject.Find("Main Camera").GetComponent<AudioSource>();

			if(clipName == "noPlace"){
				//AudioClip clip = AudioClip.Create
				source.Play();
			}

		}
		
	}
	
	[RPC] public void stopAudio(){

		
	}



}