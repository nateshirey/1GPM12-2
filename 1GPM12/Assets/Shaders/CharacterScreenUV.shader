
Shader "Custom/CharacterScreenUV"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent+500"}

        ZWrite Off

        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;

                float4 worldOrigin = mul(UNITY_MATRIX_M, float4(0, 0, 0, 1));
                float4 viewOrigin = float4(UnityObjectToViewPos(float3(0, 0, 0)), 1);

                float4 worldPos = mul(UNITY_MATRIX_M, v.vertex);
                float4 flipWorldPos = float4(-1, 1, -1, 0) * (worldPos - worldOrigin) + worldOrigin;
                float4 viewPos = flipWorldPos - worldOrigin + viewOrigin;
                float4 clipPos = mul(UNITY_MATRIX_P, viewPos);

                o.pos = clipPos;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				//fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col = fixed4(i.uv.xy, 1, 1);
                return col;
            }
            ENDCG
        }
    }
}
