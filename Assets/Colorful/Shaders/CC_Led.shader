Shader "Hidden/CC_Led"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Scale ("Scale", Float) = 80.0
		_Ratio ("Width / Height ratio", Float) = 1.0
		_Brightness ("Brightness", Float) = 1.0
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
				#include "Colorful.cginc"

				sampler2D _MainTex;
				half _Scale;
				half _Ratio;
				half _Brightness;

				fixed4 frag(v2f_img i):COLOR
				{
					half4 color = pixelate(_MainTex, i.uv, _Scale, _Ratio) * _Brightness;
					half2 coord = i.uv * half2(_Scale, _Scale / _Ratio);
					half mvx = abs(sin(coord.x * 3.1415)) * 1.5;
					half mvy = abs(sin(coord.y * 3.1415)) * 1.5;

					half s = mvx * mvy;
					half c = step(s, 1.0);
					color = ((1 - c) * color) + ((color * s) * c);

					return color;
				}

			ENDCG
		}
	}

	FallBack off
}
