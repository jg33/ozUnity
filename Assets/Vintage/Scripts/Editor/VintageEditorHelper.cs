///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;

namespace VintageImageEffects
{
  /// <summary>
  /// Utilities for the Editor.
  /// </summary>
  public static class VintageEditorHelper
  {
    /// <summary>
    /// Tooltips.
    /// </summary>
    public static readonly string TooltipAmount = "The strength of the effect.\nFrom 0 (no effect) to 100 (full effect).";
    public static readonly string TooltipOverlay = "The strength with which the texture is applied.\nFrom 0 (no texture) to 100 (full texture).";
    public static readonly string TooltipObturation = "Obturation of the vignette.\nFrom 0 (no obturation) to 100 (many obturation).";
    
    public static readonly string TooltipBrightness = "The Screen appears to be more o less radiating light.\nFrom -100 (dark) to 100 (full light).";
    public static readonly string TooltipContrast = "The difference in color and brightness.\nFrom -100 (no constrast) to 100 (full constrast).";
    public static readonly string TooltipGamma = "Optimizes the contrast and brightness in the midtones.\nFrom 0.01 to 10.";

    public static readonly string TooltipHue = "The color wheel.\nFrom 0 to 360.";
    public static readonly string TooltipSaturation = "Intensity of a colors.\nFrom 0 to 100.";

    public static readonly string TooltipGrainStrength = "Film grain or granularity is noise texture due to the presence of small particles.\nFrom 0 (no grain) to 100 (full grain).";
    public static readonly string TooltipGrainSize = "The size of the grain.\nFrom 0 (no grain) to 100 (full size).";
    public static readonly string TooltipBlinkStrength = "Brightness variation.\nFrom 0 (no fluctuation) to 100 (full epilepsy).";

    /// <summary>
    /// Errors.
    /// </summary>
    public static readonly string ErrorTextureMissing = @"Some texture channels are missing. Please check that nothing is missing in 'Vintage/Resources/Textures'.";

    /// <summary>
    /// Misc.
    /// </summary>
    public static readonly string DocumentationURL = @"http://www.ibuprogames.com/2015/05/04/vintage-image-efffects/";

    /// <summary>
    /// A slider with a reset button.
    /// </summary>
    public static float SliderWithReset(string label, string tooltip, float value, float minValue, float maxValue, float defaultValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.Slider(new GUIContent(label, tooltip), value, minValue, maxValue);

        if (GUILayout.Button("R", GUILayout.Width(18.0f), GUILayout.Height(17.0f)) == true)
          value = defaultValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }

    /// <summary>
    /// A slider with a reset button.
    /// </summary>
    public static int IntSliderWithReset(string label, string tooltip, int value, int minValue, int maxValue, int defaultValue)
    {
      EditorGUILayout.BeginHorizontal();
      {
        value = EditorGUILayout.IntSlider(new GUIContent(label, tooltip), value, minValue, maxValue);

        if (GUILayout.Button(new GUIContent("R", "Reset to '" + defaultValue + "'."), GUILayout.Width(18.0f), GUILayout.Height(17.0f)) == true)
          value = defaultValue;
      }
      EditorGUILayout.EndHorizontal();

      return value;
    }
  }
}