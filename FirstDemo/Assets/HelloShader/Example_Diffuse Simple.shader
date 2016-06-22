Shader "Example/Diffuse Simple" 
{
    SubShader 
    {
      Tags { "RenderType" = "Opaque" }

      CGPROGRAM
      #pragma surface surf Lambert
      struct Input 
      {
          float4 color : COLOR;
      };

      void surf (Input IN, inout SurfaceOutput o) 
      {
          o.Albedo = 1;
//			o.Albedo = (1,0.5,0.7,0.5);
      }
      ENDCG
    }
    Fallback "Diffuse"
}