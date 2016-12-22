Shader "Own/Unlit/Transparent Colored Grayscale"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
		_NewTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
		_AlphaMask ("Grayscale (R), Alpha (A)", 2D) = "gray" {}
		_Effect ("Base (RGB), Alpha (A)", 2D) = "black" {}
		_Cutout ("Cutout threshold", Range(0.0,1.0) ) = 1.0
		_EffectRange ("Cutout threshold", Range(0.0,0.5) ) = 0.1
		_Enabled ("Grayscale switch", Range (0.0, 1.0)) = 1.0
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
	
			sampler2D _MainTex;
			sampler2D _NewTex;
			sampler2D _AlphaMask;
			sampler2D _Effect;
			//float4 _MainTex_ST;
			float _Cutout;
			float _EffectRange;
			int _Enabled;
				
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color;
				return o;
			}
				//0.21 R + 0.72 G + 0.07 B.
//			#include "UnityCG.cginc"
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 col;
				
				if (_Cutout > 0.0f && _Cutout < 1.0f) {
					float grayscale = tex2D(_AlphaMask, i.texcoord).r;
					if (grayscale < _Cutout) {
						col = tex2D(_MainTex, i.texcoord) * i.color;
					}else {
						col = tex2D(_NewTex, i.texcoord) * i.color;
					} 
					float _min = _Cutout - _EffectRange;
					float _max = _Cutout + _EffectRange;
					if (grayscale < _max && grayscale > _min ) {
						
						if (col.a != 0.0f) {
							fixed4 ncol = tex2D(_Effect, float2( (grayscale - _min) / (_EffectRange * 2.0f), 0.5f));
							col = (col + ncol / 2.0f);
						}
					}
				}else {
					col = tex2D(_MainTex, i.texcoord) * i.color;
				}
				if (_Enabled <= 0.25f) {
					float grey = dot(col.rgb, float3(0.21f, 0.72f, 0.07f));  
					col = fixed4(grey, grey, grey, col.a);
				}
				
				return col;
//				float grey = dot(col.rgb, float3(0.21, 0.72, 0.07));  
//				float blue = _Blue;
//				if (_Blue != 1.0f) {
////					blue = (_Blue + 1.0f) / 2.0f + sin(_Time.a) * (_Blue - 1.0f) / 2.0f;
//					blue = sin(_Time.a) / 2.0f + 0.5f;
//				}
//				return fixed4(grey, grey, grey, col.a);
			}
			ENDCG
		}
	}

//	SubShader
//	{
//		LOD 100
//
//		Tags
//		{
//			"Queue" = "Transparent"
//			"IgnoreProjector" = "True"
//			"RenderType" = "Transparent"
//		}
//		
//		Pass
//		{
//			Cull Off
//			Lighting Off
//			ZWrite Off
//			Fog { Mode Off }
//			Offset -1, -1
//			ColorMask RGB
//			AlphaTest Greater .01
//			Blend SrcAlpha OneMinusSrcAlpha
//			ColorMaterial AmbientAndDiffuse
//			
//			SetTexture [_MainTex]
//			{
//				Combine Texture * Primary
//			}
//		}
//	}
}
