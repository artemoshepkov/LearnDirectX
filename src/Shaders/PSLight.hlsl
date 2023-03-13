#define NUM_POINT_LIGHTS 4

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

struct Attenuation
{
    float constant;
    float linearC;
    float quadratic;
};

struct PointLight
{
    float4 color;
    float3 position;
    Attenuation attenuationCoefs;
};

cbuffer PerFrame : register(b1)
{
    float3 CameraPosition;
    DirectionalLight DirectLight;
};

cbuffer PointLightsBuffer : register(b3)
{
    PointLight DotLight[NUM_POINT_LIGHTS];
};

float3 CaclDirectLight(float3 normal, float3 viewDir, float3 pixelPosition, float3 pixelColor)
{    
    float3 lightDir = normalize(-DirectLight.direction);
    float3 reflectDir = reflect(-lightDir, normal);
    
    float ambientStrength = 0.3;
    float3 ambient = DirectLight.color * ambientStrength;
    
    float diff = max(dot(normal, lightDir), 0);
    float3 diffuse = diff * DirectLight.color;
    
    float specularStrength = 0.5;
    float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
    float3 specular = specularStrength * spec * DirectLight.color;
    
    return (ambient + diffuse + specular) * pixelColor;
}

float3 CaclPointLight(PointLight light, float3 normal, float3 viewDir, float3 pixelPosition, float3 pixelColor)
{    
    float constant = light.attenuationCoefs.constant;
    float linearc = light.attenuationCoefs.linearC;
    float quadratic = light.attenuationCoefs.quadratic;
    
    float3 lightDir = normalize(light.position - pixelPosition);
    
    float ambientStrength = 0.3;
    float3 ambient = light.color.xyz * ambientStrength;
    
    float diff = max(dot(normal, lightDir), 0);
    float3 diffuse = diff * light.color.xyz;
    
    float3 reflectDir = reflect(-lightDir, normal);
    float specularStrength = 0.5;
    float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
    float3 specular = specularStrength * spec * light.color.xyz;
    
    float dist = length(light.position - pixelPosition);
    float attenuation = 1 / (constant + linearc * dist + quadratic * (dist * dist));
    
    return (ambient + diffuse + specular) * attenuation * pixelColor;
}

float4 PSMain(PixelShaderInput pixel) : SV_Target
{    
    float3 normal = normalize(pixel.normal);
    float3 viewDir = normalize(CameraPosition - pixel.position.xyz);
    
    float3 result = float3(0, 0, 0);
    
    for (int i = 0; i < NUM_POINT_LIGHTS; i++)
        result += CaclPointLight(DotLight[i], normal, viewDir, pixel.fragPosition.xyz, pixel.color.xyz);
    
    return float4(result, 1);
};
