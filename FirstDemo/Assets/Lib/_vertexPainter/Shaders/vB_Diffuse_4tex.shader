  Shader "vertexPainter/vB_Diffuse_4tex" {

	Properties {
		_Color 				("Main Color", Color) = (1,1,1,1)
		_MainTex1 			("Texture 1 (RGB)", 2D) = "white" {}
		_MainTex2 			("Texture 2 (RGB)", 2D) = "white" {}	
		_MainTex3 			("Texture 2 (RGB)", 2D) = "white" {}	 
		_MainTex4 			("Texture 2 (RGB)", 2D) = "white" {}
	}

	SubShader {
		
	  Tags { "RenderType" = "Opaque" }
	  CGPROGRAM
	  #pragma surface surf Lambert vertex:vert 
	  
		struct Input {
		  
			float2 uv_MainTex1 		: TEXCOORD0;
			//float2 uv_MainTex2 		: TEXCOORD1;   
			//float2 uv_MainTex3 		: TEXCOORD2;    
			//float2 uv_MainTex4 		: TEXCOORD3;
			
			float4 color;
			
		};
		  		
		void vert (inout appdata_full v, out Input o) {	
			UNITY_INITIALIZE_OUTPUT(Input,o);
			o.color = v.color;
      	}


		uniform sampler2D _MainTex1, _MainTex2, _MainTex3, _MainTex4;
	  
		fixed4 _Color;
		fixed _Tile;
	  
		void surf (Input IN, inout SurfaceOutput o) {
			
			fixed4 col  = tex2D( _MainTex1,		IN.uv_MainTex1)*IN.color.r;
			col 		+= tex2D( _MainTex2,	IN.uv_MainTex1)*IN.color.g;	   
			col 		+= tex2D( _MainTex3,	IN.uv_MainTex1)*IN.color.b;	     
			col 		+= tex2D( _MainTex4,	IN.uv_MainTex1)*IN.color.a;

			o.Albedo = col * _Color.rgb;

		}
	  
	  ENDCG

	} 

FallBack "Diffuse"
  }
