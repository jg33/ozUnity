using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_ChannelMixer))]
public class CC_ChannelMixerEditor : CC_BaseEditor
{
	SerializedProperty p_redR;
	SerializedProperty p_redG;
	SerializedProperty p_redB;
	SerializedProperty p_greenR;
	SerializedProperty p_greenG;
	SerializedProperty p_greenB;
	SerializedProperty p_blueR;
	SerializedProperty p_blueG;
	SerializedProperty p_blueB;
	SerializedProperty p_constantR;
	SerializedProperty p_constantG;
	SerializedProperty p_constantB;

	SerializedProperty p_currentChannel;

	void OnEnable()
	{
		p_redR = serializedObject.FindProperty("redR");
		p_redG = serializedObject.FindProperty("redG");
		p_redB = serializedObject.FindProperty("redB");
		p_greenR = serializedObject.FindProperty("greenR");
		p_greenG = serializedObject.FindProperty("greenG");
		p_greenB = serializedObject.FindProperty("greenB");
		p_blueR = serializedObject.FindProperty("blueR");
		p_blueG = serializedObject.FindProperty("blueG");
		p_blueB = serializedObject.FindProperty("blueB");
		p_constantR = serializedObject.FindProperty("constantR");
		p_constantG = serializedObject.FindProperty("constantG");
		p_constantB = serializedObject.FindProperty("constantB");

		p_currentChannel = serializedObject.FindProperty("currentChannel");
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		serializedObject.Update();

		int currentChannel = p_currentChannel.intValue;

		GUILayout.BeginHorizontal();

			if (GUILayout.Button("Red", (currentChannel == 0) ? tabLeftOn : tabLeft)) currentChannel = 0;
			if (GUILayout.Button("Green", (currentChannel == 1) ? tabMiddleOn : tabMiddle)) currentChannel = 1;
			if (GUILayout.Button("Blue", (currentChannel == 2) ? tabRightOn : tabRight)) currentChannel = 2;
		
		GUILayout.EndHorizontal();

		if (currentChannel == 0) ChannelUI(p_redR, p_redG, p_redB, p_constantR);
		if (currentChannel == 1) ChannelUI(p_greenR, p_greenG, p_greenB, p_constantG);
		if (currentChannel == 2) ChannelUI(p_blueR, p_blueG, p_blueB, p_constantB);

		p_currentChannel.intValue = currentChannel;

		serializedObject.ApplyModifiedProperties();
	}

	void ChannelUI(SerializedProperty red, SerializedProperty green, SerializedProperty blue, SerializedProperty constant)
	{
		EditorGUILayout.PropertyField(red, new GUIContent("% Red"));
		EditorGUILayout.PropertyField(green, new GUIContent("% Green"));
		EditorGUILayout.PropertyField(blue, new GUIContent("% Blue"));
		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(constant, new GUIContent("Constant"));
	}
}
