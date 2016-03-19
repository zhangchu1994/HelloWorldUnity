Shader "Own Mobile/Item/Logo Specular" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_Parallax ("Height", Range (0.005, 0.08)) = 0.02
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_BumpMap ("Normalmap", 2D) = "bump" {}
	_ParallaxMap ("Heightmap (A)", 2D) = "black" {}
	_ShadeRange("Shading Range",Float) = 0.02
	_ShadingStrength("Shading Strength",Range(0,1)) = 1
}
SubShader { 
	Tags { "RenderType"="Transparent"  "Queue"="Transparent+200" }
	
	Blend SrcAlpha OneMinusSrcAlpha
	Cull back
	ZWrite Off
	ZTest Always
			
	//LOD 600
		
	CGPROGRAM
	#define SHADOW_STEPS 7

	#pragma surface surf BlinnPhongShifted fullforwardshadows addshadow
	#pragma target 3.0

	sampler2D _MainTex;
	sampler2D _BumpMap;
	sampler2D _ParallaxMap;
	fixed4 _Color;
	half _Shininess;
	float _Parallax;
	float _ShadingStrength;
	float _ShadeRange;

	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
		float3 viewDir;
	};

	struct SurfaceOutputPS {
		fixed3 Albedo;
		fixed3 Normal;
		fixed3 Emission;
		half Specular;
		fixed Gloss;
		fixed Alpha;
		float2 ShadeMapUV;
	};


	inline float CheckShading(sampler2D heightTex,float2 uv,float3 lightDir)
	{
		float hn = tex2D (heightTex, uv).a;
		float ret = 1.0f;
		float2 off = lightDir.xy/SHADOW_STEPS;
		float hld = 0.0f;
		half tx = 0.0f;
		for(int i = 1;i<=SHADOW_STEPS;i++)
		{
			uv-= off;
			hld = tex2D (heightTex,uv).a;
			if(hld+lightDir.z>hn)
			{
				tx = 1.0f-(hld-hn);
				if(ret>tx)
				{
					ret = tx;
				}
			}
		}
		return (ret);
	}

	inline fixed4 LightingBlinnPhongShifted (SurfaceOutputPS s, fixed3 lightDir, fixed3 viewDir, fixed atten)
	{
		float3 xdir = -lightDir*(_ShadeRange+_Parallax);
		float mixVal = (_ShadingStrength)+(1.0f-_ShadingStrength)*CheckShading(_ParallaxMap,s.ShadeMapUV,xdir);
		
		fixed3 h = normalize (lightDir+viewDir);
		float3 normal = s.Normal;
		normal.z *= mixVal;
		fixed diff = mixVal*max (0.0f, dot (normal, lightDir));
		
		float nh = max (0.0f, dot (normal, h));
		float spec = pow (nh, s.Specular*128.0f) * s.Gloss;
		
		fixed4 c;
		
		c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * _SpecColor.rgb * spec) * (atten * 2.0f);
		c.a = s.Alpha + _LightColor0.a * _SpecColor.a * spec * atten;
		return c;
	}


	void surf (Input IN, inout SurfaceOutputPS o) {
		o.ShadeMapUV = IN.uv_BumpMap;
		fixed4 h = tex2D (_ParallaxMap, IN.uv_BumpMap);
		float2 offset = ParallaxOffset (h.w, _Parallax, IN.viewDir);
		IN.uv_MainTex += offset;
		IN.uv_BumpMap += offset;
		
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = tex.rgb * _Color.rgb;
		o.Gloss = h.a;
		if (tex.a > 0.1f)
			o.Alpha = _Color.a;
		else
			o.Alpha = tex.a * _Color.a;
			
		o.Specular = _Shininess / h.a;
		
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		
		o.Emission = tex.rgb;
		
	}
	ENDCG
	}

FallBack "Own Mobile/Item/Logo"
}
