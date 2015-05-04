using UnityEngine;
using System.Collections;
using System; 

using WebSocketSharp;


public class socketCtrl : MonoBehaviour {
	public String hostIp = "192.168.1.69";
	public String hostPort = "14949";

	WebSocket client = new WebSocket("ws://127.0.0.1:14949");
	//client = new WebSocket("ws://"+hostIp+":"+"14949");
	//client.setHost("ws://"+hostIp+":"+"14949");

	public String currentScene = "0";
	public String currentMsg;
	int disconnectedCounter = 0;
	int retryEvery = 500;
	bool isConnected = false;
	public String  myId;
	bool readyToChangeScene = false;
	bool readyToSyncTime = false;
	bool getMessage = false;
	float startTime;
	public float localTime;

	
	bool sendDeviceID; 
	// Use this for initialization
	void Start () {
		client = new WebSocket("ws://"+hostIp+":"+hostPort);


		startTime = Time.time*1000;
		client.OnMessage += (sender, e) => {
			Debug.Log ("message: " + e.Data.ToString());
			var message = e.Data.Split(' ');

			if(message[0] == "/sceneChange"){
				currentScene = message[1];
				Debug.Log("scene set to " + message[1]);
				readyToChangeScene = true;
			} else if (message[0] == "/update"){
				//Debug.Log("Local Time is: "+localTime);

				if( Mathf.Abs( localTime - float.Parse(message[1] ) )> 50 ){
					localTime = float.Parse(message[1]);
					readyToSyncTime = true;
				} 

				if (message[2] != currentScene){
					currentScene = message[2];
					Debug.Log("scene set to " + message[2]);
					readyToChangeScene = true;
				}

			} else if (message[0] == "/welcome"){
				if (message[1] != currentScene) {
					currentScene = message[1];
					readyToChangeScene = true;
					sendDeviceID = true;
				}
				

			} else if (message[0] == "/vibrate"){
				if(SystemInfo.deviceType == DeviceType.Handheld){

					#if UNITY_ANDROID || UNITY_IPHONE
					Handheld.Vibrate();
					#endif


				} else {
					Debug.Log("Would've vibrated on heldheld.");
				}
			}else if (message[0] == "/yourID"){
				//client will get its id from the server
				// and send it back later
				myId = message[1];

			}else if (message[0]=="/sendMyMsg"){
				//client can get message from server
				getMessage=true;
			}

		};

		client.OnOpen += (sender, e) => {
			Debug.Log ("opened");
			isConnected = true;
			sendDeviceID=true;
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
			//client.Send("/clientInfo/deviceID "+ SystemInfo.deviceUniqueIdentifier);
			//on connectioin, client will just send its id back
			client.Send("/myID "+ myId);
			sendDeviceID=false;
		}
		if (getMessage) {
			// when get a message from the server, it will send back with its unique id
			client.Send("/got "+ myId);
			getMessage=false;

		}
		if (!isConnected && disconnectedCounter > retryEvery) {
			Debug.Log("Attempting Retry");
			client.Connect();

		} else if (!isConnected) {
			client.Send("/close "+ myId);
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

