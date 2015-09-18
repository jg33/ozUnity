///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Sutro.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Sutro")]
  public sealed class VintageSutro : ImageEffectBase
  {
    /// <summary>
    // Levels. Default 'Resources/sutroEdgeBurn'.
    /// </summary>
    public Texture2D edgeBurnTex;

    /// <summary>
    // Levels. Default 'Resources/sutroCurves'.
    /// </summary>
    public Texture2D curvesTex;

    /// <summary>
    /// Obturation of the vignette (0 none, 2 semi closed).
    /// </summary>
    public float obturation = 1.0f;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageSutro"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      edgeBurnTex = VintageHelper.LoadTextureFromResources(@"Textures/sutroEdgeBurn");
      curvesTex = VintageHelper.LoadTextureFromResources(@"Textures/sutroCurves");

      base.CreateMaterial();
    }

    /// <summary>
    /// Set the default values of the shader.
    /// </summary>
    public override void ResetDefaultValues()
    {
      obturation = 1.0f;

      base.ResetDefaultValues();
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetTexture(VintageHelper.ShaderEdgeBurnTex, edgeBurnTex);
      this.Material.SetTexture(VintageHelper.ShaderCurvesTex, curvesTex);
      this.Material.SetFloat(VintageHelper.ShaderObturation, obturation);

      base.SendValuesToShader();
    }
  }
}