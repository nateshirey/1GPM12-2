#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"

#define USE_LIGHTING

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 pos : SV_POSITION;
    float3 normal : TEXCOORD1;
    float3 worldPos : TEXCOORD2;
    SHADOW_COORDS(5)
};

sampler2D _MainTex;
float4 _AlphaTint;
float _UseAlpha;
float4 _MainTex_ST;
sampler2D _Ramp;
float4 _ColorRamp1;
float4 _ColorRamp2;
float4 _SpecularColor;
float _Gloss;
float _Gradient;

v2f vert (appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex) * _MainTex_ST.xy + _MainTex_ST.zw;
    o.normal = UnityObjectToWorldNormal(v.normal);
    o.worldPos = mul(unity_ObjectToWorld, v.vertex);

    TRANSFER_SHADOW(o);

    return o;
}

float3 Lighting(v2f i){
    #if defined(POINT) || defined (POINTCOOKIE) || defined(SPOT)
        float3 lightDir = normalize(_WorldSpaceLightPos0.xyz - i.worldPos);
    #else
        float3 lightDir = _WorldSpaceLightPos0.xyz;
    #endif

    float ndotl = saturate(dot(i.normal, lightDir));
    float3 diffuseLight = float3(0.1, 0.1, 1);
    #if defined(_COLOR_RAMP)
        diffuseLight = tex2D(_Ramp, float2(ndotl, 0)).rgb;
    #else
        float threshold = round(ndotl);
        diffuseLight = lerp(_ColorRamp2, _ColorRamp1, threshold);
    #endif

    diffuseLight *= _LightColor0.xyz;

    float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
    float3 halfAngle = normalize(lightDir + viewDir);


    UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos);

    float specularity = saturate(dot(halfAngle, i.normal)) * (diffuseLight > 0);
    float specExp = exp2(_Gloss * 11) + 1;
    float glossiness = pow(specularity, specExp);
    glossiness = saturate(round(specularity * glossiness)) * _Gloss;
    glossiness = saturate(glossiness);
    glossiness *= _SpecularColor.xyz;

    #if defined(POINTCOOKIE)
        attenuation = round(attenuation * 2);
        return float3 (1, 0, 0);
    #endif

    return lerp(diffuseLight, _SpecularColor, glossiness) * attenuation;
}

float4 frag (v2f i) : SV_Target
{
    i.normal = normalize(i.normal);
    float4 col = tex2D(_MainTex, i.uv);
    float aTint = _UseAlpha * col.w;
    col = lerp(col, _AlphaTint, aTint);
    float4 lighting = float4(Lighting(i), 1);
    return col * lighting;
}