Shader "Unlit/Transparent Colored TutorialMask"
{
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
		_Top ("Normalized top y", Range(0.0, 1.0)) = 0.0
		_Bottom ("Normalized bottom y", Range(0.0, 1.0)) = 0.0
		_Left ("Normalized left x", Range(0.0, 1.0)) = 0.0
		_Right ("Normalized right x", Range(0.0, 1.0)) = 0.0
		_Ratio ("W/H ratio", Range(0.0, 10.0)) = 0.0
		_IsRect ("is rect", Range(0.0, 1.0)) = 1.0
	}
	
	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Pass
		{
			Cull Back
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Left, _Right, _Top, _Bottom, _Ratio, _IsRect;
			float _w, _h, _centerX, _centerY, _radius, _glowRadius;
			
			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
				half2 center: TEXCOORD1;
				half2 radius: TEXCOORD2;
				fixed4 color : COLOR;
			};
	
			
			
			v2f vert (appdata_t v)
			{
				v2f o;	
				_w = _Right - _Left;
				_h = _Top - _Bottom;
				_centerX = _Left + _w / 2.0f;
				_centerY = _Bottom + _h / 2.0f;
				//float isWide = step(_h, _w);
				//_radius = isWide * (_w / 2.0f) + (1.0f - isWide)*(_h / 2.0f);
				if (_w > _h) {
					_radius = _w / 2.0f;
				}
				else {
					_radius = _h / 2.0f;
				}
				_glowRadius = 0.03f;
				
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color;
				o.center = half2(_centerX, _centerY);
				o.radius = half2(_radius, _glowRadius);
				return o;
			}
				
			fixed4 frag (v2f IN) : COLOR
			{
				fixed4 col = tex2D(_MainTex, IN.texcoord) * IN.color;
				
				float dist2 = (IN.texcoord.x - IN.center.x)*(IN.texcoord.x - IN.center.x) + (IN.texcoord.y - IN.center.y)*(IN.texcoord.y - IN.center.y)/_Ratio/_Ratio;
				//float isCircle = step(0.5f, _IsRect) * ()
				if (_IsRect < 0.5f && dist2 < IN.radius.x * IN.radius.x) {

					float dist = sqrt(dist2);
					float alpha = .3f * (dist - IN.radius.x + IN.radius.y) / IN.radius.y;
					if (alpha < 0.0f) {
						alpha = 0.0f;
					}

					col.a = alpha;
				}else if (_IsRect > 0.5f && IN.texcoord.x > _Left && IN.texcoord.x < _Right && IN.texcoord.y > _Bottom && IN.texcoord.y < _Top) {

					col.a = 0.0f;
				}else {
					col.a = .85f;
				}

				return col;
			}
			ENDCG
		}
	}

	SubShader
	{
		LOD 100

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
		
		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Fog { Mode Off }
			Offset -1, -1
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMaterial AmbientAndDiffuse
			
			SetTexture [_MainTex]
			{
				Combine Texture * Primary
			}
		}
	}
}
