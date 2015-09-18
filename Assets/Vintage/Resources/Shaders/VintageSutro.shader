///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// \brief   Vintage - Sutro.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Hidden/Vintage/Sutro"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}
    
    // Default 'Resources/sutroEdgeBurn.png'.
    _EdgeBurnTex("Edge burn (RGB)", 2D) = "white" {}

    // Default 'Resources/sutroCurves.png'.
    _CurvesTex("Curves (RGB)", 2D) = "white" {}

    // Obturation of the vignette (0 none, 2 semi closed).
    _Obturation("Obturation", Range(0.0, 2.0)) = 1.25

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

  // Vignette effect.
  #define USE_VIGNETTE

  // Edge burn effect.
  #define USE_EDGEBURN

  /////////////////////////////////////////////////////////////
  // END CONFIGURATION REGION
  /////////////////////////////////////////////////////////////

  sampler2D _MainTex;
  sampler2D _EdgeBurnTex;
  sampler2D _CurvesTex;

  float _Obturation = 1.0f;
  
  float _Amount = 1.0f;
  
  float4 frag_gamma(v2f_img i) : COLOR
  {
    float3 pixel = tex2D(_MainTex, i.uv).rgb;

    float3 final = pixel;

    float3 rgbPrime = float3(0.1019f, 0.0f, 0.0f);
    float m = dot(float3(0.3f, 0.59f, 0.11f), final.rgb) - 0.03058f;
    final = lerp(final, rgbPrime + m, 0.32f);
    
#ifdef USE_EDGEBURN
    final *= tex2D(_EdgeBurnTex, i.uv).rgb;
#endif

    final = PixelLevels(_CurvesTex, final);

#ifdef USE_VIGNETTE
    final = Vignette(final, i.uv, _Obturation);
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

    float3 final = pixel;

    float3 rgbPrime = float3(0.1019f, 0.0f, 0.0f);
    float m = dot(float3(0.3f, 0.59f, 0.11f), final.rgb) - 0.03058f;
    final = lerp(final, rgbPrime + m, 0.32f);
    
#ifdef USE_EDGEBURN
    final = lerp(sRGB(tex2D(_EdgeBurnTex, i.uv).rgb), final, 0.5f);
#endif

    final = PixelLevels(_CurvesTex, final);

#ifdef USE_VIGNETTE
    final = Vignette(final, i.uv, _Obturation);
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
