Shader "Custom/DistortionFlow"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        
        /*
            The Flow map is a vector field, used to create
            a non static looking moving field.
            NoScaleOffset is set because we don't want any
            UV (coordinates) or offset on this vector field.
        */
        
        [NoScaleOffset] _FlowMap ("Flow (RG, A noise)", 2D) = "black" {}
        [NoScaleOffset] _NormalMap ("Normals", 2D) = "bump" {}
        _UJump ("U jump per phase", Range(-0.25, 0.25)) = 0.25
        _VJump ("V jump per phase", Range(-0.25, 0.25)) = 0.25
        _Tiling ("Tiling", Float) = 1
        _Speed ("Speed", Float) = 1
        _FlowStrength ("Flow Strength", Float) = 1
        _FlowOffset ("Flow Offset", Float) = 0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Emission ("Intensity", Range(0,3)) = 1
    }
    
    
    //Everything in here will be included in CGPROGRAM

    CGINCLUDE
    #if !defined(FLOW_INCLUDED)
    #define FLOW_INCLUDED
    
    //Returns new coordinates, moves texture.
    float3 FlowUVW (float2 uv, float2 flowVector, float2 jump, float flowOffset, float tiling, float time, bool flowB) {
        float phaseOffset = flowB ? 0.5 : 0;
        float progress = frac(time + phaseOffset);
        float3 uvw;
        uvw.xy = uv - flowVector * (progress + flowOffset);
        uvw.xy *= tiling;
        uvw.xy += phaseOffset;
        uvw.xy += (time - progress) * jump;
        uvw.z = 1 - abs(1 - 2 * progress);
        return uvw;
    }

    #endif
    ENDCG
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        
        //The Flow map is a vector field needed to simulate unpredictable movement.
        sampler2D _MainTex, _FlowMap, _NormalMap;
        float _UJump, _VJump, _Tiling, _Speed, _FlowStrength, _FlowOffset, _Emission;

        struct Input
        {
            //The input is a texture which the surf function processes.
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 flowVector = tex2D(_FlowMap, IN.uv_MainTex).rg * 2 - 1;
            flowVector *= _FlowStrength;
            float noise = tex2D(_FlowMap, IN.uv_MainTex).a;
            float time = _Time.y * _Speed + noise;
            float2 jump = float2(_UJump, _VJump);
            
            float3 uvwA = FlowUVW(IN.uv_MainTex, flowVector, jump, _FlowOffset, _Tiling ,time/8, false);
            float3 uvwB = FlowUVW(IN.uv_MainTex, flowVector, jump, _FlowOffset, _Tiling, time/8, true);
            
            float3 normalA = UnpackNormal(tex2D(_NormalMap, uvwA.xy)) * uvwA.z;
            float3 normalB = UnpackNormal(tex2D(_NormalMap, uvwB.xy)) * uvwB.z;
            o.Normal = normalize(normalA + normalB);
            
            fixed4 texA = tex2D(_MainTex, uvwA.xy) * uvwA.z;
            fixed4 texB = tex2D(_MainTex, uvwB.xy) * uvwB.z;
            
            // Albedo comes from a texture tinted by color
            fixed4 c = (texA + texB) * _Color;
            o.Albedo = c.rgb;
            //o.Albedo = float3(flowVector, 0);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            o.Emission = o.Albedo.rgb * _Emission;
        }
        ENDCG
    }
    FallBack "Diffuse"
}