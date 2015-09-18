///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// \brief   Vintage - Amaro.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Hidden/Vintage/Amaro"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/blackboard1024.png'.
    _BlowoutTex("Blowout (RGB)", 2D) = "white" {}
    
    // Default 'Resources/Textures/overlayMap.png'.
    _OverlayTex("Overlay (RGB)", 2D) = "white" {}
    
    // Default 'Resources/Textures/amaroMap.png'.
    _LevelsTex("Levels (RGB)", 2D) = "white" {}

    // Overlay strength (0 none, 1 full).
    _OverlayStrength("Overlay strength", Range(0.0, 1.0)) = 1.0

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

  // Overlay effect.
  #define USE_OVERLAY

  // Define this to change the strength of the overlay.
  #define USE_OVERLAY_STRENGTH

  /////////////////////////////////////////////////////////////
  // END CONFIGURATION REGION
  /////////////////////////////////////////////////////////////

  sampler2D _MainTex;
  sampler2D _BlowoutTex;
  sampler2D _OverlayTex;
  sampler2D _LevelsTex;

  float _OverlayStrength = 1.0f;

  float _Amount = 1.0f;

  float4 frag_gamma(v2f_img i) : COLOR
  {
    float3 pixel = tex2D(_MainTex, i.uv).rgb;

	float3 final = pixel;

#ifdef USE_OVERLAY
  #ifdef USE_OVERLAY_STRENGTH
    final = PixelBlowoutOverlayStrength(_BlowoutTex, _OverlayTex, i.uv, pixel, _OverlayStrength);
  #else
    final = PixelBlowoutOverlay(_BlowoutTex, _OverlayTex, i.uv, pixel);
  #endif
#endif

    final = PixelLevels(_LevelsTex, final);

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

#ifdef USE_OVERLAY
  #ifdef USE_OVERLAY_STRENGTH
    final = sRGB(PixelBlowoutOverlayStrength(_BlowoutTex, _OverlayTex, i.uv, pixel, _OverlayStrength));
  #else
    final = sRGB(PixelBlowoutOverlay(_BlowoutTex, _OverlayTex, i.uv, pixel));
  #endif
#endif

    final = sRGB(PixelLevels(_LevelsTex, final));

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