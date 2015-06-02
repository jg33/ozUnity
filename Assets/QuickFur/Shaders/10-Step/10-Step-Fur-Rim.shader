Shader "QuickFur/10 Step/Rim Lit" 
{
	Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
        _Length ("Length", Range(0.001, 0.2)) = 0.1
        _Gravity ("Gravity", Vector) = (0,-0.2,0,0)
        _Wind ("Wind", Vector) = (0.1,0,0.1,0)
        _WindSpeed ("Wind Speed", Range(0.1, 10)) = 1
        _Density ("Thickness", Range(0,2)) = 1
        _Brightness ("Brightness", Range(0,2)) = 1
        _Occlusion ("Fur Occlusion", Range(0,2)) = 0.5
	}

	SubShader 
    {
		    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	        LOD 200

	        Alphatest Greater 0 ZWrite Off ColorMask RGB
		    Blend SrcAlpha OneMinusSrcAlpha

            UsePass "Hidden/FurBaseRim/LAYER2"
            UsePass "Hidden/FurBaseRim/LAYER4"
            UsePass "Hidden/FurBaseRim/LAYER6"
            UsePass "Hidden/FurBaseRim/LAYER8"
            UsePass "Hidden/FurBaseRim/LAYER10"
            UsePass "Hidden/FurBaseRim/LAYER12"
            UsePass "Hidden/FurBaseRim/LAYER14"
            UsePass "Hidden/FurBaseRim/LAYER16"
            UsePass "Hidden/FurBaseRim/LAYER18"
            UsePass "Hidden/FurBaseRim/LAYER20"
	}
    Fallback "QuickFur/10 Step/Diffuse"
}
