using UnityEngine;
using System.Collections;
using System.IO;
using Vuforia;
using System.Collections.Generic;

public class androidObbManager : MonoBehaviour {
	
	void Start () {
		#if UNITY_ANDROID
	//	StartCoroutine( ExtractObbDatasets() );
		#endif
	}
	private IEnumerator ExtractObbDatasets () {
		//Persistent Datapath grabs the file from within the OBB file. 
		//the path will look something like: 
		//jar:file:///storage/emulated/0/Android/obb/org.thebuildersassociation.ozBeta2/main.1.org.thebuildersassociation.ozBeta2.obb!/assets/QCAR/ozUnity.xml
		string toDir = Application.persistentDataPath;

		//create dir if it isn't there.
		if(!Directory.Exists(Path.GetDirectoryName( toDir + "/QCAR/" ))) Directory.CreateDirectory(Path.GetDirectoryName(toDir + "/QCAR/"));
		if(!Directory.Exists(Path.GetDirectoryName( toDir + "/Video/"))) Directory.CreateDirectory(Path.GetDirectoryName(toDir + "/Video/"));

		//grab each file and load it. 
		List<string> filesInOBB = new List<string>();
		filesInOBB.Add ( Application.streamingAssetsPath + "/QCAR/ozUnity.xml" );
		filesInOBB.Add ( Application.streamingAssetsPath + "/QCAR/ozUnity.dat" );

		filesInOBB.Add ( Application.streamingAssetsPath + "/Video/rainbow_01.mp4" );
		filesInOBB.Add ( Application.streamingAssetsPath + "/Video/rainbow_02.mp4" );
		filesInOBB.Add ( Application.streamingAssetsPath + "/Video/rainbow_03.mp4" );
		filesInOBB.Add ( Application.streamingAssetsPath + "/Video/rainbow_04.mp4" );
		filesInOBB.Add ( Application.streamingAssetsPath + "/Video/rainbow_05.mp4" );
		filesInOBB.Add ( Application.streamingAssetsPath + "/Video/rainbow_06.mp4" );
		filesInOBB.Add ( Application.streamingAssetsPath + "/Video/rainbow_07.mp4" );
	
		foreach (string filename in filesInOBB) {

			//Debug.LogError("Attempting to load: " + filename + " " + Path.GetFileName(filename));
			if (!filename.EndsWith(".meta")) {

				WWW fileRequest = new WWW("file:/"+filename);

				yield return fileRequest;

				if (!string.IsNullOrEmpty (fileRequest.error)) {
					Debug.LogError ("QCAR FILES DIDN'T LOAD! " + fileRequest.error);
					Debug.Log(fileRequest.url);
				} else {
					if ( filename.EndsWith(".mp4") ) {
						Save(fileRequest, toDir + "/Video/" + Path.GetFileName(filename) );
					} else {
						Save(fileRequest, toDir + "/QCAR/" + Path.GetFileName(filename) );
					}
				}
			}
		}
		//Tell QCAR to re-search for files; it searches before the files are loaded.
		GameObject.Find ("ARCamera").GetComponent<DatabaseLoadBehaviour> ().AddOSSpecificExternalDatasetSearchDirs ();
	}
	private void Save(WWW www, string outputPath) {
		Debug.LogWarning( "Writing File: " + www.url + " to: " + outputPath);
		File.WriteAllBytes(outputPath, www.bytes);
		
		// Verify that the File has been actually stored
		if(File.Exists(outputPath))
			Debug.LogWarning("File successfully saved at: " + outputPath);
		else
			Debug.LogWarning("Failure!! - File does not exist at: " + outputPath);
	}
}
