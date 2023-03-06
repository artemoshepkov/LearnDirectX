struct VertexShaderInput
{
    float3 position : POSITION;
    float3 normal : NORMAL;
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
    float4x4 WorldInverseTranspose;
};

cbuffer PerMaterial : register(b2)
{
    float4 Color;
};

PixelShaderInput VSMain(VertexShaderInput input)
{    
    PixelShaderInput output = (PixelShaderInput) 0;
    
    output.position = mul(mul(ViewProjection, Model), float4(input.position, 1));
    output.color = Color;
    
    output.fragPosition = mul(Model, float4(input.position, 1));
    output.normal = mul((float3x3) WorldInverseTranspose, input.normal);
    
    return output;        
}