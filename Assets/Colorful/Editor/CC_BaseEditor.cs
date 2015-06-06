using UnityEngine;
using UnityEditor;

public class CC_BaseEditor : Editor
{
	public static GUIStyle tabLeft;
	public static GUIStyle tabMiddle;
	public static GUIStyle tabRight;
	public static GUIStyle tabLeftOn;
	public static GUIStyle tabMiddleOn;
	public static GUIStyle tabRightOn;

	public override void OnInspectorGUI()
	{
		if (tabLeft == null)
		{
			tabLeft = new GUIStyle(EditorStyles.miniButtonLeft);
			tabMiddle = new GUIStyle(EditorStyles.miniButtonMid);
			tabRight = new GUIStyle(EditorStyles.miniButtonRight);

			tabLeftOn = new GUIStyle(tabLeft);
			tabLeftOn.active = tabLeft.onActive;
			tabLeftOn.normal = tabLeft.onNormal;
			tabLeftOn.hover = tabLeft.onHover;

			tabMiddleOn = new GUIStyle(tabMiddle);
			tabMiddleOn.active = tabMiddle.onActive;
			tabMiddleOn.normal = tabMiddle.onNormal;
			tabMiddleOn.hover = tabMiddle.onHover;

			tabRightOn = new GUIStyle(tabRight);
			tabRightOn.active = tabRight.onActive;
			tabRightOn.normal = tabRight.onNormal;
			tabRightOn.hover = tabRight.onHover;
		}
	}
}
