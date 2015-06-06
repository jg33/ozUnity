using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Convolution3x3))]
public class CC_Convolution3x3Editor : Editor
{
	SerializedProperty p_divisor;
	SerializedProperty p_amount;
	SerializedProperty p_kernelTop;
	SerializedProperty p_kernelMiddle;
	SerializedProperty p_kernelBottom;

	int selectedPreset = 0;
	static string[] presets = { "Default", "Sharpen", "Emboss", "Gaussian Blur", "Laplacian Edge Detection", "Prewitt Edge Detection", "Frei-Chen Edge Detection" };
	static Vector3[,] presetsData = { { new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, 0f) },
									  { new Vector3(0f, -1f, 0f), new Vector3(-1f, 5f, -1f), new Vector3(0f, -1f, 0f) },
									  { new Vector3(-2f, -1f, 0f), new Vector3(-1f, 1f, 1f), new Vector3(0f, 1f, 2f) },
									  { new Vector3(1f, 2f, 1f), new Vector3(2f, 4f, 2f), new Vector3(1f, 2f, 1f) },
									  { new Vector3(0f, -1f, 0f), new Vector3(-1f, 4f, -1f), new Vector3(0f, -1f, 0f) },
									  { new Vector3(0f, 1f, 1f), new Vector3(-1f, 0f, 1f), new Vector3(-1f, -1f, 0f) },
									  { new Vector3(-1f, -1.4142f, -1f), new Vector3(0f, 0f, 0f), new Vector3(1f, 1.4142f, 1f) } };
	static float[] presetsDiv = { 1.0f, 1.0f, 1.0f, 16.0f, 1.0f, 1.0f, 1.0f };

	void OnEnable()
	{
		p_divisor = serializedObject.FindProperty("divisor");
		p_amount = serializedObject.FindProperty("amount");
		p_kernelTop = serializedObject.FindProperty("kernelTop");
		p_kernelMiddle = serializedObject.FindProperty("kernelMiddle");
		p_kernelBottom = serializedObject.FindProperty("kernelBottom");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		GUILayout.Label("Matrix", EditorStyles.boldLabel);

		EditorGUILayout.PropertyField(p_divisor);

		Vector3 temp = p_kernelTop.vector3Value;
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Kernel");
			temp.x = EditorGUILayout.FloatField(temp.x);
			temp.y = EditorGUILayout.FloatField(temp.y);
			temp.z = EditorGUILayout.FloatField(temp.z);
		EditorGUILayout.EndHorizontal();
		p_kernelTop.vector3Value = temp;

		temp = p_kernelMiddle.vector3Value;
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(" ");
			temp.x = EditorGUILayout.FloatField(temp.x);
			temp.y = EditorGUILayout.FloatField(temp.y);
			temp.z = EditorGUILayout.FloatField(temp.z);
		EditorGUILayout.EndHorizontal();
		p_kernelMiddle.vector3Value = temp;

		temp = p_kernelBottom.vector3Value;
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(" ");
			temp.x = EditorGUILayout.FloatField(temp.x);
			temp.y = EditorGUILayout.FloatField(temp.y);
			temp.z = EditorGUILayout.FloatField(temp.z);
		EditorGUILayout.EndHorizontal();
		p_kernelBottom.vector3Value = temp;

		EditorGUILayout.PropertyField(p_amount);

		GUI.changed = false;
		selectedPreset = EditorGUILayout.Popup("Preset", selectedPreset, presets);

		if (GUI.changed)
		{
			p_kernelTop.vector3Value = presetsData[selectedPreset, 0];
			p_kernelMiddle.vector3Value = presetsData[selectedPreset, 1];
			p_kernelBottom.vector3Value = presetsData[selectedPreset, 2];
			p_divisor.floatValue = presetsDiv[selectedPreset];
		}

		serializedObject.ApplyModifiedProperties();
    }
}
