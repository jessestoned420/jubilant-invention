Shader "Milehigh/HyperPBRCharacter_4D"
{
    Properties
    {
        // --- Core PBR Properties ---
        _Color ("Albedo Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white" {}
        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        _BumpMap ("Normal Map", 2D) = "bump" {}
        _BumpScale ("Normal Intensity", Float) = 1.0

        // RMAI Map: Roughness (R), Metallic (G), Ambient Occlusion (B), Iridescence Mask (A)
        _RMAIMap ("RMAI (Roughness, Metallic, AO, Iridescence)", 2D) = "white" {}
        _Metallic ("Metallic", Range(0.0, 1.0)) = 0.0
        _Glossiness ("Smoothness", Range(0.0, 1.0)) = 0.5
        _Ao ("Ambient Occlusion", Range(0.0, 1.0)) = 1.0

        // --- Parallax Occlusion Mapping (3D Depth) ---
        _ParallaxMap ("Height Map (A)", 2D) = "gray" {}
        _Parallax ("Height Scale", Range (0.005, 0.08)) = 0.02

        // --- Subsurface Scattering (for Skin) ---
        _SSSColor ("Subsurface Color", Color) = (0.7, 0.1, 0.1, 1)
        _SSSMask ("Subsurface Mask (R)", 2D) = "white" {}
        _SSSScale ("SSS Scale", Range(0, 5)) = 1.0

        // --- 4D Procedural Effects (e.g., Void Corruption) ---
        _NoiseTex ("3D Noise Texture", 3D) = "gray" {}
        _EffectTime ("Effect Time (For 4D Noise)", Float) = 0.0
        _EffectColor ("Procedural Effect Color", Color) = (0.5, 0, 1, 1)
        _EffectMask ("Effect Mask (R)", 2D) = "black" {}
        _EffectScale ("Noise Scale", Float) = 10.0
        _EffectSpeed ("Noise Speed", Float) = 0.5

        // --- Iridescence / Thin-Film ---
        _IridescenceThickness ("Iridescence Thickness", Range(100, 1000)) = 400.0 // in nm
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
        LOD 400

        CGPROGRAM
        #pragma surface surf StandardSpecular fullforwardshadows vertex:vert
        #pragma target 4.0

        // Include a 4D Simplex Noise function library
        // (For brevity, assume a full noise library like "SimplexNoise4D.cginc" is included here)
        // float snoise(float4 p); // Function signature

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldPos;
            float3 T; // Tangent
            float3 B; // Bitangent
            float3 N; // Normal
        };

        sampler2D _MainTex, _BumpMap, _RMAIMap, _SSSMask, _EffectMask, _ParallaxMap;
        sampler3D _NoiseTex;
        fixed4 _Color, _EffectColor, _SSSColor;
        half _BumpScale, _Metallic, _Glossiness, _Ao, _Cutoff, _Parallax;
        half _SSSScale, _EffectScale, _EffectSpeed, _IridescenceThickness;

        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.T = UnityObjectToWorldDir(v.tangent.xyz);
            o.B = cross(o.N, o.T) * v.tangent.w; // Bitangent
            o.N = UnityObjectToWorldNormal(v.normal);

            // Parallax Occlusion Mapping
            half h = tex2Dlod(_ParallaxMap, float4(v.texcoord.xy, 0, 0)).a;
            float2 offset = ParallaxOffset(h, _Parallax, v.viewDir);
            v.texcoord.xy += offset;
        }

        // Thin-film interference approximation for iridescence
        float3 Iridescence(float thickness, float NdotV)
        {
            float3 interference = sin(2.0 * 3.14159 * thickness * float3(1.0, 0.8, 0.6) / NdotV) * 0.5 + 0.5;
            return pow(interference, 2.0);
        }

        void surf (Input IN, inout SurfaceOutputStandardSpecular o)
        {
            fixed4 albedo = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            clip(albedo.a - _Cutoff);

            // --- PBR Properties ---
            fixed4 rmai = tex2D(_RMAIMap, IN.uv_MainTex);
            o.Smoothness = rmai.r * _Glossiness;
            o.Specular = _Metallic; // Using StandardSpecular for better control
            o.Occlusion = lerp(1, rmai.b, _Ao);

            // --- Normal Mapping ---
            o.Normal = UnpackScaleNormal(tex2D(_BumpMap, IN.uv_MainTex), _BumpScale);

            // --- 4D Procedural Effect ---
            half effectMask = tex2D(_EffectMask, IN.uv_MainTex).r;
            if (effectMask > 0)
            {
                // Sample 3D noise texture with a time component for 4D effect
                float4 noiseCoord = float4(IN.worldPos * _EffectScale, _Time.y * _EffectSpeed);
                // In a real implementation, you'd use a 4D noise function here.
                // We simulate it by sampling a 3D texture and evolving it.
                half noise = tex3D(_NoiseTex, noiseCoord.xyz + noiseCoord.w).r;

                // Add emissive glow based on noise
                o.Emission = _EffectColor.rgb * pow(noise, 3.0) * effectMask * 2.0;
            }

            // --- Iridescence ---
            half iridescenceMask = rmai.a;
            if (iridescenceMask > 0)
            {
                float NdotV = saturate(dot(o.Normal, IN.viewDir));
                float3 iridescence = Iridescence(_IridescenceThickness, NdotV);
                // Blend with albedo and apply as specular tint
                o.Specular = lerp(o.Specular, o.Specular * iridescence, iridescenceMask);
            }

            o.Albedo = albedo.rgb;

            // --- Subsurface Scattering ---
            half sssMask = tex2D(_SSSMask, IN.uv_MainTex).r;
            if (sssMask > 0)
            {
                // A more advanced SSS would use a proper lighting model.
                // This is a stylistic approximation.
                half NdotL = dot(o.Normal, _WorldSpaceLightPos0.xyz);
                half sss = pow(saturate(dot(IN.viewDir, -_WorldSpaceLightPos0.xyz)), 8.0) * _SSSScale;
                o.Albedo += _SSSColor.rgb * sss * NdotL * sssMask;
            }
        }
        ENDCG
    }
    FallBack "Transparent/Cutout/VertexLit"
}
