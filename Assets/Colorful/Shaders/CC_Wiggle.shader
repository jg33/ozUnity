Shader "Hidden/CC_Wiggle"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Timer ("Timer", Float) = 0.0
		_Scale ("Scale", Float) = 12.0
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
				half _Timer;
				half _Scale;

				fixed4 frag(v2f_img i):COLOR
				{
					half2 t = i.uv;
					t.x += sin(_Timer + t.x * _Scale) * 0.01;
					t.y += cos(_Timer + t.y * _Scale) * 0.01 - 0.01;
					return tex2D(_MainTex, t);
				}

			ENDCG
		}
	}

	FallBack off
}
