///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Kelvin.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Lord Kelvin")]
  public sealed class VintageLordKelvin : ImageEffectBase
  {
    /// <summary>
    /// Default 'Resources/kelvinMap'.
    /// </summary>
    public Texture2D levelsTex;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageLordKelvin"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      levelsTex = VintageHelper.LoadTextureFromResources(@"Textures/kelvinMap");

      base.CreateMaterial();
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetTexture(VintageHelper.ShaderLevelsTex, levelsTex);

      base.SendValuesToShader();
    }
  }
}