Shader "ew2/player with effect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_EffectColor("Effect Color", Color) = (1,1,1,1)
		_DamageColor("Attack Color", Color) = (0,0,0,0)
	}
	
	CGINCLUDE
	
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		float4 _EffectColor;
		float4 _DamageColor;
		
		struct appdata
		{
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
		};
		
		struct v2f
		{
			float4 pos : POSITION;
			float2 uv : TEXCOORD0;
		};
		
		v2f vs(appdata i) {
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uv = i.texcoord;
			return o;
		}
		
		float4 fs(v2f i) : COLOR {
			float4 c = tex2D(_MainTex, i.uv);
			c.xyz = (c.x + c.y + c.z) * _EffectColor;
			c.xyz += _DamageColor;
			return c;
		}		
	
	ENDCG
	
	SubShader {
		Tags { "Queue"="Geometry" }
		LOD 200
		
		Pass {
			CGPROGRAM
			#pragma vertex vs
			#pragma fragment fs
		
			ENDCG
		}
	} 
	SubShader {
		Tags { "Queue"="Geometry" }
		LOD 100
		
		Pass {
			Lighting Off
			SetTexture [_MainTex] 
            { 
               	constantColor [_EffectColor]
               	combine texture * constant Quad
            }
            SetTexture [_MainTex]
            {
            	constantColor [_DamageColor]
               	combine previous + constant
            }
		}
	}
	FallBack "Mobile/Diffuse"
}
