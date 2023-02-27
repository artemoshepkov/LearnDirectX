struct PixelShaderInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
    
    float3 worldPosition : WORLDPOS;
    float3 worldNormal : NORMAL;
};

struct DirectionalLight
{
    float4 Color;
    float3 Direction;
};

cbuffer PerFrame : register(b1)
{
    float3 CameraPosition;
    DirectionalLight Light;
};

cbuffer PerMaterial : register(b2)
{
    float4 Ambient;
    float4 Diffuse;
    float4 Specular;
    float Shininess;
};

float3 Lambert(float4 pixelDiffuse, float3 normal, float3 toLight)
{
    float3 diffuseAmount = saturate(dot(normal, toLight));
    
    return pixelDiffuse.rgb * diffuseAmount;
}

float4 PSMain(PixelShaderInput pixel) : SV_Target
{
    float3 normal = normalize(pixel.worldNormal);
    float3 toCamera = normalize(CameraPosition - pixel.worldPosition);
    float3 toLight = normalize(-Light.Direction);
    
    float4 sample = (float4) 1.0f;
    
    float3 diffuse = Lambert(pixel.color, normal, toLight);
    float3 color = (saturate(Ambient + Diffuse) * sample.rgb) * Light.Color.rgb;
    
    float alpha = pixel.color.a + sample.a;
    
    return float4(color, alpha);
};
