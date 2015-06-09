using UnityEngine;
using System.Collections;
using System.IO;

public class androidObbManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if UNITY_ANDROID
		string uriOfDatasetInObb = Application.streamingAssetsPath + "/QCAR/MyDataSet.xml"; 
		var xmlLoc = new WWW(uriOfDatasetInObb); 
		
		uriOfDatasetInObb = Application.streamingAssetsPath + "/QCAR/MyDataSet.dat"; // do the same same for .dat file too
		var datLoc = new WWW(uriOfDatasetInObb); 
		
		
		File.WriteAllBytes( Application.persistentDataPath + "/QCAR/MyDataSet.xml", xmlLoc.bytes );
		File.WriteAllBytes( Application.persistentDataPath + "/QCAR/MyDataSet.dat", datLoc.bytes );
		#endif
	}
	

	
	// Update is called once per frame
	void Update () {
	
	}
}
