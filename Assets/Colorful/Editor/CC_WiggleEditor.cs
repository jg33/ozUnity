using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Wiggle))]
public class CC_WiggleEditor : Editor
{
	SerializedProperty p_timer;
	SerializedProperty p_speed;
	SerializedProperty p_scale;
	SerializedProperty p_autoTimer;

	void OnEnable()
	{
		p_timer = serializedObject.FindProperty("timer");
		p_speed = serializedObject.FindProperty("speed");
		p_scale = serializedObject.FindProperty("scale");
		p_autoTimer = serializedObject.FindProperty("autoTimer");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_autoTimer);
		EditorGUI.BeginDisabledGroup(p_autoTimer.boolValue);
		EditorGUILayout.PropertyField(p_timer);
		EditorGUI.EndDisabledGroup();
		EditorGUI.BeginDisabledGroup(!p_autoTimer.boolValue);
		EditorGUILayout.PropertyField(p_speed);
		EditorGUI.EndDisabledGroup();
		EditorGUILayout.PropertyField(p_scale);

		serializedObject.ApplyModifiedProperties();
    }
}
