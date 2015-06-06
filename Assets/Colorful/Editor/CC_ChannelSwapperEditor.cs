using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_ChannelSwapper))]
public class CC_ChannelSwapperEditor : Editor
{
	static string[] channels = { "Red Channel", "Green Channel", "Blue Channel" };

	SerializedProperty p_red;
	SerializedProperty p_green;
	SerializedProperty p_blue;

	void OnEnable()
	{
		p_red = serializedObject.FindProperty("red");
		p_green = serializedObject.FindProperty("green");
		p_blue = serializedObject.FindProperty("blue");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		p_red.intValue = EditorGUILayout.Popup("Red Source", p_red.intValue, channels);
		p_green.intValue = EditorGUILayout.Popup("Green Source", p_green.intValue, channels);
		p_blue.intValue = EditorGUILayout.Popup("Blue Source", p_blue.intValue, channels);

		serializedObject.ApplyModifiedProperties();
    }
}
