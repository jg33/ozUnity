///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Uncomment to show maps in the Editor (not recommended).
//#define _SHOW_MAPS

using UnityEngine;
using UnityEditor;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Toaster Editor.
  /// </summary>
  [CustomEditor(typeof(VintageToaster))]
  public class VintageToasterEditor : ImageEffectBaseEditor
  {
    private VintageToaster thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageToaster)target;

      this.Help = "Gives your game a burnt, aged look. It also also adds a slight texture plus vignetting.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.metalTex = EditorGUILayout.ObjectField("Metal", thisTarget.metalTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.softLightTex = EditorGUILayout.ObjectField("Soft light", thisTarget.softLightTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.curvesTex = EditorGUILayout.ObjectField("Curves", thisTarget.curvesTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.overlayWarmTex = EditorGUILayout.ObjectField("Overlay warm", thisTarget.overlayWarmTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.colorShiftTex = EditorGUILayout.ObjectField("Color shift", thisTarget.colorShiftTex, typeof(Texture2D), false) as Texture2D;
#endif

      // Cheking errors.
      if (thisTarget.metalTex == null ||
          thisTarget.softLightTex == null ||
          thisTarget.curvesTex == null ||
          thisTarget.overlayWarmTex == null ||
          thisTarget.colorShiftTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}