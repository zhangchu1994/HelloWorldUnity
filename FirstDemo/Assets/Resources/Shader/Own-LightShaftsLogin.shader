Shader "Hidden/Aubergine/LightShaftsLogin" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Density ("Density", Float) = 1.0
		_Weight ("Weight", Float) = 1.0
		//_Decay ("Decay", Float) = 1.0
		//_Exposure ("Exposure", Float) = 1.0
		_LightSPos ("Light Screen Position", Vector) = (0,0,0,0) 

	}

	/*
	PLEASE PAY ATTENTION TO REMARKS BELOW
	FOR THE SAKE OF SPEED, THIS SHADER HAS TO BE HAND EDITED BY YOU
	*/

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off Lighting Off Fog { Mode off }
			CGPROGRAM
				#pragma vertex vert_img
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#pragma target 3.0
				#include "UnityCG.cginc"

				uniform sampler2D _MainTex;
				float _Density, _Weight;//, _Decay, _Exposure;
				float4 _LightSPos;
				
				float4 frag (v2f_img i) : COLOR {
					half2 dUV = (i.uv - _LightSPos.xy);
					//64 is the amount of samples, the limit for shader 2.0 is 12
					//if you want to target 2.0 lower this value to 12 and delete
					//pragma target 3.0 above
					dUV *= 1.0f / 11.0f * _Density;
					
					//half2 uv0 = i.uv - _LightSPos.xy;
					
					half4 col = tex2D(_MainTex, i.uv);
					half illum = 1.0f;
					
					//change below 64 to lower than 12 if you want to target 2.0
					for (int a = 0; a < 12; a++) {
						//i.uv -= dUV*2.0f;
						half3 sample = tex2D(_MainTex, (i.uv - _LightSPos.xy)*(1.0f-0.03f*a) - dUV*2.0f*a + _LightSPos.xy);
						sample *= _Weight;
						if (col.r >0.05f)
							col.rgb += sample*0.8f;
						else
							col.rgb += sample;
						//illum *= _Decay;
					}
					return col;
				}
			ENDCG
		}
	}
	Fallback off
}