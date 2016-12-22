// MatCap Shader, (c) 2013,2014 Jean Moreno

Shader "Own Mobile/Gold"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MatCap ("MatCap (RGB)", 2D) = "white" {}
		_MatShadow ("MatShadow (RGB)", 2D) = "white" {}
	}
	
	Subshader
	{
		Tags { "RenderType"="Opaque" }
		
		Pass
		{
			Tags { "LightMode" = "Always" }
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f
				{
					float4 pos	: SV_POSITION;
					float2 uv 	: TEXCOORD0;
					float4 cap	: TEXCOORD1;
				};
				
				uniform float4 _MainTex_ST;
				uniform float4 _MainTex_TexelSize;
				
				v2f vert (appdata_base v)
				{
					v2f o;
					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					
					half2 capCoord;
					half4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1.0f));		
					half3 viewDir = normalize(viewInLocal.xyz / viewInLocal.w - v.vertex.xyz);
					
					capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,normalize(v.normal));
					capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,normalize(v.normal));
					o.cap.xy = capCoord * 0.5f + 0.5f;
					
					capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,viewDir);
					capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,viewDir);
					o.cap.zw = capCoord * 0.5f + 0.5f;
					
					return o;
				}
				
				uniform sampler2D _MainTex;
				uniform sampler2D _MatCap;
				uniform sampler2D _MatShadow;
				
				fixed4 frag (v2f i) : COLOR
				{
					fixed off = _MainTex_TexelSize;
					
					fixed4 tex = tex2D(_MainTex, i.uv);
					fixed4 mc = tex2D(_MatCap, i.cap.xy);
					
					fixed4 shadow = tex2D(_MatShadow, i.cap.zw);
					
					
					fixed r = (tex.r/1.5f) + 0.4f;
					fixed4 col = fixed4(r,r,r,1.0f) * mc * shadow * 2.0f;
					
					return col;
				}
			ENDCG
		}
	}
	
	Fallback "VertexLit"
}