Shader "QuickFur/40 Step/Toon Basic Outline" 
{
	Properties 
    {
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
        _Length ("Length", Range(0.001, 0.8)) = 0.4
        _Gravity ("Gravity", Vector) = (0,-0.2,0,0)
        _Wind ("Wind", Vector) = (0.1,0,0.1,0)
        _WindSpeed ("Wind Speed", Range(0.1, 10)) = 1
        _Density ("Thickness", Range(0,0.5)) = 0.25
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
            UsePass "Hidden/FurBaseToonBasic/LAYER1"
            UsePass "Hidden/FurBaseToonBasic/LAYER1.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER2"
            UsePass "Hidden/FurBaseToonBasic/LAYER2.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER3"
            UsePass "Hidden/FurBaseToonBasic/LAYER3.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER4"
            UsePass "Hidden/FurBaseToonBasic/LAYER4.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER5"
            UsePass "Hidden/FurBaseToonBasic/LAYER5.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER6"
            UsePass "Hidden/FurBaseToonBasic/LAYER6.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER7"
            UsePass "Hidden/FurBaseToonBasic/LAYER7.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER8"
            UsePass "Hidden/FurBaseToonBasic/LAYER8.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER9"
            UsePass "Hidden/FurBaseToonBasic/LAYER9.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER10"
            UsePass "Hidden/FurBaseToonBasic/LAYER10.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER11"
            UsePass "Hidden/FurBaseToonBasic/LAYER11.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER12"
            UsePass "Hidden/FurBaseToonBasic/LAYER12.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER13"
            UsePass "Hidden/FurBaseToonBasic/LAYER13.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER14"
            UsePass "Hidden/FurBaseToonBasic/LAYER14.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER15"
            UsePass "Hidden/FurBaseToonBasic/LAYER15.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER16"
            UsePass "Hidden/FurBaseToonBasic/LAYER16.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER17"
            UsePass "Hidden/FurBaseToonBasic/LAYER17.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER18"
            UsePass "Hidden/FurBaseToonBasic/LAYER18.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER19"
            UsePass "Hidden/FurBaseToonBasic/LAYER19.5"
            UsePass "Hidden/FurBaseToonBasic/LAYER20"
            UsePass "Hidden/FurBaseToonBasic/LAYER20.5"
	}
    Fallback "QuickFur/40 Step/Toon Basic"
}
