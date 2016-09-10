Shader "Toon/Basic_EyeOpen" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_AttackSet ("AttackSet", Range (0.001, 0.600)) = 0.001
		_EyeOpenSet ("EyeOpen", Range (0.001, 1.000)) = 0.001
	}


	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
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
			float _EyeOpenSet;

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
				
				o.cubenormal = mul (UNITY_MATRIX_MV, float4(Normal_normalize,0)).xyz;
				
				// Light When Be Attack
				float4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1.0f));
				
				float3 viewDir = normalize(viewInLocal.xyz - v.vertex.xyz);
				
				o.texcoord.z = dot(viewDir.xyz, Normal_normalize);
				o.texcoord.w = 0.0f;
				
				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord.xy);
				fixed4 localCube = texCUBE(_ToonShade, i.cubenormal);
				
				if (col.a>0.1f)
					col.rgb *= _EyeOpenSet;
				
				col *= _Color;
				
				return fixed4(2.0f * localCube.rgb * col.rgb, col.a)+(1.0f-i.texcoord.z) * fixed4(1.0f,1.0f,1.0f,1.0f) * _AttackSet;
			}
			ENDCG			
		}
		Pass {
			Cull back
			Blend SrcAlpha One
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _EyeOpenSet;

			struct appdata {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float3 normal : NORMAL;
			};
			
			struct v2f {
				float4 pos : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.texcoord.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);	
				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord.xy);
				
				col.rgb += fixed3(0.15f,0.4f,0.4f);
				
				fixed k = _EyeOpenSet*2.0f;
				if (k > 1.0f)
					k -= (k-1.0f)*2.0f;
					
				if (k < 0.0f)
					k = 0.0f;
				col.a = col.a*col.r*k*3.0f;

				
				return col;
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
	
	Fallback "VertexLit"
}
