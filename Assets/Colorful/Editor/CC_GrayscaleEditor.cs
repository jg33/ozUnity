using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Grayscale))]
public class CC_GrayscaleEditor : Editor
{
	SerializedProperty p_redLuminance;
	SerializedProperty p_greenLuminance;
	SerializedProperty p_blueLuminance;
	SerializedProperty p_amount;

	int selectedPreset = 0;
	static string[] presets = { "Default", "Unity Default", "Desaturate" };
	static float [,] presetsData = { { 0.3f, 0.59f, 0.11f }, { 0.222f, 0.707f, 0.071f }, { 0.333f, 0.334f, 0.333f } };

	void OnEnable()
	{
		p_redLuminance = serializedObject.FindProperty("redLuminance");
		p_greenLuminance = serializedObject.FindProperty("greenLuminance");
		p_blueLuminance = serializedObject.FindProperty("blueLuminance");
		p_amount = serializedObject.FindProperty("amount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		GUILayout.Label("Luminance", EditorStyles.boldLabel);

		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(p_redLuminance, new GUIContent("Red"));
		EditorGUILayout.PropertyField(p_greenLuminance, new GUIContent("Green"));
		EditorGUILayout.PropertyField(p_blueLuminance, new GUIContent("Blue"));
		EditorGUI.indentLevel--;

		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(p_amount);

		GUI.changed = false;
		selectedPreset = EditorGUILayout.Popup("Preset", selectedPreset, presets);

		if (GUI.changed)
		{
			p_redLuminance.floatValue = presetsData[selectedPreset, 0];
			p_greenLuminance.floatValue = presetsData[selectedPreset, 1];
			p_blueLuminance.floatValue = presetsData[selectedPreset, 2];
		}

		serializedObject.ApplyModifiedProperties();
    }
}
