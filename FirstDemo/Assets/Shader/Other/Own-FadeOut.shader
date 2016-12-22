Shader "Own Mobile/FadeOut" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_FadeoutTex("Fade Out (RGB)", 2D) = "black"{}
		_FadeoutStep("Fade Step (RGB)", 2D) = "gray"{}
		_Timeline("Weight", Float) = 1.0
	}
	
	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	float4 _Color;
	sampler2D _MainTex;
	samplerCUBE _ToonShade;
	sampler2D _FadeoutTex;
	sampler2D _FadeoutStep;
	float4 _FadeoutTex_ST;
	float _Timeline;
	// LOD 400
	struct appdata400
	{
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		float3 normal : NORMAL;
	};
	struct v2f400
	{
		float4 pos : POSITION;
		float4 uvs : TEXCOORD0;
		float3 cubenormal : TEXCOORD1;
	};
	v2f400 vs400(appdata400 i)
	{
		v2f400 o;
		o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
		o.cubenormal = mul (UNITY_MATRIX_MV, float4(i.normal,0));
		o.uvs.xy = i.texcoord;
		o.uvs.zw = TRANSFORM_TEX(i.texcoord, _FadeoutTex);
		return o;
	}
	float4 fs400(v2f400 i) : COLOR
	{
		float4 c = _Color * tex2D(_MainTex, i.uvs.xy);
		float4 cube = texCUBE(_ToonShade, i.cubenormal);
		float4 f = tex2D(_FadeoutTex, i.uvs.zw);
		float s = f.r + _Timeline;
		float4 final = tex2D(_FadeoutStep, float2(s, 0));
		c.a = 1.0f;
		c *= final;

		return float4(2.0f * cube.rgb * c.rgb, c.a);

	}
	// LOD 200
	struct appdata200
	{
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD;
	};
	struct v2f200
	{
		float4 pos : POSITION;
		float4 uvs : TEXCOORD;
	};
	v2f200 vs200(appdata200 i)
	{
		v2f200 o;
		o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
		o.uvs.xy = i.texcoord;
		o.uvs.zw = TRANSFORM_TEX(i.texcoord, _FadeoutTex);
		return o;
	}
	fixed4 fs200(v2f200 i) : COLOR
	{
		half4 c = tex2D(_MainTex, i.uvs.xy);
		half4 f = tex2D(_FadeoutTex, i.uvs.zw);
		half s = f.r + _Timeline;
		fixed4 final = tex2D(_FadeoutStep, float2(s, 0));
		c *= final * 2;
		
		return c;
	}
	ENDCG
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 400
		
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		
		Pass {
		
		CGPROGRAM
		#pragma vertex vs400
		#pragma fragment fs400
		#pragma fragmentoption ARB_precision_hint_fastest 
		ENDCG
		}
	} 
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 200
		
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		
		Pass {
		
		CGPROGRAM
		#pragma vertex vs200
		#pragma fragment fs200
		ENDCG
		}
	} 
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 100
		
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Back
				
		Pass {
			Lighting Off
			SetTexture [_MainTex] 
            { 
               	combine texture
            }
		}
	} 
	FallBack "Mobile/Diffuse"
}
