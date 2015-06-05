using UnityEngine;
using System.Collections;

// カメラクラス.
public class UPFTCamera : MonoBehaviour {

	public UPFTCameraType cameraType;

	public void SetCameraConfig(UPFTCameraConfig config)
	{
		Camera camera = GetComponent<Camera>();
		if (camera != null) {

			camera.nearClipPlane = config.nearClipPlane;
			camera.farClipPlane = config.farClipPlane;

			camera.backgroundColor = config.backgroundColor;

			if (cameraType == UPFTCameraType.Left) {

				transform.localPosition = new Vector3(-config.ipd * 0.5f, 0, 0);

			} else if (cameraType == UPFTCameraType.Right) {

				transform.localPosition = new Vector3(config.ipd * 0.5f, 0, 0);
			}
		}
	}
}
