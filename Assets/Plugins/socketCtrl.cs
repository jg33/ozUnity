using UnityEngine;
using System.Collections;
using System; 

using WebSocketSharp;


public class socketCtrl : MonoBehaviour {
	public String hostIp = "192.168.1.69";

	WebSocket client = new WebSocket("ws://192.168.1.66:14949");


	public String currentScene = "0";
	public String currentMsg;
	int disconnectedCounter = 0;
	int retryEvery = 500;
	bool isConnected = false;

	bool readyToChangeScene = false;
	bool readyToSyncTime = false;

	float startTime;
	public float localTime;

	
	bool sendDeviceID; 
	// Use this for initialization
	void Start () {

		startTime = Time.time*1000;
		client.OnMessage += (sender, e) => {
			Debug.Log ("message: " + e.Data.ToString());
			var message = e.Data.Split(' ');

			if(message[0] == "/sceneChange"){
				currentScene = message[1];
				Debug.Log("scene set to " + message[1]);
				readyToChangeScene = true;
			} else if (message[0] == "/time"){
				//Debug.Log("Local Time is: "+localTime);

				if( Mathf.Abs( localTime - float.Parse(message[1] ) )> 50 ){
					localTime = float.Parse(message[1]);
					readyToSyncTime = true;
					}

			} else if (message[0] == "/welcome"){
				if (message[1] != currentScene) {
					currentScene = message[1];
					readyToChangeScene = true;
					sendDeviceID = true;
				}
				

			}

		};

		client.OnOpen += (sender, e) => {
			Debug.Log ("opened");
			isConnected = true;
			disconnectedCounter =0;
		};

		client.OnClose += (sender, e) => {
			Debug.Log ("lost connection!");
			isConnected = false;
			client.Connect();
		};

		client.Connect();

	}
	
	// Update is called once per frame
	void Update () {
		if(readyToSyncTime) {	
			var newTime = (Time.time*1000) - localTime; 
			var difference = Mathf.Abs(newTime-startTime);
			Debug.Log("Had to ReSync. "+ difference+ " milliseconds off" );
			startTime = newTime;
			readyToSyncTime = false;


		}
		localTime = (Time.time*1000) - startTime;


		if(sendDeviceID){
			client.Send("/deviceID "+ SystemInfo.deviceUniqueIdentifier);
			sendDeviceID=false;
		}

		if (!isConnected && disconnectedCounter > retryEvery) {
			Debug.Log("Attempting Retry");
			client.Connect();

		} else if (!isConnected) {
			disconnectedCounter++;
		} 

		if(readyToChangeScene){
			BroadcastMessage("setActiveScene", currentScene);
			readyToChangeScene = false;
		}

	}

	public void hitMe(){
				client.Send ("/getScene");
		}

	public void setScene(String i){
			client.Send("/setScene " + i);
	}

	public void sendMsg(String i){
			client.Send(i);
			currentMsg = i;
	}

	public void sendAgain(){
		client.Send(currentMsg);
	}

	public float getLocalTime(){
		return localTime;
	}


///CUSTOM SEND MESSAGES
	public void setHome(String i){
		currentMsg = "/home "+ i;
	}

}

