﻿Shader "Own Mobile/OutLineNew" {
 Properties {
  _MainTex("Texture", 2D) ="white"{}
  _LineSize("OutlineSize",range(0, 0.2)) = 0.02
  _LineColor ("LineColor", Color)= (0,0,0,1)
 }
 SubShader {
  Pass{
   Tags{"LightMode"="Always"}
   Cull Off //关闭剔除，模型前后都会显示
   ZWrite Off //系统默认是开的，要关闭。关闭深度缓存，后渲染的物体会根据ZTest的结果将自己渲染输出写入
   CGPROGRAM
   #pragma vertex vert
   #pragma fragment frag
   #include "UnityCG.cginc"
   float _LineSize;
   float4 _LineColor;
   struct v2f{
    float4 pos:SV_POSITION;
    float4 color : COLOR;
   };
   v2f vert(appdata_full v){
    v2f o;
    //获取模型的最终的投影坐标
    o.pos= mul(UNITY_MATRIX_MVP, v.vertex); 
    //UNITY_MATRIX_IT_MV为【模型坐标-世界坐标-摄像机坐标】【专门针对法线的变换】
    //法线乘以MV，将模型空间 转换 视图空间
    float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
    //转换 视图空间 到 投影空间 【3D转2D】,这里只处理xy.所以上面没有用UNITY_MATRIX_MVP
    float2 offset = TransformViewToProjection(norm.xy);
    //得到的offset，模型被挤的非常大，然后乘以倍率
    o.pos.xy+= offset * _LineSize;
    o.color= _LineColor;
    return o;
   }
   float4 frag(v2f i) : COLOR
   {
    return i.color;
   }
   ENDCG
  }
  Pass{
   //直接使用顶点和片段shader显示物体
   CGPROGRAM
   #pragma vertex vert
   #pragma fragment frag
   #include "UnityCG.cginc"
   sampler2D _MainTex;
   float4 _MainTex_ST;
   
   struct v2f{
    float4 pos:SV_POSITION;
    float2 uv : TEXCOORD0;// 纹理，相对自身的坐标轴,float2是一个平面
   };
   v2f vert(appdata_full v){
    v2f o;
    o.pos= mul(UNITY_MATRIX_MVP, v.vertex); 
    o.uv= TRANSFORM_TEX(v.texcoord, _MainTex);
    return o;
   }
   float4 frag(v2f i) : COLOR
   {
    float4 texCol = tex2D(_MainTex, i.uv);
    return texCol;
   }
   ENDCG
  }
 }
}