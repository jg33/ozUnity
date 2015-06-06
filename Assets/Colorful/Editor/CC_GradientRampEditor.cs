using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_GradientRamp))]
public class CC_GradientRampEditor : Editor
{
	SerializedProperty p_rampTexture;
	SerializedProperty p_amount;

	void OnEnable()
	{
		p_rampTexture = serializedObject.FindProperty("rampTexture");
		p_amount = serializedObject.FindProperty("amount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_rampTexture);
		EditorGUILayout.PropertyField(p_amount);

		serializedObject.ApplyModifiedProperties();
    }
}
