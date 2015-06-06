Shader "Hidden/CC_Glitch"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }

		// Interference
		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest 
				#include "UnityCG.cginc"
				#pragma target 3.0

				sampler2D _MainTex;
				half3 _Params; // x: speed, y: density, z: maxDisplace

				inline half rand(half2 seed)
				{
					return frac(sin(dot(seed * floor(_Time.y * _Params.x), half2(127.1, 311.7))) * 43758.5453123);
				}

				inline half rand(half seed)
				{
					return rand(half2(seed, 1.0));
				}

				fixed4 frag(v2f_img i):COLOR
				{
					half2 rblock = rand(floor(i.uv * _Params.y));
					half displaceNoise = pow(rblock.x, 8.0) * pow(rblock.x, 3.0) - pow(rand(7.2341), 17.0) * _Params.z;

					half r = tex2D(_MainTex, i.uv).r;
					half g = tex2D(_MainTex, i.uv + half2(displaceNoise * 0.05 * rand(7.0), 0.0)).g;
					half b = tex2D(_MainTex, i.uv - half2(displaceNoise * 0.05 * rand(13.0), 0.0)).b;

					return half4(half3(r, g, b), 1.0);
				}

			ENDCG
		}

		// Tearing
		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_tearing
				#pragma fragmentoption ARB_precision_hint_fastest 
				#pragma target 3.0

				#include "CC_Glitch.cginc"

			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_tearing
				#pragma fragmentoption ARB_precision_hint_fastest 
				#pragma target 3.0

				#define ALLOW_FLIPPING
				#include "CC_Glitch.cginc"

			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_tearing
				#pragma fragmentoption ARB_precision_hint_fastest 
				#pragma target 3.0

				#define YUV_COLOR_BLEEDING
				#include "CC_Glitch.cginc"

			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_tearing
				#pragma fragmentoption ARB_precision_hint_fastest 
				#pragma target 3.0
				
				#define ALLOW_FLIPPING
				#define YUV_COLOR_BLEEDING
				#include "CC_Glitch.cginc"

			ENDCG
		}
	}

	FallBack off
}
