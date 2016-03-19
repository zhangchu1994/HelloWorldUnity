Shader "Own Mobile/Item/Logo" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
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
			float3 uvs : TEXCOORD0;
		};
		
		v2f200 vslogo(appdata200 i) {
			v2f200 o;
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs.xy = i.texcoord;
			o.uvs.z =  1.0f;
			
			return o;
		}
		
		fixed4 fslogo(v2f200 i) : COLOR {
			fixed4 c = tex2D(_MainTex, i.uvs.xy);
			//fixed4 c1 = tex2D(_BackTex, i.uvs.xy);
			
			//c.a = 1.0f - c1.r;
			//c = c * (1.0f - i.uvs.z) + c1 * i.uvs.z;
			
			if (c.a > 0.2f)
				c.a = 1.0f;
				
			c *= 1.1f;
			
			//c = c * (1.0f - i.uvs.z);
			
			return c;
		}
		
	ENDCG
	SubShader {
	
		Tags { "Queue"="Transparent+200" }
		Pass {
			
			Blend SrcAlpha OneMinusSrcAlpha
			Cull back
			ZWrite Off
			ZTest Always
			
			CGPROGRAM
			#pragma vertex vslogo
			#pragma fragment fslogo
		
			ENDCG
		}
	}
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 100
		
		Pass {
			Blend SrcAlpha One
			Cull Off
			ZWrite Off
			Lighting Off
			ZTest Always
		
			SetTexture [_MainTex] {
				combine texture
			}
		}
	} 
	FallBack "Mobile/Diffuse"
}
