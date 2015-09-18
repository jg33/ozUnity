///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Vintage - Image Effects.
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace VintageImageEffects
{
  /// <summary>
  /// Utilities Vintage - Image Effects.
  /// </summary>
  public static class VintageHelper
  {
    // Shaders params.
    public static readonly string ShaderAmount = @"_Amount";
    public static readonly string ShaderBrightness = @"_Brightness";
    public static readonly string ShaderContrast = @"_Contrast";
    public static readonly string ShaderGamma = @"_Gamma";
    public static readonly string ShaderHue = @"_Hue";
    public static readonly string ShaderSaturation = @"_Saturation";
    public static readonly string ShaderFilmGrainStrength = @"_FilmGrainStrength";
    public static readonly string ShaderFilmGrainSize = @"_FilmGrainSize";
    public static readonly string ShaderFilmBlinkStrenght = @"_FilmBlinkStrenght";
    public static readonly string ShaderLevelsTex = @"_LevelsTex";
    public static readonly string ShaderBlowoutTex = @"_BlowoutTex";
    public static readonly string ShaderOverlayTex = @"_OverlayTex";
    public static readonly string ShaderOverlayStrength = @"_OverlayStrength";
    public static readonly string ShaderProcessTex = @"_ProcessTex";
    public static readonly string ShaderContrastTex = @"_ContrastTex";
    public static readonly string ShaderLumaTex = @"_LumaTex";
    public static readonly string ShaderScreenTex = @"_ScreenTex";
    public static readonly string ShaderCurvesTex = @"_CurvesTex";
    public static readonly string ShaderObturation = @"_Obturation";
    public static readonly string ShaderEdgeBurnTex = @"_EdgeBurnTex";
    public static readonly string ShaderGradientTex = @"_GradientTex";
    public static readonly string ShaderSoftLightTex = @"_SoftLightTex";
    public static readonly string ShaderMetalTex = @"_MetalTex";
    public static readonly string ShaderOverlayWarmTex = @"_OverlayWarmTex";
    public static readonly string ShaderColorShiftTex = @"_ColorShiftTex";
    public static readonly string ShaderGradientLevelsTex = @"_GradientLevelsTex";

    /// <summary>
    /// Load a 2D texture from "Resources/Textures".
    /// </summary>
    public static Texture2D LoadTextureFromResources(string texturePathFromResources)
    {
      Texture2D texture = Resources.Load<Texture2D>(texturePathFromResources);
      if (texture == null)
        Debug.LogWarning(string.Format("Texture '{0}' not found in 'Resources/Textures' folder.", texturePathFromResources));

      return texture;
    }

    /// <summary>
    /// Creates a 3D texture.
    /// </summary>
    public static Texture3D CreateTexture3DFromResources(string texturePathFromResources, int slices)
    {
      Texture3D texture3D = null;

      Texture2D texture2D = Resources.Load<Texture2D>(texturePathFromResources);
      if (texture2D != null)
      {
        int height = texture2D.height;
        int width = texture2D.width / slices;

        Color[] pixels2D = texture2D.GetPixels();
        Color[] pixels3D = new Color[pixels2D.Length];

        for (int z = 0; z < slices; ++z)
          for (int y = 0; y < height; ++y)
            for (int x = 0; x < width; ++x)
              pixels3D[x + (y * width) + (z * (width * height))] = pixels2D[x + (z * width) + (((width - y) - 1) * width * height)];

        texture3D = new Texture3D(width, height, slices, TextureFormat.ARGB32, false);
        texture3D.SetPixels(pixels3D);
        texture3D.Apply();
        texture3D.filterMode = FilterMode.Trilinear;
        texture3D.wrapMode = TextureWrapMode.Clamp;
        texture3D.anisoLevel = 1;
      }
      else
        Debug.LogWarning(string.Format("Texture '{0}' not found in 'Resources/Textures' folder.", texturePathFromResources));

      return texture3D;
    }
  }
}