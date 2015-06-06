Shader "Hidden/CC_ChannelMixer"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Red ("Red Channel", Vector) = (1, 0, 0, 1)
		_Green ("Green Channel", Vector) = (0, 1, 0, 1)
		_Blue ("Blue Channel", Vector) = (0, 0, 1, 1)
		_Constant ("Constant", Vector) = (0, 0, 0, 1)
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
				half4 _Red;
				half4 _Green;
				half4 _Blue;
				half4 _Constant;

				fixed4 frag(v2f_img i):COLOR
				{
					half4 color = tex2D(_MainTex, i.uv);

					half3 result = (color.rrr * _Red)
								 + (color.ggg * _Green)
								 + (color.bbb * _Blue)
								 + _Constant;
					
					return fixed4(result, color.a);
				}

			ENDCG
		}
	}

	FallBack off
}
