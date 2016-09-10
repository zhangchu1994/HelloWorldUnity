Shader "Own Mobile/LightShafts" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		
		_Density ("Density", Float) = 1.0
		_Weight ("Weight", Float) = 1.0
		_Decay ("Decay", Float) = 1.0
		_Exposure ("Exposure", Float) = 1.0
		_LightSPos ("Light Screen Position", Vector) = (0,0.5,0,0) 
	}

	CGINCLUDE
		#include "UnityCG.cginc"
		//#pragma target 3.0
		
		sampler2D _MainTex;
		samplerCUBE _ToonShade;
		float4 _MainTex_ST;
		float4 _Color;
		
		float _Density, _Weight, _Decay, _Exposure;
		float4 _LightSPos;

		struct appdata {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;
			float3 normal : NORMAL;
		};
		
		struct v2f {
			float4 pos : POSITION;
			float2 texcoord : TEXCOORD0;
			float3 cubenormal : TEXCOORD1;
		};

		v2f vert1 (appdata v)
		{
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);

			float3 norm   = mul ((float3x3)UNITY_MATRIX_MV, v.normal);
			//float2 offset = TransformViewToProjection(norm.xy);
			
			float temp = dot((0.0f,0.0f,1.0f), norm);
			if (temp>0.0f)
				o.pos.xy += temp  * 1.0f;
			//o.pos.xy += offset * o.pos.z * 0.05f;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		
		v2f vert2 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert3 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			half4 pos = (o.pos - _LightSPos);
			pos *= 1.0f / 12.0f * _Density;
			o.pos = o.pos - 3.0f*pos;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert4 (appdata v)
		{
			v2f o;

			float4 PosInLocal = mul(_World2Object, _LightSPos);
			
			float3 PosDir = normalize(PosInLocal.xyz / PosInLocal.w - v.vertex.xyz);
			
			float tempdot = dot(v.normal, PosDir);
			
			//float4 tempPos = v.vertex;
			
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			
			if (tempdot<0.0f)
				o.pos.xy += tempdot  * 0.4f;
			
			
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert5 (appdata v)
		{
			v2f o;

			float4 PosInWorld = mul(_Object2World, v.vertex);
			float3 normalInWorld = normalize(mul((float3x3)_Object2World, v.normal));

			float3 PosDir = normalize(_LightSPos.xyz / _LightSPos.w - PosInWorld.xyz);
			
			float tempdot = dot(normalInWorld, PosDir);
			
			float4 tempPos = v.vertex;
			
			if (tempdot<0.0f)
				tempPos *= 5.0f;
			
			o.pos = mul (UNITY_MATRIX_MVP, tempPos);
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert6 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			half4 pos = (o.pos - _LightSPos);
			pos *= 1.0f / 12.0f * _Density;
			o.pos = o.pos - 6.0f*pos;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert7 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			half4 pos = (o.pos - _LightSPos);
			pos *= 1.0f / 12.0f * _Density;
			o.pos = o.pos - 7.0f*pos;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert8 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			half4 pos = (o.pos - _LightSPos);
			pos *= 1.0f / 12.0f * _Density;
			o.pos = o.pos - 8.0f*pos;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert9 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			half4 pos = (o.pos - _LightSPos);
			pos *= 1.0f / 12.0f * _Density;
			o.pos = o.pos - 9.0f*pos;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert10 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			half4 pos = (o.pos - _LightSPos);
			pos *= 1.0f / 12.0f * _Density;
			o.pos = o.pos - 10.0f*pos;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert11 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			half4 pos = (o.pos - _LightSPos);
			pos *= 1.0f / 12.0f * _Density;
			o.pos = o.pos - 11.0f * pos;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		v2f vert12 (appdata v)
		{
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			half4 pos = (o.pos - _LightSPos);
			pos *= 1.0f / 12.0f * _Density;
			o.pos = o.pos - 12.0f*pos;
			
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0.0f));
			return o;
		}
		
		
		


		float4 frag1 (v2f i) : COLOR
		{
			float4 col = _Color * tex2D(_MainTex, i.texcoord);
			float4 cube = texCUBE(_ToonShade, i.cubenormal);
			return float4(2.0f * (cube.rgb + col.rgb), 0.1f);
		}
		
		float4 frag2 (v2f i) : COLOR
		{
			float4 col = _Color * tex2D(_MainTex, i.texcoord);
			float4 cube = texCUBE(_ToonShade, i.cubenormal);
			return float4(2.0f * cube.rgb * col.rgb, 0.7f);
		}
		
	ENDCG
		
		
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZTest Off Cull Off ZWrite Off Lighting Off Fog { Mode off }
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass {
			
			CGPROGRAM
			#pragma vertex vert4
			#pragma fragment frag1
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		
		
		Tags { "RenderType"="Opaque" }
		UsePass "Toon/Basic/BASE"
		
		/*Pass {
			
			CGPROGRAM
			#pragma vertex vert3
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert4
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert5
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert6
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert7
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert8
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert9
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert10
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert11
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}
		Pass {
			
			CGPROGRAM
			#pragma vertex vert12
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			ENDCG			
		}*/
	} 
	
	Fallback "VertexLit"
}
