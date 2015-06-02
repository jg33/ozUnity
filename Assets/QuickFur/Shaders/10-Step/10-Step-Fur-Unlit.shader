Shader "QuickFur/10 Step/Unlit" 
{
	Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
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

            UsePass "Hidden/FurBaseUnlit/LAYER2"
            UsePass "Hidden/FurBaseUnlit/LAYER4"
            UsePass "Hidden/FurBaseUnlit/LAYER6"
            UsePass "Hidden/FurBaseUnlit/LAYER8"
            UsePass "Hidden/FurBaseUnlit/LAYER10"
            UsePass "Hidden/FurBaseUnlit/LAYER12"
            UsePass "Hidden/FurBaseUnlit/LAYER14"
            UsePass "Hidden/FurBaseUnlit/LAYER16"
            UsePass "Hidden/FurBaseUnlit/LAYER18"
            UsePass "Hidden/FurBaseUnlit/LAYER20"
	}
    Fallback Off
}
