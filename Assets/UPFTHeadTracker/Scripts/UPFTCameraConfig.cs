using UnityEngine;

[System.Serializable]
public class UPFTCameraConfig {

	public float ipd = 0.064f;	// millimeters
	public float nearClipPlane = 0.1f;
	public float farClipPlane = 1000;
	public Color backgroundColor = Color.black;
}
