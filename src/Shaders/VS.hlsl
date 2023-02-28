struct VertexShaderInput
{
    float3 position : POSITION;
    float3 normal : NORMAL;
    float4 color : COLOR;
};

struct PixelShaderInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
    
    float4 fragPosition : FRAGPOS;
    float3 normal : NORMAL;
};

cbuffer PerObject : register(b0)
{
    float4x4 ViewProjection;
    float4x4 Model;
    float4x4 WorldInverseTransope;
};

cbuffer PerMaterial : register(b2)
{
    float4 MaterialAmbient;
    float4 MaterialDiffuse;
    float4 MaterialSpecular;
    float MaterialSpecularPower;
};

PixelShaderInput VSMain(VertexShaderInput input)
{    
    PixelShaderInput output = (PixelShaderInput) 0;
    
    output.position = mul(mul(ViewProjection, Model), float4(input.position, 1));
    output.color = input.color;
    
    output.fragPosition = mul(float4(input.position, 1), Model);
    output.normal = mul(input.normal, (float3x3) WorldInverseTransope);
    
    return output;        
}