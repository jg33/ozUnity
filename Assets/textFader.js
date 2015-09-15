#pragma strict

var nextColor:Color;

function Start () {
	nextColor = Color(1,1,1);
}


function changeText(newText:String){
	gameObject.GetComponent(Animator).SetBool("textIn",false);
	yield WaitForSeconds(1);
	gameObject.GetComponent(UI.Text).color = nextColor;
	gameObject.GetComponent(UI.Text).text = newText;
	gameObject.GetComponent(Animator).SetBool("textIn",true);

	
}

function setColor(newColor:Color){

	nextColor = newColor;
	Debug.Log("Set text color! " + nextColor.r + " "+nextColor.g+" "+nextColor.b);
}