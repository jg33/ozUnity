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
  /// Vintage Sutro Editor.
  /// </summary>
  [CustomEditor(typeof(VintageSutro))]
  public class VintageSutroEditor : ImageEffectBaseEditor
  {
    private VintageSutro thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageSutro)target;

      this.Help = "Sutro gives you Sepia-like tones, with an emphasis on purple and brown.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.edgeBurnTex = EditorGUILayout.ObjectField("Edge burn", thisTarget.edgeBurnTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.curvesTex = EditorGUILayout.ObjectField("Curves", thisTarget.curvesTex, typeof(Texture2D), false) as Texture2D;
#endif
      thisTarget.obturation = VintageEditorHelper.IntSliderWithReset("Obturation", VintageEditorHelper.TooltipObturation, (int)(thisTarget.obturation * 50.0f), 0, 100, 50) * 0.02f;

      // Cheking errors.
      if (thisTarget.edgeBurnTex == null ||
          thisTarget.curvesTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}