using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Vibrance))]
public class CC_VibranceEditor : Editor
{
	SerializedProperty p_amount;
	SerializedProperty p_redChannel;
	SerializedProperty p_greenChannel;
	SerializedProperty p_blueChannel;
	SerializedProperty p_advanced;

	void OnEnable()
	{
		p_amount = serializedObject.FindProperty("amount");
		p_redChannel = serializedObject.FindProperty("redChannel");
		p_greenChannel = serializedObject.FindProperty("greenChannel");
		p_blueChannel = serializedObject.FindProperty("blueChannel");
		p_advanced = serializedObject.FindProperty("advanced");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		p_advanced.boolValue = GUILayout.Toggle(p_advanced.boolValue, "Advanced Mode", EditorStyles.miniButton);

		EditorGUILayout.PropertyField(p_amount, new GUIContent("Vibrance"));

		if (p_advanced.boolValue)
		{
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(p_redChannel);
			EditorGUILayout.PropertyField(p_greenChannel);
			EditorGUILayout.PropertyField(p_blueChannel);
			EditorGUI.indentLevel--;
		}

		serializedObject.ApplyModifiedProperties();
    }
}
