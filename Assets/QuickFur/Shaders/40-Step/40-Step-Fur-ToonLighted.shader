Shader "QuickFur/40 Step/Toon Lighted" 
{
	Properties 
    {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
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

            UsePass "Hidden/FurBaseToonLighted/LAYER1"
            UsePass "Hidden/FurBaseToonLighted/LAYER1.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER2"
            UsePass "Hidden/FurBaseToonLighted/LAYER2.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER3"
            UsePass "Hidden/FurBaseToonLighted/LAYER3.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER4"
            UsePass "Hidden/FurBaseToonLighted/LAYER4.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER5"
            UsePass "Hidden/FurBaseToonLighted/LAYER5.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER6"
            UsePass "Hidden/FurBaseToonLighted/LAYER6.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER7"
            UsePass "Hidden/FurBaseToonLighted/LAYER7.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER8"
            UsePass "Hidden/FurBaseToonLighted/LAYER8.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER9"
            UsePass "Hidden/FurBaseToonLighted/LAYER9.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER10"
            UsePass "Hidden/FurBaseToonLighted/LAYER10.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER11"
            UsePass "Hidden/FurBaseToonLighted/LAYER11.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER12"
            UsePass "Hidden/FurBaseToonLighted/LAYER12.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER13"
            UsePass "Hidden/FurBaseToonLighted/LAYER13.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER14"
            UsePass "Hidden/FurBaseToonLighted/LAYER14.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER15"
            UsePass "Hidden/FurBaseToonLighted/LAYER15.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER16"
            UsePass "Hidden/FurBaseToonLighted/LAYER16.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER17"
            UsePass "Hidden/FurBaseToonLighted/LAYER17.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER18"
            UsePass "Hidden/FurBaseToonLighted/LAYER18.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER19"
            UsePass "Hidden/FurBaseToonLighted/LAYER19.5"
            UsePass "Hidden/FurBaseToonLighted/LAYER20"
            UsePass "Hidden/FurBaseToonLighted/LAYER20.5"
	}
    Fallback "QuickFur/40 Step/Toon Basic"
}
