using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_BrightnessContrastGamma))]
public class CC_BrightnessContrastGammaEditor : Editor
{
	SerializedProperty p_brightness;
	SerializedProperty p_contrast;
	SerializedProperty p_redCoeff;
	SerializedProperty p_greenCoeff;
	SerializedProperty p_blueCoeff;
	SerializedProperty p_gamma;

	void OnEnable()
	{
		p_brightness = serializedObject.FindProperty("brightness");
		p_contrast = serializedObject.FindProperty("contrast");
		p_redCoeff = serializedObject.FindProperty("redCoeff");
		p_greenCoeff = serializedObject.FindProperty("greenCoeff");
		p_blueCoeff = serializedObject.FindProperty("blueCoeff");
		p_gamma = serializedObject.FindProperty("gamma");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_brightness);

		EditorGUILayout.PropertyField(p_contrast);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(p_redCoeff, new GUIContent("Red Channel"));
		EditorGUILayout.PropertyField(p_greenCoeff, new GUIContent("Green Channel"));
		EditorGUILayout.PropertyField(p_blueCoeff, new GUIContent("Blue Channel"));
		EditorGUI.indentLevel--;

		EditorGUILayout.PropertyField(p_gamma);

		serializedObject.ApplyModifiedProperties();
    }
}
