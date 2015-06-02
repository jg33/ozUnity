Shader "Hidden/FurBaseToonBasic"
{
    CGINCLUDE

	    #pragma fragmentoption ARB_precision_hint_fastest 
        #include "UnityCG.cginc"

        
			sampler2D _MainTex;
			samplerCUBE _ToonShade;
			float4 _MainTex_ST;
			float4 _Color;
            float _Density;
            float _Occlusion;
            float _Brightness; 
            float _Length;
            float4 _Gravity;
            float4 _Wind;
            float _WindSpeed;

		struct appdata 
        {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
			float3 normal : NORMAL;
		};

        struct v2f_surf 
        {
          float4 pos : POSITION;
		  float2 texcoord : TEXCOORD0;
		  float3 cubenormal : TEXCOORD1;

        };
        
        v2f_surf VertexLight(appdata v, v2f_surf o)
        {
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
				return o;
        }

    ENDCG

	SubShader 
    {
		    
	
	    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	    LOD 200

	    Alphatest Greater 0 ZWrite Off ColorMask RGB
		Blend SrcAlpha OneMinusSrcAlpha

    Pass 
    {
		Name "LAYER1"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
    
	    
            #pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);

              return VertexLight(v,o);
        }

        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion)*(_Brightness);
                c.a = col.a*_Density;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }
        
    Pass 
    {
		Name "LAYER2"
		Tags { "LightMode" = "ForwardBase" }

        CGPROGRAM
            #pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/10);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
             
              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.9)*(_Brightness);
                c.a = col.a*_Density*0.95;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }
        Pass 
    {
		Name "LAYER3"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma fragment frag_surf
            #pragma vertex vert_surf
            

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3 * _Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          return VertexLight(v,o);
        }

        
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.8)*(_Brightness);
                c.a = col.a*_Density*0.9;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER4"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf
           

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/5);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
              
	          

              return VertexLight(v,o);
        }
         
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.7)*(_Brightness);
                c.a = col.a*_Density*0.85;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
#pragma fragment frag_surf
            #pragma vertex vert_surf
            

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/4);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/16);
              
	          

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.6)*(_Brightness);
                c.a = col.a*_Density*0.8;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER6"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3 *_Length/10);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
              
	          

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.5)*(_Brightness);
                c.a = col.a*_Density*0.75;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER7"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 7 *_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.4)*(_Brightness);
                c.a = col.a*_Density*0.7;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER8"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 2 *_Length/5);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 4*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
              
	          

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.3)*(_Brightness);
                c.a = col.a*_Density*0.65;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER9"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 9 *_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.2)*(_Brightness);
                c.a = col.a*_Density*0.6;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER10"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal *_Length/2);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/4);
              
	          

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (1-_Occlusion*0.1)*(_Brightness);
                c.a = col.a*_Density*0.55;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER11"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 11*_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 121*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.5;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER12"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3*_Length/5);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
              
	          

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.45;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER13"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 13*_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 169*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);

              return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.4;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER14"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 7*_Length/10);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
              
	          return VertexLight(v,o);
        }
         
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.35;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER15"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3*_Length/4);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/16);
              
	          return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.3;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER16"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 4*_Length/5);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 16*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
              
	          return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.25;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER17"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 17*_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 289*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.2;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER18"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 9*_Length/10);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
              
	          return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.15;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER19"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 19*_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 361*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          return VertexLight(v,o);
        }
         
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.1;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER20"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM

#pragma fragment frag_surf
            #pragma vertex vert_surf

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind)));
              
	          return VertexLight(v,o);
        } 
        
        fixed4 frag_surf (v2f_surf i) : COLOR 
            {
				float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
                c.rgb *= (_Brightness);
                c.a = col.a*_Density*0.15;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
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
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.95)*(_Brightness);
	        c.a = col.a*_Density*0.975;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);

              return VertexLight(v,o);
        }
        ENDCG

    }
        
    Pass 
    {
		Name "LAYER2.5"
		Tags { "LightMode" = "ForwardBase" }

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.85)*(_Brightness);
	        c.a = col.a*_Density*0.925;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
             
              return VertexLight(v,o);
        }
        ENDCG

        }
        Pass 
    {
		Name "LAYER3.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.75)*(_Brightness);
	        c.a = col.a*_Density*0.875;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 7 * _Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }
        ENDCG

        }
        Pass 
        {
		Name "LAYER4.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.65)*(_Brightness);
	        c.a = col.a*_Density*0.825;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 9*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }
        ENDCG

        }
        Pass 
        {
		Name "LAYER5.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.55)*(_Brightness);
	        c.a = col.a*_Density*0.775;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 11*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 121*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER6.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.45)*(_Brightness);
	        c.a = col.a*_Density*0.725;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 13 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 169*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }
        ENDCG

        }
        Pass 
        {
		Name "LAYER7.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.35)*(_Brightness);
	        c.a = col.a*_Density*0.675;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3 *_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
              
	          

              return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER8.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.25)*(_Brightness);
	        c.a = col.a*_Density*0.625;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 17 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 289*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }
        ENDCG

        }
        Pass 
        {
		Name "LAYER9.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.15)*(_Brightness);
	        c.a = col.a*_Density*0.575;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 19 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 361*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }

        ENDCG

        }

        Pass 
        {
		Name "LAYER10.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (1-_Occlusion*0.05)*(_Brightness);
	        c.a = col.a*_Density*0.525;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal *21*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 441*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER11.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.475;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 23*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 529*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER12.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.425;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 5*_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 25*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
              
	          

              return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER13.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.375;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 27*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 729*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);

              return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER14.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.325;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 29*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 841*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER15.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.275;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 31*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 961*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }
        ENDCG

        }
        Pass 
        {
		Name "LAYER16.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.225;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 33*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1089*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }

        ENDCG

        }
        Pass 
        {
		Name "LAYER17.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.175;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 7*_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
              
	          return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER18.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.125;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 37*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1369*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER19.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.075;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 39*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1521*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }
        ENDCG

        }

        Pass 
        {
		Name "LAYER20.5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        fixed4 frag_surf (v2f_surf i) : COLOR 
        {
	        float4 col = _Color * tex2D(_MainTex, i.texcoord);
				float4 cube = texCUBE(_ToonShade, i.cubenormal);
			    half4 c =  (float4(col.rgb,1));
	        c.rgb *= (_Brightness);
	        c.a = col.a*_Density*0.025;
				return float4(2.0f * cube.rgb * c.rgb, c.a);
            
            
        }

        v2f_surf vert_surf (appdata v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length * 1.025);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1.050625*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind)));
              
	          return VertexLight(v,o);
        }
        ENDCG

        }
	}
	FallBack "Diffuse"
}
