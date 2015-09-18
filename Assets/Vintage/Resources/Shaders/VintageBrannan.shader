///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// \brief   Vintage - Brannan.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// http://unity3d.com/support/documentation/Components/SL-Shader.html
Shader "Hidden/Vintage/Brannan"
{
  // http://unity3d.com/support/documentation/Components/SL-Properties.html
  Properties
  {
    _MainTex("Base (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/brannanProcess.png'.
    _ProcessTex("Process (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/brannanBlowout.png'.
    _BlowoutTex("Blowout (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/brannanContrast.png'.
    _ContrastTex("Constrast (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/brannanLuma.png'.
    _LumaTex("Luma (RGB)", 2D) = "white" {}

    // Default 'Resources/Textures/brannanScreen.png'.
    _ScreenTex("Screen (RGB)", 2D) = "white" {}

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

  // Blowout effect.
  #define USE_BLOWOUT

  // Constrast effect.
  #define USE_CONTRAST

  // Luminance effect.
  #define USE_LUMA

  // Screen effect.
  #define USE_SCREEN

  /////////////////////////////////////////////////////////////
  // END CONFIGURATION REGION
  /////////////////////////////////////////////////////////////

  sampler2D _MainTex;
  sampler2D _ProcessTex;
  sampler2D _BlowoutTex;
  sampler2D _ContrastTex;
  sampler2D _LumaTex;
  sampler2D _ScreenTex;

  float _Amount = 1.0f;

  float4 frag_gamma(v2f_img i) : COLOR
  {
    float3 pixel = tex2D(_MainTex, i.uv).rgb;

    float3 final;

    // Process.
    float2 lookup;
    lookup.y = 0.5f;
    lookup.x = pixel.r;
    final.r = tex2D(_ProcessTex, lookup).r;
    lookup.x = pixel.g;
    final.g = tex2D(_ProcessTex, lookup).g;
    lookup.x = pixel.b;
    final.b = tex2D(_ProcessTex, lookup).b;

#ifdef USE_BLOWOUT
    // Blowout.
    float2 tc = (2.0f * i.uv) - 1.0f;
    float d = dot(tc, tc);
    float3 sampled;
    lookup.x = final.r;
    sampled.r = tex2D(_BlowoutTex, lookup).r;
    lookup.x = final.g;
    sampled.g = tex2D(_BlowoutTex, lookup).g;
    lookup.x = final.b;
    sampled.b = tex2D(_BlowoutTex, lookup).b;
    float value = smoothstep(0.0f, 1.0f, d);
    final = lerp(sampled, final, value);
#endif

#ifdef USE_CONTRAST
    // Constrast.
    lookup.x = final.r;
    final.r = tex2D(_ContrastTex, lookup).r;
    lookup.x = final.g;
    final.g = tex2D(_ContrastTex, lookup).g;
    lookup.x = final.b;
    final.b = tex2D(_ContrastTex, lookup).b;
#endif

#ifdef USE_LUMA
    // Luma.
    lookup.x = Desaturate(final);
    final = lerp(tex2D(_LumaTex, lookup).rgb, final, 0.5f);
#endif

#ifdef USE_SCREEN
    // Screen.
    lookup.x = final.r;
    final.r = tex2D(_ScreenTex, lookup).r;
    lookup.x = final.g;
    final.g = tex2D(_ScreenTex, lookup).g;
    lookup.x = final.b;
    final.b = tex2D(_ScreenTex, lookup).b;
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

    float3 final;

    // Process.
    float2 lookup;
    lookup.y = 0.5f;
    lookup.x = pixel.r;
    final.r = sRGB(tex2D(_ProcessTex, lookup).rgb).r;
    lookup.x = pixel.g;
    final.g = sRGB(tex2D(_ProcessTex, lookup).rgb).g;
    lookup.x = pixel.b;
    final.b = sRGB(tex2D(_ProcessTex, lookup).rgb).b;

#ifdef USE_BLOWOUT
    // Blowout.
    float2 tc = (2.0f * i.uv) - 1.0f;
    float d = dot(tc, tc);
    float3 sampled;
    lookup.x = final.r;
    sampled.r = sRGB(tex2D(_BlowoutTex, lookup).rgb).r;
    lookup.x = final.g;
    sampled.g = sRGB(tex2D(_BlowoutTex, lookup).rgb).g;
    lookup.x = final.b;
    sampled.b = sRGB(tex2D(_BlowoutTex, lookup).rgb).b;
    float value = smoothstep(0.0f, 1.0f, d);
    final = lerp(sampled, final, value);
#endif

#ifdef USE_CONTRAST
    // Constrast.
    lookup.x = final.r;
    final.r = sRGB(tex2D(_ContrastTex, lookup).rgb).r;
    lookup.x = final.g;
    final.g = sRGB(tex2D(_ContrastTex, lookup).rgb).g;
    lookup.x = final.b;
    final.b = sRGB(tex2D(_ContrastTex, lookup).rgb).b;
#endif

#ifdef USE_LUMA
    // Luma.
    lookup.x = Desaturate(final);
    final = lerp(sRGB(tex2D(_LumaTex, lookup).rgb), final, 0.5f);
#endif

#ifdef USE_SCREEN
    // Screen.
    lookup.x = final.r;
    final.r = sRGB(tex2D(_ScreenTex, lookup).rgb).r;
    lookup.x = final.g;
    final.g = sRGB(tex2D(_ScreenTex, lookup).rgb).g;
    lookup.x = final.b;
    final.b = sRGB(tex2D(_ScreenTex, lookup).rgb).b;
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
