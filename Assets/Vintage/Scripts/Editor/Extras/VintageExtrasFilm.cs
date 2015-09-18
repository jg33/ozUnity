///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace VintageImageEffects
{
  /// <summary>
  /// Film Editor.
  /// </summary>
  [CustomEditor(typeof(VintageExtrasFilm))]
  public class VintageExtrasFilmEditor : ImageEffectBaseEditor
  {
    private VintageExtrasFilm thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageExtrasFilm)target;

      this.Help = @"Create the effect of an old film.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
      thisTarget.grainStrength = VintageEditorHelper.IntSliderWithReset(@"Grain strength", VintageEditorHelper.TooltipGrainStrength, Mathf.RoundToInt(thisTarget.grainStrength), 0, 100, 0);

      thisTarget.grainSize = VintageEditorHelper.IntSliderWithReset(@"Grain size", VintageEditorHelper.TooltipGrainSize, Mathf.RoundToInt(thisTarget.grainSize * 10000.0f), 0, 100, 50) * 0.0001f;

      thisTarget.blinkStrenght = VintageEditorHelper.IntSliderWithReset(@"Blink strength", VintageEditorHelper.TooltipBlinkStrength, Mathf.RoundToInt(thisTarget.blinkStrenght * 1000.0f), 0, 100, 0) * 0.001f;
    }
  }
}