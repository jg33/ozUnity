///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Film.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Extras/Film")]
  public class VintageExtrasFilm : ImageEffectBase
  {
    /// <summary>
    /// Grain (0 .. 100).
    /// </summary>
    public float grainStrength = 0.0f;

    /// <summary>
    /// Grain size (0 .. 1).
    /// </summary>
    public float grainSize = 0.005f;

    /// <summary>
    /// Luminance blink strength (0.0 .. 0.1).
    /// </summary>
    public float blinkStrenght = 0.0f;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/Extras/Film"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return true; } }

    /// <summary>
    /// Set the default values of the shader.
    /// </summary>
    public override void ResetDefaultValues()
    {
      grainStrength = 0.0f;
      grainSize = 0.005f;
      blinkStrenght = 0.0f;
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetFloat(VintageHelper.ShaderFilmGrainStrength, grainStrength);
      this.Material.SetFloat(VintageHelper.ShaderFilmGrainSize, grainSize);
      this.Material.SetFloat(VintageHelper.ShaderFilmBlinkStrenght, blinkStrenght);

      base.SendValuesToShader();
    }
  }
}
