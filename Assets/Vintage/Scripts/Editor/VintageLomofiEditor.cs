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
  /// Vintage Lomofi Editor.
  /// </summary>
  [CustomEditor(typeof(VintageLomofi))]
  public class VintageLomofiEditor : ImageEffectBaseEditor
  {
    private void OnEnable()
    {
      thisTarget = (VintageLomofi)target;

      this.Help = "The Lomofi efect gives your game a dreamy, blurry effect and saturated colors.";
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

    private VintageLomofi thisTarget;
  }
}