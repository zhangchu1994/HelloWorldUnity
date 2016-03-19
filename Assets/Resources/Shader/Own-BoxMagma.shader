Shader "Own/BoxMagma" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Cutout ("Cutout threshold", Range(0.0,1.0) ) = 1.0
		_Speed ("Speed", Range(0.0,1.0) ) = 1.0
		_FlowColor ("Flow Color", COLOR) = (0.827, 0.376, 0.0, 1)
	}
	SubShader {
		Tags
		{
			"Queue" = "Transparent"
			//"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		LOD 100
		
		Blend SrcAlpha One
//		BlendOp Min
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
				
			#include "UnityCG.cginc"
	
			
	
			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				half2 originaltexcoord : TEXCOORD1;
				float4 color : COLOR;
			};
	
			sampler2D _MainTex;
			//float4 _MainTex_ST;
			float _Cutout;
			float _Speed;
			float4 _FlowColor;
				
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.originaltexcoord = v.texcoord;
				o.texcoord = (v.texcoord + float2 (_Time.a, 0.0f)/64.0f) * 22.0f *_Speed;
				o.color = v.color;
				return o;
			}
				//0.21 R + 0.72 G + 0.07 B.
	//			#include "UnityCG.cginc"
			float4 frag (v2f i) : COLOR
			{
				float4 col = float4(1.0f,1.0f,1.0f,1.0f);
				
				if (i.originaltexcoord.x > _Cutout) {
//					discard;
					col.a = 0.0f;
				}else {
					float4 basecol = tex2D(_MainTex, i.originaltexcoord);
					float co = tex2D(_MainTex, i.texcoord).b;	
					
					col.rgb = _FlowColor.rgb / (1.3f + co);
					col.a = (1.0f - co) * (1.0f - co)  * basecol.g;
				}
				return col;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
