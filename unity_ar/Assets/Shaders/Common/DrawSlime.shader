Shader "Unlit/DrawSlime"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags 
        {
            // 透過を可能にするために
            "Queue"="Transparent" 
            "RenderType"="Opaque" 
        }

        LOD 100

        Pass
        {
            // 深度を書き込むために
            ZWrite On

            // 透過を可能にするために
            Blend SrcAlpha OneMinusSrcAlpha

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
                float4 pos : POSITION1;
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // 出力データ用の構造体
            struct output
            {
                // ピクセル色
                float4 col: SV_Target;
                // 深度
                float depth : SV_Depth;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // 入力 -> v2f
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // ローカル座標をワールド座標に変換
                o.pos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            // 球の距離関数
            float4 sphereDistanceFunction(float4 sphere, float3 pos)
            {
                return length(sphere.xyz - pos) - sphere.w;
            }

            // 深度計算
            inline float getDepth(float3 pos)
            {
                const float4 vpPos = mul(UNITY_MATRIX_VP, float4(pos, 1.0));
                
                float z = vpPos.z / vpPos.w;
                #if defined(SHADER_API_GLCORE) || \
                    defined(SHADER_API_OPENGL) || \
                    defined(SHADER_API_GLES) || \
                    defined(SHADER_API_GLES3)
                return z * 0.5 + 0.5;
                #else
                return z;
                #endif
            }

            // 最大の球の個数
            #define MAX_SPHERE_COUNT 256
            // 球の座標・半径を格納した配列
            float4 _Spheres[MAX_SPHERE_COUNT];
            // 処理する球の個数
            int _SphereCount;

            // smoothMin関数
            float smoothMin(float x1, float x2, float k)
            {
                return -log(exp(-k * x1) + exp(-k * x2)) / k;
            }

            // いずれかの球との最短距離を返す
            float getDistance(float3 pos)
            {
                float dist = 100000;
                for (int i = 0; i < _SphereCount; i++)
                {
                    dist = smoothMin(dist, sphereDistanceFunction(_Spheres[i], pos), 3);
                }
                return dist;
            }

            // v2f -> 出力
            output frag (const v2f i) : SV_Target
            {
                output o;

                // レイの座標(ピクセルのワールド座標で初期化)
                float3 pos = i.pos.xyz;
                // レイの進行方向(レイマーチング用)
                const float3 rayDir = normalize(pos.xyz - _WorldSpaceCameraPos);
                                
                // ここでは30ターンチェックする設定(何回レイを進行させるか)
                for (int i = 0; i < 40; i++)
                {
                    // posといずれかの球との最短距離
                    float dist = getDistance(pos);
                     
                    // 距離が0.001以下になったら、色と深度を書き込んで処理終了
                    if (dist < 0.01)
                    {
                        // 塗りつぶし
                        o.col = fixed4(0, 1, 0, 0.5);
                        // 深度書き込み
                        o.depth = getDepth(pos);
                        return o;
                    }
 
                    // レイの方向に行進
                    pos += dist * rayDir;
                }
 
                // 衝突判定がなかったら透明にする
                o.col = 0;
                o.depth = 0;
                
                return o;
            }

            ENDCG
        }
    }
}
