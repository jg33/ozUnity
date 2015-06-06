using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Posterize))]
public class CC_PosterizeEditor : Editor
{
	SerializedProperty p_levels;

	void OnEnable()
	{
		p_levels = serializedObject.FindProperty("levels");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_levels);

		serializedObject.ApplyModifiedProperties();
    }
}
