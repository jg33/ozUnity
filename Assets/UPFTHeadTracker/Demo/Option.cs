using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour {

	public UPFTHeadTracker headTracker = null;

	private const int BUTTON_WIDTH = 150;
	private const int BUTTON_HEIGHT = 100;
	private const int BUTTON_MARGIN = 10;

	private float _baseScale = 1.0f;

	void Awake() {
		_baseScale = Screen.width / 1280.0f;
	}

	void OnGUI() {

		if (headTracker != null) {

			int w = (int)(BUTTON_WIDTH * _baseScale);
			int h = (int)(BUTTON_HEIGHT * _baseScale);
			int x = Screen.width - w;
			int y = Screen.height - h;

			if (GUI.Button(new Rect(x, y, w, h), "Switch")) {
				headTracker.ToggleCameraMode();
			}

			#if UNITY_EDITOR
			#elif UNITY_ANDROID || UNITY_IPHONE
			x = (int)(x - (BUTTON_MARGIN * _baseScale) - w);
			if (GUI.Button(new Rect(x, y, w, h), "Reset")) {
				headTracker.ResetOrientation();
			}
			#endif
		}
	}
}
