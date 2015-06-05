using UnityEngine;
using System.Collections;

public class poppyInit : MonoBehaviour {


	public void Update() { 
		if (Input.GetKeyDown ("space")) {
			GameObject go = Instantiate (Resources.Load ("pp1")) as GameObject; 
		}
	}
}

