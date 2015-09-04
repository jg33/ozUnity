using UnityEngine;
using System.Collections;

public class LoadLevel_Smoke_02 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void NextLevelButton(string levelName)
	{
		Application.LoadLevel("Flamethrower");
	}



	public void OnGUI()
	{
		if (GUI.Button(new Rect(70, 70, 100, 30), "Next Effects"))
			Application.LoadLevel("Flamethrower");
	}



}
