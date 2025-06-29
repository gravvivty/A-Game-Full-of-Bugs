Shader "Unlit/SolidColorShader"
{
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Solid Color", Color) = (0.384, 0.753, 0.659, 1) // Cyan (98/255, 192/255, 168/255, 1)
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 col = _Color;
                col.a *= texColor.a;
                return col;
            }
            ENDCG
        }
    }
}
