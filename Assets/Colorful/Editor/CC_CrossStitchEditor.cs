using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_CrossStitch))]
public class CC_CrossStitchEditor : Editor
{
	SerializedProperty p_size;
	SerializedProperty p_brightness;
	SerializedProperty p_invert;
	SerializedProperty p_pixelize;

	void OnEnable()
	{
		p_size = serializedObject.FindProperty("size");
		p_brightness = serializedObject.FindProperty("brightness");
		p_invert = serializedObject.FindProperty("invert");
		p_pixelize = serializedObject.FindProperty("pixelize");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.LabelField("Size works best with power of two values !", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(p_size);
		EditorGUILayout.PropertyField(p_brightness);
		EditorGUILayout.PropertyField(p_invert);
		EditorGUILayout.PropertyField(p_pixelize);

		serializedObject.ApplyModifiedProperties();
	}
}
