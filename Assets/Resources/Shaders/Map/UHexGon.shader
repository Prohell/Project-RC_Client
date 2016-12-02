// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/UHexGon"
{
	Properties
	{
		_ATLAS("Atlas", 2D) = "white" {}
		_TexNoise("Noise", 2D) = "white" {}
		_TEXP("Noise", Vector) = (10.0, 1.0, 1.0, 1.0)

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

			sampler2D _TexNoise;
			float4 _TexNoise_ST;
			sampler2D _ATLAS;
			float4 _ARRAY[8];

			float4 _TEXP;


			v2f vert (appdata v)
			{
				v2f o;
				float4 vt = v.vertex;
				float4 vm = mul(UNITY_MATRIX_M, vt);
				o.vertex = mul(UNITY_MATRIX_VP, vm);
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
				//return (fixed4)tex2Dlod(_ATLAS, float4(frac(uv.xy)*rc.zw + rc.xy, 0, clamp(0,0,uv.w)));
				return (fixed4)tex2D(_ATLAS, frac(uv.xy)*rc.zw + rc.xy, ddx(uv*rc.zw + rc.xy).x, ddy(uv*rc.zw + rc.xy).y);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				float4 cc = 0;
				cc += tex2D(_TexNoise, i.tex.xy * 1.0 / _TexNoise_ST.xy + _TexNoise_ST.zw);
				cc += tex2D(_TexNoise, i.tex.xy * 0.1 / _TexNoise_ST.xy + _TexNoise_ST.zw);
				float4 uv;
				uv.xy = i.tex.xy;
				uv.zw = float2(0, i.ext.w);
				//float2 fr = frac(i.ext.w)*float2(-1, 1) + float2(1, 0);
				//float lod = cc.x*_TEXP.y;
				//uv = all(fr>0.001) ? uv*2.5 : uv;
				//int k = dot(i.clr.xyz > 0.01, float4(1, 2, 3))-1;

				//#define TEX8 1
				//#define NOTEX 1

				#ifdef TEX8

				#ifdef NOTEX

				fixed4 cl[8]={
					fixed4(0,0,0,1),
					fixed4(1,0,0,1),
					fixed4(0,1,0,1),
					fixed4(0,0,1,1),
					fixed4(0,1,1,1),
					fixed4(1,0,1,1),
					fixed4(1,1,0,1),
					fixed4(1,1,1,1),
				};

				#else

				fixed4 cl[8]={
					(fixed4)SampleAtlas(uv,_ARRAY[0]),
					(fixed4)SampleAtlas(uv,_ARRAY[1]),
					(fixed4)SampleAtlas(uv,_ARRAY[2]),
					(fixed4)SampleAtlas(uv,_ARRAY[3]),
					(fixed4)SampleAtlas(uv,_ARRAY[4]),
					(fixed4)SampleAtlas(uv,_ARRAY[5]),
					(fixed4)SampleAtlas(uv,_ARRAY[6]),
					(fixed4)SampleAtlas(uv,_ARRAY[7]),
				};

				#endif

				#else

				#ifdef NOTEX

				fixed4 cl[4]={
					fixed4(0,0,0,1),
					fixed4(1,0,0,1),
					fixed4(0,1,0,1),
					fixed4(0,0,1,1),
				};

				#else

				fixed4 cl[4]={
					(fixed4)SampleAtlas(uv,_ARRAY[0]),
					(fixed4)SampleAtlas(uv,_ARRAY[1]),
					(fixed4)SampleAtlas(uv,_ARRAY[2]),
					(fixed4)SampleAtlas(uv,_ARRAY[3]),
				};

				#endif

				#endif

				bool bs = i.clr.a > 0.0f;
				fixed4 col =
					cl[0] * i.clr.x + cl[1] * i.clr.y + cl[2] * i.clr.z + cl[3] * i.clr.w
					#ifdef TEX8
					+ cl[4] * i.tan.x + cl[5] * i.tan.y + cl[6] * i.tan.z + cl[7] * i.tan.w
					#endif
					;
				
				//return col;
				return col;// -float4(cc.xyz*.5, 0);
			}
			ENDCG
		}
	}
}
