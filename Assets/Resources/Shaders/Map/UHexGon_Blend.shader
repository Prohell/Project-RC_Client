// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/UHexGon_Blend"
{
	Properties
	{
		_TEXP("Tiling", Vector) = (10.0, 1.0, 1.0, 1.0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL0;
				float4 tangent : TANGENT0;
				float4 color : COLOR0;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				//float2 uv : TEXCOORD0;
				float4 clr : COLOR0;
				float4 tex : TEXCOORD0;
				float4 tan : TEXCOORD1;
				float4 ext : TEXCOORD2;
				//UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _ATLAS;
			float4 _ARRAY[8];
			float4 _TEXP;

			v2f vert (appdata v)
			{
				v2f o;
				float4 vt = v.vertex;
				float4 vm = mul(UNITY_MATRIX_M, vt);
				o.vertex = mul(UNITY_MATRIX_VP, vm);
				//test
				//o.vertex.w=-1;
				o.tex.x = dot(mul(UNITY_MATRIX_M, float4(1,0,0,0)), vm);
				o.tex.y = dot(mul(UNITY_MATRIX_M, float4(0,1,0,0)), vm);
				o.tex.xy*=1.0f/_TEXP.x;
				o.tex.zw = float2(vt.z, o.vertex.z);
				o.tan = v.tangent;
				o.clr = v.color;
				//float3 vn = mul(v.normal.xyz,unity_WorldToObject);
				float3 vn = mul((float3x3)UNITY_MATRIX_M, v.normal.xyz);
				o.ext.xyz = vn;
				o.ext.w = o.vertex.z;
				//o.idx = int(v.color.a)
				//UNITY_TRANSFER_FOG(o,vt);
				return o;
			}

			fixed4 SampleAtlas(float4 uv, float4 rc) {
				return (fixed4)tex2D(_ATLAS, frac(uv.xy)*rc.zw + rc.xy, ddx(uv*rc.zw + rc.xy).x, ddy(uv*rc.zw + rc.xy).y);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				//return i.clr.xxxx;
				//return i.clr.yyyy;
				//return i.clr.zzzz;
				//return i.clr.wwww;

				float4 uv;
				uv.xy = i.tex.xy;
				uv.zw = float2(0, i.ext.w);

				fixed4 cl[4]={
					(fixed4)SampleAtlas(uv, _ARRAY[0]),
					(fixed4)SampleAtlas(uv, _ARRAY[1]),
					(fixed4)SampleAtlas(uv, _ARRAY[2]),
					(fixed4)SampleAtlas(uv, _ARRAY[3]),  // sampling 4
				};

				fixed4 col =
					0
					//+ cl[0]
					//+ cl[1]
					//+ cl[2]
					//+ cl[3]
					+ cl[0] * i.clr.x
					+ cl[1] * i.clr.y
					+ cl[2] * i.clr.z
					+ cl[3] * i.clr.w
					;

				//return col;
				return col;// -float4(cc.xyz*.5, 0);
			}
			ENDCG
		}
	}
}
