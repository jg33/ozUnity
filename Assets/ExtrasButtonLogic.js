#pragma strict

function Start () {

}

function Update () {

}


function click(){
	if(PlayerPrefs.GetInt("CompletedShow",0) != 0){
		GameObject.Find("MenuContainer").GetComponent(Animator).SetTrigger("toggleBonus");
		
	} else{
		GameObject.Find("No Extras Alert").GetComponent(Animator).SetTrigger("extrasAlert");
	}


}