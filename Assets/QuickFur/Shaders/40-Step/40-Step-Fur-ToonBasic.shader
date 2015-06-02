Shader "QuickFur/40 Step/Toon Basic" 
{
	Properties 
    {
        _Color ("Main Color", Color) = (.5,.5,.5,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
        _Length ("Length", Range(0.001, 0.8)) = 0.4
        _Gravity ("Gravity", Vector) = (0,-0.2,0,0)
        _Wind ("Wind", Vector) = (0.1,0,0.1,0)
        _WindSpeed ("Wind Speed", Range(0.1, 10)) = 1
        _Density ("Thickness", Range(0,0.5)) = 0.25
        _Brightness ("Brightness", Range(0,2)) = 1
        _Occlusion ("Fur Occlusion", Range(0,2)) = 0.5
	}

	SubShader 
    {
		    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	        LOD 200

	        Alphatest Greater 0 ZWrite Off ColorMask RGB
		    Blend SrcAlpha OneMinusSrcAlpha
            
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
    Fallback "QuickFur/40 Step/Unlit"
}
