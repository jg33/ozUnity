Shader "Hidden/CC_Convolution3x3"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_PX ("Pixel Width", Float) = 1
		_PY ("Pixel Height", Float) = 1
		_Amount ("Amount", Range(0.0, 1.0)) = 1.0
		_KernelT ("Kernel Top Row", Vector) = (0, 0, 0, 0)
		_KernelM ("Kernel Middle Row", Vector) = (0, 0, 0, 0)
		_KernelB ("Kernel Bottom Row", Vector) = (0, 0, 0, 0)
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
				#pragma exclude_renderers flash
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				half _PX;
				half _PY;
				half _Amount;
				half4 _KernelT;
				half4 _KernelM;
				half4 _KernelB;

				fixed4 frag(v2f_img i):COLOR
				{
					half4 origin;
					half4 color = half4(0, 0, 0, 0);
					half4 temp;

					// Top
					temp = tex2D(_MainTex, i.uv + half2(-_PX, -_PY));
					color += temp * _KernelT.x;

					temp = tex2D(_MainTex, i.uv + half2(   0, -_PY));
					color += temp * _KernelT.y;

					temp = tex2D(_MainTex, i.uv + half2( _PX, -_PY));
					color += temp * _KernelT.z;

					// Middle
					temp = tex2D(_MainTex, i.uv + half2(-_PX,   0));
					color += temp * _KernelM.x;

					origin = tex2D(_MainTex, i.uv);
					color += origin * _KernelM.y;

					temp = tex2D(_MainTex, i.uv + half2( _PX,   0));
					color += temp * _KernelM.z;

					// Bottom
					temp = tex2D(_MainTex, i.uv + half2(-_PX,  _PY));
					color += temp * _KernelB.x;

					temp = tex2D(_MainTex, i.uv + half2(   0,  _PY));
					color += temp * _KernelB.y;

					temp = tex2D(_MainTex, i.uv + half2( _PX,  _PY));
					color += temp * _KernelB.z;

					return lerp(origin, color, _Amount);
				}

			ENDCG
		}
	}

	FallBack off
}
