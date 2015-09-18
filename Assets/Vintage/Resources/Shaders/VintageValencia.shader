///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// \brief   Vintage - Valencia.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Hidden/Vintage/Valencia"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}

    // Default 'Resources/valenciaMap.png'.
    _LevelsTex("Levels (RGB)", 2D) = "white" {}

    // Default 'Resources/valenciaGradientMap.png'.
    _GradientLevelsTex("PixelLevels (RGB)", 2D) = "white" {}

    // Amount of the effect (0 none, 1 full).
    _Amount("Amount", Range(0.0, 1.0)) = 1.0
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "Vintage.cginc"

  /////////////////////////////////////////////////////////////
  // BEGIN CONFIGURATION REGION
  /////////////////////////////////////////////////////////////

  // Define this to change the strength of the effect.
  #define USE_AMOUNT

  // Gradient effect.
  #define USE_GRADIENT

  /////////////////////////////////////////////////////////////
  // END CONFIGURATION REGION
  /////////////////////////////////////////////////////////////

  sampler2D _MainTex;
  sampler2D _LevelsTex;
  sampler2D _GradientLevelsTex;

  float4x4 saturateMatrix = float4x4(1.1402f, -0.0598f, -0.061f,  0.0f,
                                    -0.1174f,  1.0826f, -0.1186f, 0.0f,
                                    -0.0228f, -0.0228f,  1.1772f, 0.0f,
                                     0.0f,     0.0f,     0.0f,    0.0);

  float _Amount = 1.0f;

  float4 frag_gamma(v2f_img i) : COLOR
  {
    float3 pixel = tex2D(_MainTex, i.uv).rgb;

    float3 final = PixelLevels(_LevelsTex, pixel);

    final = mul((float3x3)saturateMatrix, final);

#ifdef USE_GRADIENT
    float luma = 1.0f - Desaturate(final);

    final.r = tex2D(_GradientLevelsTex, float2(final.r, luma)).r;
    final.g = tex2D(_GradientLevelsTex, float2(final.g, luma)).g;
    final.b = tex2D(_GradientLevelsTex, float2(final.b, luma)).b;
#endif

#ifdef USE_AMOUNT
    final = PixelAmount(pixel, final, _Amount);
#endif

#ifdef ENABLE_ALL_DEMO
    final = PixelDemo(pixel, final, i.uv);
#endif

    return float4(final, 1.0f);
  }

  float4 frag_linear(v2f_img i) : COLOR
  {
    float3 pixel = sRGB(tex2D(_MainTex, i.uv).rgb);

    float3 final = sRGB(PixelLevels(_LevelsTex, pixel));

    final = mul((float3x3)saturateMatrix, final);

#ifdef USE_GRADIENT
    float luma = 1.0f - Desaturate(final);

    final.r = sRGB(tex2D(_GradientLevelsTex, float2(final.r, luma)).rgb).r;
    final.g = sRGB(tex2D(_GradientLevelsTex, float2(final.g, luma)).rgb).g;
    final.b = sRGB(tex2D(_GradientLevelsTex, float2(final.b, luma)).rgb).b;
#endif

#ifdef USE_AMOUNT
    final = PixelAmount(pixel, final, _Amount);
#endif

#ifdef ENABLE_ALL_DEMO
    final = PixelDemo(pixel, final, i.uv);
#endif

    return float4(Linear(final), 1.0f);
  }
  ENDCG

  // Techniques (http://unity3d.com/support/documentation/Components/SL-SubShader.html).
  SubShader
  {
    // Tags (http://docs.unity3d.com/Manual/SL-CullAndDepth.html).
    ZTest Always
    Cull Off
    ZWrite Off
    Fog { Mode off }

    // Pass 0: Color Space Gamma.
    Pass
    {
      CGPROGRAM
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma target 2.0
      #pragma vertex vert_img
      #pragma fragment frag_gamma
      ENDCG
    }

    // Pass 1: Color Space Linear.
    Pass
    {
      CGPROGRAM
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma target 3.0
      #pragma vertex vert_img
      #pragma fragment frag_linear
      ENDCG
    }
  }

  Fallback off
}
