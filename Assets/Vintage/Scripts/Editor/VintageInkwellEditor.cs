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
  /// Vintage Inkwell Editor.
  /// </summary>
  [CustomEditor(typeof(VintageInkwell))]
  public class VintageInkwellEditor : ImageEffectBaseEditor
  {
    private VintageInkwell thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageInkwell)target;

      this.Help = "Inkwell adds high contrast and also makes black and white.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.levelsTex = EditorGUILayout.ObjectField("Mapped", thisTarget.levelsTex, typeof(Texture2D), false) as Texture2D;
#endif

      // Cheking errors.
      if (thisTarget.levelsTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}