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
  /// Vintage Amaro Editor.
  /// </summary>
  [CustomEditor(typeof(VintageAmaro))]
  public class VintageAmaroEditor : ImageEffectBaseEditor
  {
    private VintageAmaro thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageAmaro)target;

      this.Help = @"This effect adds more light to the centre of the screen and darkens around the edges.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.blowoutTex = EditorGUILayout.ObjectField(@"BlowOut", thisTarget.blowoutTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.overlayTex = EditorGUILayout.ObjectField(@"Overlay", thisTarget.overlayTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.levelsTex = EditorGUILayout.ObjectField(@"Mapped", thisTarget.levelsTex, typeof(Texture2D), false) as Texture2D;
#endif
      thisTarget.overlayStrength = VintageEditorHelper.IntSliderWithReset(@"Overlay", VintageEditorHelper.TooltipOverlay, (int)(thisTarget.overlayStrength * 100.0f), 0, 100, 50) * 0.01f;

      // Cheking errors.
      if (thisTarget.blowoutTex == null ||
          thisTarget.overlayTex == null ||
          thisTarget.levelsTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}