using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Sharpen))]
public class CC_SharpenEditor : Editor
{
	SerializedProperty p_strength;
	SerializedProperty p_clamp;

	void OnEnable()
	{
		p_strength = serializedObject.FindProperty("strength");
		p_clamp = serializedObject.FindProperty("clamp");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_strength);
		EditorGUILayout.PropertyField(p_clamp);

		serializedObject.ApplyModifiedProperties();
    }
}
