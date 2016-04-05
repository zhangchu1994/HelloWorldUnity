Shader "Own Mobile/Item/Transparent Diffuse" {

Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Main Color", Color) = (1,1,1,1)
		_MatCap ("MatCap (RGB)", 2D) = "white" {}
	}
	Subshader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		
		
		Pass
		{
			ZWrite Off
			Ztest Lequal
			Cull Off			
			Blend SrcAlpha OneMinusSrcAlpha
			Lighting Off
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				
				float4 _MainTex_ST;
				
				struct v2f
				{
					float4 pos	: SV_POSITION;
					float2 cap	: TEXCOORD0;
					float2 uv 	: TEXCOORD1;
					
				};
				
				v2f vert (appdata_base v)
				{
					v2f o;
					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					
					half2 capCoord;
					capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,v.normal);
					capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,v.normal);
					o.cap = capCoord * 0.5f + 0.5f;
					
					return o;
				}
				
				uniform float4 _Color;
				sampler2D _MainTex;
				uniform sampler2D _MatCap;
				
				float4 frag (v2f i) : COLOR
				{
					float4 mc = tex2D(_MatCap, i.cap);
					float4 col = tex2D(_MainTex, i.uv);

					return _Color * mc * 2.0f * col;
				}
			ENDCG
		}
	}

Fallback "Transparent/VertexLit"
}