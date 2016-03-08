  Shader "vertexPainter/vB_map_Diffuse_2tex" {
	Properties {
		_Color 			("Main Color", Color) = (1,1,1,1)
		_blendPower 	("Blend Factor", float) = 50.0	
		_cutoff 		("Cutoff", float) = 5.0
															 
		_MainTex1 		("Texture 1 (RGB) BlendMap (Alpha Channel)", 2D) = "white" {}	

		_MainTex2 		("Texture 2 (RGB) ", 2D) = "white" {}	
	}
	SubShader {
		
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		#pragma target 3.0
	  
		struct Input {
		  
			float2 uv_MainTex1		 	: TEXCOORD0;
			float2 uv_MainTex2 			: TEXCOORD1; 
			float4 customColor;
			
		};
		
		void vert (inout appdata_full v, out Input o) {	
			UNITY_INITIALIZE_OUTPUT(Input,o);
			o.customColor = v.color;
      	}
      

		uniform sampler2D _MainTex1,_MainTex2;
	  
		fixed4 _Color;
		fixed  _blendVal, _blendPower , _cutoff;
	  
		void surf (Input IN, inout SurfaceOutput o) {
			
			fixed4 mTex1 		= tex2D (_MainTex1, IN.uv_MainTex1);
			fixed3 mTex2 		= tex2D (_MainTex2, IN.uv_MainTex2).rgb;
			
			fixed vertBlend 		= IN.customColor.r;	 
			
			//_blendVal = pow((vertBlend + (	(1.0f - vertBlend)	*	mTex1.a) + (mTex1.a *	vertBlend)	* _blendPower	), _cutoff);
																					
			//_blendVal = clamp(_blendVal, 0.0f, 1.0f);
			
			o.Albedo = lerp(mTex2,mTex1,vertBlend) * _Color;	
									
		}
	  
	  ENDCG

	} 
FallBack "Diffuse"
  }
