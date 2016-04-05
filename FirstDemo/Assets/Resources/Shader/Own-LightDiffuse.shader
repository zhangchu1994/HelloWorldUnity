// Simplified Alpha Blended Particle shader. Differences from regular Alpha Blended Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Own Mobile/LightDiffuse" {
	Properties {

		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Main Texture", 2D) = "white" {}
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
			half4 _Color;
			half _SunShine;
	        
			struct Input {
					half2 uv_MainTex;
					half3 worldRefl;
					half3 worldNormal;
				};
		
			void surf (Input IN, inout SurfaceOutput o) 
			{
				half4 col = tex2D (_MainTex, IN.uv_MainTex);
				
				float dotx = max (0, 1.4f*dot (normalize(IN.worldRefl), normalize(IN.worldNormal)));
				
				float temp = max (0,(1.25f + _SunShine - dotx));
				
				o.Albedo = (temp * temp + 0.7f + _SunShine) * col.rgb ;
				
				o.Emission = 0.1f * _Color * col.rgb ;
				
			}
			ENDCG
			
	}

	FallBack "Diffuse"
}
