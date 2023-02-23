struct VertexShaderInput
{
    float4 position : POSITION;
    float3 normal : NORMAL;
    float4 color : COLOR;
};

struct PixelShaderInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
    
    float3 worldPosition : WORLDPOS;
    float3 worldNormal : NORMAL;
};

cbuffer PerObject : register(b0)
{
    float4x4 WorldViewProjection; // replace to PerFrame and change to ViewProj
    float4x4 World;
    float4x4 WorldInverseTranspose;
};

cbuffer PerFrame : register(b1)
{
    float3 CameraPosition;
};

PixelShaderInput VSMain(VertexShaderInput input)
{
    input.position.w = 1;
    
    PixelShaderInput output = (PixelShaderInput) 0;
    
    output.position = mul(input.position, WorldViewProjection);
    output.color = input.color;
    output.worldNormal = mul(input.normal, (float3x3) WorldInverseTranspose);
    output.worldPosition = mul(input.position, World).xyz;
    
    return output;        
}