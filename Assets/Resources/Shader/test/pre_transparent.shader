Shader "ew2/pre-transparent" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags {"Queue"="Transparent-1"} 
		LOD 100              	
			Pass {
                	Cull Back
					Lighting Off
					Blend SrcAlpha OneMinusSrcAlpha
					SetTexture [_MainTex] 
					{ 
                    	combine texture
					}
			}
	} 
}
