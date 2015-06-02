using UnityEngine;
using System.Collections;

public class cueSystem : MonoBehaviour{
	public int cueNumber = 0;

	public void Update(){
		if (Network.isServer && Input.GetKeyDown (KeyCode.Space)) {
			cueNumber += 1;

		}
			if (Network.isServer && Input.GetKeyDown (KeyCode.PageDown)){
				cueNumber -=1;
			}
		
	}

	public void GUI(){
		GUILayout.Label (cueNumber.ToString ());
	}

	private void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting) {
			stream.Serialize (ref cueNumber);
		} else {
			stream.Serialize (ref cueNumber);
		}
	}

	[RPC] void vibrate(){
		Handheld.Vibrate();
	}
	
	[RPC] void playMovie(string clipName){

		if(Network.isClient){

			if(clipName == "randomRainbow"){
				Debug.Log("played rainbow");
				//if(DeviceType.Handheld){
					//GameObject.Find("IOSVideoPlayer").SendMessage("ShouldPauseUnity", false);
					//GameObject.Find("IOSVideoPlayer").SendMessage("PlayVideo" , "Teaser_Final" );
					Handheld.PlayFullScreenMovie("Teaser_Final");
				//}
			
			}
		
		}
	}
	
	[RPC] void  stopMovie(){
		//GameObject.Find("IOSVideoPlayer").SendMessage(
		
		
	}
	
	[RPC] void  playAudio(string clipName){
		if(Network.isClient){
			AudioSource source = (AudioSource)GameObject.Find("Main Camera").GetComponent<AudioSource>();

			if(clipName == "noPlace"){
				//AudioClip clip = AudioClip.Create
				source.Play();
			}

		}
		
	}
	
	[RPC] void stopAudio(){
		
		
	}



}