// Simplified Alpha Blended Particle shader. Differences from regular Alpha Blended Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Own Mobile/Particles/IceBody" {
	Properties {

		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_IceColor ("Ice Color", Color) = (.5,.5,.5,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_IceTex ("Ice Texture", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_IceHightLine ("Ice Hight Line", Range(0, 10)) = 0	
	}

	SubShader {
		Tags { "RenderType"="Opaque"}
		Cull back
		
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma target 3.0

		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		sampler2D _IceTex;
		samplerCUBE _ToonShade;
		half4 _Color;
		half4 _IceColor;
		half _IceHightLine;

		struct Input {
				half2 uv_MainTex;
				half2 uv_IceTex;
				half3 normal_ToonShade;
				half3 worldPos; 
				half3 worldRefl;
				half3 worldNormal;
			};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 col = tex2D (_MainTex, IN.uv_MainTex);

			half4 icecol = tex2D (_IceTex, IN.uv_IceTex);
			
			//half3 cubenormal = mul (UNITY_MATRIX_MV, float4(IN.normal_ToonShade,0)).rgb;
			
			half4 cubeColor = texCUBE (_ToonShade, IN.normal_ToonShade);
			
			if (IN.worldPos.y <_IceHightLine - 0.05f)
			{
				o.Albedo = 3.0f * cubeColor.rgb * col.rgb;
				o.Emission = _IceColor + 0.6f * icecol.rgb ;
			}else if (IN.worldPos.y <_IceHightLine)
			{
				o.Albedo = 3.0f * cubeColor.rgb * col.rgb;
				//o.Emission = cubeColor.rgb + icecol.rgb ;
				o.Emission = cubeColor.rgb + icecol.rgb;
				
			}else
				o.Emission = 1.7f * cubeColor.rgb * (_Color * col.rgb);
		}
		ENDCG
		
	}

	FallBack "Diffuse"
}
