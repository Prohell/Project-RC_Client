Shader "Unlit/waterPostEffect" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_NoiseTex("Base (RGB)", 2D) = "white" {}
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Stencil{
		Ref  5
		Comp equal
		}		
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform float4 _MainTex_TexelSize;
uniform float4 _CenterRadius;
uniform float4x4 _RotationMatrix;

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
};

v2f vert( appdata_img v )
{
	v2f o;
	o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
	o.uv = v.texcoord;
	return o;
}
sampler2D _NoiseTex;

float4 frag (v2f i) : SV_Target
{
	float2 offset = i.uv;
	offset.y = offset.y * 10 +_Time.x;
	offset.x = offset.x*0.9;

	
	float4 noiseColor = tex2D(_NoiseTex, offset);
	//return noiseColor;
	float a = tex2D(_MainTex, i.uv).a;
	float4 initialColor = tex2D(_MainTex, i.uv);
	float4 adjustColor = tex2D(_MainTex, i.uv + float2((noiseColor.x-0.5)*0.05,0));
	return adjustColor;
	//if (adjustColor.a > 0.5)
		//return initialColor;
	//return adjustColor;
}
ENDCG

	}
}

Fallback off

}
