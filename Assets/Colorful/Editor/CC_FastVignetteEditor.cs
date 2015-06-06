using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_FastVignette))]
public class CC_FastVignetteEditor : Editor
{
	SerializedProperty p_center;
	SerializedProperty p_sharpness;
	SerializedProperty p_darkness;
	SerializedProperty p_desaturate;

	void OnEnable()
	{
		p_center = serializedObject.FindProperty("center");
		p_sharpness = serializedObject.FindProperty("sharpness");
		p_darkness = serializedObject.FindProperty("darkness");
		p_desaturate = serializedObject.FindProperty("desaturate");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_center);
		EditorGUILayout.PropertyField(p_sharpness);
		EditorGUILayout.PropertyField(p_darkness);
		EditorGUILayout.PropertyField(p_desaturate);

		serializedObject.ApplyModifiedProperties();
    }
}
