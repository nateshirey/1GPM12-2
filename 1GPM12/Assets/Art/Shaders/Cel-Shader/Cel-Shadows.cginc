#if !defined(SHADOWS_INCLUDED)
#define SHADOWS_INCLUDED

#include "UnityCG.cginc"

#if defined(_RENDERING_FADE) || defined(_RENDERING_TRANSPARENT)
    #if defined (_SEMITRANSPARENT_SHADOWS)
        #define SHADOWS_SEMITRANSPARENT 1
    #else
        #define _RENDERING_CUTOUT
    #endif
#endif

#if defined(_RENDERING_CUTOUT) || defined(SHADOWS_SEMITRANSPARENT)
    #define SHADOWS_NEED_UV 1
#endif

float4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
float _Cutoff;
sampler3D _DitherMaskLOD;

struct MeshData{
    float4 position : POSITION;
    float3 normal : NORMAL;
    float2 uv : TEXCOORD0;
};

struct v2f {
    float4 position : SV_POSITION;
    #if defined(SHADOWS_CUBE)
        float3 lightVec : TEXCOORD0;
    #endif
    #if SHADOWS_NEED_UV
        float2 uv : TEXCOORD1;
    #endif
};

struct fragInterpolators {
    #if defined(SHADOWS_SEMITRANSPARENT) || defined(LOD_FADE_CROSSFADE)
        UNITY_VPOS_TYPE vpos : VPOS;
    #else
        float4 positions : SV_POSITION;
    #endif
    #if defined(SHADOWS_CUBE)
        float3 lightVec : TEXCOORD0;
    #endif
    #if SHADOWS_NEED_UV
        float2 uv : TEXCOORD1;
    #endif
};

v2f shadowvert (MeshData v) {
    v2f o;
    #if defined(SHADOWS_CUBE)
        o.position = UnityObjectToClipPos(v.position);
        o.lightVec = mul(unity_ObjectToWorld, v.position).xyz - _LightPositionRange.xyz;
    #else
        o.position = UnityClipSpaceShadowCasterPos(v.position.xyz, v.normal);
        o.position = UnityApplyLinearShadowBias(o.position);
    #endif

    #if SHADOWS_NEED_UV
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    #endif
    return o;
}

float GetAlpha(v2f i){
    float alpha = _Color.a;
    #if SHADOWS_NEED_UV
        alpha *= tex2D(_MainTex, i.uv).a;
    #endif
    return alpha;
}

float4 shadowfrag (fragInterpolators i) : SV_TARGET{

    #if defined(LOD_FADE_CROSSFADE)
        UnityApplyDitherCrossFade(i.vpos);
    #endif

    float alpha = GetAlpha(i);
    #if defined(_RENDERING_CUTOUT)
        clip(alpha - _Cutoff);
    #endif

    #if SHADOWS_SEMITRANSPARENT
        float dither = tex3D(_DitherMaskLOD, float3(i.vpos.xy * .25, alpha * 0.9375)).a;
        clip(dither - 0.01);
    #endif

    #if defined(SHADOWS_CUBE)
        float depth = length(i.lightVec) + unity_LightShadowBias.x;
        depth *= _LightPositionRange.w;
        return UnityEncodeCubeShadowDepth(depth);
    #else
        return 0;
    #endif
}

#endif