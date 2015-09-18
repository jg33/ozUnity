///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// \brief   Vintage - Extra - Brightness / Contrast / Gamma.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Hidden/Vintage/Extras/BrightnessContrastGamma"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}

    // Brightness.
    _Brightness("Brightness", Range(-1.0, 1.0)) = 0.0

    // Contrast.
    _Contrast("Contrast", Range(-1.0, 1.0)) = 1.0

    // Gamma.
    _Gamma("Gamma", Range(0.01, 10.0)) = 0.0

    // Amount of the effect (0 none, 1 full).
    _Amount("Amount", Range(0.0, 1.0)) = 1.0
  }

  CGINCLUDE
  #include "UnityCG.cginc"
  #include "../Vintage.cginc"

  /////////////////////////////////////////////////////////////
  // BEGIN CONFIGURATION REGION
  /////////////////////////////////////////////////////////////

  // Define this to change the strength of the effect.
  #define USE_AMOUNT

  /////////////////////////////////////////////////////////////
  // END CONFIGURATION REGION
  /////////////////////////////////////////////////////////////

  sampler2D _MainTex;

  float _Brightness = 0.0f;
  float _Contrast = 1.0f;
  float _Gamma = 1.0f;

  float _Amount;

  float4 frag_gamma(v2f_img i) : COLOR
  {
    float3 pixel = tex2D(_MainTex, i.uv).rgb;
    
	float3 final = PixelBrightnessContrastGamma(pixel, _Brightness, _Contrast, _Gamma);

#ifdef USE_AMOUNT
    final = PixelAmount(pixel, final, _Amount);
#endif
    
	return float4(final, 1.0);
  }

  float4 frag_linear(v2f_img i) : COLOR
  {
    float3 pixel = sRGB(tex2D(_MainTex, i.uv).rgb);
    
	float3 final = PixelBrightnessContrastGamma(pixel, _Brightness, _Contrast, _Gamma);

#ifdef USE_AMOUNT
    final = PixelAmount(pixel, final, _Amount);
#endif

    return float4(Linear(final), 1.0);
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
      #pragma target 2.0
      #pragma vertex vert_img
      #pragma fragment frag_linear
      ENDCG
    }
  }

  Fallback off
}