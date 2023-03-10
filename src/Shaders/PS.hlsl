struct PixelShaderInput
{
    float4 position : SV_POSITION;
    float4 color : COLOR;
    
    float4 fragPosition : FRAGPOS;
    float3 normal : NORMAL;
};

float4 PSMain(PixelShaderInput pixel) : SV_Target
{
    return pixel.color;
};
