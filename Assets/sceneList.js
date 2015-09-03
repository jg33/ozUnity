#pragma strict

@HideInInspector var sceneArray: List.<GameObject> ;

function Start () {

	var numScenes: int = transform.childCount  ;
		for (var i = 0; i  < numScenes+1 ; i++) {
		sceneArray.Add(GameObject.Find("Scene"+i)) ;
		if(sceneArray[i] != null){
			sceneArray[i].SetActive(false);
		}
	};
	
	sceneArray[0].SetActive(true);
	
}

function Update () {

}

