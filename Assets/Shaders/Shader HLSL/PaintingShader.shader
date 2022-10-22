Shader "Custom/PaintingShader"
{
    Properties
    {
        _albedo ("albedo", 2D) = "white" {}
        _surfaceColor ("surface color", Color) = (0.4, 0.1, 0.9)
        _shadowColor ("shadow color", Color) = (0.4, 0.1, 0.9)
        _ambientColor ("ambient color", Color) = (0.4, 0.1, 0.9)
        _paintBlendIntensity("paint blend intensity", Float) = 0.1
        _gloss ("gloss", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" "Queue"="Geometry"}
        LOD 200

         Pass
        {
            Tags { "LightMode"="UniversalForward" }
            // The HLSL code block. Unity SRP uses the HLSL language.
            HLSLPROGRAM
            // This line defines the name of the vertex shader. 
            #pragma vertex vert
            // This line defines the name of the fragment shader. 
            #pragma fragment frag

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl , SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #define MAX_SPECULAR_POWER 512

            sampler2D _albedo; float4 _albedo_ST;
            float3 _surfaceColor;
            float3 _shadowColor;
            float3 _ambientColor;
            float _paintBlendIntensity;
            float _gloss;

            float rand3D (float3 uv3)
            {
                return frac(sin(dot(uv3.xyz, float3(12.9898, 78.233, 84.394))) * 43758.5453123);
            }

            // modified to take in an extra coordinate z, representing time
            float noise (float2 uv, float z) {
                float2 ipos = floor(uv);
                float2 fpos = frac(uv);
                float iz = floor(z);
                float fz = frac(z);
                
                float o  = rand3D(float3(ipos, iz));
                float x  = rand3D(float3(ipos, iz) + float3(1, 0, 0));
                float y  = rand3D(float3(ipos, iz) + float3(0, 1, 0));
                float xy = rand3D(float3(ipos, iz) + float3(1, 1, 0));
                float oz  = rand3D(float3(ipos, iz) + float3(0, 0, 1));
                float xz  = rand3D(float3(ipos, iz) + float3(1, 0, 1));
                float yz  = rand3D(float3(ipos, iz) + float3(0, 1, 1));
                float xyz = rand3D(float3(ipos, iz) + float3(1, 1, 1));

                float2 smooth = smoothstep(0, 1, fpos);
                float smoothz = smoothstep(0, 1, fz);
                
                return lerp (
                    lerp(
                        lerp(o,  x, smooth.x),
                        lerp(y, xy, smooth.x), smooth.y),
                    lerp(
                        lerp(oz, xz, smooth.x),
                        lerp(yz, xyz, smooth.x), smooth.y),
                smoothz);
            }

            float fractal_noise (float2 uv, int n, float time) {
                float fn = 0;
                
                for(int j = 0; j < n; j++)
                {
                    fn += (1.0 / pow(2, j + 1)) * noise(uv * pow(2, j), time * pow(2, j));
                }
                
                return fn;
            }

            struct MeshData
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
                float3 posWorld : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.normal = TransformObjectToWorldNormal(v.normal); // Unity Object to World Normal
                o.vertex = TransformObjectToHClip(v.vertex); // Unity Object to Clip
                o.uv = v.uv;
                o.posWorld = mul(UNITY_MATRIX_M, v.vertex);
                
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float3 color = 0;
                float4 albedo = tex2D(_albedo, i.uv);

                float3 normal = normalize(i.normal);
                normal = 1 - (1 - normal) / pow(fractal_noise(normal.xy * 10, 3, normal.z * 10), _paintBlendIntensity);
                
                float3 lightDirection = _MainLightPosition;
                float3 lightColor = _MainLightColor; // includes intensity


                float3 viewPosition = _WorldSpaceCameraPos.xyz;
                float3 viewDir = normalize(viewPosition - i.posWorld);

                // Reflection
                float3 halfDir = normalize(viewDir + lightDirection);
                float specularFalloff = max(0, dot(normal, halfDir));
                specularFalloff = pow(specularFalloff, _gloss * MAX_SPECULAR_POWER + 0.00001) * _gloss;
                float3 specular = lightColor * specularFalloff;


                float surfaceDiffuse = dot(normal, lightDirection);
                surfaceDiffuse = surfaceDiffuse * pow(fractal_noise(surfaceDiffuse * 20, 4, surfaceDiffuse * 10), 0.2);

                // Terminator
                float3 terminator = abs(surfaceDiffuse);
                float3 diffuse = (1 - pow(1 - terminator, 6));

                float3 terminatorLight = smoothstep(0.3, 1, pow(1 - abs(surfaceDiffuse - 0.1), 8));
                terminatorLight = (1 - terminatorLight) + terminatorLight * albedo;
                diffuse *= terminatorLight;


                diffuse *= clamp(0.2, 0.8, smoothstep(0, 0, surfaceDiffuse) + 0.3f * _shadowColor); // Shadow

                // Surface Color
                float diffuseFalloff = max(0.5, smoothstep(0, 1, surfaceDiffuse));
                diffuse *= albedo / (1 - smoothstep(-0.4, 1, diffuseFalloff * lightColor) * 0.8);

                // Ambient
                float ambientFalloff = max(0, dot(normal, -lightDirection));
                float3 ambient = ambientFalloff * albedo * _ambientColor;
                ambient = pow(ambient, 2) * 1;
                

                color =  diffuse + ambient + specular;

                return float4(color, 1.0);
            }
            ENDHLSL
        }
    }
}
