struct PixelShaderInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
    
    float4 fragPosition : FRAGPOS;
    float3 normal : NORMAL;
};

struct DirectionalLight
{
    float4 color;
    float3 direction;
};

cbuffer PerFrame : register(b1)
{
    float3 CameraPosition;
    DirectionalLight Light;
};

float4 PSMain(PixelShaderInput pixel) : SV_Target
{    
    float3 normal = normalize(pixel.normal);
    float3 lightDir = normalize(-Light.direction);
    
    float ambientStrength = 0.4;
    float3 ambient = ambientStrength * Light.color;
    
    float diff = max(dot(normal, lightDir), 0);
    float3 diffuse = diff * Light.color;
    
    float3 result = (ambient + diffuse) * pixel.color.xyz;
    
    return float4(result, 1);
};
