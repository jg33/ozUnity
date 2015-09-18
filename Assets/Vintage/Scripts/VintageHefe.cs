///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Hefe.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Hefe")]
  public sealed class VintageHefe : ImageEffectBase
  {
    /// <summary>
    /// Edge burn. Default 'Resources/edgeBurn.png'.
    /// </summary>
    public Texture2D edgeBurnTex;

    /// <summary>
    // Levels. Default 'Resources/hefeMap.png'.
    /// </summary>
    public Texture2D levelsTex;

    /// <summary>
    /// PixelLevels. Default 'Resources/hefeGradientMap.png'.
    /// </summary>
    public Texture2D gradientTex;

    /// <summary>
    /// Soft light. Default 'Resources/hefeSoftLight.png'.
    /// </summary>
    public Texture2D softLightTex;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageHefe"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      edgeBurnTex = VintageHelper.LoadTextureFromResources(@"Textures/edgeBurn");
      levelsTex = VintageHelper.LoadTextureFromResources(@"Textures/hefeMap");
      gradientTex = VintageHelper.LoadTextureFromResources(@"Textures/hefeGradientMap");
      softLightTex = VintageHelper.LoadTextureFromResources(@"Textures/hefeSoftLight");

      base.CreateMaterial();
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetTexture(VintageHelper.ShaderEdgeBurnTex, edgeBurnTex);
      this.Material.SetTexture(VintageHelper.ShaderLevelsTex, levelsTex);
      this.Material.SetTexture(VintageHelper.ShaderGradientTex, gradientTex);
      this.Material.SetTexture(VintageHelper.ShaderSoftLightTex, softLightTex);

      base.SendValuesToShader();
    }
  }
}