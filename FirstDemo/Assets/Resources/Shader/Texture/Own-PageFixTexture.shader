Shader "Own Mobile/Texture/PageFixTexture" {
	Properties {
		_MainTex ("MainTex (RGB)", 2D) = "white" {}
		_PaperColor ("Paper Color", Color) = (1, 1, 1, 1)
		_GrounTex ("GrounTex (RGB)", 2D) = "white" {}
		_Fix ("Fix", Float) = 0.0
	}

	SubShader {
		Pass {
			Tags { "Queue"="Transparent"}
			ZTest Always Cull Off ZWrite On Lighting Off Fog { Mode off }
			CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				uniform sampler2D _GrounTex;
				uniform half4 _PaperColor;
				//uniform float4 _MainTex_TexelSize;
				uniform float _Fix;

				half4 frag (v2f_img i) : COLOR {

					half4 col1 = tex2D(_MainTex, i.uv);
					half4 col2 = tex2D(_GrounTex, i.uv);
					//half4 col2 = (1.0f - dot(eSqr, 0.1f)) * tex2D(_GrounTex, i.uv);
					if (_Fix>1.0f)
						_Fix = 1.0f;
					else if (_Fix<0.0f)
						_Fix = 0.0f;
						
					half4 col = half4(1.0f,1.0f,1.0f,1.0f);
					
					half dr = col1.r - col2.r;
					half dg = col1.g - col2.g;
					half db = col1.b - col2.b;
					if (dr*dr<0.001f && dg*dg<0.001f && db*db<0.001f)
						col = col1;
					else
					{
						half tempr = 0.2f + 0.9f* col2.r;
						col = col1*_Fix + (0.3f*half4(tempr,tempr,tempr,1.0f)+0.7f*col2)*(1.0f-_Fix)*_PaperColor;
					}
					
					return col;
				}
			ENDCG
		}
	}
	Fallback off
}