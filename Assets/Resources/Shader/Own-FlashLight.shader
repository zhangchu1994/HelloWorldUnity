Shader "Own Mobile/FlashLight" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }
		_MainColor("Main Color", Color) = (0.5,0.5,0.5,1)
		_RimLightTex("Rim Light Texture", 2D) = "black" {}
		_RimLightColor("Rim Light Color", Color) = (1,1,1,1)
		_AttackColor("Attack Color", Color) = (0,0,0,0)
		
		_SkillScale("SkillScale", float) = 1.0
		_SkillTransparent("_SkillTransparent", float) = 1.0
	}
	CGINCLUDE
	
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		sampler2D _RimLightTex;
		float4 _AttackColor;
		float4 _MainColor;
		float4 _RimLightColor;
		samplerCUBE _ToonShade;
		float _SkillScale;
		float _SkillTransparent;
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
			float4 uvs : TEXCOORD0;
			float3 cubenormal : TEXCOORD1;
		};
		v2f400 vs400(appdata400 i)
		{
			v2f400 o;
			
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs.xy = i.texcoord;
			
			// HACK : normally transformation matrix don't change w element in vector.(1 instance of exception is skewing by set row 4 of matrix). Hence normalize is not necessary.
			float4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1));
			float3 Normal_normalize = normalize(i.normal);
			float3 viewDir = normalize(viewInLocal.xyz - i.vertex.xyz);
			
			o.cubenormal = mul (UNITY_MATRIX_MV, float4(Normal_normalize,0));
			float time = _Time.w * 2.0f;
			
			o.uvs.z = dot(viewDir.xyz, Normal_normalize) + sin(time)* sin(time)*0.4;
			o.uvs.w = 0.0f;
			
			return o;
		}
		float4 fs400(v2f400 i) : COLOR
		{
			//float4 c = tex2D(_MainTex, i.uvs.xy);
			float4 rim = tex2D(_RimLightTex, i.uvs.zw);
			
			float4 c = _MainColor * tex2D(_MainTex, i.uvs.xy);
			float4 cube = texCUBE(_ToonShade, i.cubenormal);
			c = float4(2.0f * cube.rgb * c.rgb, c.a);
			
			//c.xyz *= _MainColor.xyz * 2;
			c.xyz += rim.x * _RimLightColor.xyz;
			c.xyz += _AttackColor.xyz;
			return c;
		}
		
		
		// LOD 200
		struct appdata200
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
		};
		struct v2f200
		{
			float4 pos : POSITION;
			float4 uvs : TEXCOORD0;
		};
		v2f200 vs200(appdata200 i)
		{
			v2f200 o;
			
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs.xy = i.texcoord;
			
			// HACK : normally transformation matrix don't change w element in vector.(1 instance of exception is skewing by set row 4 of matrix). Hence normalize is not necessary.
			float4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1));
			
			float3 viewDir = normalize(viewInLocal.xyz - i.vertex.xyz);
			float time = _Time.w * 2.0f;
			float3 Normal_normalize = normalize(i.normal);
			o.uvs.z = dot(viewDir.xyz, Normal_normalize) + sin(time)* sin(time)*0.4;
			//o.uvs.z = dot(viewDir.xyz, i.normal) + _SinTime.w * _SinTime.w;
			o.uvs.w = 0.0f;
			
			return o;
		}
		fixed4 fs200(v2f200 i) : COLOR
		{
			fixed4 c = tex2D(_MainTex, i.uvs.xy);
			fixed4 rim = tex2D(_RimLightTex, i.uvs.zw);
			
			c.xyz *= _MainColor.xyz * 2;
			c.xyz += rim.x * _RimLightColor.xyz;
		
			c.xyz += _AttackColor.xyz;
			
			return c;
		}
		
		// Transparent
		v2f400 vsSkillTransparent(appdata400 i)
		{
			v2f400 o;
			
			//float4 Temp_pos = mul(UNITY_MATRIX_MVP, i.vertex);
			float4 worldPos = mul(_Object2World, i.vertex);
			
			if (worldPos.y > 0.0f)
				o.uvs.w = (worldPos.y-0.0f) *0.6f;
			else
				o.uvs.w = 0.0f;
			
			if (o.uvs.w > 1.0f)
				o.uvs.w = 1.0f;
				
			
			i.vertex.xyz *= _SkillScale;
			
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs.xy = i.texcoord;
			
			float4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1.0f));
			
			float3 viewDir = normalize(viewInLocal.xyz / viewInLocal.w - i.vertex.xyz);
			float3 Normal_normalize = normalize(i.normal);
			o.uvs.z = dot(viewDir.xyz, Normal_normalize);
			if (o.uvs.z < 0.0f)
				o.uvs.z = 0.0f - o.uvs.z;
				
			o.uvs.z = 1.0f - o.uvs.z;	
			
			o.cubenormal = float4(1.0f,1.0f,1.0f,1.0f);
 
			return o;
		}
		float4 fsSkillTransparent(v2f400 i) : COLOR
		{
			if (i.uvs.w > 0.1f)
				i.uvs.w = 1.0f;
			else
				i.uvs.w = 0.0f;

			float4 c = float4(2.0f * tex2D(_MainTex, i.uvs.xy).rgb, _SkillTransparent * i.uvs.z * i.uvs.w);
			
			c += float4(1.0f,1.0f,1.0f,0.0f) * i.uvs.z * 0.5f;
			
			return c;
		}
	ENDCG
	SubShader {
		Tags {"Queue"="Transparent"}
		LOD 400
		Pass {
			Cull Back
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

			CGPROGRAM
			#pragma vertex vsSkillTransparent
			#pragma fragment fsSkillTransparent
		
			ENDCG
		}
		
		Tags { "Queue"="Geometry"}
		Pass {
			
			Blend Off
			Cull Back
			
			CGPROGRAM
			#pragma vertex vs400
			#pragma fragment fs400
		
			ENDCG
		}
	}
	SubShader {
		Tags {"Queue"="Transparent"}
		LOD 200
		Pass {
			Cull Back
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite On
			
			CGPROGRAM
			#pragma vertex vsSkillTransparent
			#pragma fragment fsSkillTransparent
		
			ENDCG
		}
		Tags { "Queue"="Geometry"}
		Pass {
			
			Blend Off
			Cull Back
			
			CGPROGRAM
			#pragma vertex vs200
			#pragma fragment fs200
		
			ENDCG
		}
	}
	SubShader {
		Tags { "Queue"="Geometry" }
		LOD 100
		Pass {
			Lighting Off
			SetTexture [_MainTex]
			{
				constantcolor [_MainColor]
				combine texture * constant DOUBLE
			}
			SetTexture [_MainTex] 
            { 
            	constantcolor [_AttackColor]
               	combine previous + constant
            }
		}
	}
	FallBack "Mobile/Diffuse"
}
