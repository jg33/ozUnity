Shader "Hidden/CC_ContrastVignette"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Data ("Sharpness (X), Darkness (Y), Contrast (Z), Edge (W)", Vector) = (0.1, 0.3, 0.25, 0.5)
		_Coeffs ("Luminance coeffs (RGB)", Vector) = (0.5, 0.5, 0.5, 1.0)
		_Center ("Center point (XY)", Vector) = (0.5, 0.5, 1.0, 1.0)
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
				half4 _Data;
				half4 _Coeffs;
				half4 _Center;

				fixed4 frag(v2f_img i):COLOR
				{
					half4 color = tex2D(_MainTex, i.uv);

					half d = distance(i.uv, _Center.xy);
					half multiplier = smoothstep(0.8, _Data.x * 0.799, d * (_Data.y + _Data.x));
					color.rgb = (color.rgb - _Coeffs.rgb) * max((1.0 - _Data.z * (multiplier - 1.0) - _Data.w), 1.0) + _Coeffs.rgb;
					color.rgb *= multiplier;

					return color;
				}

			ENDCG
		}
	}

	FallBack off
}
