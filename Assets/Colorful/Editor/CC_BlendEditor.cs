using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Blend))]
public class CC_BlendEditor : Editor
{
	SerializedProperty p_amount;
	SerializedProperty p_texture;
	SerializedProperty p_mode;

	static string[] modes = { "Darken", "Multiply", "Color Burn", "Linear Burn", "Darker Color", "",
							  "Lighten", "Screen", "Color Dodge", "Linear Dodge (Add)", "Lighter Color", "",
							  "Overlay", "Soft Light", "Hard Light", "Vivid Light", "Linear Light", "Pin Light", "Hard Mix", "",
							  "Difference", "Exclusion", "Subtract", "Divide" };

	void OnEnable()
	{
		p_amount = serializedObject.FindProperty("amount");
		p_texture = serializedObject.FindProperty("texture");
		p_mode = serializedObject.FindProperty("mode");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		p_mode.intValue = EditorGUILayout.Popup("Mode", p_mode.intValue, modes);
		EditorGUILayout.PropertyField(p_texture);
		EditorGUILayout.PropertyField(p_amount);

		serializedObject.ApplyModifiedProperties();
    }
}
