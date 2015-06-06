using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_PhotoFilter))]
public class CC_PhotoFilterEditor : Editor
{
	SerializedProperty p_color;
	SerializedProperty p_density;

	int selectedPreset = 0;
	static string[] presets = { "Warming Filter (85)", "Warming Filter (LBA)", "Warming Filter (81)",
								"Cooling Filter (80)", "Cooling Filter (LBB)", "Cooling Filter (82)",
								"Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Violet", "Magenta",
								"Sepia", "Deep Red", "Deep Blue", "Deep Emerald", "Deep Yellow", "Underwater" };

	static float [,] presetsData = { { 0.925f, 0.541f, 0.0f }, { 0.98f, 0.541f, 0.0f }, { 0.922f, 0.694f, 0.075f },
									 { 0.0f, 0.427f, 1.0f }, { 0.0f, 0.365f, 1.0f }, { 0.0f, 0.71f, 1.0f },
									 { 0.918f, 0.102f, 0.102f }, { 0.956f, 0.518f, 0.09f }, { 0.976f, 0.89f, 0.11f },
									 { 0.098f, 0.788f, 0.098f }, { 0.114f, 0.796f, 0.918f }, { 0.114f, 0.209f, 0.918f },
									 { 0.608f, 0.114f, 0.918f }, { 0.89f, 0.094f, 0.89f }, { 0.675f, 0.478f, 0.2f },
									 { 1.0f, 0.0f, 0.0f }, { 0.0f, 0.133f, 0.804f }, { 0.0f, 0.553f, 0.0f },
									 { 1.0f, 0.835f, 0.0f }, { 0.0f, 0.761f, 0.694f } };

	void OnEnable()
	{
		p_color = serializedObject.FindProperty("color");
		p_density = serializedObject.FindProperty("density");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_color);
		EditorGUILayout.PropertyField(p_density);

		GUI.changed = false;
		selectedPreset = EditorGUILayout.Popup("Preset", selectedPreset, presets);

		if (GUI.changed)
		{
			p_color.colorValue = new Color(
					presetsData[selectedPreset, 0],
					presetsData[selectedPreset, 1],
					presetsData[selectedPreset, 2]
				);
		}

		serializedObject.ApplyModifiedProperties();
    }
}
