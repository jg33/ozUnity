///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace VintageImageEffects
{
  /// <summary>
  /// ImageEffect Editor Base.
  /// </summary>
  [CustomEditor(typeof(ImageEffectBase))]
  public abstract class ImageEffectBaseEditor : Editor
  {
    /// <summary>
    /// Help text.
    /// </summary>
    public string Help { get; set; }

    /// <summary>
    /// Warnings.
    /// </summary>
    public string Warnings { get; set; }

    /// <summary>
    /// Errors.
    /// </summary>
    public string Errors { get; set; }

    private ImageEffectBase baseTarget;

    /// <summary>
    /// OnInspectorGUI.
    /// </summary>
    public override void OnInspectorGUI()
    {
      if (baseTarget == null)
        baseTarget = this.target as ImageEffectBase;

      EditorGUIUtility.LookLikeControls();
      
      EditorGUI.indentLevel = 0;

      EditorGUIUtility.labelWidth = 125.0f;

      EditorGUILayout.BeginVertical();
      {
        EditorGUILayout.Separator();

#if (UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6)
        if (EditorGUIUtility.isProSkin == true)
#endif
        {
          /////////////////////////////////////////////////
          // Common.
          /////////////////////////////////////////////////
          baseTarget.amount = VintageEditorHelper.IntSliderWithReset("Amount", VintageEditorHelper.TooltipAmount, Mathf.RoundToInt(baseTarget.amount * 100.0f), 0, 100, 100) * 0.01f;
          
          Inspector();

          EditorGUILayout.Separator();

          /////////////////////////////////////////////////
          // Misc.
          /////////////////////////////////////////////////

          EditorGUILayout.BeginHorizontal();
          {
            if (GUILayout.Button(new GUIContent("[web]", "Open website"), GUI.skin.label) == true)
              Application.OpenURL(VintageEditorHelper.DocumentationURL);

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Reset ALL") == true)
              baseTarget.ResetDefaultValues();
          }
          EditorGUILayout.EndHorizontal();

          EditorGUILayout.Separator();
        }
#if (UNITY_4_0 || UNITY_4_0_1 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6)
        else
        {
          this.Help = string.Empty;
          this.Errors = "'Vintage - Image Effects' require Unity Pro version!";
        }
#endif
        if (string.IsNullOrEmpty(Warnings) == false)
        {
          EditorGUILayout.HelpBox(Warnings, MessageType.Warning);

          EditorGUILayout.Separator();
        }

        if (string.IsNullOrEmpty(Errors) == false)
        {
          EditorGUILayout.HelpBox(Errors, MessageType.Error);

          EditorGUILayout.Separator();
        }

        if (string.IsNullOrEmpty(Help) == false)
          EditorGUILayout.HelpBox(Help, MessageType.Info);
      }
      EditorGUILayout.EndVertical();

      Warnings = Errors = string.Empty;

      if (GUI.changed == true)
        EditorUtility.SetDirty(target);

      EditorGUIUtility.LookLikeControls();

      EditorGUI.indentLevel = 0;

      EditorGUIUtility.labelWidth = 125.0f;
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected virtual void Inspector()
    {
      DrawDefaultInspector();
    }
  }
}