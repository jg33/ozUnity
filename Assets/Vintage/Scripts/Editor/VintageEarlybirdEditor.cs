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
  /// Vintage Earlybird Editor.
  /// </summary>
  [CustomEditor(typeof(VintageEarlybird))]
  public class VintageEarlybirdEditor : ImageEffectBaseEditor
  {
    private VintageEarlybird thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageEarlybird)target;

      this.Help = "Use Earlybird to get a retro 'Polaroid' feel with soft faded colors and a hint of yellow.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.curvesTex = EditorGUILayout.ObjectField("Curves", thisTarget.curvesTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.overlayTex = EditorGUILayout.ObjectField("Overlay", thisTarget.overlayTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.blowoutTex = EditorGUILayout.ObjectField("Blowout", thisTarget.blowoutTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.levelsTex = EditorGUILayout.ObjectField("Mapped", thisTarget.levelsTex, typeof(Texture2D), false) as Texture2D;
#endif

      thisTarget.obturation = VintageEditorHelper.IntSliderWithReset("Obturation", VintageEditorHelper.TooltipObturation, (int)(thisTarget.obturation * 50.0f), 0, 100, 50) * 0.02f;

      // Cheking errors.
      if (thisTarget.levelsTex == null ||
          thisTarget.overlayTex == null ||
          thisTarget.blowoutTex == null ||
          thisTarget.levelsTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}