using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Frost))]
public class CC_FrostEditor : Editor
{
	SerializedProperty p_scale;
	SerializedProperty p_sharpness;
	SerializedProperty p_darkness;
	SerializedProperty p_enableVignette;

	void OnEnable()
	{
		p_scale = serializedObject.FindProperty("scale");
		p_sharpness = serializedObject.FindProperty("sharpness");
		p_darkness = serializedObject.FindProperty("darkness");
		p_enableVignette = serializedObject.FindProperty("enableVignette");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_scale);
		EditorGUILayout.PropertyField(p_enableVignette, new GUIContent("Vignette"));

		if (p_enableVignette.boolValue)
		{
			EditorGUILayout.PropertyField(p_sharpness);
			EditorGUILayout.PropertyField(p_darkness);
		}

		serializedObject.ApplyModifiedProperties();
    }
}
