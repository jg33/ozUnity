///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Vintage Slumber.
  /// </summary>
  [ExecuteInEditMode]
  [RequireComponent(typeof(Camera))]
  [AddComponentMenu("Image Effects/Vintage/Vintage Slumber")]
  public sealed class VintageSlumber : ImageEffectBase
  {
    /// <summary>
    /// Default 'Textures/slumberLut.png'.
    /// </summary>
    public Texture3D lutTex = null;

    /// <summary>
    /// Shader path.
    /// </summary>
    protected override string ShaderPath { get { return @"Shaders/VintageSlumber"; } }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public override bool IsExtraEffect { get { return false; } }

    /// <summary>
    /// Destroy resources.
    /// </summary>
    protected override void OnDestroy()
    {
      DestroyLut();

      base.OnDestroy();
    }

    protected override bool CheckHardwareRequirements()
    {
      if (base.CheckHardwareRequirements() && SystemInfo.supports3DTextures == false)
      {
        Debug.LogWarning(string.Format("Hardware not support 3D textures. '{0}' disabled.", this.GetType().ToString()));

        return false;
      }

      return true;
    }

    private void DestroyLut()
    {
      if (lutTex != null)
      {
        DestroyImmediate(lutTex);

        lutTex = null;
      }
    }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected override void CreateMaterial()
    {
      DestroyLut();

      lutTex = VintageHelper.CreateTexture3DFromResources(@"Textures/slumberLut", 16);

      base.CreateMaterial();
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected override void SendValuesToShader()
    {
      if (lutTex != null)
      {
        int lutSize = lutTex.width;

        this.Material.SetFloat(@"_Scale", (lutSize - 1) / (1.0f * lutSize));
        this.Material.SetFloat(@"_Offset", 1.0f / (2.0f * lutSize));
        this.Material.SetTexture(@"_LutTex", lutTex as Texture);
      }

      base.SendValuesToShader();
    }
  }
}