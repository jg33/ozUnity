///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage XPro II.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage XPro II")]
  public sealed class VintageXProII : ImageEffectBase
  {
    /// <summary>
    /// Default 'Resources/xproMap'.
    /// </summary>
    public Texture2D levelsTex;

    /// <summary>
    /// Obturation of the vignette (0 none, 2 semi closed).
    /// </summary>
    public float obturation = 1.0f;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageXProII"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      levelsTex = VintageHelper.LoadTextureFromResources(@"Textures/xproMap");

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