Shader "Own Mobile/Item/WaveGlow" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Tint ("Tint Color", Color) = (1.0, 1.0, 1.0, 0.0)
		_NormalTex("Normals (2D)", 2D) = "white" {}
		_Strength("Distort strength", Float) = 1.0
		_Speed("Distort speed", Float) = 3.0
		_EdgeFade("Edge fade (0-1)", Float) = 1.0
		_Aphla("Aphla (0-1)", Float) = 1.0
	}
	
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		Cull Off
    	Lighting Off
    	ZWrite Off
    	AlphaTest Greater .01
    	Blend SrcAlpha OneMinusSrcAlpha
    
    	//GrabPass { "_ScreenTex" }

		Pass {
			CGPROGRAM
			#include "UnityCG.cginc"
			
			//#pragma exclude_renderers flash
			#pragma vertex vert
			#pragma fragment frag
	
			sampler2D _MainTex : register(s0);
			sampler2D _NormalTex : register(s1);
			
			float _Strength;
			float _Speed;
			float _EdgeFade;
			float _Aphla;
			
			fixed4 _Tint;
	
			struct v2f {
				float4 pos: SV_POSITION;
				float4 TexPos: TEXCOORD0;
				float2 uvbump: TEXCOORD1;
				float4 color: TEXCOORD2;
			};
			
			v2f vert(appdata_full v) {
			
				v2f o;
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.TexPos = v.texcoord;
				o.color = v.color;
				o.uvbump.xy = v.texcoord.xy;
				
				return o;
			}
			
			
			float4 frag(v2f i): COLOR {
				
				float2 uv1 = i.uvbump + _Time.x * _Speed;
				
				float2 edgeFactor = 1.0f;
				
				float fadeStart = 1.0f - _EdgeFade;
				float fadeEnd = 1.0f;
				
				
				edgeFactor.x = lerp(fadeStart, fadeEnd, i.uvbump.x) * lerp(fadeStart, fadeEnd, saturate(1.0f-i.uvbump.x));
				edgeFactor.y = lerp(fadeStart, fadeEnd, i.uvbump.y) * lerp(fadeStart, fadeEnd, saturate(1.0f-i.uvbump.y));
				
				float2 localNormal = float2(tex2D(_NormalTex, uv1).r,tex2D(_NormalTex, uv1).w) * 2.0f - 1.0f;
				
				float4 uv = i.TexPos + float4(localNormal * (edgeFactor.y * edgeFactor.x)*4.0f, 0.0f, 0.0f) * _Strength;
				
				float4 f = tex2D(_MainTex, uv.xy);
				f.a = (f.x + f.y + f.z)/3.0f;
				f.rgb *= _Tint.rgb * 2.0f;
				float4 col = tex2D(_MainTex, i.uvbump);
				f.a = (f.x + f.y + f.z)/3.0f * i.color.a;
				f.rgb = ( _Tint.rgb + 1.5f*col.rgb*col.rgb*col.rgb)/2.0f;
				//f.rgb *= tex2D(_MainTex, i.uvbump);//				finalColor.a *=_Aphla;
				//finalColor.a = tex2D(_MainTex, i.TexPos.xy).a;
				
				return f;
			}
			ENDCG

		}
	
	}
}