Shader "Unlit/GaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", Int) = 3
        _Sigma ("Sigma", Float) = 1.0
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

                return col;
            }
            ENDCG
        }
    }
}
