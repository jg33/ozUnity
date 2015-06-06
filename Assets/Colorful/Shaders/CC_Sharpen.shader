Shader "Hidden/CC_Sharpen"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_PX ("Pixel Width", Float) = 1
		_PY ("Pixel Height", Float) = 1 
		_Strength ("Strength", Range(0, 5.0)) = 0.60
		_Clamp ("Clamp", Range(0, 1.0)) = 0.05
	}

	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest 
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				half _PX;
				half _PY;
				half _Strength;
				half _Clamp;

				fixed4 frag(v2f_img i):COLOR
				{
					half2 coords = i.uv;
					half4 color = tex2D(_MainTex, coords);

					half4 blur  = tex2D(_MainTex, coords + half2(0.5 *  _PX,       -_PY));
						  blur += tex2D(_MainTex, coords + half2(      -_PX, 0.5 * -_PY));
						  blur += tex2D(_MainTex, coords + half2(       _PX, 0.5 *  _PY));
						  blur += tex2D(_MainTex, coords + half2(0.5 * -_PX,        _PY));
					blur /= 4;

					half4 lumaStrength = half4(0.2126, 0.7152, 0.0722, 0) * _Strength * 0.666;
					half4 sharp = color - blur;
					color += clamp(dot(sharp, lumaStrength), -_Clamp, _Clamp);

					return color;
				}

			ENDCG
		}
	}

	FallBack off
}
