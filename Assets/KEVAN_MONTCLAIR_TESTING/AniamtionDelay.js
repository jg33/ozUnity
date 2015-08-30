#pragma strict

var startDelay : int;
  
function Start(){ 
      WaitSeconds(); //wait random seconds for animation
}


function WaitSeconds () {
    while(true) {
           yield WaitForSeconds(startDelay);
           GetComponent.<Animator>().Play("Take 001");
		  // GetComponent.<Animator>().Play("PoppyGrowing_Petals");    
		  }         
}