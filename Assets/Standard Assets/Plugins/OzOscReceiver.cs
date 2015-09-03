using UnityEngine;
using System.Collections;

enum TriggeredEvents{ MOE_VIDEO, RANDOM_RAINBOW, TORNADO_ALERT, APPLAUSE, AWW, NO_PLACE };


public class OzOscReceiver : MonoBehaviour {

	/// OSC Stuff ///
	public string RemoteIP = "127.0.0.1"; //127.0.0.1 signifies a local host (if testing locally
	public int SendToPort = 9000; //the port you will be sending from
	public int ListenerPort = 8050; //the port you will be listening on
	private Osc handler;
	//public var controller : Transform;
	//public var gameReceiver = "Cube"; //the tag of the object on stage that you want to manipulate
	
	
	/// Game Objects ///
	public cueSystem cueControl;
	public float damping;

	public int currentCue;
	public int tornadoState;
	public int poppyState;
	public int munchkinState;
	public int monkeyState;
	public int textSelection;

	// one-shot events that fire on cue or never //

	
	public void Start ()
	{
		//Initializes on start up to listen for messages
		//make sure this game object has both UDPPackIO and OSC script attached
		
		UDPPacketIO udp = (UDPPacketIO)GetComponent("UDPPacketIO");
		udp.init(RemoteIP, SendToPort, ListenerPort);
		handler = (Osc)GetComponent("Osc");
		handler.init(udp);
		handler.SetAllMessageHandler(AllMessageHandler);
		
		
		Debug.Log("Osc Running");
		
	}
	
	public void Update () {
		
		///Set Variables///
		
		
	}
	
	
	
	//These functions are called when messages are received
	//Access values via: oscMessage.Values[0], oscMessage.Values[1], etc
	
	public void AllMessageHandler(OscMessage oscMessage){
		
		
		string msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
		string msgAddress = oscMessage.Address; //the message parameters
		var msgValue = oscMessage.Values[0]; //the message value
		//Debug.Log(msgString); //log the message and values coming from OSC
		
		//FUNCTIONS YOU WANT CALLED WHEN A SPECIFIC MESSAGE IS RECEIVED
		switch (msgAddress){
	
		case "/cue":
			int cue = (int)oscMessage.Values[0];
			cueControl.cueNumber = cue;

			break;
		case "/triggerEvent":
			int eventID = (int)oscMessage.Values[0];

			cueControl.tempEventTriggers = eventID;
			Debug.Log ("Event! " + eventID);
//
//			switch (eventID){
//				//MOE_VIDEO, RANDOM_RAINBOW, TORNADO_ALERT, APPLAUSE, AWW, NO_PLACE
//			case 0:
//				break;
//
//			case 1:
//				break;
//
//
//				default: 
//
//				break;	
//
//			}

			break;
		case "/selectText":
			cueControl.textSelection = (int)oscMessage.Values[0];

			break;	
		case "/sendText":

			break;

		case "/tornadoState":
			cueControl.tornadoState = (int)oscMessage.Values[0];

			break;
		case "/munchkinState":
			cueControl.munchkinState = (int)oscMessage.Values[0]; 

			break;

		case "/poppyState":
			cueControl.poppyState = (int)oscMessage.Values[0];
			break;

		case "/monkeyState":
			cueControl.monkeyState = (int)oscMessage.Values[0];

			break;
		case "/forcePassive":
			cueControl.forcePassive = (bool)oscMessage.Values[0];
			break;

		default:
			Debug.Log("unhandled osc: " + msgValue );
			break;
		}
		
	}


	


}
