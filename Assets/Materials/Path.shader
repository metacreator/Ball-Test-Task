Shader "Custom/PathFadeURP"
{
    Properties
    {
        _Color("Base Color", Color) = (1, 0, 0, 0.5)
        _FadeStart("Fade Start (0-1)", Range(0,1)) = 0.0
        _FadeEnd("Fade End (0-1)", Range(0,1)) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;
            float _FadeStart;
            float _FadeEnd;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float uvAlong = i.uv.x;
                float t = saturate((uvAlong - _FadeStart) / (_FadeEnd - _FadeStart));
                float alpha = _Color.a * (1.0 - t);

                return half4(_Color.rgb, alpha);
            }
            ENDHLSL
        }
    }
}