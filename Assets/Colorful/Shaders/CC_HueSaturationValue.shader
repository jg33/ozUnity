Shader "Hidden/CC_HueSaturationValue"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Master ("Master (HSV)", Vector) = (0, 0, 0, 0)
		_Reds ("Reds (HSV)", Vector) = (0, 0, 0, 0)
		_Yellows ("Yellows (HSV)", Vector) = (0, 0, 0, 0)
		_Greens ("Greens (HSV)", Vector) = (0, 0, 0, 0)
		_Cyans ("Cyans (HSV)", Vector) = (0, 0, 0, 0)
		_Blues ("Blues (HSV)", Vector) = (0, 0, 0, 0)
		_Magentas ("Magentas (HSV)", Vector) = (0, 0, 0, 0)
	}

	CGINCLUDE
	
		#include "UnityCG.cginc"
		#include "Colorful.cginc"

		sampler2D _MainTex;
		half4 _Master;
		half4 _Reds;
		half4 _Yellows;
		half4 _Greens;
		half4 _Cyans;
		half4 _Blues;
		half4 _Magentas;

		half4 frag_simple(v2f_img i):COLOR
		{
			half4 color = tex2D(_MainTex, i.uv);

			half3 hsv = RGBtoHSV(color.rgb);
			hsv.x = rot10(hsv.x + _Master.x);
			hsv.y = saturate(hsv.y + _Master.y);
			hsv.z = saturate(hsv.z + _Master.z);

			return half4(HSVtoRGB(hsv), color.a);
		}

		half4 frag_advanced(v2f_img i):COLOR
		{
			half4 color = tex2D(_MainTex, i.uv);

			// Master
			half3 hsv = RGBtoHSV(color.rgb);
			hsv.x = rot10(hsv.x + _Master.x);
			hsv.y = saturate(hsv.y + _Master.y);
			hsv.z = saturate(hsv.z + _Master.z);

			half ts = 1.0 / 360.0;
			half c15  = ts *  15.0;
			half c45  = ts *  45.0;
			half c75  = ts *  75.0;
			half c105 = ts * 105.0;
			half c135 = ts * 135.0;
			half c165 = ts * 165.0;
			half c195 = ts * 195.0;
			half c225 = ts * 225.0;
			half c255 = ts * 255.0;
			half c285 = ts * 285.0;
			half c315 = ts * 315.0;
			half c345 = ts * 345.0;
			
			half dr, dy, dg, dc, db, dm;

			// Reds
			hsv.x = rot10(hsv + ts * 60.0);
			dr = saturate(invlerp(c15, c45, hsv.x)) * (1.0 - saturate(invlerp(c75, c105, hsv.x)));
			hsv.x = rot10(hsv - ts * 60.0);
			
			// Yellow
			dy = saturate(invlerp(c15, c45, hsv.x)) * (1.0 - saturate(invlerp(c75, c105, hsv.x)));

			// Greens
			dg = saturate(invlerp(c75, c105, hsv.x)) * (1.0 - saturate(invlerp(c135, c165, hsv.x)));

			// Cyans
			dc = saturate(invlerp(c135, c165, hsv.x)) * (1.0 - saturate(invlerp(c195, c225, hsv.x)));

			// Blues
			db = saturate(invlerp(c195, c225, hsv.x)) * (1.0 - saturate(invlerp(c255, c285, hsv.x)));

			// Magentas
			dm = saturate(invlerp(c255, c285, hsv.x)) * (1.0 - saturate(invlerp(c315, c345, hsv.x)));

			hsv.x = rot10(hsv.x + dr * _Reds.x + dy * _Yellows.x + dg * _Greens.x + dc * _Cyans.x + db * _Blues.x + dm * _Magentas.x);
			hsv.y = saturate(hsv.y + dr * _Reds.y + dy * _Yellows.y + dg * _Greens.y + dc * _Cyans.y + db * _Blues.y + dm * _Magentas.y);
			hsv.z = saturate(hsv.z + dr * _Reds.z + dy * _Yellows.z + dg * _Greens.z + dc * _Cyans.z + db * _Blues.z + dm * _Magentas.z);

			return half4(HSVtoRGB(hsv), color.a);
		}

	ENDCG

	SubShader
	{
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }

		// (0) Simple
		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_simple
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma glsl
				#pragma exclude_renderers flash

			ENDCG
		}

		// (1) Advanced
		Pass
		{
			CGPROGRAM

				#pragma vertex vert_img
				#pragma fragment frag_advanced
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma target 3.0
				#pragma glsl
				#pragma exclude_renderers flash

			ENDCG
		}
	}

	FallBack off
}
