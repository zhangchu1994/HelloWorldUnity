// Simplified Alpha Blended Particle shader. Differences from regular Alpha Blended Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Own Mobile/LightDiffuse-StoneToNomal" {
	Properties {

		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_StoneTex ("Stone Texture", 2D) = "white" {}
		_FadeoutTex("Fade Out (RGB)", 2D) = "black"{}
		_Weight ("Weight", Range (-1.000, 1.000)) = .001
		_SunShine ("SunShine Strongth", Range(0.0, 1.0)) = 0.0	
	}

	SubShader {
				
			Tags { "RenderType"="Opaque"}
			Cull back
			//Zwrite off
			
			CGPROGRAM
			#pragma surface surf Lambert
			#pragma target 3.0

			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _StoneTex;
			sampler2D _FadeoutTex;
			half4 _Color;
			half _SunShine;
			half _Weight;
	        
			struct Input {
					half2 uv_MainTex;
					half2 uv_FadeoutTex;
					half3 worldRefl;
					half3 worldNormal;
				};
		
			void surf (Input IN, inout SurfaceOutput o) 
			{
				half4 col = tex2D (_MainTex, IN.uv_MainTex);
				half4 stonecol = tex2D (_StoneTex, IN.uv_MainTex);
				half4 alphacol = tex2D (_FadeoutTex, IN.uv_FadeoutTex);
				
				float dotx = max (0, 1.4f*dot (normalize(IN.worldRefl), normalize(IN.worldNormal)));
				
				float temp = max (0,(1.25f + _SunShine - dotx));
				
				//o.Albedo = (temp * temp + 0.7f + _SunShine) * col.rgb ;
				
				half tempweight = min (1.0f,(alphacol.g+_Weight));
				tempweight = max (0.0f,tempweight);

				if (tempweight<0.5f)
				{
					o.Albedo = (temp * temp + 0.5f + _SunShine) * stonecol.rgb ;
					o.Emission =  stonecol;
				}
				else if (tempweight>0.5f && tempweight<0.6f)
				{
					tempweight = tempweight - 0.5f;
					o.Albedo = (temp * temp + 1.0f + _SunShine) * col.rgb ;
					o.Emission =  tempweight/0.1f*o.Albedo + (0.1f-tempweight)/0.1f*stonecol;	
				}
				else
				{
					o.Albedo = (temp * temp + 0.5f + _SunShine) * col.rgb ;
					o.Emission =  col;
				}
				
				//o.Emission = 0.1f * _Color * col.rgb ;
				
			}
			ENDCG
			
	}

	FallBack "Diffuse"
}
