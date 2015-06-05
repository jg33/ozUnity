using UnityEngine;
using System.Collections;
using UnityEditor;

public class MenuManager : MonoBehaviour {

	public Menu CurrentMenu;

	// Use this for initialization
	void Start () {

		ShowMenu (CurrentMenu);
	
	}
	
	public void ShowMenu(Menu menu)
	{
		if (CurrentMenu != null) CurrentMenu.IsOpen = false;
		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}
}

