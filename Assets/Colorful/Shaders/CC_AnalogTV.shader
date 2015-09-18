Shader "Hidden/CC_AnalogTV"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_Phase ("Phase (time)", Float) = 0.01
		_NoiseIntensity ("Static noise intensity", Float) = 0.5
		_ScanlinesIntensity ("Scanlines intensity", Float) = 2.0
		_ScanlinesCount ("Scanlines count", Float) = 1024
		_ScanlinesOffset ("Scanlines vertical offset", Float) = 0.0

		_Distortion ("Distortion", Float) = 0.2
		_CubicDistortion ("Cubic Distortion", Float) = 0.6
		_Scale ("Scale (Zoom)", Float) = 0.8
	}

	CGINCLUDE

		#include "UnityCG.cginc"
		#include "./Colorful.cginc"

		sampler2D _MainTex;

		half _Phase;
		half _NoiseIntensity;
		half _ScanlinesIntensity;
		half _ScanlinesCount;
		half _ScanlinesOffset;

		half _Distortion;
		half _CubicDistortion;
		half _Scale;

		half2 barrelDistortion(half2 coord) 
		{
			// Inspired by SynthEyes lens distortion algorithm
			// See http://www.ssontech.com/content/lensalg.htm

			half2 h = coord.xy - half2(0.5, 0.5);
			half r2 = h.x * h.x + h.y * h.y;
			half f = 1.0 + r2 * (_Distortion + _CubicDistortion * sqrt(r2));

			return f * _Scale * h + 0.5;
		}

		half4 filterpass(half2 uv)
		{
			half2 coord = barrelDistortion(uv);
			half4 color = tex2D(_MainTex, coord);

			half n = simpleNoise(coord.x, coord.y, 1234.0, _Phase);
			half dx = fmod(n, 0.01);

			half3 result = color.rgb + color.rgb * saturate(0.1 + dx * 100.0);
			half2 sc = half2(sin(coord.y * _ScanlinesCount + _ScanlinesOffset), cos(coord.y * _ScanlinesCount + _ScanlinesOffset));
			result += color.rgb * sc.xyx * _ScanlinesIntensity;
			result = color.rgb + saturate(_NoiseIntensity) * (result - color.rgb);

			return half4(result, color.a);
		}

		fixed4 frag(v2f_img i):COLOR
		{
			return filterpass(i.uv);
		}

		fixed4 frag_grayscale(v2f_img i):COLOR
		{
			half4 result = filterpass(i.uv);

			fixed lum = luminance(result.rgb);
			result = half4(lum, lum, lum, result.a);

			return result;
		}

	ENDCG

	SubShader
	{
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest 
				#pragma glsl

			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_grayscale
				#pragma fragmentoption ARB_precision_hint_fastest 
				#pragma glsl

			ENDCG
		}
	}

	FallBack off
}
