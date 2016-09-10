Shader "Own Mobile/Item/HeatHaze" {
	Properties {
		_NormalTex("Normals (2D)", 2D) = "white" {}
		_Strength("Distort strength", Float) = 1.0
		_Speed("Distort speed", Float) = 3.0
		_EdgeFade("Edge fade (0-1)", Float) = 1.0
	}
	
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }

		Cull Off
    	Lighting Off
    	ZWrite Off
    
    	GrabPass { "_ScreenTex" }

		Pass {
			CGPROGRAM
			#include "UnityCG.cginc"
			
			//#pragma exclude_renderers flash
			#pragma vertex vert
			#pragma fragment frag
	
			sampler2D _ScreenTex : register(s0);
			sampler2D _NormalTex : register(s1);
			
			float _Strength;
			float _Speed;
			float _EdgeFade;
	
			struct v2f {
				float4 pos: SV_POSITION;
				float4 screenPos: TEXCOORD0;
				float2 uvbump: TEXCOORD1;
			};
			
			v2f vert(appdata_full v) {
			
				v2f o;
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeGrabScreenPos(o.pos);
				
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
				
				float4 tempfloat4 = tex2D(_NormalTex, uv1);
				
				float2 localNormal = float2(tempfloat4.r,tempfloat4.w) * 2.0f - 1.0f;
				
				float4 uv = i.screenPos + float4(localNormal * (edgeFactor.y * edgeFactor.x)*4.0f, 0.0f, 0.0f) * _Strength;
				
				float4 finalColor = tex2Dproj(_ScreenTex, UNITY_PROJ_COORD(uv));
				
				float temp = 0.0f;
				temp = finalColor.r + finalColor.g + finalColor.b;
				
				if (temp < 1.0f)
					finalColor *= 0.86f;
				
				finalColor.a = 1.0f;
				
				return finalColor;
			}
			ENDCG

		}
	
	}
}
