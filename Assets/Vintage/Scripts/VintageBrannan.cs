///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Brannan.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Brannan")]
  public sealed class VintageBrannan : ImageEffectBase
  {
    /// <summary>
    /// Process. Default 'Resources/brannanProcess.png'.
    /// </summary>
    public Texture2D processTex;

    /// <summary>
    /// Blowout. Default 'Resources/brannanBlowout.png'.
    /// </summary>
    public Texture2D blowoutTex;

    /// <summary>
    /// Contrast. Default 'Resources/brannanContrast.png'.
    /// </summary>
    public Texture2D contrastTex;

    /// <summary>
    /// Luma. Default 'Resources/brannanLuma.png'.
    /// </summary>
    public Texture2D lumaTex;

    /// <summary>
    /// Screen. Default 'Resources/brannanScreen.png'.
    /// </summary>
    public Texture2D screenTex;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageBrannan"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      processTex = VintageHelper.LoadTextureFromResources(@"Textures/brannanProcess");
      blowoutTex = VintageHelper.LoadTextureFromResources(@"Textures/brannanBlowout");
      contrastTex = VintageHelper.LoadTextureFromResources(@"Textures/brannanContrast");
      lumaTex = VintageHelper.LoadTextureFromResources(@"Textures/brannanLuma");
      screenTex = VintageHelper.LoadTextureFromResources(@"Textures/brannanScreen");

      base.CreateMaterial();
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      this.Material.SetTexture(VintageHelper.ShaderProcessTex, processTex);
      this.Material.SetTexture(VintageHelper.ShaderBlowoutTex, blowoutTex);
      this.Material.SetTexture(VintageHelper.ShaderContrastTex, contrastTex);
      this.Material.SetTexture(VintageHelper.ShaderLumaTex, lumaTex);
      this.Material.SetTexture(VintageHelper.ShaderScreenTex, screenTex);

      base.SendValuesToShader();
    }
  }
}