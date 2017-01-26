// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/UHexGon_Opt"
{
	Properties
	{
		_TEXP("Tiling", Vector) = (10.0, 1.0, 1.0, 1.0)
		//_NormalMap("NormalMap",2D) = "Bump"{}
		//_NormalMap2("NormalMap2",2D) = "Bump"{}
		//_NormalMap3("NormalMap3",2D) = "Bump"{}
		_NormalMap4("NormalMap4",2D) = "Bump"{}
		_NormalMap5("NormalMap5",2D) = "Bump"{}
		_NormalMap6("NormalMap6",2D) = "Bump"{}
		_LowValue("Low Value", Range(0,1)) = 0.34
		_HighValue("High Value", Range(0,1)) = 0.54
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
				float2 uv2 : TEXCOORD2;
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
			uniform sampler2D _NormalMap;
			float4 _NormalMap_ST;
			uniform sampler2D _NormalMap2;
			uniform sampler2D _NormalMap3;
			uniform sampler2D _NormalMap4;
			uniform sampler2D _NormalMap5;
			uniform sampler2D _NormalMap6;
			uniform float _LowValue;
			uniform float _HighValue;

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
				o.tex.zw = v.uv2;
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
				float4 uv;
				uv.xy = i.tex.xy;
				uv.zw = float2(0, i.ext.w);

				float2 uv2 = i.tex.zw;

				float4 encodedNormal = 1;
				/*if(i.tan.z < 1)
				{
					encodedNormal = tex2D(_NormalMap, uv2);
					//return float4(1, 0, 0, 1);
				}
				else if(i.tan.z < 2)
				{
					encodedNormal = tex2D(_NormalMap2, uv2);
					//return float4(0, 1, 0, 1);
				}
				else if(i.tan.z < 3)
				{
					encodedNormal = tex2D(_NormalMap3, uv2);
					//return float4(0, 0, 1, 1);
				}
				*/

				//return float4(uv2, 0, 1);

				if (i.tan.z < 3)
				{

				}
				else if(i.tan.z < 4)
				{
					encodedNormal = tex2D(_NormalMap4, uv2);
					//return float4(0, 0, 1, 1);
				}
				else if (i.tan.z < 5)
				{
					encodedNormal = tex2D(_NormalMap5, uv2);
					//return float4(0, 0, 1, 1);
				}
				else if (i.tan.z < 6)
				{
					encodedNormal = tex2D(_NormalMap6, uv2);
					//return float4(0, 0, 1, 1);
				}
				//return encodedNormal;
				encodedNormal = normalize(encodedNormal);
				//return encodedNormal;
				

				float tmpN = encodedNormal.x;
				tmpN = (tmpN - _LowValue) / (_HighValue - _LowValue);
				encodedNormal = float4(tmpN, tmpN, tmpN, 1);
				//return encodedNormal;



				#define TEX4 1
				//#define TEX3 1
				//#define TEX2 1

				#ifdef TEX4

				fixed4 cl[4]={
					(fixed4)SampleAtlas(uv, _ARRAY[0]),
					(fixed4)SampleAtlas(uv, _ARRAY[1]),
					(fixed4)SampleAtlas(uv, _ARRAY[2]),
					(fixed4)SampleAtlas(uv, _ARRAY[3]),  // sampling 4
				};

				#endif

				#ifdef TEX3

				fixed4 cl[3]={
					(fixed4)SampleAtlas(uv, _ARRAY[0]),
					(fixed4)SampleAtlas(uv, _ARRAY[1]),
					(fixed4)SampleAtlas(uv, _ARRAY[2]),  // sampling 3
				};

				#endif

				#ifdef TEX2

				fixed4 cl[2]={
					(fixed4)SampleAtlas(uv, _ARRAY[0]),
					(fixed4)SampleAtlas(uv, _ARRAY[1]),  // sampling 3
				};

				#endif

				//return cl[0] * i.clr.x;
				//return cl[1] * i.clr.y;
				//return cl[2] * i.clr.z;
				//return cl[3] * i.clr.w;

				#ifdef TEX4
				fixed4 col =
					cl[0] * i.clr.x + cl[1] * i.clr.y + cl[2] * i.clr.z + cl[3] * i.clr.w;
				#endif

				#ifdef TEX3
				fixed4 col =
					cl[0] * i.clr.x + cl[1] * i.clr.y + cl[2] * i.clr.z;
				#endif

				#ifdef TEX2
				fixed4 col =
					cl[0] * i.clr.x + cl[1] * i.clr.y;
				#endif

				//return col;

				fixed3 withSpecular = fixed3(1, 1, 1) - (fixed3(1, 1, 1) - encodedNormal.xyz) / col.xyz;
				return fixed4(withSpecular, 1)*0.3 + col*0.8;
			}
			ENDCG
		}
	}
}
