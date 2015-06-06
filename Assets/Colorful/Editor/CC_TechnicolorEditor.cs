using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CC_Technicolor))]
public class CC_TechnicolorEditor : Editor
{
	SerializedProperty p_exposure;
	SerializedProperty p_balance;
	SerializedProperty p_amount;

	void OnEnable()
	{
		p_exposure = serializedObject.FindProperty("exposure");
		p_balance = serializedObject.FindProperty("balance");
		p_amount = serializedObject.FindProperty("amount");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(p_exposure);

		EditorGUILayout.LabelField("Balance", EditorStyles.boldLabel);

		EditorGUI.indentLevel++;
		Vector3 balance = p_balance.vector3Value;
		balance.x = EditorGUILayout.Slider("Red", balance.x, 0f, 1f);
		balance.y = EditorGUILayout.Slider("Green", balance.y, 0f, 1f);
		balance.z = EditorGUILayout.Slider("Blue", balance.z, 0f, 1f);
		p_balance.vector3Value = balance;
		EditorGUI.indentLevel--;

		EditorGUILayout.PropertyField(p_amount);

		serializedObject.ApplyModifiedProperties();
	}
}
