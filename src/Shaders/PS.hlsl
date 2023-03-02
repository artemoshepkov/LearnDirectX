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
    PointLight DotLight;
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

float3 CaclPointLight(float3 normal, float3 viewDir, float3 pixelPosition, float3 pixelColor)
{
    float constant = DotLight.attenuationCoefs.constant;
    float linearc = DotLight.attenuationCoefs.linearC;
    float quadratic = DotLight.attenuationCoefs.quadratic; 
    
    float3 lightDir = normalize(DotLight.position - pixelPosition);
    
    float ambientStrength = 0.3;
    float3 ambient = DotLight.color.xyz * ambientStrength * pixelColor;
    
    float diff = max(dot(normal, lightDir), 0);
    float3 diffuse = diff * DotLight.color.xyz * pixelColor;
    
    float3 reflectDir = reflect(-lightDir, normal);
    float specularStrength = 0.5;
    float spec = pow(max(dot(viewDir, reflectDir), 0), 32);
    float3 specular = specularStrength * spec * DotLight.color.xyz * pixelColor;
    
    float dist = length(DotLight.position - pixelPosition);
    float attenuation = 1 / (constant + linearc * dist + quadratic * (dist * dist));
    
    return (ambient + diffuse + specular) * attenuation;
}

float4 PSMain(PixelShaderInput pixel) : SV_Target
{    
    float3 normal = normalize(pixel.normal);
    float3 viewDir = normalize(CameraPosition - pixel.position.xyz);
    
    return float4(CaclPointLight(normal, viewDir, pixel.fragPosition.xyz, pixel.color.xyz), 1);
};
