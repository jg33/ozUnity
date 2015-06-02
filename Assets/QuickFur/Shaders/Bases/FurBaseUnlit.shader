Shader "Hidden/FurBaseUnlit"
{
    CGINCLUDE
    
        
                #include "UnityCG.cginc"

		        struct v2f
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    
                    float3 normal: TEXCOORD1; 
		        };

         uniform sampler2D _MainTex;
         uniform float4 _Color;
         uniform float _Length;
         uniform float4 _Gravity;
         uniform float4 _Wind;
         uniform float _Density;
         uniform float _Brightness;
         uniform float _WindSpeed;
         uniform float _Occlusion;
         uniform float4 _MainTex_ST;

    ENDCG

	SubShader 
    {
		    Tags { "RenderType"="Transparent" "IgnoreProjector"="True" "Queue"="Transparent"}
		    LOD 400
			ZWrite Off
            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            Name "LAYER1"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * _Length/20);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion)*(_Brightness);
                    c.a *= f.a*_Density;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER2"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * _Length/10);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100); //Gravity*(Layer/10)^2
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.9)*(_Brightness);
                    c.a *= f.a*_Density*0.95;
			        return c;
		        }
		    ENDCG
        } 
        Pass
        {
            Name "LAYER3"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 3*_Length/20);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.8)*(_Brightness);
                    c.a *= f.a*_Density*0.9;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER4"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * _Length/5);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c = (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.7) * (_Brightness);
                    c.a *= f.a*_Density*0.85;
			        return c;
		        }
		    ENDCG
        } 
        Pass
        {
            Name "LAYER5"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * _Length/4);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/16);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.6)*(_Brightness);
                    c.a *= f.a*_Density*0.8;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER6"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 3*_Length/10);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {  
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.5) * (_Brightness);
                    c.a *= f.a*_Density*0.75;
			        return c;
		        }
		    ENDCG
        }  
        Pass
        {
            Name "LAYER7"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 7 * _Length/20);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.4)*(_Brightness);
                    c.a *= f.a*_Density*0.7;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER8"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 2*_Length/5);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 4*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {  
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.3) * (_Brightness);
                    c.a *= f.a*_Density*0.65;
			        return c;
		        }
		    ENDCG
        } 
        Pass
        {
            Name "LAYER9"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 9*_Length/20);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.2)*(_Brightness);
                    c.a *= f.a*_Density*0.6;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER10"

		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * _Length/2);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/4);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (1-_Occlusion*0.1) * (_Brightness);
                    c.a *= f.a*_Density*0.55;
			        return c;
		        }
		    ENDCG
        }  
        Pass
        {
            Name "LAYER11"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 11*_Length/20);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 121*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.5;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER12"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 3*_Length/5);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.45;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER13"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 13*_Length/20);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 169*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.4;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER14"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 7*_Length/10);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.35;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER15"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 3*_Length/4);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/16);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.3;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER16"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 4*_Length/5);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 16*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.25;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER17"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 17*_Length/20);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 289*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.2;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER18"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 9*_Length/10);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.15;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER19"
		
		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * 19*_Length/20);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 361*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.1;
			        return c;
		        }
		    ENDCG
        }
        Pass
        {
            Name "LAYER20"

		    CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

	            v2f vert (appdata_base v) 
	            {
                  v2f o;
		          float3 p = (v.normal * _Length);
                  o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind)));
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
	            }

		        half4 frag(v2f i) : COLOR
                {
                    half4 f = tex2D (_MainTex, i.uv)*_Color;
			        half4 c =  (float4(f.rgb,1));
                    c.rgb *= (_Brightness);
                    c.a *= f.a*_Density*0.05;
			        return c;
		        }
		    ENDCG
        }


        //40-Step ONLY

         Pass 
    {
		Name "LAYER1.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

        half4 frag(v2f i) : COLOR
                {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.95)*(_Brightness);
	        c.a *= _Density*0.975;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 3*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

    }
        
    Pass 
    {
		Name "LAYER2.5"
		Tags { "LightMode" = "ForwardBase" }

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.85)*(_Brightness);
	        c.a *= _Density*0.925;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * _Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }
        Pass 
    {
		Name "LAYER3.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.75)*(_Brightness);
	        c.a *= _Density*0.875;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 7 * _Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }
        Pass 
        {
		Name "LAYER4.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.65)*(_Brightness);
	        c.a *= _Density*0.825;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 9*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }
        Pass 
        {
		Name "LAYER5.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.55)*(_Brightness);
	        c.a *= _Density*0.775;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 11*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 121*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER6.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.45)*(_Brightness);
	        c.a *= _Density*0.725;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 13 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 169*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }
        Pass 
        {
		Name "LAYER7.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.35)*(_Brightness);
	        c.a *= _Density*0.675;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 3 *_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER8.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR 
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.25)*(_Brightness);
	        c.a *= _Density*0.625;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 17 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 289*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }
        Pass 
        {
		Name "LAYER9.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.15)*(_Brightness);
	        c.a *= _Density*0.575;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 19 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 361*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER10.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (1-_Occlusion*0.05)*(_Brightness);
	        c.a *= _Density*0.525;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal *21*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 441*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER11.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.475;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 23*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 529*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER12.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.425;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 5*_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 25*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER13.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.375;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 27*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 729*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER14.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.325;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 29*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 841*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER15.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.275;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 31*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 961*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }
        Pass 
        {
		Name "LAYER16.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.225;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 33*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1089*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }
        Pass 
        {
		Name "LAYER17.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.175;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 7*_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER18.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.125;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 37*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1369*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER19.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.075;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * 39*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1521*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER20.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
		        half4 frag(v2f i) : COLOR
        {
	        half4 f = tex2D(_MainTex, i.uv)  * _Color;
			        half4 c =  (float4(f.rgb,1));
	       c.rgb *= (_Brightness);
	        c.a *= _Density*0.025;
			        return c;
            
            
        }

         v2f vert (appdata_base v) 
        {
               v2f o;

              float3 p = (v.normal * _Length * 1.025);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1.050625*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind)));
                  o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                  o.normal = normalize(mul(float4(v.normal,0), _Object2World));
                  return o;
        }

        ENDCG

        }
	}
	FallBack "Diffuse"
}
