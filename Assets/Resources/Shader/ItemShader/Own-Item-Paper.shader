Shader "Own Mobile/Item/Paper" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainColor ("Main Color", Color) = (1,1,1,1)
	}
	
	CGINCLUDE
	
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		float4 _MainTex_ST;
		float4 _MainColor;
		
		// LOD 200
		struct appdata200
		{
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};
		
		struct v2f200
		{
			float4 pos : POSITION;
			float2 uvs : TEXCOORD0;
		};
		
		v2f200 vs200(appdata200 i) {
			v2f200 o;
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs = i.texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			
			return o;
		}
		
		fixed4 fs200(v2f200 i) : COLOR {
			fixed4 c = tex2D(_MainTex, i.uvs);
			c.a = 1.0f;
			return c;
		}
		
	ENDCG
	
	SubShader {
		Tags { "RenderType" = "Geometry" "Queue"="Transparent" }
		LOD 200
		ZWrite On
		ZTest Always
		Lighting Off
		
		Pass {
			
			//Blend SrcAlpha One
			Cull Off
			
			CGPROGRAM
			#pragma vertex vs200
			#pragma fragment fs200
		
			ENDCG
		}
	}
	SubShader {
		Tags { "RenderType" = "Geometry" "Queue"="Transparent" }
		LOD 100
		ZWrite On
		ZTest Always
		Lighting Off
		Pass {
			//Blend SrcAlpha One
			Cull back
		
			SetTexture [_MainTex] {
				combine texture
			}
		}
		Pass {
			//Blend SrcAlpha One
			Cull front
		
			SetTexture [_BackTex] {
				combine texture
			}
		}
	} 
	FallBack "Mobile/Diffuse"
}
