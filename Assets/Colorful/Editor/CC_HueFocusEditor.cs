using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_HueFocus))]
public class CC_HueFocusEditor : CC_BaseEditor
{
	SerializedProperty p_hue;
	SerializedProperty p_range;
	SerializedProperty p_boost;
	SerializedProperty p_amount;
	Texture2D hueRamp;

	void OnEnable()
	{
		p_hue = serializedObject.FindProperty("hue");
		p_range = serializedObject.FindProperty("range");
		p_boost = serializedObject.FindProperty("boost");
		p_amount = serializedObject.FindProperty("amount");

		hueRamp = Resources.Load<Texture2D>("HueRamp");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.Separator();

		Rect rect = GUILayoutUtility.GetRect(0, 20);
		GUI.DrawTextureWithTexCoords(rect, hueRamp, new Rect(0.5f + p_hue.floatValue / 360f, 0f, 1f, 1f));

		GUI.enabled = false;
		float min = 180f - p_range.floatValue;
		float max = 180f + p_range.floatValue;
		EditorGUILayout.MinMaxSlider(ref min, ref max, 0f, 360f);
		GUI.enabled = true;

		EditorGUILayout.Separator();
		EditorGUILayout.PropertyField(p_hue);
		EditorGUILayout.PropertyField(p_range);
		EditorGUILayout.PropertyField(p_boost);
		EditorGUILayout.PropertyField(p_amount);

		serializedObject.ApplyModifiedProperties();
    }
}
