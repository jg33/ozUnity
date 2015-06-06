using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_ChannelClamper))]
public class CC_ChannelClamperEditor : Editor
{
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

		Vector2 red = p_red.vector2Value;
		Vector2 green = p_green.vector2Value;
		Vector2 blue = p_blue.vector2Value;

		EditorGUILayout.MinMaxSlider(new GUIContent("Red Channel"), ref red.x, ref red.y, 0f, 1f);
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(" ");
			red.x = EditorGUILayout.FloatField(red.x, GUILayout.Width(60));
			GUILayout.FlexibleSpace();
			red.y = EditorGUILayout.FloatField(red.y, GUILayout.Width(60));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.MinMaxSlider(new GUIContent("Green Channel"), ref green.x, ref green.y, 0f, 1f);
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(" ");
			green.x = EditorGUILayout.FloatField(green.x, GUILayout.Width(60));
			GUILayout.FlexibleSpace();
			green.y = EditorGUILayout.FloatField(green.y, GUILayout.Width(60));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.MinMaxSlider(new GUIContent("Blue Channel"), ref blue.x, ref blue.y, 0f, 1f);
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel(" ");
			blue.x = EditorGUILayout.FloatField(blue.x, GUILayout.Width(60));
			GUILayout.FlexibleSpace();
			blue.y = EditorGUILayout.FloatField(blue.y, GUILayout.Width(60));
		EditorGUILayout.EndHorizontal();

		p_red.vector2Value = red;
		p_green.vector2Value = green;
		p_blue.vector2Value = blue;

		serializedObject.ApplyModifiedProperties();
    }
}
