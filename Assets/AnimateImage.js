#pragma strict

var movieName: String;
var fps:float;
var frames:int;

var texArray: Texture[];

var isPlaying: boolean;

var outputTex: UI.RawImage;


function Start () {
	outputTex = this.gameObject.GetComponent(UI.RawImage);
	texArray = new Texture[256];

	loadMovie(movieName);
	
}

function Update () {

	if (isPlaying){
		var idx: int = Mathf.Floor( Time.time*fps % frames );
		outputTex.texture = texArray[idx];
	}

}

function loadMovie(name:String){

	movieName = name;
	for(var i =0;i<frames;i++){
		var numString = i.ToString();
		var num:String = pad(numString,4,'0');
		texArray[i] = Resources.Load(movieName+"/Resources/"+movieName+num) as Texture;
	}


}

function loadMovie(name:String, _fps:int, _frames:int){
	fps = _fps || 24;
	frames = _frames || 100;
	
	loadMovie(name);

}

function setFrames(_frames:int){
	frames = _frames;
}

function setFps(_fps:int){
	fps = _fps;
}

function play(){
	isPlaying = true;
}

function stop(){
	isPlaying = false;

}

function pad(n:String, width:int, z:String) {
  z = z || '0';
  n = n + '';
  return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}