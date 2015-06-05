using UnityEngine;
using System.Collections;

// 単眼カメラ管理用クラス.
public class UPFTNormalCameraManager : UPFTBaseCameraManager {

	public override void SetCameraMode(UPFTCameraMode cameraMode) {
		
		bool active = (cameraMode == UPFTCameraMode.Normal);
		
		foreach (UPFTCamera camera in this.cameras) {
			if (camera.gameObject.activeSelf != active) {
				camera.gameObject.SetActive(active);
			}
		}
	}
}
