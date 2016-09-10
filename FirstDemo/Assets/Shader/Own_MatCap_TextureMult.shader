 // MatCap Shader, (c) 2013,2014 Jean Moreno

Shader "Own Mobile/MatCap/Textured Multiply"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SkinSet("SkinSet", Float) = 2.0
		_MatCapSkin ("MatSkin (RGB)", 2D) = "white" {}
		_IronSet("IronSet", Float) = 2.0
		_MatCapIron ("MatIron (RGB)", 2D) = "white" {}
		
		_AttackSet ("AttackSet", Range (0.001, 0.600)) = 0.001
		
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (0.000, 0.060)) = .001
	}
	
	Subshader
	{
	
		Pass{
			Tags { "RenderType"="Opaque"}
			Cull Off
			Zwrite Off
			ZTest LEqual
			
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
				half4 col = _OutlineColor;
				
				if (_Outline <= 0.001f)
					col = tex2D (_MainTex, i.uvs);
				
				return col;
					
			}
		
			ENDCG
		}
			
		Tags { "RenderType"="Opaque" }
		
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
					
					float2 capCoord;
					capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,Normal_normalize);
					capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,Normal_normalize);
					o.cap = capCoord * 0.5f + float2(0.5f,0.5f);
					
					// Light When Be Attack
					float4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1.0f));
					
					float3 viewDir = normalize(viewInLocal.xyz - v.vertex.xyz);
					
					o.uv.z = dot(viewDir.xyz, Normal_normalize);
					o.uv.w = 0.0f;
					
					return o;
				}
				
				uniform sampler2D _MainTex;
				uniform float _SkinSet;
				uniform sampler2D _MatCapSkin;
				uniform float _IronSet;
				uniform sampler2D _MatCapIron;
				
				uniform float _AttackSet;
				
				float4 frag (v2f i) : COLOR
				{
					float4 tex = tex2D(_MainTex, i.uv.xy);
					float4 mcSkin = tex2D(_MatCapSkin, i.cap);
					float4 mcIron = tex2D(_MatCapIron, i.cap);
					
					float4 col = float4(0.0f,0.0f,0.0f,0.0f);
					
					// Don't RETURN value in "if else" struct.on AMD card will get a wrong result
					if (tex.a>0.1f)
						col = tex * mcIron * mcIron * _IronSet;
					else
						col = tex * mcSkin * _SkinSet;
					
					col = col+(1.0f-i.uv.z) * float4(1.0f,1.0f,1.0f,1.0f) * _AttackSet;
					col.a = 1.0f;
					return col;
				}
			ENDCG
		}
	}
	
	Fallback "VertexLit"
}