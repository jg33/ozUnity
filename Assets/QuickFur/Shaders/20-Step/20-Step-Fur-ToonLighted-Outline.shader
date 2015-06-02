Shader "QuickFur/20 Step/Toon Lighted Outline" 
{
	Properties 
    {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
        _Length ("Length", Range(0.001, 0.4)) = 0.2
        _Gravity ("Gravity", Vector) = (0,-0.2,0,0)
        _Wind ("Wind", Vector) = (0.1,0,0.1,0)
        _WindSpeed ("Wind Speed", Range(0.1, 10)) = 1
        _Density ("Thickness", Range(0,1)) = 0.5
        _Brightness ("Brightness", Range(0,2)) = 1
        _Occlusion ("Fur Occlusion", Range(0,2)) = 0.5
	}
    
    CGINCLUDE
	#include "UnityCG.cginc"

    struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : POSITION;
		float4 color : COLOR;
	};
	
	uniform float _Outline;
	uniform float4 _OutlineColor;
            float _Length;
            float4 _Gravity;
            float4 _Wind;
            float _WindSpeed;

	v2f vert(appdata v) {
		v2f o;
              float3 p = (v.normal * _Length);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind)));

		float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		return o;
	}
    ENDCG

	SubShader 
    {
		    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	        LOD 200

	        Alphatest Greater 0 ZWrite Off ColorMask RGB
		    Blend SrcAlpha OneMinusSrcAlpha

            Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half4 frag(v2f i) :COLOR { return i.color; }
			ENDCG
		}
            UsePass "Hidden/FurBaseToonLighted/LAYER1"
            UsePass "Hidden/FurBaseToonLighted/LAYER2"
            UsePass "Hidden/FurBaseToonLighted/LAYER3"
            UsePass "Hidden/FurBaseToonLighted/LAYER4"
            UsePass "Hidden/FurBaseToonLighted/LAYER5"
            UsePass "Hidden/FurBaseToonLighted/LAYER6"
            UsePass "Hidden/FurBaseToonLighted/LAYER7"
            UsePass "Hidden/FurBaseToonLighted/LAYER8"
            UsePass "Hidden/FurBaseToonLighted/LAYER9"
            UsePass "Hidden/FurBaseToonLighted/LAYER10"
            UsePass "Hidden/FurBaseToonLighted/LAYER11"
            UsePass "Hidden/FurBaseToonLighted/LAYER12"
            UsePass "Hidden/FurBaseToonLighted/LAYER13"
            UsePass "Hidden/FurBaseToonLighted/LAYER14"
            UsePass "Hidden/FurBaseToonLighted/LAYER15"
            UsePass "Hidden/FurBaseToonLighted/LAYER16"
            UsePass "Hidden/FurBaseToonLighted/LAYER17"
            UsePass "Hidden/FurBaseToonLighted/LAYER18"
            UsePass "Hidden/FurBaseToonLighted/LAYER19"
            UsePass "Hidden/FurBaseToonLighted/LAYER20"
	}
    Fallback "QuickFur/20 Step/Toon Basic Outline"
}
