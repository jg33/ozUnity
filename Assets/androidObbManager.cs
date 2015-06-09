using UnityEngine;
using System.Collections;
using System.IO;

public class androidObbManager : MonoBehaviour {
	
	void Start () {
		#if UNITY_ANDOID
		StartCoroutine(ExtractObbDatasets());
		AddOSSpecificExternalDatasetSearchDirs ();
		#endif
	}

	private IEnumerator ExtractObbDatasets () {
		string[] filesInOBB = {"3LD_OzWrkShop5.dat", "3LD_OzWrkShop5.xml"};
		foreach (var filename in filesInOBB) {
			string uri = "file://" + Application.streamingAssetsPath + "/QCAR/" + filename;
			
			string outputFilePath = Application.persistentDataPath + "/QCAR/" + filename;
			if(!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
				Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
			
			var www = new WWW(uri);
			yield return www;
			
			Save(www, outputFilePath);
			yield return new WaitForEndOfFrame();
		}
	}
	private void Save(WWW www, string outputPath) {
		File.WriteAllBytes(outputPath, www.bytes);
		
		// Verify that the File has been actually stored
		if(File.Exists(outputPath))
			Debug.Log("File successfully saved at: " + outputPath);
		else
			Debug.Log("Failure!! - File does not exist at: " + outputPath);
		Application.LoadLevel("SceneMenu1");
	}
}
