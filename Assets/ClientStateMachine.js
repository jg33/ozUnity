#pragma strict


private var extraButt:GameObject;
function Start () {
	
	if(PlayerPrefs.GetInt("FirstLaunch",0) != 0){
	
		GameObject.Find("Intro").SetActive(false);
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
		Debug.Log("EXTRAS BUTTON UP");	
	
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