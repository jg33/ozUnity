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
  /// Vintage Crema Editor.
  /// </summary>
  [CustomEditor(typeof(VintageCrema))]
  public class VintageCremaEditor : ImageEffectBaseEditor
  {
    private VintageCrema thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageCrema)target;

      this.Help = "Crema makes games look creamy and smooth.\n\nRequires hardware that supports 3D textures.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
#if _SHOW_MAPS
      thisTarget.lutTex = EditorGUILayout.ObjectField("Lut", thisTarget.lutTex, typeof(Texture3D), false) as Texture3D;
#endif

      // Cheking errors.
      if (thisTarget.lutTex == null)
        this.Errors += VintageEditorHelper.ErrorTextureMissing;
    }
  }
}