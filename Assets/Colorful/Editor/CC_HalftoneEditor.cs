using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Halftone))]
public class CC_HalftoneEditor : Editor
{
	SerializedProperty p_density;
	SerializedProperty p_antialiasing;
	SerializedProperty p_showOriginal;
	SerializedProperty p_mode;

	static string[] modes = { "Black and White", "CMYK" };

	void OnEnable()
	{
		p_density = serializedObject.FindProperty("density");
		p_antialiasing = serializedObject.FindProperty("antialiasing");
		p_showOriginal = serializedObject.FindProperty("showOriginal");
		p_mode = serializedObject.FindProperty("mode");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		p_mode.intValue = EditorGUILayout.Popup("Mode", p_mode.intValue, modes);

		if (p_mode.intValue == 1)
			EditorGUILayout.LabelField("CMYK mode will only be available on SM3.0 compatible GPUs !", EditorStyles.boldLabel);

		if (p_mode.intValue == 0)
			EditorGUILayout.PropertyField(p_showOriginal);

		EditorGUILayout.PropertyField(p_density);
		EditorGUILayout.PropertyField(p_antialiasing);

		serializedObject.ApplyModifiedProperties();
	}
}
