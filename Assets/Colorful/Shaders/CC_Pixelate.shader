Shader "Hidden/CC_Pixelate"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Scale ("Scale", Float) = 80.0
		_Ratio ("Width / Height ratio", Float) = 1.0
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

				fixed4 frag(v2f_img i):COLOR
				{
					return pixelate(_MainTex, i.uv, _Scale, _Ratio);
				}

			ENDCG
		}
	}

	FallBack off
}
