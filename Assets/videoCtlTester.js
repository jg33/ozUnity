#pragma strict

function Start () {

}

function Update () {
	if(Input.GetMouseButtonDown(0)){
		SendMessage("gotoPosition", Random.Range(0,1000)/1000f) ;
		
	}
}