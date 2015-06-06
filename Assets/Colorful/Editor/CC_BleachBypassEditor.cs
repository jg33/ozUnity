using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_BleachBypass))]
public class CC_BleachBypassEditor : Editor
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
