Shader "QuickFur/40 Step/Rim Lit" 
{
	Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
        _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
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
            
            UsePass "Hidden/FurBaseRim/LAYER1"
            UsePass "Hidden/FurBaseRim/LAYER1.5"
            UsePass "Hidden/FurBaseRim/LAYER2"
            UsePass "Hidden/FurBaseRim/LAYER2.5"
            UsePass "Hidden/FurBaseRim/LAYER3"
            UsePass "Hidden/FurBaseRim/LAYER3.5"
            UsePass "Hidden/FurBaseRim/LAYER4"
            UsePass "Hidden/FurBaseRim/LAYER4.5"
            UsePass "Hidden/FurBaseRim/LAYER5"
            UsePass "Hidden/FurBaseRim/LAYER5.5"
            UsePass "Hidden/FurBaseRim/LAYER6"
            UsePass "Hidden/FurBaseRim/LAYER6.5"
            UsePass "Hidden/FurBaseRim/LAYER7"
            UsePass "Hidden/FurBaseRim/LAYER7.5"
            UsePass "Hidden/FurBaseRim/LAYER8"
            UsePass "Hidden/FurBaseRim/LAYER8.5"
            UsePass "Hidden/FurBaseRim/LAYER9"
            UsePass "Hidden/FurBaseRim/LAYER9.5"
            UsePass "Hidden/FurBaseRim/LAYER10"
            UsePass "Hidden/FurBaseRim/LAYER10.5"
            UsePass "Hidden/FurBaseRim/LAYER11"
            UsePass "Hidden/FurBaseRim/LAYER11.5"
            UsePass "Hidden/FurBaseRim/LAYER12"
            UsePass "Hidden/FurBaseRim/LAYER12.5"
            UsePass "Hidden/FurBaseRim/LAYER13"
            UsePass "Hidden/FurBaseRim/LAYER13.5"
            UsePass "Hidden/FurBaseRim/LAYER14"
            UsePass "Hidden/FurBaseRim/LAYER14.5"
            UsePass "Hidden/FurBaseRim/LAYER15"
            UsePass "Hidden/FurBaseRim/LAYER15.5"
            UsePass "Hidden/FurBaseRim/LAYER16"
            UsePass "Hidden/FurBaseRim/LAYER16.5"
            UsePass "Hidden/FurBaseRim/LAYER17"
            UsePass "Hidden/FurBaseRim/LAYER17.5"
            UsePass "Hidden/FurBaseRim/LAYER18"
            UsePass "Hidden/FurBaseRim/LAYER18.5"
            UsePass "Hidden/FurBaseRim/LAYER19"
            UsePass "Hidden/FurBaseRim/LAYER19.5"
            UsePass "Hidden/FurBaseRim/LAYER20"
            UsePass "Hidden/FurBaseRim/LAYER20.5"
	}
    Fallback "QuickFur/40 Step/Diffuse"
}
