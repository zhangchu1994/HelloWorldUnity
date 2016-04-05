Shader "Custom/DepthBlending" {

	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	
		_ThresholdH("clip threshold of height", Range(0.0, 0.5)) = 0.1
		_ThresholdW("clip threshold of width", Range(0.0, 0.5)) = 0.1

	}
	
    SubShader {

		 Pass {
        	ZTest Always
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

			#include "UnityCG.cginc"

			fixed4 frag(v2f_img i) : COLOR {
				fixed4 col = fixed4(0.0f, 0.0f, 0.0f, 1.0f);
				return col;
			}

			ENDCG
		}

        Pass {
        	ZTest Always
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
            float _ThresholdW;
            float _ThresholdH;

			struct a2v {
				float4 pos : POSITION;
				float2 texcoord: TEXCOORD0;
			};

			struct v2f {
				float4 pos : POSITION;
				float2 texcoord: TEXCOORD0;
			};

			v2f vert(a2v v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.pos);
				o.pos.x *= (1.0f - 2.0f * _ThresholdW);
				o.pos.y *= (1.0f - 2.0f * _ThresholdH);

				o.texcoord = v.texcoord * float2((1.0f - 2.0f * _ThresholdW), (1.0f - 2.0f * _ThresholdH)) + float2(_ThresholdW, _ThresholdH);
				return o;
			}

            fixed4 frag(v2f i) : COLOR {
            	fixed4 col = tex2D(_MainTex, i.texcoord);
				return col;
            }
            ENDCG
        }
    }
	
	
	
	FallBack "Diffuse"
}
