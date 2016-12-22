Shader "Own/StoveShift" {
	Properties {
		_BrightTex ("Base (RGB)", 2D) = "white" {}
		_DarkTex ("Base (RGB)", 2D) = "white" {}
		_YCutout ("y cutout", Range(0.0, 1.0)) = 0.0
		
	}
	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Cull Back
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Offset -1, -1
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
				
			#include "UnityCG.cginc"
	
			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			sampler2D _BrightTex;
			sampler2D _DarkTex;
			
			float _YCutout;
				
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color;
				return o;
			}
				
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col = float4(1.0f, 1.0f, 1.0f, 1.0f);
				
				if (i.texcoord.y > _YCutout + 0.5f || i.texcoord.y < 0.5f - _YCutout) {
					col = tex2D(_DarkTex, i.texcoord);
				}else if (i.texcoord.y > _YCutout + 0.4f) {// || i.texcoord.y < 0.5f - _YCutout)
					col = (10.0f * (_YCutout - i.texcoord.y) + 5.0f) * tex2D(_BrightTex, i.texcoord)
							+ (1.0f - (10.0f * (_YCutout - i.texcoord.y) + 5.0f)) * tex2D(_DarkTex, i.texcoord);
				}else if (i.texcoord.y < 0.6f - _YCutout) {
					col = (10.0f * (i.texcoord.y + _YCutout) - 5.0f) * tex2D(_BrightTex, i.texcoord)
							+ (1.0f - (10.0f * (i.texcoord.y + _YCutout) - 5.0f)) * tex2D(_DarkTex, i.texcoord);
				}else {
					col = tex2D(_BrightTex, i.texcoord);
				}
				//else {
//					
//				}
				
				return col;
			}
			ENDCG
		}
	}

	FallBack "Diffuse"
}
