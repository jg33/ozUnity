using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_LookupFilter))]
public class CC_LookupFilterEditor : Editor
{
	SerializedProperty p_lookupTexture;
	SerializedProperty p_amout;

	void OnEnable()
	{
		p_lookupTexture = serializedObject.FindProperty("lookupTexture");
		p_amout = serializedObject.FindProperty("amount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_lookupTexture);
		EditorGUILayout.PropertyField(p_amout);
		EditorGUILayout.LabelField("Read the documentation for more information about this effect.", EditorStyles.boldLabel);

		serializedObject.ApplyModifiedProperties();
    }
}
