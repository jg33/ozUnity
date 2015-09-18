///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Toaster.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Toaster")]
  public sealed class VintageToaster : ImageEffectBase
  {
    /// <summary>
    /// Default 'Resources/toasterMetal'.
    /// </summary>
    public Texture2D metalTex;

    /// <summary>
    /// Default 'Resources/toasterSoftLight'.
    /// </summary>
    public Texture2D softLightTex;

    /// <summary>
    /// Default 'Resources/toasterCurves'.
    /// </summary>
    public Texture2D curvesTex;

    /// <summary>
    /// Default 'Resources/toasterOverlayMapWarm'.
    /// </summary>
    public Texture2D overlayWarmTex;

    /// <summary>
    /// Default 'Resources/toasterColorShift'.
    /// </summary>
    public Texture2D colorShiftTex;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageToaster"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      metalTex = VintageHelper.LoadTextureFromResources(@"Textures/toasterMetal");
      softLightTex = VintageHelper.LoadTextureFromResources(@"Textures/toasterSoftLight");
      curvesTex = VintageHelper.LoadTextureFromResources(@"Textures/toasterCurves");
      overlayWarmTex = VintageHelper.LoadTextureFromResources(@"Textures/toasterOverlayMapWarm");
      colorShiftTex = VintageHelper.LoadTextureFromResources(@"Textures/toasterColorShift");

      base.CreateMaterial();
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetTexture(VintageHelper.ShaderMetalTex, metalTex);
      this.Material.SetTexture(VintageHelper.ShaderSoftLightTex, softLightTex);
      this.Material.SetTexture(VintageHelper.ShaderCurvesTex, curvesTex);
      this.Material.SetTexture(VintageHelper.ShaderOverlayWarmTex, overlayWarmTex);
      this.Material.SetTexture(VintageHelper.ShaderColorShiftTex, colorShiftTex);

      base.SendValuesToShader();
    }
  }
}