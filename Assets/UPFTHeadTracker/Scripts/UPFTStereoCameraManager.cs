using UnityEngine;
using System.Collections;

// 複眼カメラ管理用クラス.
public class UPFTStereoCameraManager : UPFTBaseCameraManager {

	public override void SetCameraMode(UPFTCameraMode cameraMode) {

		bool active = (cameraMode == UPFTCameraMode.Stereoscopic);
		
		foreach (UPFTCamera camera in this.cameras) {
			if (camera.gameObject.activeSelf != active) {
				camera.gameObject.SetActive(active);
			}
		}
	}
}
