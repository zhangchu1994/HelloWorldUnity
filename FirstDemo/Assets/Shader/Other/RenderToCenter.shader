Shader "Custom/169ToCenter" {

	Properties {
		_MainTex ("Base (RGBA)", 2D) = "white" {}
	
		_ThresholdH("center threshold of height", Range(0.0, 0.5)) = 0.1
		_ThresholdW("center threshold of width", Range(0.0, 0.5)) = 0.1

	}
	
    SubShader {
    
    	Tags {
    		"Queue" = "Transparent"
    		"RenderType" = "Transparent"
    	}
        Pass {
        	ZTest Always Cull Off ZWrite Off Lighting Off Fog { Mode off }
        	Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            float _ThresholdW;
            float _ThresholdH;

            fixed4 frag(v2f_img i) : COLOR {
            	fixed4 col = tex2D(_MainTex, float2(i.uv.x * (1.0f/(1.0f-2.0f*_ThresholdW)) + _ThresholdW / (2.0f * _ThresholdW - 1.0f), i.uv.y * (1.0f/(1.0f-2.0f*_ThresholdH)) + _ThresholdH / (2.0f * _ThresholdH - 1.0f) ));
//				fixed4 col = tex2D(_MainTex, i.uv);
				if (i.uv.x < _ThresholdW) {
					col = fixed4(0.0f, 0.0f, 0.0f, 1.0f);
				}
				if (i.uv.y < _ThresholdH) {
					col = fixed4(0.0f, 0.0f, 0.0f, 1.0f);
				}
				if (i.uv.x > 1.0f - _ThresholdW) {
					col = fixed4(0.0f, 0.0f, 0.0f, 1.0f);
				}
				if (i.uv.y > 1.0f - _ThresholdH) {
					col = fixed4(0.0f, 0.0f, 0.0f, 1.0f);
				}
				
				return col;
//            	int inner = step(_ThresholdW,i.uv.x) * step(i.uv.x,(1.0f - _ThresholdW)) * step(_ThresholdH,i.uv.y) * step(i.uv.y,(1.0f - _ThresholdH));
//
//            	return (fixed4(0.0f, 0.0f, 0.0f, 1.0f) * (1 - inner) + tex2D(_MainTex, i.uv) * (inner) );
            }
            ENDCG
        }
    }
	
	
	
	FallBack "Diffuse"
}
