#pragma strict


function Start () {

}


function changeText(newText:String){
	gameObject.GetComponent(Animator).SetBool("textIn",false);
	yield WaitForSeconds(1);
	gameObject.GetComponent(UI.Text).text = newText;
	gameObject.GetComponent(Animator).SetBool("textIn",true);

	
}