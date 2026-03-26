Shader "Custom/LineBoil"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BoilStrength ("Boil Strength", Range(0, 0.1)) = 0.02
        _BoilSpeed ("Boil Speed", Range(0, 5)) = 2.0
        _NoiseScale ("Noise Scale", Range(1, 20)) = 5.0
        _LineSharpness ("Line Sharpness", Range(0.5, 2)) = 1.0
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
            
            sampler2D _MainTex;
            float _BoilStrength;
            float _BoilSpeed;
            float _NoiseScale;
            float _LineSharpness;
            
            // Simple pseudo-random function
            float rand(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
            }
            
            // Smooth noise function
            float noise(float2 uv)
            {
                float2 i = floor(uv);
                float2 f = frac(uv);
                
                float a = rand(i);
                float b = rand(i + float2(1, 0));
                float c = rand(i + float2(0, 1));
                float d = rand(i + float2(1, 1));
                
                float2 u = f * f * (3.0 - 2.0 * f);
                
                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Create boiling effect using time-based noise
                float time = _Time.y * _BoilSpeed;
                
                // Different offsets for X and Y with different frequencies
                float2 boilUV = i.uv;
                boilUV.x += noise(float2(i.uv.x * _NoiseScale, time)) * _BoilStrength;
                boilUV.y += noise(float2(i.uv.y * _NoiseScale, time * 1.3)) * _BoilStrength;
                
                // Sample texture with distorted UVs
                fixed4 col = tex2D(_MainTex, boilUV);
                
                // Enhance line sharpness
                col.a = pow(col.a, _LineSharpness);
                
                // Apply vertex color tint
                col *= i.color;
                
                return col;
            }
            ENDCG
        }
    }
}