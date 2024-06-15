Shader "Unlit/Metaball2D"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", Range(0,5)) = 3
        _Sigma ("Sigma", Range(0,5)) = 1.0
        _CutOff ("Alpha CutOff", Range(0,1)) = 0.5
        _Color ("Color", Color) = (1,1,1,1)
        _Stroke ("Stroke Alpha", Range(0,1)) = 0.1
        _StrokeColor ("Stroke Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            int _Radius;
            float _Sigma;
            float4 _Color;
            float _CutOff;
            fixed _Stroke;
            float4 _StrokeColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float4 col = float4(0, 0, 0, 0);
                float2 offset = _MainTex_TexelSize.xy;
                float weightSum = 0.0;
                float sigma2 = _Sigma * _Sigma;
                int radius = _Radius;

                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        float weight = exp(-0.5 * (x*x + y*y) / sigma2) / (2.0 * 3.14159265 * sigma2);
                        weightSum += weight;
                        col += tex2D(_MainTex, uv + offset * float2(x, y)) * weight;
                    }
                }

                col /= weightSum;
                clip(col.a - _CutOff);
                if (col.a < _Stroke) 
                {
                    col = _StrokeColor;
                } 
                else 
                {
                    col *= _Color;
                }

                return col;
            }
            ENDCG
        }
    }
}
