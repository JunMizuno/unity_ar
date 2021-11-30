Shader "Unlit/WipeCircle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius("Radius", Range(0, 2)) = 2
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Tags { "RenderType"="Opaque" }
        LOD 100

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // SubShaderにするとdiscardなどで対応できる。
                //i.uv -= fixed2(0.5, 0.5);
                //i.uv.x *= 16.0 / 9.0;
                float2 checkUV = i.uv;
                checkUV.x -= 0.5;
                checkUV.y -= 0.5;
                if (distance(checkUV, fixed2(0, 0)) < _Radius)
                {
                    //discard;
                    return tex2D(_MainTex, i.uv);
                }
                return fixed4(0, 0, 0, 1);
            }

            ENDCG
        }
    }
}
