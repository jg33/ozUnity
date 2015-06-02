using UnityEngine;
using System.Collections;

public class GUI_button : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()

	{
		GUI.Button(new Rect(15,15,150,50), "Take Screenshot");
		
		}

}