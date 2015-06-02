// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_LightmapInd', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D
// Upgrade NOTE: replaced tex2D unity_LightmapInd with UNITY_SAMPLE_TEX2D_SAMPLER

Shader "Hidden/FurBaseToonLighted"
{
    CGINCLUDE
    
        
        #pragma fragmentoption ARB_precision_hint_fastest
        #pragma multi_compile_fwdbasealpha nodirlightmap
        #include "HLSLSupport.cginc"
        #include "UnityShaderVariables.cginc"
        #define UNITY_PASS_FORWARDBASE
        #include "UnityCG.cginc"
        #include "Lighting.cginc"
        #include "AutoLight.cginc"

        #define INTERNAL_DATA
        #define WorldReflectionVector(data,normal) data.worldRefl
        #define WorldNormalVector(data,normal) normal
        
        sampler2D _Ramp;
        sampler2D _MainTex;
        fixed4 _Color;
        float _Density;
        float _Occlusion;
        float _Brightness; 
        float _Length;
        float4 _Gravity;
        float4 _Wind;
        float _WindSpeed;

        struct Input 
        {
	        float2 uv_MainTex : TEXCOORD0;
        };

        #pragma lighting ToonRamp exclude_path:prepass
        inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
        {
	        #ifndef USING_DIRECTIONAL_LIGHT
	        lightDir = normalize(lightDir);
	        #endif
	
	        half d = dot (s.Normal, lightDir)*0.5 + 0.5;
	        half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;
	
	        half4 c;
	        c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
	        c.a = 0;
	        return c;
        }

        #ifdef LIGHTMAP_OFF

        struct v2f_surf {
          float4 pos : SV_POSITION;
          float2 pack0 : TEXCOORD0;
          fixed3 normal : TEXCOORD1;
          fixed3 vlight : TEXCOORD2;
          LIGHTING_COORDS(3,4)
        };
        #endif
        #ifndef LIGHTMAP_OFF
        struct v2f_surf {
          float4 pos : SV_POSITION;
          float2 pack0 : TEXCOORD0;
          float2 lmap : TEXCOORD1;
          LIGHTING_COORDS(2,3)
        };
        #endif
        #ifndef LIGHTMAP_OFF
        // float4 unity_LightmapST;
        #endif
        float4 _MainTex_ST;


        #ifndef LIGHTMAP_OFF
        // sampler2D unity_Lightmap;
        #ifndef DIRLIGHTMAP_OFF
        // sampler2D unity_LightmapInd;
        #endif
        #endif

        v2f_surf VertexLight(appdata_full v, v2f_surf o)
        {
              o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
              #ifndef LIGHTMAP_OFF
              o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
              #endif
              float3 worldN = mul((float3x3)_Object2World, SCALED_NORMAL);
              #ifdef LIGHTMAP_OFF
              o.normal = worldN;
              #endif
              #ifdef LIGHTMAP_OFF
              float3 shlight = ShadeSH9 (float4(worldN,1.0));
              o.vlight = shlight;
              #ifdef VERTEXLIGHT_ON
              float3 worldPos = mul(_Object2World, v.vertex).xyz;
              o.vlight += Shade4PointLights (
                unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
                unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
                unity_4LightAtten0, worldPos, worldN );
              #endif // VERTEXLIGHT_ON
              #endif // LIGHTMAP_OFF
              TRANSFER_VERTEX_TO_FRAGMENT(o);
              return o;
        }

        SurfaceOutput SetupSurf(inout Input surfIN, v2f_surf IN)
        {
              surfIN.uv_MainTex = IN.pack0.xy;
              #ifdef UNITY_COMPILER_HLSL
              SurfaceOutput o = (SurfaceOutput)0;
              #else
              SurfaceOutput o;
              #endif
              o.Albedo = 0.0;
              o.Emission = 0.0;
              o.Specular = 0.0;
              o.Alpha = 0.0;
              o.Gloss = 0.0;
              #ifdef LIGHTMAP_OFF
              o.Normal = IN.normal;
              #endif
              return o;
        }

        fixed4 SurfLight(v2f_surf IN, SurfaceOutput o)
        {
              fixed atten = LIGHT_ATTENUATION(IN);
              fixed4 c = 0;
              #ifdef LIGHTMAP_OFF
              c = LightingToonRamp (o, _WorldSpaceLightPos0.xyz, atten);
              #endif // LIGHTMAP_OFF
              #ifdef LIGHTMAP_OFF
              c.rgb += o.Albedo * IN.vlight;
              #endif // LIGHTMAP_OFF
              #ifndef LIGHTMAP_OFF
              #ifdef DIRLIGHTMAP_OFF
              fixed4 lmtex = UNITY_SAMPLE_TEX2D(unity_Lightmap, IN.lmap.xy);
              fixed3 lm = DecodeLightmap (lmtex);
              #else
              fixed4 lmtex = UNITY_SAMPLE_TEX2D(unity_Lightmap, IN.lmap.xy);
              fixed4 lmIndTex = UNITY_SAMPLE_TEX2D_SAMPLER(unity_LightmapInd,unity_Lightmap, IN.lmap.xy);
              half3 lm = LightingToonRamp_DirLightmap(o, lmtex, lmIndTex, 0).rgb;
              #endif
              #ifdef SHADOWS_SCREEN
              #if defined(SHADER_API_GLES) && defined(SHADER_API_MOBILE)
              c.rgb += o.Albedo * min(lm, atten*2);
              #else
              c.rgb += o.Albedo * max(min(lm,(atten*2)*lmtex.rgb), lm*atten);
              #endif
              #else // SHADOWS_SCREEN
              c.rgb += o.Albedo * lm;
              #endif // SHADOWS_SCREEN
              c.a = o.Alpha;
            #endif // LIGHTMAP_OFF
              c.a = o.Alpha;
              return c;
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
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion)*(_Brightness);
	        o.Alpha = c.a*_Density;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);

              return VertexLight(v,o);
        }

        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }
        
    Pass 
    {
		Name "LAYER2"
		Tags { "LightMode" = "ForwardBase" }

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.9)*(_Brightness);
	        o.Alpha = c.a*_Density*0.95;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/10);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
             
              return VertexLight(v,o);
        }

       
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }
        Pass 
    {
		Name "LAYER3"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.8)*(_Brightness);
	        o.Alpha = c.a*_Density*0.9;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3 * _Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          return VertexLight(v,o);
        }

       
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER4"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.7)*(_Brightness);
	        o.Alpha = c.a*_Density*0.85;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/5);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER5"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.6)*(_Brightness);
	        o.Alpha = c.a*_Density*0.8;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/4);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/16);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER6"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.5)*(_Brightness);
	        o.Alpha = c.a*_Density*0.75;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3 *_Length/10);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
              
	          

              return VertexLight(v,o);
        }

       
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER7"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.4)*(_Brightness);
	        o.Alpha = c.a*_Density*0.7;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 7 *_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER8"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.3)*(_Brightness);
	        o.Alpha = c.a*_Density*0.65;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 2 *_Length/5);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 4*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER9"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.2)*(_Brightness);
	        o.Alpha = c.a*_Density*0.6;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 9 *_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER10"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.1)*(_Brightness);
	        o.Alpha = c.a*_Density*0.55;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal *_Length/2);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/4);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER11"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.5;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 11*_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 121*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          

              return VertexLight(v,o);
        }

        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER12"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.45;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3*_Length/5);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
              
	          

              return VertexLight(v,o);
        }

       
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER13"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.4;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 13*_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 169*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER14"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.35;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 7*_Length/10);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER15"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.3;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3*_Length/4);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/16);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER16"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.25;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 4*_Length/5);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 16*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/25);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }
        Pass 
        {
		Name "LAYER17"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.2;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 17*_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 289*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER18"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.15;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 9*_Length/10);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/100);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER19"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.1;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 19*_Length/20);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 361*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/400);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }

        Pass 
        {
		Name "LAYER20"
		Tags { "LightMode" = "ForwardBase" }
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
            #pragma vertex vert_surf
            #pragma fragment frag_surf

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.05;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind)));
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.95)*(_Brightness);
	        o.Alpha = c.a*_Density*0.975;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);

              return VertexLight(v,o);
        }

        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.85)*(_Brightness);
	        o.Alpha = c.a*_Density*0.925;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + (_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
             
              return VertexLight(v,o);
        }

       
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.75)*(_Brightness);
	        o.Alpha = c.a*_Density*0.875;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 7 * _Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }

       
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.65)*(_Brightness);
	        o.Alpha = c.a*_Density*0.825;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 9*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 81*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.55)*(_Brightness);
	        o.Alpha = c.a*_Density*0.775;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 11*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 121*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.45)*(_Brightness);
	        o.Alpha = c.a*_Density*0.725;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 13 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 169*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }

       
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.35)*(_Brightness);
	        o.Alpha = c.a*_Density*0.675;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 3 *_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 9*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.25)*(_Brightness);
	        o.Alpha = c.a*_Density*0.625;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 17 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 289*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.15)*(_Brightness);
	        o.Alpha = c.a*_Density*0.575;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 19 *_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 361*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(1-_Occlusion*0.05)*(_Brightness);
	        o.Alpha = c.a*_Density*0.525;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal *21*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 441*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.475;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 23*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 529*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          

              return VertexLight(v,o);
        }

        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.425;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 5*_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 25*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
              
	          

              return VertexLight(v,o);
        }

       
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.375;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 27*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 729*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);

              return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.325;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 29*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 841*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.275;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 31*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 961*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.225;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 33*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1089*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.175;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 7*_Length/8);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 49*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/64);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.125;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 37*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1369*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.075;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * 39*_Length/40);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1521*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind))/1600);
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
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

        void surf (Input IN, inout SurfaceOutput o) 
        {
	        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	        o.Albedo = c.rgb*(_Brightness);
	        o.Alpha = c.a*_Density*0.025;
            
            
        }

        v2f_surf vert_surf (appdata_full v) 
        {
              v2f_surf o;

              float3 p = (v.normal * _Length * 1.025);
              o.pos = mul(UNITY_MATRIX_MVP, v.vertex + float4(p,0) + 1.050625*(_Gravity+(sin(_Time.y*_WindSpeed)*_Wind)));
              
	          return VertexLight(v,o);
        }

        
        //Interprets "surf" shader
        fixed4 frag_surf (v2f_surf IN) : COLOR 
            {
              Input surfIN;
              SurfaceOutput o = SetupSurf(surfIN, IN);
              surf (surfIN, o);
              return SurfLight(IN, o);
            }
        ENDCG

        }


	}
	FallBack "Diffuse"
}
