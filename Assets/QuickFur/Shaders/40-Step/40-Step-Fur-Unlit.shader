Shader "QuickFur/40 Step/Unlit" 
{
	Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Fur Colour (RGB) Map (A)", 2D) = "black" {}
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
            
            UsePass "Hidden/FurBaseUnlit/LAYER1"
            UsePass "Hidden/FurBaseUnlit/LAYER1.5"
            UsePass "Hidden/FurBaseUnlit/LAYER2"
            UsePass "Hidden/FurBaseUnlit/LAYER2.5"
            UsePass "Hidden/FurBaseUnlit/LAYER3"
            UsePass "Hidden/FurBaseUnlit/LAYER3.5"
            UsePass "Hidden/FurBaseUnlit/LAYER4"
            UsePass "Hidden/FurBaseUnlit/LAYER4.5"
            UsePass "Hidden/FurBaseUnlit/LAYER5"
            UsePass "Hidden/FurBaseUnlit/LAYER5.5"
            UsePass "Hidden/FurBaseUnlit/LAYER6"
            UsePass "Hidden/FurBaseUnlit/LAYER6.5"
            UsePass "Hidden/FurBaseUnlit/LAYER7"
            UsePass "Hidden/FurBaseUnlit/LAYER7.5"
            UsePass "Hidden/FurBaseUnlit/LAYER8"
            UsePass "Hidden/FurBaseUnlit/LAYER8.5"
            UsePass "Hidden/FurBaseUnlit/LAYER9"
            UsePass "Hidden/FurBaseUnlit/LAYER9.5"
            UsePass "Hidden/FurBaseUnlit/LAYER10"
            UsePass "Hidden/FurBaseUnlit/LAYER10.5"
            UsePass "Hidden/FurBaseUnlit/LAYER11"
            UsePass "Hidden/FurBaseUnlit/LAYER11.5"
            UsePass "Hidden/FurBaseUnlit/LAYER12"
            UsePass "Hidden/FurBaseUnlit/LAYER12.5"
            UsePass "Hidden/FurBaseUnlit/LAYER13"
            UsePass "Hidden/FurBaseUnlit/LAYER13.5"
            UsePass "Hidden/FurBaseUnlit/LAYER14"
            UsePass "Hidden/FurBaseUnlit/LAYER14.5"
            UsePass "Hidden/FurBaseUnlit/LAYER15"
            UsePass "Hidden/FurBaseUnlit/LAYER15.5"
            UsePass "Hidden/FurBaseUnlit/LAYER16"
            UsePass "Hidden/FurBaseUnlit/LAYER16.5"
            UsePass "Hidden/FurBaseUnlit/LAYER17"
            UsePass "Hidden/FurBaseUnlit/LAYER17.5"
            UsePass "Hidden/FurBaseUnlit/LAYER18"
            UsePass "Hidden/FurBaseUnlit/LAYER18.5"
            UsePass "Hidden/FurBaseUnlit/LAYER19"
            UsePass "Hidden/FurBaseUnlit/LAYER19.5"
            UsePass "Hidden/FurBaseUnlit/LAYER20"
            UsePass "Hidden/FurBaseUnlit/LAYER20.5"
	}
    Fallback Off
}
