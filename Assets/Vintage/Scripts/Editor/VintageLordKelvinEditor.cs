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
  /// Vintage Kelvin Editor.
  /// </summary>
  [CustomEditor(typeof(VintageLordKelvin))]
  public class VintageLordKelvinEditor : ImageEffectBaseEditor
  {
    private VintageLordKelvin thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageLordKelvin)target;

      this.Help = "Gives a retro look by boosting the earth tones green, brown and orange and adds brightness.";
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