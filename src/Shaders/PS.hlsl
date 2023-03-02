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
    //
    float3 lightPosition = float3(2, 4, 0);
    float3 lightColor = float3(1, 1, 1);
    float3 normal = normalize(pixel.normal);
    //
    
    float3 lightDir = normalize(lightPosition - pixel.fragPosition.xyz);
    float3 viewDir = normalize(CameraPosition - pixel.fragPosition.xyz);
    float3 reflectDir = reflect(-lightDir, normal);
    
    float ambientStrength = 0.3;
    float3 ambient = lightColor * ambientStrength;
    
    float diff = max(dot(normal, lightDir), 0);
    float3 diffuse = diff * lightColor;
    
    float specularStrength = 0.5;
    float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
    float3 specular = specularStrength * spec * lightColor;
    
    float3 result = (ambient + diffuse + specular) * pixel.color.xyz;
    
    return float4(result, 1);
};
