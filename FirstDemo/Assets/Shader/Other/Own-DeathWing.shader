Shader "Own Mobile/DeathWing" {
Properties {
		_BaseColor ("Base Color", Color) = (1, 1, 1, 1)
		_MainTex ("Base Texture", 2D) = "white" {}	
		
		_LightColor ("Light Color (A)", Color) = (1, 1, 1, 1)
		_LightFlowMap ("NoiseMap (RG)IronMap(G)LightMap(B)", 2D) = ""{}		
				
		_SkinSet("SkinSet", Float) = 2.0
		_MatCapSkin ("MatSkin (RGB)", 2D) = "white" {}
		_IronSet("IronSet", Float) = 2.0
		_MatCapIron ("MatIron (RGB)", 2D) = "white" {}
		
		_AttackSet ("AttackSet", Range (0.001, 0.600)) = 0.001
		_LightSet("LightSet", Range (0.001, 1.000)) = 0.001
		
			
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
		Tags { "RenderType"="Opaque" }
		cull back
		Fog { Mode Off }
		
		Pass
		{
			Tags { "LightMode" = "Always" }
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f
				{
					float4 pos	: SV_POSITION;
					float4 uv 	: TEXCOORD0;
					float2 cap	: TEXCOORD1;
				};
				
				uniform float4 _MainTex_ST;
				
				v2f vert (appdata_base v)
				{
					v2f o;
					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
					
					float3 Normal_normalize = normalize(v.normal);
					
					half2 capCoord;
					capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,Normal_normalize);
					capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,Normal_normalize);
					o.cap = capCoord * 0.5f + 0.5f;
					
					// Light When Be Attack
					float4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1));
					
					float3 viewDir = normalize(viewInLocal.xyz - v.vertex.xyz);

					o.uv.z = dot(viewDir.xyz, Normal_normalize);
					o.uv.w = 0.0f;
					
					return o;
				}
				
				uniform float4 _BaseColor;
				uniform sampler2D _MainTex;
				uniform float _SkinSet;
				uniform sampler2D _MatCapSkin;
				uniform float _IronSet;
				uniform sampler2D _MatCapIron;
				uniform float _LightSet;
				uniform float4 _LightColor;
				uniform sampler2D _LightFlowMap;
				uniform float _AttackSet;
				
				fixed4 frag (v2f i) : COLOR
				{
					fixed4 tex = tex2D(_MainTex, i.uv.xy);
					fixed4 temp = tex2D(_LightFlowMap, i.uv.xy);
					fixed4 mcSkin = tex2D(_MatCapSkin, i.cap);
					fixed4 mcIron = tex2D(_MatCapIron, i.cap);
					
					fixed4 col = (1.0f,1.0f,1.0f,1.0f);
					
					if (temp.g > 0.1f )						
						col = tex  * mcIron * _IronSet * _BaseColor;
					else
						col = tex * mcSkin * _SkinSet * _BaseColor;
					
					if (temp.b > 0.05f)
					{
						fixed time = _Time.w * 0.3f;
						fixed sin2 = sin(time)*sin(time);
						
						fixed set = 0.55f*max(0.1f,(1.0f-_LightSet))+0.45f*min(1.0f,(1.0f+_LightSet))*sin2;
						
						col = tex *(set*tex + (1.0f-set)*_LightColor) * (0.8f + temp.r * 0.2f);
						col *= _BaseColor ;
						 
					}
					
					col += (1.0f-i.uv.z) * (1.0f,1.0f,1.0f,1.0f) * _AttackSet;
						
					return col;
						
				}
			ENDCG
		}
		
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	AlphaTest Greater .01
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
	
	// ---- Fragment program cards
	Pass {
	
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma multi_compile_particles

		#include "UnityCG.cginc"

		sampler2D _MainTex;
		
		struct appdata_t {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};
		
		float4 _MainTex_ST;

		v2f vert (appdata_t v)
		{
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.color = v.color;
			o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
			return o;
		}

		sampler2D _CameraDepthTexture;
		sampler2D _LightFlowMap;
		float _LightSet;
		
		fixed4 frag (v2f i) : COLOR
		{
			fixed4 temp = tex2D(_LightFlowMap, i.texcoord);
			
			if (temp.b > 0.3f)
			{
				return (1.3f * _LightSet) * tex2D(_MainTex, i.texcoord) * (0.5f + temp.r * 0.5f);
			}else
			{
			    return (1.0f,1.0f,1.0f,0.0f);
			}
		}
		ENDCG 
	}
	// ---- Fragment program cards
	Pass {
	
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#pragma multi_compile_particles

		#include "UnityCG.cginc"

		sampler2D _MainTex;
		
		struct appdata_t {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex : POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
		};
		
		float4 _MainTex_ST;

		v2f vert (appdata_t v)
		{
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.color = v.color;
			o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
			return o;
		}

		sampler2D _CameraDepthTexture;
		sampler2D _LightFlowMap;
		float _LightSet;
		
		fixed4 frag (v2f i) : COLOR
		{
			fixed4 temp = tex2D(_LightFlowMap, i.texcoord);
			
			if (temp.b > 0.3f)
			{
				return (1.3f * _LightSet) * tex2D(_MainTex, i.texcoord) * (0.5f + temp.r * 0.5f);
			}else
			{
			    return (1.0f,1.0f,1.0f,0.0f);
			}
		}
		ENDCG 
	}
		
	}
	
	Fallback "VertexLit"

}
