Shader "Custom/Transition"
{
    Properties
    {
        [HideInInspector]
        _MainTex ("Texture", 2D) = "white" 
        _TransitionTex ("Transition Texture", 2D) = "white" {}
        _CutoffValue ("Cutoff Value", range(0, 1)) = 0
        _FadeValue ("Fade Value", range(0, 1)) = 0
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
            sampler2D _TransitionTex;
            float4 _TransitionTex_ST;

            float _CutoffValue;
            float _FadeValue;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = TRANSFORM_TEX(v.uv, _TransitionTex);

                #if UNITY_UV_STARTS_AT_TOP
                if (_MainTex_TexelSize.y < 0)
                    o.uv.y = 1 - o.uv.y;
                #endif

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 transition = tex2D(_TransitionTex, i.uv);

                if (transition.x < _CutoffValue)
                    return _Color;

                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
