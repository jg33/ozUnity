Shader "Hidden/CC_Grayscale"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Data ("Luminance (RGB) + Amount (A)", Vector) = (0.30, 0.59, 0.11, 1.0)
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
				half4 _Data;

				fixed4 frag(v2f_img i):COLOR
				{
					half4 color = tex2D(_MainTex, i.uv);
					half lum = dot(color.rgb, _Data.rgb);
					half4 result = half4(lum, lum, lum, color.a);
					return lerp(color, result, _Data.a);
				}

			ENDCG
		}
	}

	FallBack off
}
