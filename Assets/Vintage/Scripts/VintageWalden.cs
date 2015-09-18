///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Walden.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Walden")]
  public sealed class VintageWalden : ImageEffectBase
  {
    /// <summary>
    /// Default 'Resources/waldenMap'.
    /// </summary>
    public Texture2D levelsTex;

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageWalden"; } }

    /// <summary>
    /// Obturation of the vignette (0 none, 2 semi closed).
    /// </summary>
    public float obturation = 1.0f;

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      levelsTex = VintageHelper.LoadTextureFromResources(@"Textures/waldenMap");

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
      this.Material.SetTexture(VintageHelper.ShaderLevelsTex, levelsTex);
      this.Material.SetFloat(VintageHelper.ShaderObturation, obturation);

      base.SendValuesToShader();
    }
  }
}