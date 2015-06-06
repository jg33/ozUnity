using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_ContrastVignette))]
public class CC_ContrastVignetteEditor : Editor
{
	SerializedProperty p_center;
	SerializedProperty p_sharpness;
	SerializedProperty p_darkness;
	SerializedProperty p_contrast;
	SerializedProperty p_redCoeff;
	SerializedProperty p_greenCoeff;
	SerializedProperty p_blueCoeff;
	SerializedProperty p_edge;

	void OnEnable()
	{
		p_center = serializedObject.FindProperty("center");
		p_sharpness = serializedObject.FindProperty("sharpness");
		p_darkness = serializedObject.FindProperty("darkness");
		p_contrast = serializedObject.FindProperty("contrast");
		p_redCoeff = serializedObject.FindProperty("redCoeff");
		p_greenCoeff = serializedObject.FindProperty("greenCoeff");
		p_blueCoeff = serializedObject.FindProperty("blueCoeff");
		p_edge = serializedObject.FindProperty("edge");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_center);
		EditorGUILayout.PropertyField(p_sharpness);
		EditorGUILayout.PropertyField(p_darkness);

		EditorGUILayout.Separator();

		EditorGUILayout.PropertyField(p_contrast);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(p_redCoeff, new GUIContent("Red Channel"));
		EditorGUILayout.PropertyField(p_greenCoeff, new GUIContent("Green Channel"));
		EditorGUILayout.PropertyField(p_blueCoeff, new GUIContent("Blue Channel"));
		EditorGUI.indentLevel--;

		EditorGUILayout.Separator();

		EditorGUILayout.PropertyField(p_edge);

		serializedObject.ApplyModifiedProperties();
    }
}
