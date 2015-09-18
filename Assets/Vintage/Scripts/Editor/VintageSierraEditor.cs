///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Uncomment to show maps in the Editor.
//#define _SHOW_MAPS

using UnityEngine;
using UnityEditor;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Rise Editor.
  /// </summary>
  [CustomEditor(typeof(VintageSierra))]
  public class VintageSierraEditor : ImageEffectBaseEditor
  {
    private VintageSierra thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageSierra)target;

      this.Help = "Sierra makes the game appear softer by adding bluish tones while emphasizing darks and yellows.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.blowoutTex = EditorGUILayout.ObjectField("Blowout", thisTarget.blowoutTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.overlayTex = EditorGUILayout.ObjectField("Overlay", thisTarget.overlayTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.levelsTex = EditorGUILayout.ObjectField("Mapped", thisTarget.levelsTex, typeof(Texture2D), false) as Texture2D;
#endif

      thisTarget.overlayStrength = VintageEditorHelper.IntSliderWithReset("Overlay", VintageEditorHelper.TooltipOverlay, (int)(thisTarget.overlayStrength * 100.0f), 0, 100, 100) * 0.01f;

      // Cheking errors.
      if (thisTarget.blowoutTex == null ||
          thisTarget.overlayTex == null ||
          thisTarget.levelsTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}