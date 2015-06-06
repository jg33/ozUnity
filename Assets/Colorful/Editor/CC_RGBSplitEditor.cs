using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_RGBSplit))]
public class CC_RGBSplitEditor : Editor
{
	SerializedProperty p_amount;
	SerializedProperty p_angle;

	void OnEnable()
	{
		p_amount = serializedObject.FindProperty("amount");
		p_angle = serializedObject.FindProperty("angle");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_amount);
		EditorGUILayout.PropertyField(p_angle);

		serializedObject.ApplyModifiedProperties();
    }
}
