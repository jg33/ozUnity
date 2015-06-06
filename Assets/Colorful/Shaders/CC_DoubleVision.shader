Shader "Hidden/CC_DoubleVision"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Displace ("Displace", Vector) = (0.7, 0.0, 0.0, 0.0)
		_Amount ("Amount", Range(0.0, 1.0)) = 1.0
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
				half2 _Displace;
				half _Amount;

				fixed4 frag(v2f_img i):COLOR
				{
					half4 c = tex2D(_MainTex, i.uv);
					half4 n = c.rgba;

					n += tex2D(_MainTex, i.uv + half2(_Displace.x * 8, _Displace.y * 8)) * 0.5;
					n += tex2D(_MainTex, i.uv + half2(_Displace.x * 16, _Displace.y * 16)) * 0.3;
					n += tex2D(_MainTex, i.uv + half2(_Displace.x * 24, _Displace.y * 24)) * 0.2;

					n *= 0.5;

					return lerp(c, n, _Amount);
				}

			ENDCG
		}
	}

	FallBack off
}
