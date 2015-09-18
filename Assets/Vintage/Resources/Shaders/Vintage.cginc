///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// \brief   Utils and global config.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Ibuprogames. All rights reserved.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////
// BEGIN CONFIGURATION REGION
/////////////////////////////////////////////////////////////

// Define this to change the strength of ALL effects.
#define ENABLE_ALL_AMOUNT

// Do not activate. Only to promotion videos.
//#define ENABLE_ALL_DEMO

/////////////////////////////////////////////////////////////
// END CONFIGURATION REGION
/////////////////////////////////////////////////////////////


// Gamma <-> Linear.
inline float3 sRGB(float3 pixel)
{
  return (pixel <= float3(0.0031308f, 0.0031308f, 0.0031308f)) ? pixel * 12.9232102f : 1.055f * pow(pixel, 0.41666f) - 0.055f;
}

// Gamma <-> Linear.
inline float3 Linear(float3 pixel)
{
  return (pixel <= float3(0.0404482f, 0.0404482f, 0.0404482f)) ? pixel / 12.9232102f : pow((pixel + 0.055f) * 0.9478672f, 2.4f);
}

// Strength of the effect.
inline float3 PixelAmount(float3 pixel, float3 final, float amount)
{
#ifdef ENABLE_ALL_AMOUNT
  return lerp(pixel, final, amount);
#else
  return final;
#endif
}

// Film grain.
inline float3 PixelFilm(float3 pixel, float2 uv, float grainStrength, float grainSize, float blinkStrenght)
{
  pixel *= (1.0f - blinkStrenght) + blinkStrenght * sin(100.0f * _Time.y);

  float x = (uv.x + 4.0f) * (uv.y + 4.0f) * _Time.y;
  
  float3 grain = (fmod((fmod(x, 13.0f) + 1.0f) * (fmod(x, 123.0f) + 1.0f), grainSize) - 0.005f) * grainStrength;
  grain = 1.0f - grain;

  return pixel * grain;
}

// Desaturate.
inline float Desaturate(float3 pixel)
{
  return dot(float3(0.3f, 0.59f, 0.11f), pixel);
}

// Corrects the brightness, contrast and gamma.
inline float3 PixelBrightnessContrastGamma(float3 pixel, float brightness, float contrast, float gamma)
{
  pixel = (pixel - 0.5f) * contrast + 0.5f + brightness;

  pixel = clamp(pixel, 0.0f, 1.0f);

  pixel = pow(pixel, gamma);
  
  return pixel;
}

// RGB to HSV
inline float3 RGB2HSV(float3 c)
{
  const float4 K = float4(0.0f, -1.0f / 3.0f, 2.0f / 3.0f, -1.0f);
  float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
  float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

  float d = q.x - min(q.w, q.y);
  float e = 1.0e-10;

  return float3(abs(q.z + (q.w - q.y) / (6.0f * d + e)), d / (q.x + e), q.x);
}

// HSV to RGB
inline float3 HSV2RGB(float3 c)
{
  const float4 K = float4(1.0f, 2.0f / 3.0f, 1.0f / 3.0f, 3.0f);
  float3 p = abs(frac(c.xxx + K.xyz) * 6.0f - K.www);

  return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0f, 1.0f), c.y);
}

// http://lolengine.net/blog/2013/07/27/rgb-to-hsv-in-glsl
inline float3 PixelHueSaturation(float3 pixel, float hue, float saturation)
{
  float3 hsv = RGB2HSV(pixel);
  
  hsv.x += hue;
  hsv.y *= saturation;

  return HSV2RGB(hsv);
}

// Vignette.
inline float3 Vignette(float3 pixel, float2 uv, float obturation)
{
  float2 tc = (obturation * uv) - (obturation * 0.5f);
  
  float vignette = 1.0f - dot(tc, tc);

  pixel *= vignette * vignette * vignette;

  return pixel;
}

// Color levels.
inline float3 PixelLevels(sampler2D levels, float3 pixel)
{
  pixel.r = tex2D(levels, float2(pixel.r, 1.0f - 0.16666f)).r;
  pixel.g = tex2D(levels, float2(pixel.g, 0.5f)).g;
  pixel.b = tex2D(levels, float2(pixel.b, 1.0f - 0.83333f)).b;

  return pixel;
}

// Texture overlay.
inline float3 PixelBlowoutOverlay(sampler2D blowout, sampler2D overlay, float2 uv, float3 pixel)
{
  float3 bo = tex2D(blowout, uv).rgb;

  float3 final;
  final.r = tex2D(overlay, float2(pixel.r, 1.0f - bo.r)).r;
  final.g = tex2D(overlay, float2(pixel.g, 1.0f - bo.g)).g;
  final.b = tex2D(overlay, float2(pixel.b, 1.0f - bo.b)).b;

  return final;
}

// Texture overlay.
inline float3 PixelBlowoutOverlayStrength(sampler2D blowout, sampler2D overlay, float2 uv, float3 pixel, float strength)
{
  float3 bo = tex2D(blowout, uv).rgb;

  float3 final;
  final.r = tex2D(overlay, float2(pixel.r, 1.0f - bo.r)).r;
  final.g = tex2D(overlay, float2(pixel.g, 1.0f - bo.g)).g;
  final.b = tex2D(overlay, float2(pixel.b, 1.0f - bo.b)).b;

  return lerp(pixel, final, strength);
}

#ifdef ENABLE_ALL_DEMO
inline float3 PixelDemo(float3 pixel, float3 final, float2 uv)
{
  float separator = (sin(_Time.x * 15.0f) * 0.15f) + 0.65f;

  if (abs(uv.x - separator) < 0.002f)
    final = float4(1.0f, 1.0f, 1.0f, 1.0f);
  else if (uv.x > separator)
    final = pixel;

  return final;
}
#endif