Shader "Own Mobile/Item/PaperTwoFace" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BackTex ("Back (RGB)", 2D) = "white" {}
		_MainColor ("Main Color", Color) = (1,1,1,1)
	}
	
	CGINCLUDE
	
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		sampler2D _BackTex;
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
			o.uvs = i.texcoord;
			
			return o;
		}
		
		fixed4 fs200(v2f200 i) : COLOR {
			fixed4 c = tex2D(_MainTex, i.uvs);
			c.a = 1.0f;
			return c;
		}
		
		fixed4 fs201(v2f200 i) : COLOR {
			fixed4 c = tex2D(_BackTex, float2(1.0f-i.uvs.x,i.uvs.y));
			c.a = 1.0f;
			return c;
		}
	ENDCG
	
	SubShader {
		Tags { "RenderType" = "Transparent" "Queue"="Transparent" }
		LOD 200
		ZWrite Off
		ZTest Always
		Lighting Off
		
		Pass {
			
			//Blend SrcAlpha One
			Cull back
			
			CGPROGRAM
			#pragma vertex vs200
			#pragma fragment fs200
		
			ENDCG
		}
		Pass {
			
			//Blend SrcAlpha One
			Cull front
			
			CGPROGRAM
			#pragma vertex vs200
			#pragma fragment fs201
		
			ENDCG
		}
	}
	SubShader {
		Tags { "RenderType" = "Transparent" "Queue"="Transparent" }
		LOD 100
		ZWrite Off
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
