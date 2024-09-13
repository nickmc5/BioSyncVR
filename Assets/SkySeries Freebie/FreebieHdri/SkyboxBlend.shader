Shader "Custom/SkyboxBlend" {
    Properties {
        _Blend ("Blend", Range(0, 1)) = 0
        _SkyboxSunny ("Skybox Sunny", Cube) = "" {}
        _SkyboxStormy ("Skybox Stormy", Cube) = "" {}
    }
    SubShader {
        Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
        Cull Off ZWrite Off

        CGPROGRAM
        #pragma surface surf NoLighting noshadow noforwardadd

        samplerCUBE _SkyboxSunny;
        samplerCUBE _SkyboxStormy;
        float _Blend;

        struct Input {
            float3 worldRefl;
        };

        fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
            return fixed4(s.Albedo, s.Alpha);
        }

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 sunny = texCUBE(_SkyboxSunny, IN.worldRefl);
            fixed4 stormy = texCUBE(_SkyboxStormy, IN.worldRefl);
            o.Albedo = lerp(sunny.rgb, stormy.rgb, _Blend);
            o.Alpha = 1.0;
        }
        ENDCG
    }
    Fallback "Skybox/Cubemap"
}