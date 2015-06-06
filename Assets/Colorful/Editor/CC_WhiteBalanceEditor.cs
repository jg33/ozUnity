using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_WhiteBalance))]
public class CC_WhiteBalanceEditor : Editor
{
	static string[] modes = { "Simple", "Complex" };

	SerializedProperty p_white;
	SerializedProperty p_mode;

	void OnEnable()
	{
		p_white = serializedObject.FindProperty("white");
		p_mode = serializedObject.FindProperty("mode");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		p_mode.intValue = EditorGUILayout.Popup("Mode", p_mode.intValue, modes);
		EditorGUILayout.PropertyField(p_white, new GUIContent("White Point"));

		serializedObject.ApplyModifiedProperties();
    }
}
