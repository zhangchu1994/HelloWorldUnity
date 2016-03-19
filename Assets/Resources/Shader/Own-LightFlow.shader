Shader "Own Mobile/LightFlow" {
Properties {
		_BaseColor ("Base Color", Color) = (1, 1, 1, 1)
		_MainTex ("Base Texture", 2D) = "white" {}		
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_FlowColor ("Flow Color (A)", Color) = (1, 1, 1, 1)
		//_FlowTexture ("Flow Texture", 2D) = ""{}
		_FlowMap ("FlowMap (RG) Alpha (B)", 2D) = ""{}	
		//_RimLightTex("Rim Light Texture", 2D) = "black" {}
		_RimLightColor("Rim Light Color", Color) = (1,1,1,1)
		//_Strength ("Noise strength", Range(0, 1)) = 0					
		_Noise ("Flow Noise (R)", 2D) = ""{}	
		//_Emission("Flow Emission", Range(0, 2)) = 0	
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		cull back
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		#pragma target 3.0

		fixed4 _BaseColor;
		sampler2D _MainTex;
		samplerCUBE _ToonShade;
		fixed4 _FlowColor;
		//sampler2D _FlowTexture;
		sampler2D _FlowMap;
		//sampler2D _RimLightTex;
		float4 _RimLightColor;
		//half _Strength;
		sampler2D _Noise;
		//half _Emission;
		//float4 _FlowMapOffset;
		

		struct Input {
			float2 uv_MainTex;
			//float2 uv_FlowTexture;
			float2 uv_FlowMap;
			//float2 uv_Noise;
			//float2 uv_Rim;
			float3 worldRefl;
			float3 worldNormal;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 mainColor = tex2D (_MainTex, IN.uv_MainTex);

			half4 flowMap = tex2D (_FlowMap, IN.uv_FlowMap);
			//flowMap.r = flowMap.r * 2.0f - 1.011765;
			//flowMap.g = flowMap.g * 2.0f - 1.003922;
			
			//float phase1 = _FlowMapOffset.x;
			//float phase2 = _FlowMapOffset.y;
			
			half time = _Time.w * 0.09f;

			half4 noise = tex2D (_Noise, IN.uv_MainTex);// * _Strength;

			half4 final = mainColor * 0.9f; 		 	
			
						
			//half time = _Time.w * 0.09f;
			half tempDot = (dot(normalize(IN.worldRefl),normalize(IN.worldNormal))*0.6f +0.4f);
			
			half rimx = tempDot - 0.1f + sin(time)*sin(time);
			
			rimx = max(0.0f,rimx);
			rimx = min(1.0f,rimx);
			
			if (rimx>0.5f) rimx = 1.0f - rimx;
			
			rimx = rimx*2.5f + 0.5f;

			//if (rimx < 0.1f)
			//	rimx = 0.1f;

			//half4 rim = tex2D(_RimLightTex, half2(rimx,0.0f));
			
			//rim.r = rim.r * 0.5f +0.5f;
			//half4 t2 = tex2D (_FlowTexture, IN.uv_FlowTexture + flowMap.rg * (timetemp + noise)*10f); 		 	
			
			//half4 final = lerp( t1, t2, _FlowMapOffset.z );
			
			half flowMapColor = flowMap.b * _FlowColor.a;

			mainColor.rgb *= _BaseColor.rgb * (1.0f - flowMapColor);
			
			mainColor.rgb *= tempDot;
			
			if (flowMapColor < 0.3f)
				final.rgb *= flowMapColor * _FlowColor.rgb;
			else
				final.rgb *= flowMapColor * ((_FlowColor.rgb * (rimx+0.01f) + (1.0f-rimx)* _RimLightColor.rgb)*(0.5f*rimx+0.5f) + noise* (0.3f+_CosTime.w*_CosTime.w));

			o.Albedo = mainColor.rgb + final.rgb;// * (_SinTime.w*_SinTime.w*0.8f);
			
			//o.Albedo *= (dot(normalize(IN.worldRefl),normalize(IN.worldNormal))*0.3f +0.7f);
			
			half4 cubeColor = texCUBE (_ToonShade, IN.worldRefl);
			
			//final.rgb += rim.r * _RimLightColor.rgb;
			if (flowMapColor < 0.05f)
				o.Emission = 2.5f * cubeColor.rgb * o.Albedo.rgb;// * flowMap.b;
			else
				o.Emission = 1.5f * cubeColor.rgb * o.Albedo.rgb * (0.8f*rimx+0.2f);
			//o.Emission = o.Albedo.rgb * _Emission * flowMap.b;

			o.Alpha = mainColor.a * _BaseColor.a * cubeColor.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
