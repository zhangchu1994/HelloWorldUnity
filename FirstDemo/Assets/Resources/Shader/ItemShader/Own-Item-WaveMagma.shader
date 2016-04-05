Shader "Own Mobile/Item/WaveMagma" {
	Properties {
		//_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NormalTex("Normals (2D)", 2D) = "white" {}
		_Strength("Distort strength", Float) = 1.0
		_Speed("Distort speed", Float) = 3.0
		_EdgeFade("Edge fade (0-1)", Float) = 1.0
		//_Aphla("Aphla (0-1)", Float) = 1.0
		_Rim("Rim (0-1)", Float) = 0.0
	}
	
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		
		//Cull back
    	//Lighting Off
    	//ZWrite Off
    	//AlphaTest Greater .01
    	//Blend SrcAlpha OneMinusSrcAlpha
    
    	//GrabPass { "_ScreenTex" }

		Pass {
			CGPROGRAM
			#include "UnityCG.cginc"
			
			//#pragma exclude_renderers flash
			#pragma vertex vert
			#pragma fragment frag
	
			sampler2D _MainTex : register(s0);
			sampler2D _NormalTex : register(s1);
			float4 _MainTex_ST;
			half _Strength;
			half _Speed;
			half _EdgeFade;
			half _Rim;
	
			struct v2f {
				half4 pos: SV_POSITION;
				half4 TexPos: TEXCOORD0;
				half3 uvbump: TEXCOORD1;
			};
			
			v2f vert(appdata_full v) {
			
				v2f o;
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				
				o.TexPos = v.texcoord;
				o.TexPos.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
				
				o.uvbump.xy = v.texcoord.xy;
				
				half4 viewInLocal = mul(_World2Object, half4(_WorldSpaceCameraPos, 1.0f));
			
				half3 viewDir = normalize(viewInLocal.xyz / viewInLocal.w - v.vertex.xyz);
				o.uvbump.z = dot(viewDir.xyz, v.normal);
				
				return o;
			}
			
			
			half4 frag(v2f i): COLOR {
				
				half2 uv1 = i.uvbump.xy + _Time.x * _Speed;
				
				half2 edgeFactor = 1.0f;
				
				half fadeStart = 1.0f - _EdgeFade;
				half fadeEnd = 1.0f;
				
				
				edgeFactor.x = lerp(fadeStart, fadeEnd, i.uvbump.x) * lerp(fadeStart, fadeEnd, saturate(1.0f-i.uvbump.x));
				edgeFactor.y = lerp(fadeStart, fadeEnd, i.uvbump.y) * lerp(fadeStart, fadeEnd, saturate(1.0f-i.uvbump.y));
				
				half2 normal = half2(tex2D(_NormalTex, uv1).r,tex2D(_NormalTex, uv1).w) * 2.0f - 1.0f;
				
				half4 uv = i.TexPos + half4(normal * (edgeFactor.y * edgeFactor.x)*4.0f, 0.0f, 0.0f) * _Strength;
				
				half4 finalColor = tex2D(_MainTex, uv.xy);
				
				finalColor *= 1.2f;
				
				//finalColor.a = tex2D(_MainTex, i.TexPos.xy).a;
				
				finalColor.rgb += _Rim*half3(1.0f,1.0f,1.0f)* (1.0f-i.uvbump.z);
				
				return finalColor;
			}
			ENDCG

		}
	}
}