using UnityEngine;
using System.Collections;

public class webCamToTexture : MonoBehaviour {

	WebCamDevice cameras;
	WebCamTexture camfeed;

public void Start(){


    camfeed = new WebCamTexture(WebCamTexture.devices[0].name, 640, 480, 30);
 
    camfeed.Play();

    GameObject bgimg = GameObject.Find("BGPlane");
    bgimg.renderer.material.SetTexture("_MainTex",camfeed);
  
	}
}
