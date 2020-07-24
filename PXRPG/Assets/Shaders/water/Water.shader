Shader "Custom/Water"
{
    Properties
    {
        _SurfaceNoise("Surface Noise", 2D) = "white" {}
        _NoiseCutoff("Noise cutoff", Range(0,1)) = 0.5
        _FoamDepth("Foam Depth", Float) = 0.2
        
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _WaterFogColor ("Water Fog Color", Color) = (0, 0, 0, 0)
        _WaterFogDensity ("Water Fog Density", Range(0, 2)) = 0.1
        
        _DistortionTexture("Distorion texture", 2D) = "white" {}
        _RefractionValue("Refraction value", Range(0,1)) = 0.2
        _WiggleAmount("Wiggle value", Range(0, 4)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200
        
        GrabPass {"_WaterBackgroundColor"}
        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha finalcolor:ResetAlpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex, _SurfaceNoise;
        sampler2D _CameraDepthTexture, _WaterBackgroundColor, _DistortionTexture;
        float4 _CameraDepthTexture_TexelSize;

        struct Input
        {
            float2 uv_MainTex; // [0, 1]
            float2 uv_SurfaceNoise : TEXCOORD1;
            float4 screenPos; //Screen pos is the camera view coordinates (not [0, 1])
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _WaterFogDensity, _NoiseCutoff, _FoamDepth, _RefractionValue, _WiggleAmount;
        float3 _WaterFogColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
        void ResetAlpha (Input IN, SurfaceOutputStandard o, inout fixed4 color) {
            color.a = 1;
        }
        
        //use inout to be able to modify the variable. (Like a reference in c++)
        float3 colorBelowWater(float4 screenPos, float2 noiseUV, float2 uvMain) {
            /*
                Här använde vi screenPos för att fetcha från distortion texturen, detta ledde till att
                samples var beroende av camerans position så när vi zoomade in & ut vinglade vattnet snabbt.
                Fetcha med noiseUV istället (eller valfri uv som inte är beroende av kameran).
            */
            //This 2D texture has range [-1, 1] not [0, 1] hence we perform *2-1. 
            float2 uvOffset = (tex2D(_DistortionTexture, noiseUV*_RefractionValue+_Time.y*0.03).xy*2-1)*_WiggleAmount;//normal.xy;
            uvOffset.y *= _CameraDepthTexture_TexelSize.z * abs(_CameraDepthTexture_TexelSize.y);
            float2 uv = (screenPos.xy + uvOffset) / screenPos.w;
            float2 ogUV = (screenPos.xy) / screenPos.w;
            //ogUV.y *= _CameraDepthTexture_TexelSize.z * abs(_CameraDepthTexture_TexelSize.y);
            //We use two diff variables, one for getting the foam, and the other for background color.
            //These are different since for background color we want to use distorted uv, and for foam
            //we want to use the original uv because we don't want the foam to be distorted.
            float backgroundDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
            float surfaceDepth = UNITY_Z_0_FAR_FROM_CLIPSPACE(screenPos.z);
            float diff = backgroundDepth - surfaceDepth;
            
            float nonDistortedBackgroundDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, ogUV));
            float diffForFoam = nonDistortedBackgroundDepth - surfaceDepth;
            //Foam
            float noiseSample = tex2D (_SurfaceNoise, noiseUV+_Time.y*0.03).r;
            float x = saturate(diffForFoam/_FoamDepth);
            float noiseCutoff = x*_NoiseCutoff;
            if(diffForFoam < _FoamDepth && diffForFoam > 0 && noiseSample > noiseCutoff ) {
                return 1;
            }
            
            //Use this check to prevent refracting objects above the water surface.
            if(diff < 0) {
                uv = screenPos.xy / screenPos.w;
                #if UNITY_UV_STARTS_AT_TOP
                    if (_CameraDepthTexture_TexelSize.y < 0) {
                uv.y = 1 - uv.y;
                }
                backgroundDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
                diff = backgroundDepth - surfaceDepth;
                #endif
            }
            //Here we want to use distorted uv to fetch from background (image projected in water)
            float3 backgroundColor = tex2D(_WaterBackgroundColor, uv).rgb;
            /*
                The way this is built is that when the heigh difference is greater (when we are deeper in water),
                The fogFactor becomes small and our the water color becomes what we assigned in variable
                _WaterFogColor (meaning pure blue).
                So the overall technique is to interpolate between the original color and the
                max depth color.
                In the toon water tutorial they didn't use original color like we do, they used two pre-assigned colors
                that they interpolated between.
            */
            float fogFactor = exp2(-_WaterFogDensity*diff);
            return lerp(_WaterFogColor, backgroundColor, fogFactor); //diff/5.0
        }
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = _Color;
            
            /*
            float noise = tex2D (_SurfaceNoise, IN.uv_SurfaceNoise+_Time.y*0.03).r;
            if(noise > _NoiseCutoff) {
                c.a = _Color.a;
                noise = 1;
            } else {
                noise = 0;
                c.a = 0;
            }
            
            
            o.Albedo = noise+c;
            */
            //c = _Color;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            o.Normal = float3(tex2D(_DistortionTexture, IN.uv_SurfaceNoise*3.+_Time.y*0.1).xy, 1.);
            o.Emission = colorBelowWater(IN.screenPos, IN.uv_SurfaceNoise, IN.uv_MainTex)*(1-c.a);
         }
        ENDCG
    }
    //FallBack "Diffuse"
}
