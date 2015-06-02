Shader "QuickFur/20 Step/Diffuse" 
{
	Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
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

            UsePass "Hidden/FurBase/LAYER1"
            UsePass "Hidden/FurBase/LAYER2"
            UsePass "Hidden/FurBase/LAYER3"
            UsePass "Hidden/FurBase/LAYER4"
            UsePass "Hidden/FurBase/LAYER5"
            UsePass "Hidden/FurBase/LAYER6"
            UsePass "Hidden/FurBase/LAYER7"
            UsePass "Hidden/FurBase/LAYER8"
            UsePass "Hidden/FurBase/LAYER9"
            UsePass "Hidden/FurBase/LAYER10"
            UsePass "Hidden/FurBase/LAYER11"
            UsePass "Hidden/FurBase/LAYER12"
            UsePass "Hidden/FurBase/LAYER13"
            UsePass "Hidden/FurBase/LAYER14"
            UsePass "Hidden/FurBase/LAYER15"
            UsePass "Hidden/FurBase/LAYER16"
            UsePass "Hidden/FurBase/LAYER17"
            UsePass "Hidden/FurBase/LAYER18"
            UsePass "Hidden/FurBase/LAYER19"
            UsePass "Hidden/FurBase/LAYER20"
	}
    Fallback "QuickFur/20 Step/Unlit"
}
