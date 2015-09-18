///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Valencia.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Valencia")]
  public sealed class VintageValencia : ImageEffectBase
  {
    /// <summary>
    // Levels. Default 'Resources/valenciaMap'.
    /// </summary>
    public Texture2D levelsTex;

    /// <summary>
    // Levels. Default 'Resources/valenciaGradientMap'.
    /// </summary>
    public Texture2D gradTex;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageValencia"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      levelsTex = VintageHelper.LoadTextureFromResources(@"Textures/valenciaMap");
      gradTex = VintageHelper.LoadTextureFromResources(@"Textures/valenciaGradientMap");

      base.CreateMaterial();
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetTexture(VintageHelper.ShaderLevelsTex, levelsTex);
      this.Material.SetTexture(VintageHelper.ShaderGradientLevelsTex, gradTex);

      base.SendValuesToShader();
    }
  }
}