using UnityEngine;
using System.Collections;

public class NetworkMenu : MonoBehaviour {

	public string connectionIP = "127.0.0.1";
	public int portNumber = 16261;
	private bool connected = false;

	private void OnConnectedToServer()
	{
		Debug.Log ("Connected To Server");
		connected = true;
	}

	private void OnServerInitialized(){
		Debug.Log ("Server Initialized");
		connected = true;
	}

	private void OnDisconnectedFromServer(){
		Debug.Log ("Disconnected From Server");
		connected = false;
	}

	private void OnGUI()
	{
		if (!connected) {
			connectionIP = GUILayout.TextField (connectionIP);
			int.TryParse(GUILayout.TextField (portNumber.ToString()), out portNumber);

			if (GUILayout.Button ("Connect")) {
				Network.Connect (connectionIP, portNumber);
			}
			if (GUILayout.Button ("Host")) {
				Network.InitializeServer(4,portNumber,true);
				Debug.Log("Initializing Server");
			}
		} else {
			GUILayout.Label ("Connections: " + Network.connections.Length.ToString());
		}
	}
}
