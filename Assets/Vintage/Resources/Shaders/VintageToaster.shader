///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// \brief   Vintage - Toaster.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Hidden/Vintage/Toaster"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/TexturestoasterMetal.png'.
    _MetalTex("Levels (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/TexturestoasterSoftLight.png'.
    _SoftLightTex("Metal (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/TexturestoasterCurves.png'.
    _CurvesTex("Soft light (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/TexturestoasterOverlayMapWarm.png'.
    _OverlayWarmTex("Edge burn (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/TexturestoasterColorShift.png'.
    _ColorShiftTex("Curves (RGB)", 2D) = "white" {}
    
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

  // SoftLight effect.
  #define USE_SOFTLIGHT

  // Overlay effect.
  #define USE_OVERLAY

  // Color shift effect.
  #define USE_COLORSHIFT

  /////////////////////////////////////////////////////////////
  // END CONFIGURATION REGION
  /////////////////////////////////////////////////////////////

  sampler2D _MainTex;
  sampler2D _MetalTex;
  sampler2D _SoftLightTex;
  sampler2D _CurvesTex;
  sampler2D _OverlayWarmTex;
  sampler2D _ColorShiftTex;
  
  float _Amount = 1.0f;
  
  float4 frag_gamma(v2f_img i) : COLOR
  {
    float3 pixel = tex2D(_MainTex, i.uv).rgb;

    float3 final = pixel;
	
	float2 lookup = float2(0.0, 0.0);

#ifdef USE_SOFTLIGHT
    float3 metal = tex2D(_MetalTex, i.uv).rgb;

    lookup = float2(metal.r, pixel.r);
    final.r = tex2D(_SoftLightTex, 1.0f - lookup).r;

    lookup = float2(metal.g, pixel.g);
    final.g = tex2D(_SoftLightTex, 1.0f - lookup).g;

    lookup = float2(metal.b, pixel.b);
    final.b = tex2D(_SoftLightTex, 1.0f - lookup).b;
#endif

    final = PixelLevels(_CurvesTex, final);
    
#ifdef USE_OVERLAY
	float2 tc = ((2.0f * i.uv) - 1.0);
    lookup.x = dot(tc, tc);
    lookup.y = 1.0f - final.r;
    final.r = tex2D(_OverlayWarmTex, lookup).r;
    lookup.y = 1.0f - final.g;
    final.g = tex2D(_OverlayWarmTex, lookup).g;
    lookup.y = 1.0f - final.b;
    final.b = tex2D(_OverlayWarmTex, lookup).b;
#endif

#ifdef USE_COLORSHIFT
    final = PixelLevels(_ColorShiftTex, final);
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

	float2 lookup = float2(0.0, 0.0);

#ifdef USE_SOFTLIGHT
    float3 metal = sRGB(tex2D(_MetalTex, i.uv).rgb);

    lookup = float2(metal.r, pixel.r);
    final.r = sRGB(tex2D(_SoftLightTex, 1.0f - lookup).rgb).r;

    lookup = float2(metal.g, pixel.g);
    final.g = sRGB(tex2D(_SoftLightTex, 1.0f - lookup).rgb).g;

    lookup = float2(metal.b, pixel.b);
    final.b = sRGB(tex2D(_SoftLightTex, 1.0f - lookup).rgb).b;
#endif

    final = sRGB(PixelLevels(_CurvesTex, final));
    
#ifdef USE_OVERLAY
	float2 tc = ((2.0f * i.uv) - 1.0);
    lookup.x = dot(tc, tc);
    lookup.y = 1.0f - final.r;
    final.r = sRGB(tex2D(_OverlayWarmTex, lookup).rgb).r;
    lookup.y = 1.0f - final.g;
    final.g = sRGB(tex2D(_OverlayWarmTex, lookup).rgb).g;
    lookup.y = 1.0f - final.b;
    final.b = sRGB(tex2D(_OverlayWarmTex, lookup).rgb).b;
#endif

#ifdef USE_COLORSHIFT
    final = sRGB(PixelLevels(_ColorShiftTex, final));
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
      #pragma target 3.0
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
