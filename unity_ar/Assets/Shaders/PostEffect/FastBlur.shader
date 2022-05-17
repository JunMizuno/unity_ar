Shader "Unlit/FastBlur"     // @memo.mizuno CameraFilter.cs��OnRenderImage�ɃX�N���v�g���̏������������Ă��܂��B
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

            // �ڂ������߂��̂܂܁B
            /*
            static const int BLUR_SAMPLE_COUNT = 4;
            static const float2 BLUR_KERNEL[BLUR_SAMPLE_COUNT] = {
                float2(-1.0, -1.0),
                float2(-1.0, 1.0),
                float2(1.0, -1.0),
                float2(1.0, 1.0),
            };
            */

            // �ڂ������߂ŃA�[�e�B�t�@�N�g���C�ɂȂ�ꍇ�͂�����Ƃ̂��ƁB
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
                // �𑜓x���قȂ��Ă������������ɂȂ�悤�ɁB
                float2 scale = _BlurSize / 1000;
                scale.y *= _MainTex_TexelSize.y / _MainTex_TexelSize.x;

                fixed4 col = 0;
                for (int j = 0; j < BLUR_SAMPLE_COUNT; j++) {
                    col += tex2D(_MainTex, i.uv + BLUR_KERNEL[j] * scale);
                }

                // �T���v�����O���Ŋ�����1�F���𔽉f�����Ă���B
                col.rgb /= BLUR_SAMPLE_COUNT;
                col.a = 1;

                return col;
            }

            ENDCG
        }
    }

    Fallback "Diffuse"
}
