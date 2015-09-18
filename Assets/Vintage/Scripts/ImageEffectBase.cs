///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;

using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Image effect base.
  /// </summary>
  public abstract class ImageEffectBase : MonoBehaviour
  {
    /// <summary>
    /// Amount of the effect (0 none, 1 full).
    /// </summary>
    public float amount = 1.0f;

    private Shader shader;

    private Material material;

    /// <summary>
    /// Get/set the shader.
    /// </summary>
    public Shader Shader
    {
      get { return shader; }
      set
      {
        if (shader != value)
        {
          shader = value;

          CreateMaterial();
        }
      }
    }

    /// <summary>
    /// Get the material.
    /// </summary>
    public Material Material
    {
      get
      {
        if (material == null && shader != null)
          CreateMaterial();

        return material;
      }
    }

    /// <summary>
    /// Shader path.
    /// </summary>
    protected abstract string ShaderPath { get; }

    /// <summary>
    /// Is an 'extra' effect?
    /// </summary>
    public abstract bool IsExtraEffect { get; }

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
      if (CheckHardwareRequirements() == true)
      {
        shader = Resources.Load<Shader>(ShaderPath);
        if (shader != null)
        {
          if (shader.isSupported == true)
          {
            CreateMaterial();

            if (material == null)
            {
              Debug.LogWarning(string.Format("'{0}' material null.", this.name));

              this.enabled = false;
            }
          }
          else
          {
            Debug.LogWarning(string.Format("'{0}' shader not supported.", this.GetType().ToString()));

            this.enabled = false;
          }
        }
        else
        {
          Debug.LogError(string.Format("'{0}' shader not found.", ShaderPath));

          this.enabled = false;
        }
      }
      else
        this.enabled = false;
    }

    /// <summary>
    /// Destroy resources.
    /// </summary>
    protected virtual void OnDestroy()
    {
      if (material != null)
        DestroyImmediate(material);
    }

    /// <summary>
    /// Check hardware requirements.
    /// </summary>
    protected virtual bool CheckHardwareRequirements()
    {
      if (SystemInfo.supportsImageEffects == false)
      {
        Debug.LogWarning(string.Format("Hardware not support Image Effects. '{0}' disabled.", this.GetType().ToString()));

        return false;
      }

      return true;
    }

    /// <summary>
    /// Set the default values of the shader.
    /// </summary>
    public virtual void ResetDefaultValues()
    {
      amount = 1.0f;
    }

    /// <summary>
    /// Creates the material and textures.
    /// </summary>
    protected virtual void CreateMaterial()
    {
      if (material != null)
        DestroyImmediate(material);

      material = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
      if (material != null)
      {
        SendValuesToShader();

        Graphics.Blit(source, destination, material, QualitySettings.activeColorSpace == ColorSpace.Linear ? 1 : 0);
      }
    }

    /// <summary>
    /// Set the values to shader.
    /// </summary>
    protected virtual void SendValuesToShader()
    {
      material.SetFloat(VintageHelper.ShaderAmount, amount);
    }
  }
}
