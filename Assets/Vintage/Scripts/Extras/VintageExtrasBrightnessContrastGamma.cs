///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Brightness / Contrast / Gamma.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Extras/Vintage Brightness Contrast Gamma")]
  public class VintageExtrasBrightnessContrastGamma : ImageEffectBase
  {
    /// <summary>
    /// Brightness (-1 .. 1).
    /// </summary>
    public float brightness = 0.0f;

    /// <summary>
    /// Contrast (-1 .. 1).
    /// </summary>
    public float contrast = 0.0f;

    /// <summary>
    /// Gamma (0.1 .. 10).
    /// </summary>
    public float gamma = 1.0f;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/Extras/BrightnessContrastGamma"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return true; } }

    /// <summary>
    /// Set the default values of the shader.
    /// </summary>
    public override void ResetDefaultValues()
    {
      brightness = 0.0f;
      contrast = 0.0f;
      gamma = 1.0f;
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetFloat(VintageHelper.ShaderBrightness, brightness);
      this.Material.SetFloat(VintageHelper.ShaderContrast, contrast + 1.0f);
      this.Material.SetFloat(VintageHelper.ShaderGamma, 1.0f / gamma);

      base.SendValuesToShader();
    }
  }
}
