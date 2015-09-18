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
  /// Vintage Hefe Editor.
  /// </summary>
  [CustomEditor(typeof(VintageHefe))]
  public class VintageHefeEditor : ImageEffectBaseEditor
  {
    private VintageHefe thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageHefe)target;

      this.Help = "Hefe slightly increases saturation and gives a warm fuzzy tone to your game.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.edgeBurnTex = EditorGUILayout.ObjectField("Edge burn", thisTarget.edgeBurnTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.levelsTex = EditorGUILayout.ObjectField("Mapped", thisTarget.levelsTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.gradientTex = EditorGUILayout.ObjectField("PixelLevels", thisTarget.gradientTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.softLightTex = EditorGUILayout.ObjectField("Soft light", thisTarget.softLightTex, typeof(Texture2D), false) as Texture2D;
#endif

      // Cheking errors.
      if (thisTarget.edgeBurnTex == null ||
          thisTarget.levelsTex == null ||
          thisTarget.gradientTex == null ||
          thisTarget.softLightTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}