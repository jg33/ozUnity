///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Earlybird.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Earlybird")]
  public sealed class VintageEarlybird : ImageEffectBase
  {
    /// <summary>
    /// Default 'Resources/earlyBirdCurves.png'.
    /// </summary>
    public Texture2D curvesTex;

    /// <summary>
    /// Default 'Resources/earlybirdOverlayMap.png'.
    /// </summary>
    public Texture2D overlayTex;

    /// <summary>
    /// Default 'Resources/earlybirdBlowout.png'.
    /// </summary>
    public Texture2D blowoutTex;

    /// <summary>
    /// Default 'Resources/earlybirdMap.png'.
    /// </summary>
    public Texture2D levelsTex;

    /// <summary>
    /// Obturation of the vignette (0 none, 2 semi closed).
    /// </summary>
    public float obturation = 1.0f;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageEarlybird"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      curvesTex = VintageHelper.LoadTextureFromResources(@"Textures/earlyBirdCurves");
      overlayTex = VintageHelper.LoadTextureFromResources(@"Textures/earlybirdOverlayMap");
      blowoutTex = VintageHelper.LoadTextureFromResources(@"Textures/earlybirdBlowout");
      levelsTex = VintageHelper.LoadTextureFromResources(@"Textures/earlybirdMap");

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
      this.Material.SetTexture(VintageHelper.ShaderCurvesTex, curvesTex);
      this.Material.SetTexture(VintageHelper.ShaderOverlayTex, overlayTex);
      this.Material.SetTexture(VintageHelper.ShaderBlowoutTex, blowoutTex);
      this.Material.SetTexture(VintageHelper.ShaderLevelsTex, levelsTex);
      this.Material.SetFloat(VintageHelper.ShaderObturation, obturation);

      base.SendValuesToShader();
    }
  }
}