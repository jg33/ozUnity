using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Kuwahara))]
public class CC_KuwaharaEditor : Editor
{
	SerializedProperty p_radius;

	void OnEnable()
	{
		p_radius = serializedObject.FindProperty("radius");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_radius);

		serializedObject.ApplyModifiedProperties();
    }
}
