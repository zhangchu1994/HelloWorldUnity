Shader "ew2/rimlight&envmap(solid)" {
Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_RimLightTex("Rim Light Texture", 2D) = "black" {}
		_EnvTex("Environment map", CUBE) = "" {}
		_AttackColor("Attack Color", Color) = (0,0,0,0)
		_MainColor("Main Color", Color) = (0.5, 0.5, 0.5, 1)
	}
	CGINCLUDE
	
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		sampler2D _RimLightTex;
		samplerCUBE _EnvTex;
		float4 _AttackColor;
		float4 _MainColor;
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
			float4 reflect : TEXCOORD1;
		};
		v2f400 vs400(appdata400 i)
		{
			v2f400 o;
			
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs.xy = i.texcoord;	
			// HACK : normally transformation matrix don't change w element in vector.(1 instance of exception is skewing by set row 4 of matrix). Hence normalize is not necessary.
			float4 posInWorld = mul(_Object2World, i.vertex);
			float3 normalInWorld = mul((float3x3)_Object2World, i.normal);
			o.reflect.xyz = normalInWorld;
			o.uvs.zw = posInWorld.xy;
			o.reflect.w = posInWorld.z;
			return o;
		}
		float4 fs400(v2f400 i) : COLOR
		{
			float4 c = tex2D(_MainTex, i.uvs.xy);
			
			float3 viewDir = normalize(_WorldSpaceCameraPos - float3(i.uvs.zw, i.reflect.w));
			float3 normal = i.reflect.xyz;
			float3 ref = reflect(viewDir, normal);
			float4 env = texCUBE(_EnvTex, ref);
			float d = max(0, dot(viewDir, normal));
			c.xyz *= env.xyz * (c.a * 2 * d) + 1;
			c.xyz *= _MainColor.xyz * 2;
			
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
			float4 reflect : TEXCOORD1;
		};
		v2f200 vs200(appdata200 i)
		{
			v2f200 o;
			
			o.pos = mul(UNITY_MATRIX_MVP, i.vertex);
			o.uvs.xy = i.texcoord;
			
			// HACK : normally transformation matrix don't change w element in vector.(1 instance of exception is skewing by set row 4 of matrix). Hence normalize is not necessary.
			float4 posInWorld = mul(_Object2World, i.vertex);
			float3 normalInWorld = mul((float3x3)_Object2World, i.normal);
			
			o.reflect.xyz = normalInWorld;
			o.uvs.zw = posInWorld.xy;
			o.reflect.w = posInWorld.z;
			return o;
		}
		fixed4 fs200(v2f200 i) : COLOR
		{
			fixed4 c = tex2D(_MainTex, i.uvs.xy);
			
			float3 viewDir = normalize(_WorldSpaceCameraPos - float3(i.uvs.zw, i.reflect.w));
			float3 normal = i.reflect.xyz;
			float3 ref = reflect(viewDir, normal);
			fixed4 env = texCUBE(_EnvTex, ref);
			c.xyz *= env.xyz * (c.a * 2) + 1;
			c.xyz *= _MainColor.xyz * 2;
			
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
               	combine texture + constant
            }
		}
	}
	FallBack "Mobile/Diffuse"
}