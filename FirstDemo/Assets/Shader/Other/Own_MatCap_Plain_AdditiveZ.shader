// MatCap Shader, (c) 2013,2014 Jean Moreno

Shader "Own Mobile/MatCap/Plain Additive Z"
{
	Properties
	{
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MatCap ("MatCap (RGB)", 2D) = "white" {}
	}
	
	Subshader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Lighting Off
		
	    Pass
	    {
			ColorMask 0
	    }
		
		Pass
		{
			ZWrite Off
			Ztest Lequal
			Cull Off			
			Blend One OneMinusSrcAlpha
			ColorMask RGB
			Lighting Off
			Tags { "LightMode" = "Always" }
			
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				
				struct v2f
				{
					float4 pos	: SV_POSITION;
					float2 cap	: TEXCOORD0;
				};
				
				v2f vert (appdata_base v)
				{
					v2f o;
					o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
					
					half2 capCoord;
					capCoord.x = dot(UNITY_MATRIX_IT_MV[0].xyz,normalize(v.normal));
					capCoord.y = dot(UNITY_MATRIX_IT_MV[1].xyz,normalize(v.normal));
					o.cap = capCoord * 0.5 + 0.5;
					
					return o;
				}
				
				uniform float4 _Color;
				uniform sampler2D _MatCap;
				
				float4 frag (v2f i) : COLOR
				{
					float4 mc = tex2D(_MatCap, i.cap);
					
					return _Color * mc * 2.0;
				}
			ENDCG
		}
	}
	
	Fallback "VertexLit"
}