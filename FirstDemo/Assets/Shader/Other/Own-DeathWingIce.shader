Shader "Own Mobile/DeathWingIce" {
Properties {
		_BaseColor ("Base Color", Color) = (1, 1, 1, 1)
		_MainTex ("Base Texture", 2D) = "white" {}	
		
		_LightSet("LightSet", Range (0.001, 1.000)) = 0.001
		_LightColor ("Light Color (A)", Color) = (1, 1, 1, 1)
		_LightFlowMap ("NoiseMap (RG)IronMap(G)LightMap(B)", 2D) = ""{}	
			
				
		_SkinSet("SkinSet", Float) = 2.0
		_MatCapSkin ("MatSkin (RGB)", 2D) = "white" {}
	
		_MatCapIce ("Huoshan Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		
		_IceLine("IceLine", Range (-0.001, 10.000)) = -0.001
				
		//_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		//_FlowColor ("Flow Color (A)", Color) = (1, 1, 1, 1)
		//_FlowTexture ("Flow Texture", 2D) = ""{}
		
		//_RimLightTex("Rim Light Texture", 2D) = "black" {}
		//_RimLightColor("Rim Light Color", Color) = (1,1,1,1)
		//_Strength ("Noise strength", Range(0, 1)) = 0					
		//_Noise ("Flow Noise (R)", 2D) = ""{}	
		//_Emission("Flow Emission", Range(0, 2)) = 0	
	}

	Subshader
	{
		Pass
		{
			Tags { "RenderType"="Opaque" }
			cull back
			Tags { "LightMode" = "Always" }
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f
				{
					float4 pos	: SV_POSITION;
					float3 uv 	: TEXCOORD0;
					float2 cap	: TEXCOORD1;
				};
				
				uniform float4 _MainTex_ST;
				uniform float _IceLine;
				
				v2f vert (appdata_base v)
				{
					v2f o;
					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
					
					
					float4 WorldPos = mul(_Object2World, v.vertex);
					if (WorldPos.y>=_IceLine)
						o.uv.z = 1.0f;
					else
						o.uv.z = 0.0f;
						
					float3 Normal_normalize = normalize(v.normal);
					half2 capCoord;
					capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,Normal_normalize);
					capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,Normal_normalize);
					o.cap = capCoord * 0.5f + 0.5f;
					
					return o;
				}
				
				uniform float4 _BaseColor;
				uniform sampler2D _MainTex;
				uniform float _SkinSet;
				uniform sampler2D _MatCapSkin;
				
				//uniform sampler2D _MatCapIron;
				uniform float _LightSet;
				uniform float4 _LightColor;
				uniform sampler2D _LightFlowMap;

				fixed4 frag (v2f i) : COLOR
				{
					fixed4 tex = tex2D(_MainTex, i.uv.xy);
					fixed4 temp = tex2D(_LightFlowMap, i.uv.xy*2.0f)*1.3f;
					fixed4 mcSkin = tex2D(_MatCapSkin, i.cap);
					
					fixed4 col = fixed4(1.0f,1.0f,1.0f,1.0f);
					
					if (i.uv.z>0.1f)
						col = (0.2f * temp + tex) * mcSkin * _SkinSet * _BaseColor ;
					else
						col = (temp + tex) * mcSkin * _SkinSet * _BaseColor ;
					
					
					//fixed time = _Time.w * 0.3f;
					//fixed sin2 = sin(time)*sin(time);
					
					//fixed set = 0.55f+0.45f*sin2;
					
					//col = set*tex* (0.8f + _LightSet);
					//col *= _BaseColor ;
						 
						
					return col;
						
				}
			ENDCG
		}
		
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off
		
	    Pass
	    {
			ColorMask 0
	    }
		
		Pass
		{
			//ZWrite Off
			Cull back			
			Blend SrcAlpha OneMinusSrcAlpha
			//ColorMask RGB
			//Lighting Off
			Tags { "LightMode" = "Always" }
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				struct v2f
				{
					float4 pos	: SV_POSITION;
					float2 cap	: TEXCOORD0;
					float3 uv 	: TEXCOORD1;
					float3 cubenormal : TEXCOORD2;
				};
				
				uniform float4 _MainTex_ST;
				uniform float _IceLine;
					
				v2f vert (appdata_base v)
				{
					v2f o;
					float3 Normal_normalize = normalize(v.normal);
					//float4 vertex_ = (v.normal,0.0f)*0.02f+v.vertex;
					v.vertex.xyz += Normal_normalize * 0.005f;
					
					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					
					o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
					
					half2 capCoord;
					capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,Normal_normalize);
					capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,Normal_normalize);
					o.cap = capCoord * 0.5f + 0.5f;
					
					float4 WorldPos = mul(_Object2World, v.vertex);
					if (WorldPos.y>=_IceLine)
						o.uv.z = 1.0f;
					else
						o.uv.z = 0.0f;
					
					o.cubenormal = mul (UNITY_MATRIX_MV, float4(Normal_normalize,0.0f)).rgb;
					
					return o;
				}
				
				uniform sampler2D _MainTex;
				uniform samplerCUBE _MatCapIce;
				uniform sampler2D _MatCapSkin;
				uniform sampler2D _LightFlowMap;
				
				float4 frag (v2f i) : COLOR
				{
					
					
					//fixed4 tex = tex2D(_MainTex, i.uv.xy*1.05f);
					
					fixed4 tex = fixed4(0.5f,0.5f,0.5f,0.0f);
					
					if (i.uv.z>0.1f)
					{
						//tex.a = 0.0f;
						return tex;
					}
					else
					{
						fixed4 tex = tex2D(_LightFlowMap, i.uv.xy*2.0f)*1.3f;;
						fixed4 mc = tex2D(_MatCapSkin, i.cap);
						fixed4 localCube = texCUBE(_MatCapIce, i.cubenormal)*2.0f;
						
						//tex.a = 0.15f;
						mc.a = 0.7f;
						localCube.a = 0.3f;
						tex.a = 0.7f;
						return 0.5f * localCube + 0.5f * mc * tex;
					}
				}
			ENDCG
		}
		
	}
	
	Fallback "VertexLit"

}
