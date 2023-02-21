struct VertexShaderInput
{
    float4 pos : POSITION;
    float4 col : COLOR;
};

struct PixelShaderInput
{
    float4 pos : SV_POSITION;
    float4 col : COLOR;
};

PixelShaderInput VS(VertexShaderInput input)
{
    PixelShaderInput output = (PixelShaderInput) 0;
	
    output.pos = input.pos;
    output.col = input.col;
	
    return output;
}

float4 PS(PixelShaderInput input) : SV_Target
{
    return input.col;
}

technique10 Render
{
    pass P0
    {
        SetVertexShader(CompileShader(vs_4_0, VS()));
        SetGeometryShader(0);
        SetPixelShader(CompileShader(ps_4_0, PS()));
    }
}