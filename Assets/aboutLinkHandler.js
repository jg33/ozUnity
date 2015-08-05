#pragma strict

function Start () {

}

function Update () {

}

public function openURL(name:String){

	switch (name){
		case "tba":
		Application.OpenURL("http://thebuildersassociation.org");
		break;
		
		case "parable":
		Application.OpenURL("http://www.shsu.edu/his_rtc/2014_FALL/Wizard_of_Oz_Littlefield.pdf");
		break;
		
		case "wallace":
		Application.OpenURL("https://www.youtube.com/watch?v=1ooKsv_SX4Y");
		break;		
		
		case "historical":
		Application.OpenURL("https://www.youtube.com/watch?v=rwmKSw4sftM");
		break;
		
		case "moreGay":
		Application.OpenURL("https://www.youtube.com/watch?v=N7CfKk0rov8");
		break;
		

		case "darkSide":
		Application.OpenURL("https://www.youtube.com/watch?v=MDz2cNL1vuM");
		break;
		
		case "mcDirections":
		Application.OpenURL("http://www.peakperfs.org/getting-here");
		break;
		
		
		default:

		break;
	
	
	
	
	}


}