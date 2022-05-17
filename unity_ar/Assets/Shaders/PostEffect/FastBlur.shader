Shader "Unlit/FastBlur"     // @memo.mizuno CameraFilter.csのOnRenderImageにスクリプト側の処理を実装しています。
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            ZTest Always Cull Off ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurSize;

            // ぼかし強めそのまま。
            /*
            static const int BLUR_SAMPLE_COUNT = 4;
            static const float2 BLUR_KERNEL[BLUR_SAMPLE_COUNT] = {
                float2(-1.0, -1.0),
                float2(-1.0, 1.0),
                float2(1.0, -1.0),
                float2(1.0, 1.0),
            };
            */

            // ぼかし強めでアーティファクトが気になる場合はこちらとのこと。
            static const int BLUR_SAMPLE_COUNT = 8;
            static const float2 BLUR_KERNEL[BLUR_SAMPLE_COUNT] = {
                float2(-1.0, -1.0),
                float2(-1.0, 1.0),
                float2(1.0, -1.0),
                float2(1.0, 1.0),
                float2(-0.70711, 0),
                float2(0, 0.70711),
                float2(0.70711, 0),
                float2(0, -0.70711),
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // 解像度が異なっても同じ見え方になるように。
                float2 scale = _BlurSize / 1000;
                scale.y *= _MainTex_TexelSize.y / _MainTex_TexelSize.x;

                fixed4 col = 0;
                for (int j = 0; j < BLUR_SAMPLE_COUNT; j++) {
                    col += tex2D(_MainTex, i.uv + BLUR_KERNEL[j] * scale);
                }

                // サンプリング数で割って1色分を反映させている。
                col.rgb /= BLUR_SAMPLE_COUNT;
                col.a = 1;

                return col;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
