Shader "Own Mobile/Texture/ScreenNew" {
 Properties {
  _MainTex ("Base (RGB)", 2D) ="white" {}
  _NormalTex("Normals (2D)", 2D) = "white" {}
  _Strength("Distort strength", Float) = 0.1
  _Speed("Distort speed", Float) = 0.1
  _EdgeFade("Edge fade (0-1)", Float) = 0.1
 }
 SubShader {
  //在所有不透明对象之后绘制自己，更加靠近屏幕
  Tags{"Queue"="Transparent"}
  
  Cull Off
  Blend One Zero

  //通道1：捕捉屏幕内容放到_GrabTexture纹理中
  //GrabPass{} 
  //通道2：设置材质
  Pass{
   CGPROGRAM
   #pragma vertex vert
           #pragma fragment frag
   #include "UnityCG.cginc"

   sampler2D _MainTex : register(s0);
			sampler2D _NormalTex : register(s1);
			
			float _Strength;
			float _Speed;
			float _EdgeFade;
	
			struct v2f {
				float4 pos: SV_POSITION;
				float4 screenPos: TEXCOORD0;
				float2 uvbump: TEXCOORD1;
			};
			
			v2f vert(appdata_full v) {
			
				v2f o;
				
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeGrabScreenPos(o.pos);
				
				o.uvbump.xy = v.texcoord.xy;
				
				return o;
			}
			
			
			float4 frag(v2f i): COLOR {
				
				float2 uv1 = i.uvbump + _Time.x * _Speed;
				
				float2 edgeFactor = 1.0f;
				
				float fadeStart = 1.0f - _EdgeFade;
				float fadeEnd = 1.0f;
				
				
				edgeFactor.x = lerp(fadeStart, fadeEnd, i.uvbump.x) * lerp(fadeStart, fadeEnd, saturate(1.0f-i.uvbump.x));
				edgeFactor.y = lerp(fadeStart, fadeEnd, i.uvbump.y) * lerp(fadeStart, fadeEnd, saturate(1.0f-i.uvbump.y));
				
				float2 localNormal = float2(tex2D(_NormalTex, uv1).r,tex2D(_NormalTex, uv1).w) * 2.0f - 1.0f;
				
				float4 uv = i.screenPos + float4(localNormal * (edgeFactor.y * edgeFactor.x)*4.0f, 0.0f, 0.0f) * _Strength;
				
				//float4 finalColor = tex2Dproj(_MainTex, UNITY_PROJ_COORD(uv));
				float4 finalColor = tex2D(_MainTex, uv.xy/i.screenPos.w);
				
				return finalColor;
			}
   ENDCG
  }
 }
 FallBack "Diffuse"
}