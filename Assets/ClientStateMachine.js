#pragma strict


private var extraButt:GameObject;
private var cameraObj:GameObject;

private var triggeredSepia:boolean = false;


function Start () {
	
	if(PlayerPrefs.GetInt("FirstLaunch",0) != 0){
	
		var intro:GameObject = GameObject.Find("Intro");
		if(intro) intro.SetActive(false);
		Debug.Log("INTRO OFF");	

	}

	extraButt = GameObject.Find("Extras Button");

	PlayerPrefs.SetInt("FirstLaunch", 1);
	PlayerPrefs.Save();
	
}

function Update () {

	if(!extraButt){
		extraButt = GameObject.Find("Extras Button");

	} else if (PlayerPrefs.GetInt("CompletedShow",0) != 0 && extraButt.GetComponent(UI.Image).color.a != 255){
		
		extraButt.GetComponent(UI.Image).color.a = 255;
		GameObject.Find("Camera").GetComponent(Animator).SetBool("isSepia", false);
		Debug.Log("EXTRAS BUTTON UP");	
	
	} else if (PlayerPrefs.GetInt("CompletedShow",0) == 0  && !cameraObj){
		cameraObj = GameObject.Find("Camera");
		cameraObj.GetComponent(Animator).SetBool("isSepia", true);
		Debug.Log("sepia true!");
		triggeredSepia = true;
	}
	
	
	
	if(Input.GetKey('d')) {
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	
	}
	
	if (Input.GetKey('c') ){
	
		setCompletedShow();
		PlayerPrefs.Save();
	
	}
	
}


function setCompletedShow(){
	
	PlayerPrefs.SetInt("CompletedShow",1);
	PlayerPrefs.Save();

}