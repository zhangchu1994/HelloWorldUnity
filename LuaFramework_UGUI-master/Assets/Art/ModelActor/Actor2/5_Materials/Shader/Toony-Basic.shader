// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Toon/Basic" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_AttackSet ("AttackSet", Range (0.001, 0.600)) = 0.001
	}


	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Name "BASE"
			Cull back
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			samplerCUBE _ToonShade;
			float4 _MainTex_ST;
			float4 _Color;
			float _AttackSet;

			struct appdata {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct v2f {
				float4 pos : POSITION;
				float4 texcoord : TEXCOORD0;
				float3 cubenormal : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.texcoord.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				
				float3 Normal_normalize = normalize(v.normal);
				
				o.cubenormal = mul (UNITY_MATRIX_MV, float4(Normal_normalize,0.0)).xyz;
				
				// Light When Be Attack
				float4 viewInLocal = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1.0));
				
				float3 viewDir = normalize(viewInLocal.xyz - v.vertex.xyz);
				
				o.texcoord.z = dot(viewDir.xyz, Normal_normalize);
				o.texcoord.w = 0.0;
				
				return o;
			}

			half4 frag (v2f i) : COLOR
			{
				half4 col = _Color * tex2D(_MainTex, i.texcoord.xy);
				half4 localCube = texCUBE(_ToonShade, i.cubenormal);
				
				return half4(2.0 * localCube.rgb * col.rgb, col.a)+(1.0-i.texcoord.z) * half4(1.0,1.0,1.0,1.0) * _AttackSet;
			}
			ENDCG			
		}
	} 

	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Name "BASE"
			Cull Off
			SetTexture [_MainTex] {
				constantColor [_Color]
				Combine texture * constant
			} 
			SetTexture [_ToonShade] {
				combine texture * previous DOUBLE, previous
			}
		}
	} 
	
	Fallback "Diffuse"
}
