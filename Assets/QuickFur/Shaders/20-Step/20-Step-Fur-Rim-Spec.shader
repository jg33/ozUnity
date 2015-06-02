Shader "QuickFur/20 Step/Rim Lit Specular" 
{
	Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
	    _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	    _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
        _Length ("Length", Range(0.001, 0.4)) = 0.2
        _Gravity ("Gravity", Vector) = (0,-0.2,0,0)
        _Wind ("Wind", Vector) = (0.1,0,0.1,0)
        _WindSpeed ("Wind Speed", Range(0.1, 10)) = 1
        _Density ("Thickness", Range(0,1)) = 0.5
        _Brightness ("Brightness", Range(0,2)) = 1
        _Occlusion ("Fur Occlusion", Range(0,2)) = 0.5
	}

	SubShader 
    {
		    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	        LOD 200

	        Alphatest Greater 0 ZWrite Off ColorMask RGB
		    Blend SrcAlpha OneMinusSrcAlpha

            UsePass "Hidden/FurBaseRimSpec/LAYER1"
            UsePass "Hidden/FurBaseRimSpec/LAYER2"
            UsePass "Hidden/FurBaseRimSpec/LAYER3"
            UsePass "Hidden/FurBaseRimSpec/LAYER4"
            UsePass "Hidden/FurBaseRimSpec/LAYER5"
            UsePass "Hidden/FurBaseRimSpec/LAYER6"
            UsePass "Hidden/FurBaseRimSpec/LAYER7"
            UsePass "Hidden/FurBaseRimSpec/LAYER8"
            UsePass "Hidden/FurBaseRimSpec/LAYER9"
            UsePass "Hidden/FurBaseRimSpec/LAYER10"
            UsePass "Hidden/FurBaseRimSpec/LAYER11"
            UsePass "Hidden/FurBaseRimSpec/LAYER12"
            UsePass "Hidden/FurBaseRimSpec/LAYER13"
            UsePass "Hidden/FurBaseRimSpec/LAYER14"
            UsePass "Hidden/FurBaseRimSpec/LAYER15"
            UsePass "Hidden/FurBaseRimSpec/LAYER16"
            UsePass "Hidden/FurBaseRimSpec/LAYER17"
            UsePass "Hidden/FurBaseRimSpec/LAYER18"
            UsePass "Hidden/FurBaseRimSpec/LAYER19"
            UsePass "Hidden/FurBaseRimSpec/LAYER20"
	}
    Fallback "QuickFur/20 Step/Rim Lit"
}
