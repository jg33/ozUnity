///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace VintageImageEffects
{
  /// <summary>
  /// Brightness / Contrast / Gamma Editor.
  /// </summary>
  [CustomEditor(typeof(VintageExtrasBrightnessContrastGamma))]
  public class VintageExtrasBrightnessContrastGammaEditor : ImageEffectBaseEditor
  {
    private VintageExtrasBrightnessContrastGamma thisTarget;

    private void OnEnable()
    {
      thisTarget = (VintageExtrasBrightnessContrastGamma)target;

      this.Help = @"Controls the brightness, contrast and gamma.";
    }

    /// <summary>
    /// Inspector.
    /// </summary>
    protected override void Inspector()
    {
      thisTarget.brightness = VintageEditorHelper.IntSliderWithReset("Brightness", VintageEditorHelper.TooltipBrightness, Mathf.RoundToInt(thisTarget.brightness * 100.0f), -100, 100, 0) * 0.01f;

      thisTarget.contrast = VintageEditorHelper.IntSliderWithReset("Contrast", VintageEditorHelper.TooltipContrast, Mathf.RoundToInt(thisTarget.contrast * 100.0f), -100, 100, 0) * 0.01f;

      thisTarget.gamma = VintageEditorHelper.SliderWithReset("Gamma", VintageEditorHelper.TooltipGamma, thisTarget.gamma, 0.01f, 10.0f, 1.0f);
    }
  }
}