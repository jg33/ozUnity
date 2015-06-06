using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_AnalogTV))]
public class CC_AnalogTVEditor : Editor
{
	SerializedProperty p_autoPhase;
	SerializedProperty p_phase;
	SerializedProperty p_grayscale;
	SerializedProperty p_noiseIntensity;
	SerializedProperty p_scanlinesIntensity;
	SerializedProperty p_scanlinesCount;
	SerializedProperty p_scanlinesOffset;
	SerializedProperty p_distortion;
	SerializedProperty p_cubicDistortion;
	SerializedProperty p_scale;

	void OnEnable()
	{
		p_autoPhase = serializedObject.FindProperty("autoPhase");
		p_phase = serializedObject.FindProperty("phase");
		p_grayscale = serializedObject.FindProperty("grayscale");
		p_noiseIntensity = serializedObject.FindProperty("noiseIntensity");
		p_scanlinesIntensity = serializedObject.FindProperty("scanlinesIntensity");
		p_scanlinesCount = serializedObject.FindProperty("scanlinesCount");
		p_scanlinesOffset = serializedObject.FindProperty("scanlinesOffset");
		p_distortion = serializedObject.FindProperty("distortion");
		p_cubicDistortion = serializedObject.FindProperty("cubicDistortion");
		p_scale = serializedObject.FindProperty("scale");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_autoPhase);
		EditorGUI.BeginDisabledGroup(p_autoPhase.boolValue);
		EditorGUILayout.PropertyField(p_phase, new GUIContent("Phase (time)"));
		EditorGUI.EndDisabledGroup();

		EditorGUILayout.PropertyField(p_grayscale, new GUIContent("Convert to Grayscale"));

		GUILayout.Label("Analog Effect", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(p_noiseIntensity);
		EditorGUILayout.PropertyField(p_scanlinesIntensity);
		EditorGUILayout.PropertyField(p_scanlinesCount);
		EditorGUILayout.PropertyField(p_scanlinesOffset);
		EditorGUI.indentLevel--;

		GUILayout.Label("Barrel Distortion", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		EditorGUILayout.PropertyField(p_distortion);
		EditorGUILayout.PropertyField(p_cubicDistortion);
		EditorGUILayout.PropertyField(p_scale, new GUIContent("Scale (Zoom)"));
		EditorGUI.indentLevel--;

		serializedObject.ApplyModifiedProperties();
    }
}
