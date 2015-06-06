using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Negative))]
public class CC_NegativeEditor : Editor
{
	SerializedProperty p_amount;

	void OnEnable()
	{
		p_amount = serializedObject.FindProperty("amount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_amount);

		serializedObject.ApplyModifiedProperties();
    }
}
