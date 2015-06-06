Shader "Hidden/CC_Halftone"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Density ("Density (Float)", Float) = 64.0
		_AspectRatio ("Camera Aspect Ratio (Float)", Float) = 1.0
	}

	CGINCLUDE
	
		#include "UnityCG.cginc"
		#include "Colorful.cginc"

		sampler2D _MainTex;
		half _Density;
		half _AspectRatio;

		half aastep(half threshold, half value)
		{
			half afwidth = _Density * 0.005;
			return smoothstep(threshold - afwidth, threshold + afwidth, value);
		}

		half halftone_dist_bw(half2 uv)
		{
			half2 uv2 = mul(half2x2(0.707, -0.707 / _AspectRatio, 0.707, 0.707 / _AspectRatio), uv);
			half2 nearest = 2.0 * frac(_Density * uv2) - 1.0;
			return length(nearest);
		}

		fixed4 frag_bw(v2f_img i):COLOR
		{
			half dist = halftone_dist_bw(i.uv);
			half3 texcolor = tex2D(_MainTex, i.uv).rgb;
			half radius = sqrt(1.0 - luminance(texcolor));
			half3 color = lerp(half3(0.0, 0.0, 0.0), half3(1.0, 1.0, 1.0), step(radius, dist));
			return fixed4(color, 1.0);
		}

		fixed4 frag_bw_aa(v2f_img i):COLOR
		{
			half dist = halftone_dist_bw(i.uv);
			half3 texcolor = tex2D(_MainTex, i.uv).rgb;
			half radius = sqrt(1.0 - luminance(texcolor));
			half3 color = lerp(half3(0.0, 0.0, 0.0), half3(1.0, 1.0, 1.0), aastep(radius, dist));
			return fixed4(color, 1.0);
		}

		fixed4 frag_bw_blend(v2f_img i):COLOR
		{
			half dist = halftone_dist_bw(i.uv);
			half3 texcolor = tex2D(_MainTex, i.uv).rgb;
			half radius = sqrt(1.0 - luminance(texcolor));
			half3 color = lerp(half3(0.0, 0.0, 0.0), half3(1.0, 1.0, 1.0), step(radius, dist));
			return fixed4(color, 1.0) * half4(texcolor, 1.0);
		}

		fixed4 frag_bw_aa_blend(v2f_img i):COLOR
		{
			half dist = halftone_dist_bw(i.uv);
			half3 texcolor = tex2D(_MainTex, i.uv).rgb;
			half radius = sqrt(1.0 - luminance(texcolor));
			half3 color = lerp(half3(0.0, 0.0, 0.0), half3(1.0, 1.0, 1.0), aastep(radius, dist));
			return fixed4(color, 1.0) * half4(texcolor, 1.0);
		}

		fixed4 frag_cmyk(v2f_img i):COLOR
		{
			half3 texcolor = tex2D(_MainTex, i.uv).rgb;

			half4 cmyk = half4(0.0, 0.0, 0.0, 0.0);
			cmyk.xyz = 1.0 - texcolor;
			cmyk.w = min(cmyk.x, min(cmyk.y, cmyk.z));
			cmyk.xyz -= cmyk.w;

			half2 Kst = _Density * mul(half2x2(0.707, -0.707, 0.707, 0.707), i.uv);
			half2 Kuv = 2.0 * frac(Kst) - 1.0;
			half k = step(0.0, sqrt(cmyk.w) - length(Kuv));
			half2 Cst = _Density * mul(half2x2(0.966, -0.259, 0.259, 0.966), i.uv);
			half2 Cuv = 2.0 * frac(Cst) - 1.0;
			half c = step(0.0, sqrt(cmyk.x) - length(Cuv));
			half2 Mst = _Density * mul(half2x2(0.966, 0.259, -0.259, 0.966), i.uv);
			half2 Muv = 2.0 * frac(Mst) - 1.0;
			half m = step(0.0, sqrt(cmyk.y) - length(Muv));
			half2 Yst = _Density * i.uv;
			half2 Yuv = 2.0 * frac(Yst) - 1.0;
			half y = step(0.0, sqrt(cmyk.z) - length(Yuv));

			half3 color = 1.0 - 0.9 * half3(c, m, y);
			color = lerp(color, half3(0.0, 0.0, 0.0), 0.85 * k);
			half blend = smoothstep(0.7, 1.4, _Density * 0.005);
			return fixed4(lerp(color, texcolor, blend), 1.0);
		}

		fixed4 frag_cmyk_aa(v2f_img i):COLOR
		{
			half3 texcolor = tex2D(_MainTex, i.uv).rgb;

			half4 cmyk = half4(0.0, 0.0, 0.0, 0.0);
			cmyk.xyz = 1.0 - texcolor;
			cmyk.w = min(cmyk.x, min(cmyk.y, cmyk.z));
			cmyk.xyz -= cmyk.w;

			half2 Kst = _Density * mul(half2x2(0.707, -0.707, 0.707, 0.707), i.uv);
			half2 Kuv = 2.0 * frac(Kst) - 1.0;
			half k = aastep(0.0, sqrt(cmyk.w) - length(Kuv));
			half2 Cst = _Density * mul(half2x2(0.966, -0.259, 0.259, 0.966), i.uv);
			half2 Cuv = 2.0 * frac(Cst) - 1.0;
			half c = aastep(0.0, sqrt(cmyk.x) - length(Cuv));
			half2 Mst = _Density * mul(half2x2(0.966, 0.259, -0.259, 0.966), i.uv);
			half2 Muv = 2.0 * frac(Mst) - 1.0;
			half m = aastep(0.0, sqrt(cmyk.y) - length(Muv));
			half2 Yst = _Density * i.uv;
			half2 Yuv = 2.0 * frac(Yst) - 1.0;
			half y = aastep(0.0, sqrt(cmyk.z) - length(Yuv));

			half3 color = 1.0 - 0.9 * half3(c, m, y);
			color = lerp(color, half3(0.0, 0.0, 0.0), 0.85 * k);
			half blend = smoothstep(0.7, 1.4, _Density * 0.005);
			return fixed4(lerp(color, texcolor, blend), 1.0);
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
				#pragma fragment frag_bw
				#pragma fragmentoption ARB_precision_hint_fastest
				
			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_bw_aa
				#pragma fragmentoption ARB_precision_hint_fastest
				
			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_bw_blend
				#pragma fragmentoption ARB_precision_hint_fastest
				
			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_bw_aa_blend
				#pragma fragmentoption ARB_precision_hint_fastest
				
			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_cmyk
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma target 3.0
				
			ENDCG
		}

		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_cmyk_aa
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma target 3.0
				
			ENDCG
		}
	}

	FallBack off
}
