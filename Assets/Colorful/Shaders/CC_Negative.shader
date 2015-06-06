Shader "Hidden/CC_Negative"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Amount ("Amount (Float)", Range(0.0, 1.0)) = 1.0
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
				half _Amount;

				fixed4 frag(v2f_img i):COLOR
				{
					half4 oc = tex2D(_MainTex, i.uv);
					half4 nc = half4(1, 1, 1, 1) - oc;
					nc.a = 1.0;
					return lerp(oc, nc, _Amount);
				}

			ENDCG
		}
	}

	FallBack off
}
