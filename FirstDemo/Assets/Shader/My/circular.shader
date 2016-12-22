Shader "Ian/circular" {  
    Properties {  
        _MainTex ("Base (RGB)", 2D) = "white" {}  
        _banjin ("Rodius",Range(0.01,0.25)) = 0.25  
    }  
    SubShader {  
        Tags { "RenderType"="Opaque" "Queue"="Transparent"}  
        LOD 200  
          
        CGPROGRAM  
        #pragma surface surf Lambert alpha  
  
        sampler2D _MainTex;  
        float _banjin;  
  
        struct Input {  
            float2 uv_MainTex;  
        };  
  
        void surf (Input IN, inout SurfaceOutput o) {  
            float2 uv_XY = IN.uv_MainTex;  
            float2 middle = float2(0.5,0.5);  
            float2 cha = uv_XY - middle;  
            float value = cha.x * cha.x  + cha.y * cha.y /3.2f/3.2f;  
  			//value = value  / 1.8f;
            half4 c = tex2D (_MainTex, uv_XY);  
              
            if(value > 0.01*0.01){  
                o.Albedo = float3(1,1,1);  
                o.Alpha = 0;  
            }else{  
                o.Albedo = c.rgb;  
                o.Alpha = c.a;  
            }  
              
        }  
        ENDCG  
    }   
    FallBack "Diffuse"  
}  