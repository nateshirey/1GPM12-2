Shader "Custom/Nate_Cel_Shade"
{
    Properties
    {
        _MainTex ("Color", 2D) = "white" {}
        _AlphaTint ("Alpha Tint", Color) = (1, 1, 1, 1)
        _UseAlpha ("Use Alpha", range(0,1)) = 0
        [NoScaleOffset] _Ramp ("Color Ramp", 2D) = "white" {}
        _ColorRamp1 ("Ramp Color 1", Color) = (.9, .9, .9, 1)
        _ColorRamp2 ("Ramp Color 2", Color) = (.2, .2, .2, 1)
        _SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
        _Gloss ("Gloss", range(0, 1)) = 0
        _Gradient ("Gradient", range(0, 1)) = 0
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "LightMode" = "ForwardBase" }

        Pass
        {
            CGPROGRAM

            #pragma multi_compile_fwdbase
            #pragma multi_compile _ SHADOWS_SCREEN

            #pragma vertex vert
            #pragma fragment frag

            #pragma shader_feature _COLOR_RAMP

            #include "Cel_Shade_Lighting.cginc"

            ENDCG
        }


        Pass{
            Tags{
                "LightMode" = "ForwardAdd"
            }

            Blend OneMinusDstColor One
            ZWrite Off

            CGPROGRAM

            #pragma target 3.0

            #pragma multi_compile_fwdadd_fullshadows

            #pragma vertex vert
            #pragma fragment frag

            #include "Cel_Shade_Lighting.cginc"

            ENDCG
            
        }

        Pass{
            Tags{
                "LightMode" = "ShadowCaster"
            }

            CGPROGRAM

            #pragma target 3.0

            #pragma multi_compile_shadowcaster

            #pragma vertex shadowvert
            #pragma fragment shadowfrag

            #include "Cel-Shadows.cginc"

            ENDCG
            
        }

    }
    CustomEditor "CelShader_GUI"
}
