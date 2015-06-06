using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Vintage))]
public class CC_VintageEditor : Editor
{
	SerializedProperty p_filter;
	SerializedProperty p_amout;

	void OnEnable()
	{
		p_filter = serializedObject.FindProperty("filter");
		p_amout = serializedObject.FindProperty("amount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_filter);
		EditorGUILayout.PropertyField(p_amout);

		serializedObject.ApplyModifiedProperties();
	}
}
