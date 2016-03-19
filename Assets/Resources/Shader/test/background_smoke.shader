Shader "ew2/menu/smoke" {
Properties {
	_MainTex ("Base layer (RGB)", 2D) = "white" {}
	_DetailTex ("2nd layer (RGB)", 2D) = "white" {}
	_ScrollX ("Base layer Scroll speed X", Float) = 1.0
	_ScrollY ("Base layer Scroll speed Y", Float) = 0.0
	_Scroll2X ("2nd layer Scroll speed X", Float) = 1.0
	_Scroll2Y ("2nd layer Scroll speed Y", Float) = 0.0
	_Color("Color", Color) = (1,1,1,1)
	
	_MMultiplier ("Layer Multiplier", Float) = 2.0
}

CGINCLUDE

#include "UnityCG.cginc"

sampler2D _MainTex;
sampler2D _DetailTex;

float _ScrollX;
float _ScrollY;
float _Scroll2X;
float _Scroll2Y;
float _MMultiplier;
	
float4 _Color;

//LOD 200
struct appdata200
{
	float4 vertex : POSITION;
	float2 texcoord : TEXCOORD0;
	float4 color : COLOR;
};

struct v2f200
{
	float4 pos : POSITION;
	float4 uv : TEXCOORD0;
	fixed4 color : TEXCOORD1;
};

v2f200 vs200(appdata200 i)
{
	v2f200 o;
	o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
	o.uv.xy = i.texcoord + (float2(_ScrollX, _ScrollY) * _Time);
	o.uv.zw = i.texcoord + (float2(_Scroll2X, _Scroll2Y) * _Time);
	o.color = _MMultiplier * _Color * i.color;
	return o;
}

fixed4 fs200(v2f200 i) : COLOR
{
	return tex2D(_MainTex, i.uv.xy) * tex2D(_DetailTex, i.uv.zw) * i.color;
}

ENDCG
	
SubShader {
	Tags { "Queue"="Transparent-2"}
	
	LOD 200	
	Pass {
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off ZWrite Off
		CGPROGRAM   
		#pragma vertex vs200
		#pragma fragment fs200
	
		ENDCG
	}
	}

SubShader {
	Tags { "Queue"="Transparent-2"}
		
	LOD 100	
	Pass {
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off ZWrite Off
		SetTexture [_MainTex] 
		{ 
        	combine texture
		}
	}
	}
}