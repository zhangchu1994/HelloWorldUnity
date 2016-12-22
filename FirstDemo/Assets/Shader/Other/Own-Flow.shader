Shader "Own Mobile/Flow" {
Properties {
		_BaseColor ("Base Color", Color) = (1, 1, 1, 1)
		_MainTex ("Base Texture", 2D) = "white" {}		
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_FlowColor ("Flow Color (A)", Color) = (1, 1, 1, 1)
		_FlowTexture ("Flow Texture", 2D) = ""{}
		_FlowMap ("FlowMap (RG) Alpha (B)", 2D) = ""{}	
		_Strength ("Noise strength", Range(0, 1)) = 0					
		_Noise ("Flow Noise (R)", 2D) = ""{}	
		//_Emission("Flow Emission", Range(0, 2)) = 0	
	}
	SubShader {
		Tags { "RenderType"="Opaque" "FlowTag"="Flow" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma target 3.0

		fixed4 _BaseColor;
		sampler2D _MainTex;
		samplerCUBE _ToonShade;
		fixed4 _FlowColor;
		sampler2D _FlowTexture;
		sampler2D _FlowMap;
		half _Strength;
		sampler2D _Noise;
		//half _Emission;
		float4 _FlowMapOffset;
		

		struct Input {
			float2 uv_MainTex;
			float2 uv_FlowTexture;
			//float2 uv_Noise;
			float3 worldRefl;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 mainColor = tex2D (_MainTex, IN.uv_MainTex);

			half4 flowMap = tex2D (_FlowMap, IN.uv_MainTex);
			flowMap.r = flowMap.r * 2.0f - 1.011765f;
			flowMap.g = flowMap.g * 2.0f - 1.003922f;
			
			float phase1 = _FlowMapOffset.x;
			float phase2 = _FlowMapOffset.y;

			float noise = tex2D (_Noise, IN.uv_MainTex).r * _Strength;

			half4 t1 = tex2D (_FlowTexture, IN.uv_FlowTexture + flowMap.rg * (phase1 + noise)); 		 	
			half4 t2 = tex2D (_FlowTexture, IN.uv_FlowTexture + flowMap.rg * (phase2 + noise)); 		 	
			
			half4 final = lerp( t1, t2, _FlowMapOffset.z );
			
			half flowMapColor = flowMap.b * _FlowColor.a;

			mainColor.rgb *= _BaseColor.rgb * (1 - flowMapColor);
			final.rgb *= _FlowColor.rgb * flowMapColor;

			o.Albedo = mainColor.rgb + final.rgb;
			
			half4 cubeColor = texCUBE (_ToonShade, IN.worldRefl);
			o.Emission = 2.0f * cubeColor.rgb * o.Albedo.rgb;// * flowMap.b;
			//o.Emission = o.Albedo.rgb * _Emission * flowMap.b;

			o.Alpha = mainColor.a * _BaseColor.a * cubeColor.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
