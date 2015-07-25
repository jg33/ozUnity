#pragma strict

function Start () {
	
	if(PlayerPrefs.GetInt("FirstLaunch",0) != 0){
	
		GameObject.Find("Intro").SetActive(false);
	
	}

	if (PlayerPrefs.GetInt("CompletedShow",0) != 0){
		
		//GameObject.Find("Extras Button").GetComponent("Image").color.a = 255;
	
	}
	
	PlayerPrefs.SetInt("FirstLaunch", 1);
	PlayerPrefs.Save();
	
}

function Update () {


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