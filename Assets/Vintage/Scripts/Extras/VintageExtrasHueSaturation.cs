///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Hue / Saturation.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Extras/Vintage Hue Saturation")]
  public class VintageExtrasHueSaturation : ImageEffectBase
  {
    /// <summary>
    /// Hue (0 .. 1).
    /// </summary>
    public float hue = 0.0f;

    /// <summary>
    /// Saturation (-1 .. 1).
    /// </summary>
    public float saturation = 1.0f;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/Extras/HueSaturation"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return true; } }

    /// <summary>
    /// Set the default values of the shader.
    /// </summary>
    public override void ResetDefaultValues()
    {
      hue = 0.0f;
      saturation = 1.0f;
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetFloat(VintageHelper.ShaderHue, hue);
      this.Material.SetFloat(VintageHelper.ShaderSaturation, saturation);

      base.SendValuesToShader();
    }
  }
}
