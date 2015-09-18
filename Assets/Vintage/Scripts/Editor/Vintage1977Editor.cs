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
  /// Vintage 1977 Editor.
  /// </summary>
  [CustomEditor(typeof(Vintage1977))]
  public class Vintage1977Editor : ImageEffectBaseEditor
  {
    private Vintage1977 thisTarget;

    private void OnEnable()
    {
      thisTarget = (Vintage1977)target;

      this.Help = @"As the name suggests, this effect gives a you nostalgic 70’s feel. The increased exposure with a red tint gives the photograph a rosy, brighter, faded look.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.levelsTex = EditorGUILayout.ObjectField(@"Mapped", thisTarget.levelsTex, typeof(Texture2D), false) as Texture2D;
#endif

      // Cheking errors.
      if (thisTarget.levelsTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}