using UnityEngine;
using System.Collections;

public class cueSystem : MonoBehaviour{
	public int cueNumber = 0;

	public void Update(){
		if (Network.isServer && Input.GetKeyDown (KeyCode.Space)) {
			cueNumber += 1;
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
}