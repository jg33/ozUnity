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
  /// Vintage XPro II Editor.
  /// </summary>
  [CustomEditor(typeof(VintageXProII))]
  public class VintageXProIIEditor : ImageEffectBaseEditor
  {
    private VintageXProII thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageXProII)target;

      this.Help = "This effect gives your game a warm vintage feeling and saturated tones.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.levelsTex = EditorGUILayout.ObjectField("Mapped", thisTarget.levelsTex, typeof(Texture2D), false) as Texture2D;
#endif

      thisTarget.obturation = VintageEditorHelper.IntSliderWithReset("Obturation", VintageEditorHelper.TooltipObturation, (int)(thisTarget.obturation * 50.0f), 0, 100, 50) * 0.02f;

      // Cheking errors.
      if (thisTarget.levelsTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}