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
  [CustomEditor(typeof(VintageBrannan))]
  public class VintageBrannanEditor : ImageEffectBaseEditor
  {
    private VintageBrannan thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageBrannan)target;

      this.Help = @"This low-key effect brings out the grays and greens in your game.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
    thisTarget.processTex = EditorGUILayout.ObjectField(@"Process", thisTarget.processTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.blowoutTex = EditorGUILayout.ObjectField(@"Blowout", thisTarget.blowoutTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.contrastTex = EditorGUILayout.ObjectField(@"Contrast", thisTarget.contrastTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.lumaTex = EditorGUILayout.ObjectField(@"Luma", thisTarget.lumaTex, typeof(Texture2D), false) as Texture2D;
    thisTarget.screenTex = EditorGUILayout.ObjectField(@"Screen", thisTarget.screenTex, typeof(Texture2D), false) as Texture2D;
#endif

      // Cheking errors.
      if (thisTarget.processTex == null ||
          thisTarget.blowoutTex == null ||
          thisTarget.contrastTex == null ||
          thisTarget.lumaTex == null ||
          thisTarget.screenTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}