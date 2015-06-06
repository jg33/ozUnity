using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_DoubleVision))]
public class CC_DoubleVisionEditor : Editor
{
	SerializedProperty p_displace;
	SerializedProperty p_amount;

	void OnEnable()
	{
		p_displace = serializedObject.FindProperty("displace");
		p_amount = serializedObject.FindProperty("amount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_displace);
		EditorGUILayout.PropertyField(p_amount);

		serializedObject.ApplyModifiedProperties();
    }
}
