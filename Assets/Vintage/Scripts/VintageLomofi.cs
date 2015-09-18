///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Lomofi.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Lomofi")]
  public sealed class VintageLomofi : ImageEffectBase
  {
    /// <summary>
    // Levels. Default 'Resources/lomoMap.png'.
    /// </summary>
    public Texture2D levelsTex;

    /// <summary>
    /// Obturation of the vignette (0 none, 2 semi closed).
    /// </summary>
    public float obturation = 1.0f;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageLomofi"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      levelsTex = VintageHelper.LoadTextureFromResources(@"Textures/lomoMap");

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