namespace DefaultNamespace;

// code to be used to replace shader code in unity to render game object as triangle

// Vertex Shader
struct Attributes
{
    float4 positionOS : POSITION;
    float2 uv : TEXCOORD0;
};

struct Varyings
{
    float4 positionCS : SV_POSITION;
    float2 uv : TEXCOORD0;
};

Varyings vert (Attributes input)
{
    Varyings output;
    output.positionCS = UnityObjectToClipPos(input.positionOS);
    output.uv = input.uv;
    return output;
}

// Fragment Shader
float4 frag (Varyings input) : SV_Target
{
    // Set the color of the triangle to red
    return float4(1, 0, 0, 1);
}