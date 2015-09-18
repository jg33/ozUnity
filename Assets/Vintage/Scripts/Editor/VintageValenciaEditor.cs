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
  /// Vintage Valencia Editor.
  /// </summary>
  [CustomEditor(typeof(VintageValencia))]
  public class VintageValenciaEditor : ImageEffectBaseEditor
  {
    private VintageValencia thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageValencia)target;

      this.Help = "Gives your game a slight faded, 1980’s touch by adding a light brown and gray tint.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.levelsTex = EditorGUILayout.ObjectField("Mapped", thisTarget.levelsTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.gradTex = EditorGUILayout.ObjectField("PixelLevels", thisTarget.gradTex, typeof(Texture2D), false) as Texture2D;
#endif

      // Cheking errors.
      if (thisTarget.levelsTex == null ||
          thisTarget.gradTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}