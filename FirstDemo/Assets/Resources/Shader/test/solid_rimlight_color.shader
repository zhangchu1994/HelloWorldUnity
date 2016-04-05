Shader "ew2/flash rim light(solid)" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainColor("Main Color", Color) = (0.5,0.5,0.5,1)
		_RimLightTex("Rim Light Texture", 2D) = "black" {}
		_RimLightColor("Rim Light Color", Color) = (1,1,1,1)
		_AttackColor("Attack Color", Color) = (0,0,0,0)
	}
	CGINCLUDE
	
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		sampler2D _RimLightTex;
		float4 _AttackColor;
		float4 _MainColor;
		float4 _RimLightColor;
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
		};
		v2f400 vs400(appdata400 i)
		{
			v2f400 o;
			
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs.xy = i.texcoord;
			
			// HACK : normally transformation matrix don't change w element in vector.(1 instance of exception is skewing by set row 4 of matrix). Hence normalize is not necessary.
			float4 viewInLocal = mul(_World2Object, float4(_WorldSpaceCameraPos, 1));
			
			float3 viewDir = normalize(viewInLocal.xyz - i.vertex.xyz);
			o.uvs.z = dot(viewDir.xyz, i.normal) + _SinTime.w * _SinTime.w;
			o.uvs.w = 0.0f;
			
			return o;
		}
		float4 fs400(v2f400 i) : COLOR
		{
			float4 c = tex2D(_MainTex, i.uvs.xy);
			float4 rim = tex2D(_RimLightTex, i.uvs.zw);
			
			c.xyz *= _MainColor.xyz * 2;
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
			o.uvs.z = dot(viewDir.xyz, i.normal) + _SinTime.w * _SinTime.w;
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
	ENDCG
	SubShader {
		Tags { "Queue"="Geometry" }
		LOD 400
		
		Pass {
			
			Blend Off
			Cull Off
			
			CGPROGRAM
			#pragma vertex vs400
			#pragma fragment fs400
		
			ENDCG
		}
	}
	SubShader {
		Tags { "Queue"="Geometry" }
		LOD 200
		
		Pass {
			
			Blend Off
			Cull Off
			
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
