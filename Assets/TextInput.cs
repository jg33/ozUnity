using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

	//private string textFieldString = "DummyText";
	public Text UpdatedText;

	// Gui Live Update

	void Update () {
	// newRect (pos x, pos y, size x sizey), textFieldString, # OF CHARACTERS)
		//textFieldString = GUI.TextField (new Rect (500, 500, 500, 500), textFieldString, 25);

		//GUI.Label (new Rect (505, 75, 100, 100), textFieldString);

		UpdatedText.text = "Hello";
	}






}
