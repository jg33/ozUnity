Shader "Hidden/CC_Vintage_Combine"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Range ("Hue Range (X) Min (Y) Max", Vector) = (-0.16, 0.16, 0.0, 0.0)
		_Params ("Hue (X), Boost (Y), Amount (Z)", Vector) = (0.0, 0.5, 1.0, 0.0)
		_LookupTex ("Lookup (RGB)", 2D) = "white" {}
		_Amount ("Amount (Float)", Range(0.0, 1.0)) = 1.0
	}
	
	CGINCLUDE

		#pragma vertex vert_img
		#include "UnityCG.cginc"
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma exclude_renderers flash
		#pragma target 3.0
		#include "Colorful.cginc"
		
		sampler2D _MainTex;
		sampler2D _LookupTex;
		half _Amount;
		
		// sRGB <-> Linear from http://entropymine.com/imageworsener/srgbformula/
		// using a bit more precise values than the IEC 61966-2-1 standard
		// see http://en.wikipedia.org/wiki/SRGB for more information
		half4 sRGB(half4 color)
		{
			color.rgb = (color.rgb <= half3(0.0031308, 0.0031308, 0.0031308)) ? color.rgb * 12.9232102 : 1.055 * pow(color.rgb, 0.41666) - 0.055;
			return color;
		}

		half4 Linear(half4 color)
		{
			color.rgb = (color <= half3(0.0404482, 0.0404482, 0.0404482)) ? color.rgb / 12.9232102 : pow((color.rgb + 0.055) * 0.9478672, 2.4);
			return color;
		}
		// ...

		half4 LUT(half4 color)
		{
			half blue = color.b * 63.0;

			half2 quad1 = half2(0.0, 0.0);
			quad1.y = floor(floor(blue) * 0.125);
			quad1.x = floor(blue) - quad1.y * 8.0;

			half2 quad2 = half2(0.0, 0.0);
			quad2.y = floor(ceil(blue) * 0.125);
			quad2.x = ceil(blue) - quad2.y * 8.0;

			half c1 = 0.0009765625 + (0.123046875 * color.r);
			half c2 = 0.0009765625 + (0.123046875 * color.g);

			half2 texPos1 = half2(0.0, 0.0);
			texPos1.x = quad1.x * 0.125 + c1;
			texPos1.y = -(quad1.y * 0.125 + c2);

			half2 texPos2 = half2(0.0, 0.0);
			texPos2.x = quad2.x * 0.125 + c1;
			texPos2.y = -(quad2.y * 0.125 + c2);

			half4 newColor = lerp(tex2D(_LookupTex, texPos1),
								  tex2D(_LookupTex, texPos2),
								  frac(blue));
			newColor.a = color.a;
			return lerp(color, newColor, _Amount);
		}

	ENDCG
	
	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
			CGPROGRAM

				#pragma fragment frag

				half2 _Range;
				half3 _Params; // x: hue, y: boost, z: amount

				fixed4 frag(v2f_img i):COLOR
				{
					
					half4 color = saturate(tex2D(_MainTex, i.uv));
					
					half3 target = color.rgb;
					half lum = luminance(target);
					half hue = RGBtoHUE(target);

					if (_Range.y > 1.0 && hue < _Range.y - 1.0) hue += 1.0;
					if (_Range.x < 0.0 && hue > _Range.x + 1.0) hue -= 1.0;
						
					target = (hue < _Params.x) ?
							 lerp(lum.xxx, target, smoothstep(_Range.x, _Params.x, hue) * _Params.y) :
							 lerp(lum.xxx, target, (1.0 - smoothstep(_Params.x, _Range.y, hue)) * _Params.y);

					color.rgb = lerp(color.rgb, target, _Params.z);
					//return color;
					return LUT(saturate(color));
				}

			ENDCG
		}
	}
	SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			
			CGPROGRAM

				#pragma fragment frag_linear

				half2 _Range;
				half3 _Params; // x: hue, y: boost, z: amount

				fixed4 frag_linear(v2f_img i):COLOR
				{
					
					half4 color = sRGB( saturate(tex2D(_MainTex, i.uv)) );
					
					half3 target = color.rgb;
					half lum = luminance(target);
					half hue = RGBtoHUE(target);

					if (_Range.y > 1.0 && hue < _Range.y - 1.0) hue += 1.0;
					if (_Range.x < 0.0 && hue > _Range.x + 1.0) hue -= 1.0;
						
					target = (hue < _Params.x) ?
							 lerp(lum.xxx, target, smoothstep(_Range.x, _Params.x, hue) * _Params.y) :
							 lerp(lum.xxx, target, (1.0 - smoothstep(_Params.x, _Range.y, hue)) * _Params.y);

					color.rgb = lerp(color.rgb, target, _Params.z);
					//return color;
					return Linear(LUT(saturate(color)));
				}

			ENDCG
		}
	}
	
	

	FallBack off
}
