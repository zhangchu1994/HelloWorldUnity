// Simplified Alpha Blended Particle shader. Differences from regular Alpha Blended Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Own Mobile/Particles/ToonStoneBody" {
	Properties {

		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_IceColor ("Ice Color", Color) = (.5,.5,.5,1)
		_IceTex ("Ice Texture", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_FadeoutTex("Fade Out (RGB)", 2D) = "black"{}
		_Weight ("Weight", Range (0.000, 1.000)) = .001
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (0.000, 0.030)) = .001
	}

	SubShader {
		Pass{
				Tags { "RenderType"="Opaque"}
				Cull front
				//Zwrite on
				
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest 

				#include "UnityCG.cginc"
				
				float _Outline;
			    float4 _OutlineColor;
			    sampler2D _MainTex;
				
				struct appdata {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD;
				};

				struct v2f {
					float4 pos : POSITION;
					//float4 color : COLOR;
					float2 uvs : TEXCOORD;
				};
				
				v2f vert(appdata v) {
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					if (_Outline > 0.001f)
					{
						float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, normalize(v.normal));
						float2 offset = TransformViewToProjection(norm.xy);
						o.pos.xy += offset * o.pos.z * _Outline;
					}
					o.uvs = v.texcoord;
					//o.color = _OutlineColor;
					return o;
				}
				
				half4 frag (v2f i) : COLOR
				{
					//half4 col = tex2D (_MainTex, i.pos);
					if (_Outline > 0.001f)
						return _OutlineColor;//half4(_OutlineColor.rgb,1.0f);
					else 
						return tex2D (_MainTex, i.uvs);
				}
			
				ENDCG
			}
				
			Tags { "RenderType"="Opaque"}
			Cull back
			//Zwrite off
			
			CGPROGRAM
			#pragma surface surf Unlit
			#pragma target 3.0

			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _IceTex;
			samplerCUBE _ToonShade;
			sampler2D _FadeoutTex;
			half4 _Color;
			half4 _IceColor;
			half _Weight;
			
			inline half4 LightingUnlit (SurfaceOutput s, fixed3 lightDir, fixed atten)  
	        {  
	            half4 c = half4(1.0f,1.0f,1.0f,1.0f);  
	            c.rgb = s.Albedo;  
	            c.a = s.Alpha;  
	            return c;  
	        } 
	        
			struct Input {
					half2 uv_MainTex;
					half2 uv_IceTex;
					half2 uv_FadeoutTex;
					half3 worldPos; 
					half3 worldRefl;
					half3 worldNormal;
				};
		
			void surf (Input IN, inout SurfaceOutput o) 
			{
				half4 col = tex2D (_MainTex, IN.uv_MainTex);

				half4 icecol = tex2D (_IceTex, IN.uv_IceTex);
				
				half4 alphacol = tex2D (_FadeoutTex, IN.uv_FadeoutTex);
				
				//half3 cubenormal = mul (UNITY_MATRIX_MV, float4(IN.normal_ToonShade,0)).rgb;
				
				half4 cubeColor = texCUBE (_ToonShade, IN.worldNormal);

				//o.Albedo = 0.20f * col.rgb + 0.3f * icecol.rgb;
				//o.Albedo *=tex2D (_MainTex, IN.uv_MainTex).a;
				float dotx = max (0.0f, dot (normalize(IN.worldRefl), normalize(IN.worldNormal)));
				if (dotx < 0.2f)
					dotx = 0.1f - (0.1f-dotx)*0.5f;
					
				half mix = (col.r + col.g + col.b)*0.3f;
				//o.Emission *= _IceColor.rgb;
				
				half3 coltemp1 = icecol.rgb * cubeColor.rgb * 5.0f  * _Color.rgb * _IceColor.rgb;
				
				half3 coltemp2 =  cubeColor.rgb * (_Color.rgb * col.rgb) +  _Color.rgb * col.rgb;
				
				half tempweight = min (1.0f,(alphacol.g+_Weight));

				if (tempweight<0.7f)
					o.Emission =  coltemp2;
				else if (tempweight>0.7f && tempweight<0.72f)
					o.Emission =  coltemp2*5.0f;	
				else
					o.Emission =  (coltemp2*(1.0f- tempweight) + coltemp1 * tempweight) * .5f;
			}
			ENDCG
			
	}

	FallBack "Diffuse"
}
