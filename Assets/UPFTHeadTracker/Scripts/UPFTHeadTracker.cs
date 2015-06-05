using UnityEngine;
using System.Collections;

// 端末傾き算出用クラス.
[System.Serializable]
public class UPFTHeadTracker : MonoBehaviour {

#region static readonly variable

	#if UNITY_EDITOR
	#elif UNITY_ANDROID
	private static readonly Vector3 INIT_FORWARD_VECTOR = Vector3.forward;
	#elif UNITY_IPHONE
	private static readonly Vector3 INIT_FORWARD_VECTOR = Vector3.right;
	private static readonly Quaternion CORRECT_ROTATION_X = Quaternion.Euler(90, 0, 0);
	#endif

	#if UNITY_EDITOR
	#elif UNITY_ANDROID || UNITY_IPHONE
	private static readonly Vector3 EXPECT_INIT_FORWARD_VECTOR = Vector3.forward;
	#endif

#endregion

#region public property

	// カメラモード.
	public UPFTCameraMode cameraMode {
		get { return _cameraMode; }
		set {
			if (_cameraMode != value) {
				SetCameraMode(value);
			}
		}
	}

	// カメラオブジェクト取得用.
	// Stereoscopicモードの場合は右側のカメラを参照する.
	public Camera currentCamera {
		get;
		private set;
	}

#endregion

#region inspector variable

	[SerializeField]
	private UPFTCameraMode _cameraMode = UPFTCameraMode.Stereoscopic;

	// カメラ設定用パラメーター.
	public UPFTCameraConfig cameraConfig;

#endregion

#region private variable

	// カメラ管理オブジェクト.
	private UPFTBaseCameraManager[] _cameraManagers = null;

	#if UNITY_EDITOR
	#elif UNITY_ANDROID
	private static AndroidJavaObject _plugin = null;
	#endif

	#if UNITY_EDITOR
	#elif UNITY_ANDROID || UNITY_IPHONE
	private Quaternion correctRotationY;
	#endif

#endregion
	 
#region unity event

	void Awake()
	{
		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		_plugin = new AndroidJavaObject("com.upft.vr.cardboardbridge.CardboardBridge");
		if (_plugin != null) {
			_plugin.Call("init");
		}

		correctRotationY = Quaternion.identity;
		#elif UNITY_IPHONE
		correctRotationY = Quaternion.FromToRotation(INIT_FORWARD_VECTOR, EXPECT_INIT_FORWARD_VECTOR);
		#endif
	}
	
	
	// Use this for initialization
	void Start () {
		
		_cameraManagers = GetComponentsInChildren<UPFTBaseCameraManager>();
		SetCameraConfig(cameraConfig);
		SetCameraMode(cameraMode);


		StartTracking();
	}
	
	// Update is called once per frame
	void Update () {

		#if UNITY_EDITOR
		#elif UNITY_ANDROID || UNITY_IPHONE
		transform.localRotation = UpdateTracking();
		#endif 
	}
	
	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus) {
			PauseTracking();
		} else {
			RestartTracking();
		}
	}

	void OnDestroy()
	{
		StopTracking();
	}

	void OnApplicationQuit()
	{
		StopTracking();
	}


#endregion

#region public method

	// カメラモードの変更処理.
	// Normalだった場合はStereoscopicに、Stereoscopicだった場合はNormalに変更.
	public void ToggleCameraMode()
	{
		switch (_cameraMode) {
		case UPFTCameraMode.Normal:
			SetCameraMode(UPFTCameraMode.Stereoscopic);
			break;
		case UPFTCameraMode.Stereoscopic:
			SetCameraMode(UPFTCameraMode.Normal);
			break;
		}
	}

	public void ResetOrientation() {

		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		Vector3 f = transform.forward;
		correctRotationY = Quaternion.FromToRotation(new Vector3(f.x, 0, f.z).normalized, INIT_FORWARD_VECTOR) * correctRotationY;
		#elif UNITY_IPHONE
		Vector3 f = transform.forward;
		correctRotationY = Quaternion.FromToRotation(new Vector3(f.x, 0, f.z).normalized, EXPECT_INIT_FORWARD_VECTOR) * correctRotationY;
		#endif
	}

#endregion


#region private method

	private void StartTracking() {

		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		if (_plugin != null) {
			_plugin.Call("startTracking");
		}
		#elif UNITY_IPHONE
		Input.gyro.enabled = true;
		#endif
	}

	private void StopTracking() {
		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		if (_plugin != null) {
			_plugin.Call("stopTracking");
		}
		#elif UNITY_IPHONE
		Input.gyro.enabled = false;
		#endif
	}

	private void PauseTracking() {
		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		if (_plugin != null) {
			_plugin.Call("stopTracking");
		}
		Vector3 f = transform.forward;
		correctRotationY = Quaternion.FromToRotation(INIT_FORWARD_VECTOR, new Vector3(f.x, 0, f.z).normalized);
		#endif
	}

	private void RestartTracking() {
		#if UNITY_EDITOR
		#elif UNITY_ANDROID
		if (_plugin != null) {
			_plugin.Call("startTracking");
		}
		#endif
	}

	private Quaternion UpdateTracking() {

		#if UNITY_EDITOR
		return Quaternion.identity;
		#elif UNITY_ANDROID
		if (_plugin != null) {
			float[] q = _plugin.Call<float[]>("getQuaternion");
			return correctRotationY *  new Quaternion(q[0], q[1], q[2], q[3]);
		}

		return Quaternion.identity;

		#elif UNITY_IPHONE
		Quaternion q = Input.gyro.attitude;
		Quaternion qq = new Quaternion(-q.x, -q.z, -q.y, q.w);
		return correctRotationY * qq * CORRECT_ROTATION_X;
		#endif 
	}

	// カメラモード設定処理.
	private void SetCameraMode(UPFTCameraMode mode)
	{
		_cameraMode = mode;
		
		foreach (UPFTBaseCameraManager manager in _cameraManagers) {
			manager.SetCameraMode(mode);
			
			UPFTCamera[] cameras = manager.cameras;
			foreach (UPFTCamera camera in cameras) {
				if (camera.gameObject.activeSelf && camera.gameObject.CompareTag("MainCamera")) {
					currentCamera = camera.gameObject.GetComponent<Camera>();
					break;
				}
			}
		}
	}
	
	// カメラパラメータ設定処理.
	// 初回起動時にしか設定されません.
	private void SetCameraConfig(UPFTCameraConfig config)
	{
		foreach (UPFTBaseCameraManager manager in _cameraManagers) {
			manager.SetCameraConfig(config);
		}
	}

#endregion
}
