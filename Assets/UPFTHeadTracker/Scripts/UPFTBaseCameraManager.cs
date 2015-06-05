using UnityEngine;
using System.Collections;

// カメラ管理用基底クラス.
public abstract class UPFTBaseCameraManager : MonoBehaviour {

	public UPFTCamera[] cameras {
		get;
		private set;
	}

	void Awake()
	{
		this.cameras = transform.GetComponentsInChildren<UPFTCamera>();
	}
	
	public void SetCameraConfig(UPFTCameraConfig config) {
		
		if (cameras != null) {
			foreach (UPFTCamera camera in this.cameras) {
				camera.SetCameraConfig(config);
			}
		}
	}

	public virtual void SetCameraMode(UPFTCameraMode cameraMode) {
	}
}
