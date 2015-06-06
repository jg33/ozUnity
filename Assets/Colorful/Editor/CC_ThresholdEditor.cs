using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Threshold))]
public class CC_ThresholdEditor : Editor
{
	SerializedProperty p_threshold;
	SerializedProperty p_useNoise;
	SerializedProperty p_noiseRange;

	void OnEnable()
	{
		p_threshold = serializedObject.FindProperty("threshold");
		p_useNoise = serializedObject.FindProperty("useNoise");
		p_noiseRange = serializedObject.FindProperty("noiseRange");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_threshold);
		EditorGUILayout.PropertyField(p_useNoise, new GUIContent("Noise"));

		if (p_useNoise.boolValue)
			EditorGUILayout.PropertyField(p_noiseRange);

		serializedObject.ApplyModifiedProperties();
    }
}
