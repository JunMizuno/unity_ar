Shader "Unlit/WithNormalMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _BumpScale ("Normal Scale", Range(0, 1)) = 1.0
        _Shininess ("Shininess", Range(0.0, 1.0)) = 0.078125
        _Color ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags 
        {
            "Queue"="Geometry"
            "RenderType"="Opaque"
        }

        LOD 200

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
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                half3 lightDir : TEXCOORD1;
                half3 viewDir : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _BumpMap;
            half _BumpScale;

            float4 _LightColor0;
            half _Shininess;

            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = v.uv.xy
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // 接空間におけるライト方向のベクトルと視点方向のベクトルを求める。
                TANGENT_SPACE_ROTATION;
                o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex));
                o.viewDir = mul(rotation, ObjSpaceViewDir(v.vertex));

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.lightDir = normalize(i.lightDir);
                i.viewDir = normalize(i.viewDir);
                half3 halfDir = normalize(i.lightDir + i.viewDir);
                
                fixed4 col = tex2D(_MainTex, i.uv);

                // ノーマルマップから法線情報を取得する。
                half3 normal = UnpackNormal(tex2D(_BumpMap, i.uv));

                // ノーマルマップから得た法線情報をつかってライティング計算をする。
                half4 diff = saturate(dot(normal, i.lightDir)) * _LightColor0;
                half3 spec = pow(max(0, dot(normal, halfDir)), _Shininess * 128.0) * _LightColor0.rgb * col.rgb;

                col.rgb  = col.rgb * diff + spec;

                return col;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}
