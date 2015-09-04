using UnityEngine;
using System.Collections;
using System.IO;
using Vuforia;

public class androidObbManager : MonoBehaviour {
	
	void Start () {
		#if UNITY_ANDROID
		StartCoroutine(ExtractObbDatasets());
		#endif
	}
	private IEnumerator ExtractObbDatasets () {

		//Persistent Datapath grabs the file from within the OBB file. 
		//the path will look something like: 
		//jar:file:///storage/emulated/0/Android/obb/org.thebuildersassociation.ozBeta2/main.1.org.thebuildersassociation.ozBeta2.obb!/assets/QCAR/ozUnity.xml
		string toDir = Application.persistentDataPath + "/QCAR/";

		//create dir if it isn't there.
		if(!Directory.Exists(Path.GetDirectoryName(toDir)))  Directory.CreateDirectory(Path.GetDirectoryName(toDir));

		//grab each file and load it. 
		string[] filesInOBB = {"ozUnity.dat", "ozUnity.xml"};	
		foreach (var filename in filesInOBB) {
			string uri = Application.streamingAssetsPath + "/QCAR/" + filename;

			//Debug.Log (uri + " URI from 29 Extract Obb Datasets ");

			var fileRequest = new WWW(uri);

			yield return fileRequest;

			if (!string.IsNullOrEmpty (fileRequest.error)) {
				Debug.LogError ("QCAR FILES DIDN'T LOAD! " + fileRequest.error);
			} else {
				Save(fileRequest, toDir + filename);
			}
		}
		//Tell QCAR to research for files; it searches before the files are loaded.
		GameObject.Find ("ARCamera").GetComponent<DataSetLoadBehaviour> ().AddOSSpecificExternalDatasetSearchDirs ();
	}
	private void Save(WWW www, string outputPath) {
		Debug.Log( "Writing File: " + www.url + " to: " + outputPath);
		File.WriteAllBytes(outputPath, www.bytes);
		
		// Verify that the File has been actually stored
		if(File.Exists(outputPath))
			Debug.LogError("File successfully saved at: " + outputPath);
		else
			Debug.LogError("Failure!! - File does not exist at: " + outputPath);
	}
}
