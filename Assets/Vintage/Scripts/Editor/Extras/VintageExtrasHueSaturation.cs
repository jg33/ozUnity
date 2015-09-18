///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace VintageImageEffects
{
  /// <summary>
  /// Hue / Saturation Editor.
  /// </summary>
  [CustomEditor(typeof(VintageExtrasHueSaturation))]
  public class VintageExtrasHueSaturationEditor : ImageEffectBaseEditor
  {
    private VintageExtrasHueSaturation thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageExtrasHueSaturation)target;

      this.Help = @"Controls the color hue and saturation.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
      thisTarget.hue = VintageEditorHelper.IntSliderWithReset("Hue", VintageEditorHelper.TooltipHue, Mathf.RoundToInt(thisTarget.hue * 360.0f), 0, 360, 0) / 360.0f;

      thisTarget.saturation = VintageEditorHelper.SliderWithReset("Saturation", VintageEditorHelper.TooltipSaturation, Mathf.RoundToInt(thisTarget.saturation * 100.0f), 0, 100, 100) * 0.01f;
    }
  }
}