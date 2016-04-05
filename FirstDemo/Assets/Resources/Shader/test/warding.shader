Shader "ew2/warding" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	CGINCLUDE
	
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		// LOD 400
		struct appdata400
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
		};
		struct v2f400
		{
			float4 pos : POSITION;
			float3 uvs : TEXCOORD0;
		};
		v2f400 vs400(appdata400 i)
		{
			v2f400 o;
			
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs.xy = i.texcoord;
			
			float4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1));
			
			float3 viewDir = normalize(viewInLocal.xyz / viewInLocal.w - i.vertex.xyz);
			o.uvs.z = dot(viewDir.xyz, i.normal);
			
			return o;
		}
		float4 fs400(v2f400 i) : COLOR
		{
			float4 c = tex2D(_MainTex, i.uvs.xy);
			
			c.a *= 1 - i.uvs.z;
			
			return c;
		}
	ENDCG
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 100
		
		Pass {
			
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Back
			
			CGPROGRAM
			#pragma vertex vs400
			#pragma fragment fs400
		
			ENDCG
		}
	}
	FallBack "Mobile/Diffuse"
}
