using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_HueSaturationValue))]
public class CC_HueSaturationValueEditor : Editor
{
	static string[] channels = { "Master", "Reds", "Yellows", "Greens", "Cyans", "Blues", "Magentas" };

	SerializedProperty p_masterHue;
	SerializedProperty p_masterSaturation;
	SerializedProperty p_masterValue;

	SerializedProperty p_redsHue;
	SerializedProperty p_redsSaturation;
	SerializedProperty p_redsValue;

	SerializedProperty p_yellowsHue;
	SerializedProperty p_yellowsSaturation;
	SerializedProperty p_yellowsValue;

	SerializedProperty p_greensHue;
	SerializedProperty p_greensSaturation;
	SerializedProperty p_greensValue;

	SerializedProperty p_cyansHue;
	SerializedProperty p_cyansSaturation;
	SerializedProperty p_cyansValue;

	SerializedProperty p_bluesHue;
	SerializedProperty p_bluesSaturation;
	SerializedProperty p_bluesValue;

	SerializedProperty p_magentasHue;
	SerializedProperty p_magentasSaturation;
	SerializedProperty p_magentasValue;

	SerializedProperty p_advanced;
	SerializedProperty p_currentChannel;

	void OnEnable()
	{
		p_masterHue = serializedObject.FindProperty("masterHue");
		p_masterSaturation = serializedObject.FindProperty("masterSaturation");
		p_masterValue = serializedObject.FindProperty("masterValue");

		p_redsHue = serializedObject.FindProperty("redsHue");
		p_redsSaturation = serializedObject.FindProperty("redsSaturation");
		p_redsValue = serializedObject.FindProperty("redsValue");

		p_yellowsHue = serializedObject.FindProperty("yellowsHue");
		p_yellowsSaturation = serializedObject.FindProperty("yellowsSaturation");
		p_yellowsValue = serializedObject.FindProperty("yellowsValue");

		p_greensHue = serializedObject.FindProperty("greensHue");
		p_greensSaturation = serializedObject.FindProperty("greensSaturation");
		p_greensValue = serializedObject.FindProperty("greensValue");

		p_cyansHue = serializedObject.FindProperty("cyansHue");
		p_cyansSaturation = serializedObject.FindProperty("cyansSaturation");
		p_cyansValue = serializedObject.FindProperty("cyansValue");

		p_bluesHue = serializedObject.FindProperty("bluesHue");
		p_bluesSaturation = serializedObject.FindProperty("bluesSaturation");
		p_bluesValue = serializedObject.FindProperty("bluesValue");

		p_magentasHue = serializedObject.FindProperty("magentasHue");
		p_magentasSaturation = serializedObject.FindProperty("magentasSaturation");
		p_magentasValue = serializedObject.FindProperty("magentasValue");

		p_advanced = serializedObject.FindProperty("advanced");
		p_currentChannel = serializedObject.FindProperty("currentChannel");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		bool advanced = p_advanced.boolValue;
		int channel = p_currentChannel.intValue;

		EditorGUILayout.BeginHorizontal();
			if (advanced) channel = EditorGUILayout.Popup(channel, channels);
			else channel = 0;

			advanced = GUILayout.Toggle(advanced, "Advanced Mode", EditorStyles.miniButton);
		EditorGUILayout.EndHorizontal();

		if (advanced)
			EditorGUILayout.LabelField("Advanced mode will only be available on SM3.0 compatible GPUs !", EditorStyles.boldLabel);

		switch (channel)
		{
			case 1: Channel(p_redsHue, p_redsSaturation, p_redsValue);
				break;
			case 2: Channel(p_yellowsHue, p_yellowsSaturation, p_yellowsValue);
				break;
			case 3: Channel(p_greensHue, p_greensSaturation, p_greensValue);
				break;
			case 4: Channel(p_cyansHue, p_cyansSaturation, p_cyansValue);
				break;
			case 5: Channel(p_bluesHue, p_bluesSaturation, p_bluesValue);
				break;
			case 6: Channel(p_magentasHue, p_magentasSaturation, p_magentasValue);
				break;
			default: Channel(p_masterHue, p_masterSaturation, p_masterValue);
				break;
		}

		p_advanced.boolValue = advanced;
		p_currentChannel.intValue = channel;

		serializedObject.ApplyModifiedProperties();
    }

	void Channel(SerializedProperty hue, SerializedProperty saturation, SerializedProperty value)
	{
		EditorGUILayout.PropertyField(hue, new GUIContent("Hue"));
		EditorGUILayout.PropertyField(saturation, new GUIContent("Saturation"));
		EditorGUILayout.PropertyField(value, new GUIContent("Value"));
	}
}
