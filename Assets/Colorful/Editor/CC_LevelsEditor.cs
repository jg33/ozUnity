using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(CC_Levels))]
public class CC_LevelsEditor : CC_BaseEditor
{
	GUIContent rampContent;
	int[] histogram;

	static Color32 lumColor;
	static Color32 redColor;
	static Color32 greenColor;
	static Color32 blueColor;

	static string[] channels = { "Red", "Green", "Blue" };

	int selectedPreset = 0;
	static string[] presets = { "Default", "Darker", "Increase Contrast 1", "Increase Contrast 2", "Increase Contrast 3",
								"Lighten Shadows", "Lighter", "Midtones Brighter", "Midtones Darker" };

	static float [,] presetsData = { { 0, 1, 255, 0, 255 }, { 15, 1, 255, 0, 255 }, { 10, 1, 245, 0, 255 },
									 { 20, 1, 235, 0, 255 }, { 30, 1, 225, 0, 255 }, { 0, 1.6f, 255, 0, 255 },
									 { 0, 1, 230, 0, 255 }, { 0, 1.25f, 255, 0, 255 }, { 0, 0.75f, 255, 0, 255 } };

	static bool IsLinear { get { return QualitySettings.activeColorSpace == ColorSpace.Linear; } }

	SerializedProperty p_isRGB;
	SerializedProperty p_inputMinL;
	SerializedProperty p_inputMaxL;
	SerializedProperty p_inputGammaL;
	SerializedProperty p_inputMinR;
	SerializedProperty p_inputMaxR;
	SerializedProperty p_inputGammaR;
	SerializedProperty p_inputMinG;
	SerializedProperty p_inputMaxG;
	SerializedProperty p_inputGammaG;
	SerializedProperty p_inputMinB;
	SerializedProperty p_inputMaxB;
	SerializedProperty p_inputGammaB;

	SerializedProperty p_outputMinL;
	SerializedProperty p_outputMaxL;
	SerializedProperty p_outputMinR;
	SerializedProperty p_outputMaxR;
	SerializedProperty p_outputMinG;
	SerializedProperty p_outputMaxG;
	SerializedProperty p_outputMinB;
	SerializedProperty p_outputMaxB;

	SerializedProperty p_currentChannel;
	SerializedProperty p_logarithmic;

	void OnEnable()
	{
		p_isRGB = serializedObject.FindProperty("isRGB");
		p_inputMinL = serializedObject.FindProperty("inputMinL");
		p_inputMaxL = serializedObject.FindProperty("inputMaxL");
		p_inputGammaL = serializedObject.FindProperty("inputGammaL");
		p_inputMinR = serializedObject.FindProperty("inputMinR");
		p_inputMaxR = serializedObject.FindProperty("inputMaxR");
		p_inputGammaR = serializedObject.FindProperty("inputGammaR");
		p_inputMinG = serializedObject.FindProperty("inputMinG");
		p_inputMaxG = serializedObject.FindProperty("inputMaxG");
		p_inputGammaG = serializedObject.FindProperty("inputGammaG");
		p_inputMinB = serializedObject.FindProperty("inputMinB");
		p_inputMaxB = serializedObject.FindProperty("inputMaxB");
		p_inputGammaB = serializedObject.FindProperty("inputGammaB");

		p_outputMinL = serializedObject.FindProperty("outputMinL");
		p_outputMaxL = serializedObject.FindProperty("outputMaxL");
		p_outputMinR = serializedObject.FindProperty("outputMinR");
		p_outputMaxR = serializedObject.FindProperty("outputMaxR");
		p_outputMinG = serializedObject.FindProperty("outputMinG");
		p_outputMaxG = serializedObject.FindProperty("outputMaxG");
		p_outputMinB = serializedObject.FindProperty("outputMinB");
		p_outputMaxB = serializedObject.FindProperty("outputMaxB");

		p_currentChannel = serializedObject.FindProperty("currentChannel");
		p_logarithmic = serializedObject.FindProperty("logarithmic");

		rampContent = new GUIContent((Texture2D)Resources.Load(IsLinear ? "GrayscaleRampLinear" : "GrayscaleRamp"));

		if (EditorGUIUtility.isProSkin)
		{
			lumColor = new Color32(255, 255, 255, 255);
			redColor = new Color32(215, 0, 0, 255);
			greenColor = new Color32(0, 215, 0, 255);
			blueColor = new Color32(0, 110, 205, 255);
		}
		else
		{
			lumColor = new Color32(20, 20, 20, 255);
			redColor = new Color32(215, 0, 0, 255);
			greenColor = new Color32(0, 180, 0, 255);
			blueColor = new Color32(0, 110, 205, 255);
		}

		histogram = new int[256];
		ComputeHistogram();
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();

		// Mode & channels
		EditorGUILayout.BeginHorizontal();
			EditorGUI.BeginChangeCheck();
			if (p_isRGB.boolValue) p_currentChannel.intValue = EditorGUILayout.Popup(p_currentChannel.intValue, channels);
			p_isRGB.boolValue = GUILayout.Toggle(p_isRGB.boolValue, "Multi-channel Mode", EditorStyles.miniButton);
			if (EditorGUI.EndChangeCheck())
				ComputeHistogram();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Separator();

		// Top buttons
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
			GUILayout.FlexibleSpace();
			EditorGUILayout.BeginHorizontal(GUILayout.Width(256));

				EditorGUI.BeginChangeCheck();
				p_logarithmic.boolValue = GUILayout.Toggle(p_logarithmic.boolValue, "Log", EditorStyles.miniButton, GUILayout.Width(70));
				if (EditorGUI.EndChangeCheck())
					ComputeHistogram();

				GUILayout.FlexibleSpace();

				if (GUILayout.Button("Auto B&W", EditorStyles.miniButton, GUILayout.Width(70)))
				{
					// Find min and max value on the current channel
					int min = 0, max = 255;

					for (int i = 0; i < 256; i++)
					{
						if (histogram[255 - i] > 0)
							min = 255 - i;

						if (histogram[i] > 0)
							max = i;
					}

					if (!p_isRGB.boolValue)
					{
						p_inputMinL.floatValue = min;
						p_inputMaxL.floatValue = max;
					}
					else
					{
						int c = p_currentChannel.intValue;

						if (c == 0)
						{
							p_inputMinR.floatValue = min;
							p_inputMaxR.floatValue = max;
						}
						else if (c == 1)
						{
							p_inputMinG.floatValue = min;
							p_inputMaxG.floatValue = max;
						}
						else if (c == 2)
						{
							p_inputMinB.floatValue = min;
							p_inputMaxB.floatValue = max;
						}
					}
				}

				GUILayout.FlexibleSpace();

				if (GUILayout.Button("Refresh", EditorStyles.miniButton, GUILayout.Width(70)))
				{
					ComputeHistogram();
				}

			EditorGUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		// Histogram
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
			GUILayout.FlexibleSpace();

			Rect histogramRect = GUILayoutUtility.GetRect(258, 128);
			GUI.Box(histogramRect, "");

			if (!p_isRGB.boolValue) Handles.color = lumColor;
			else if (p_currentChannel.intValue == 0) Handles.color = redColor;
			else if (p_currentChannel.intValue == 1) Handles.color = greenColor;
			else if (p_currentChannel.intValue == 2) Handles.color = blueColor;

			for (int i = 0; i < 256; i++)
			{
				Handles.DrawLine(
						new Vector2(histogramRect.x + i + 2f, histogramRect.yMax - 1f),
						new Vector2(histogramRect.x + i + 2f, histogramRect.yMin - 1f + (histogramRect.height - histogram[i]))
					);
			}

			GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		// Bottom buttons
		EditorGUILayout.Separator();
		if (!p_isRGB.boolValue)
		{
			ChannelUI(p_inputMinL, p_inputGammaL, p_inputMaxL, p_outputMinL, p_outputMaxL);
		}
		else
		{
			if (p_currentChannel.intValue == 0) ChannelUI(p_inputMinR, p_inputGammaR, p_inputMaxR, p_outputMinR, p_outputMaxR);
			else if (p_currentChannel.intValue == 1) ChannelUI(p_inputMinG, p_inputGammaG, p_inputMaxG, p_outputMinG, p_outputMaxG);
			else if (p_currentChannel.intValue == 2) ChannelUI(p_inputMinB, p_inputGammaB, p_inputMaxB, p_outputMinB, p_outputMaxB);
		}

		// Presets
		EditorGUI.BeginChangeCheck();
		selectedPreset = EditorGUILayout.Popup("Preset", selectedPreset, presets);
		if (EditorGUI.EndChangeCheck())
		{
			p_isRGB.boolValue = false; p_currentChannel.intValue = 0;
			p_inputMinL.floatValue = presetsData[selectedPreset, 0];
			p_inputGammaL.floatValue = presetsData[selectedPreset, 1];
			p_inputMaxL.floatValue = presetsData[selectedPreset, 2];
			p_outputMinL.floatValue = presetsData[selectedPreset, 3];
			p_outputMaxL.floatValue = presetsData[selectedPreset, 4];
			ComputeHistogram();
		}

		// Done
		serializedObject.ApplyModifiedProperties();
	}

	void ChannelUI(SerializedProperty inputMinP, SerializedProperty inputGammaP, SerializedProperty inputMaxP, SerializedProperty outputMinP, SerializedProperty outputMaxP)
	{
		float inputMin = inputMinP.floatValue;
		float inputGamma = inputGammaP.floatValue;
		float inputMax = inputMaxP.floatValue;
		float outputMin = outputMinP.floatValue;
		float outputMax = outputMaxP.floatValue;

		// Input
		GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.MinMaxSlider(ref inputMin, ref inputMax, 0, 255, GUILayout.Width(256));
			inputMinP.floatValue = inputMin;
			inputMaxP.floatValue = inputMax;
			GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(GUILayout.Width(256));
				inputMin = EditorGUILayout.FloatField((int)inputMin, GUILayout.Width(50));
				GUILayout.FlexibleSpace();
				inputGamma = EditorGUILayout.FloatField(inputGamma, GUILayout.Width(50));
				GUILayout.FlexibleSpace();
				inputMax = EditorGUILayout.FloatField((int)inputMax, GUILayout.Width(50));
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		EditorGUILayout.Separator();

		// Ramp
		GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.LabelField(rampContent, GUILayout.Width(256), GUILayout.Height(20));
			GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		// Output
		GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			EditorGUILayout.MinMaxSlider(ref outputMin, ref outputMax, 0, 255, GUILayout.Width(256));
			outputMinP.floatValue = outputMin;
			outputMaxP.floatValue = outputMax;
			GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(GUILayout.Width(256));
				outputMin = EditorGUILayout.FloatField((int)outputMin, GUILayout.Width(50));
				GUILayout.FlexibleSpace();
				outputMax = EditorGUILayout.FloatField((int)outputMax, GUILayout.Width(50));
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		inputMinP.floatValue = inputMin;
		inputGammaP.floatValue = Mathf.Clamp(inputGamma, 0.1f, 9.99f);
		inputMaxP.floatValue = inputMax;
		outputMinP.floatValue = outputMin;
		outputMaxP.floatValue = outputMax;
	}

	void ComputeHistogram()
	{
		int channel = !p_isRGB.boolValue ? 0 : p_currentChannel.intValue + 1;

		// Current camera
		MonoBehaviour target = (MonoBehaviour)this.target;
		CC_Levels comp = (CC_Levels)target.GetComponent<CC_Levels>();
		Camera camera = target.GetComponent<Camera>();

		if (camera == null || !camera.enabled || !target.enabled || !target.gameObject.activeInHierarchy)
			return;

		// Prepare the texture to render the camera to. Base width will be 640 pixels (precision should be good enough
		// to get an histogram) and the height depends on the camera aspect ratio
		Texture2D cameraTexture = new Texture2D(640, (int)(640f * camera.aspect), TextureFormat.ARGB32, false, IsLinear);
		cameraTexture.hideFlags = HideFlags.HideAndDontSave;
		cameraTexture.filterMode = FilterMode.Point;

		RenderTexture rt = RenderTexture.GetTemporary(cameraTexture.width, cameraTexture.height, 24, RenderTextureFormat.ARGB32);

		// Backup the current states
		bool prevCompEnabled = comp.enabled;
		RenderTexture prevTargetTexture = camera.targetTexture;

		// Disable the current CC_Levels component, we don't want it to be applied before getting the histogram data
		comp.enabled = false;

		// Render
		camera.targetTexture = rt;
		RenderTexture.active = rt;
		camera.Render();
		cameraTexture.ReadPixels(new Rect(0, 0, cameraTexture.width, cameraTexture.height), 0, 0, false);
		cameraTexture.Apply();
		Color32[] pixels = cameraTexture.GetPixels32();

		// Cleanup
		camera.targetTexture = prevTargetTexture;
		RenderTexture.active = null;
		RenderTexture.ReleaseTemporary(rt);
		DestroyImmediate(cameraTexture);
		comp.enabled = prevCompEnabled;

		// Gets the histogram for the given channel
		histogram = new int[256];
		int l = pixels.Length;

		if (channel == 0) // Lum
		{
			for (int i = 0; i < l; i++)
				histogram[(int)(pixels[i].r * 0.3f + pixels[i].g * 0.59f + pixels[i].b * 0.11f)]++;
		}
		else if (channel == 1) // Red
		{
			for (int i = 0; i < l; i++)
				histogram[pixels[i].r]++;
		}
		else if (channel == 2) // Green
		{
			for (int i = 0; i < l; i++)
				histogram[pixels[i].g]++;
		}
		else if (channel == 3) // Blue
		{
			for (int i = 0; i < l; i++)
				histogram[pixels[i].b]++;
		}

		// Scale the histogram values
		float max = histogram.Max();

		if (p_logarithmic.boolValue) // Log
		{
			float factor = 126f / Mathf.Log10(max);

			for (int i = 0; i < 256; i++)
				histogram[i] = (histogram[i] == 0) ? 0 : (int)Mathf.Round(Mathf.Log10(histogram[i]) * factor);
		}
		else // Linear
		{
			float factor = 126f / max;

			for (int i = 0; i < 256; i++)
				histogram[i] = (int)Mathf.Round(histogram[i] * factor);
		}
	}
}
