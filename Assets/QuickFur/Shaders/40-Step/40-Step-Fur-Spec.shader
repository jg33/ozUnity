Shader "QuickFur/40 Step/Specular" 
{
	Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
	    _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	    _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
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
            
            UsePass "Hidden/FurBaseSpec/LAYER1"
            UsePass "Hidden/FurBaseSpec/LAYER1.5"
            UsePass "Hidden/FurBaseSpec/LAYER2"
            UsePass "Hidden/FurBaseSpec/LAYER2.5"
            UsePass "Hidden/FurBaseSpec/LAYER3"
            UsePass "Hidden/FurBaseSpec/LAYER3.5"
            UsePass "Hidden/FurBaseSpec/LAYER4"
            UsePass "Hidden/FurBaseSpec/LAYER4.5"
            UsePass "Hidden/FurBaseSpec/LAYER5"
            UsePass "Hidden/FurBaseSpec/LAYER5.5"
            UsePass "Hidden/FurBaseSpec/LAYER6"
            UsePass "Hidden/FurBaseSpec/LAYER6.5"
            UsePass "Hidden/FurBaseSpec/LAYER7"
            UsePass "Hidden/FurBaseSpec/LAYER7.5"
            UsePass "Hidden/FurBaseSpec/LAYER8"
            UsePass "Hidden/FurBaseSpec/LAYER8.5"
            UsePass "Hidden/FurBaseSpec/LAYER9"
            UsePass "Hidden/FurBaseSpec/LAYER9.5"
            UsePass "Hidden/FurBaseSpec/LAYER10"
            UsePass "Hidden/FurBaseSpec/LAYER10.5"
            UsePass "Hidden/FurBaseSpec/LAYER11"
            UsePass "Hidden/FurBaseSpec/LAYER11.5"
            UsePass "Hidden/FurBaseSpec/LAYER12"
            UsePass "Hidden/FurBaseSpec/LAYER12.5"
            UsePass "Hidden/FurBaseSpec/LAYER13"
            UsePass "Hidden/FurBaseSpec/LAYER13.5"
            UsePass "Hidden/FurBaseSpec/LAYER14"
            UsePass "Hidden/FurBaseSpec/LAYER14.5"
            UsePass "Hidden/FurBaseSpec/LAYER15"
            UsePass "Hidden/FurBaseSpec/LAYER15.5"
            UsePass "Hidden/FurBaseSpec/LAYER16"
            UsePass "Hidden/FurBaseSpec/LAYER16.5"
            UsePass "Hidden/FurBaseSpec/LAYER17"
            UsePass "Hidden/FurBaseSpec/LAYER17.5"
            UsePass "Hidden/FurBaseSpec/LAYER18"
            UsePass "Hidden/FurBaseSpec/LAYER18.5"
            UsePass "Hidden/FurBaseSpec/LAYER19"
            UsePass "Hidden/FurBaseSpec/LAYER19.5"
            UsePass "Hidden/FurBaseSpec/LAYER20"
            UsePass "Hidden/FurBaseSpec/LAYER20.5"
	}
    Fallback "QuickFur/40 Step/Diffuse"
}