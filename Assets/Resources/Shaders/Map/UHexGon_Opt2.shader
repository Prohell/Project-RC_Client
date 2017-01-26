// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/UHexGon_Opt2"
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
			#ifdef UNITY_COMPILER_HLSL2GLSL
	    	#define uflat nointerpolation
	    	#else
	    	#define uflat flat
			#endif

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
			float4 _EXTRA;
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

				o.ext = int4(0,0,0,0);
				o.ext.x = v.normal.z;

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

				//if( _EXTRA.z<0.0 )
				//	o.vertex.z = -1;
				//UNITY_TRANSFER_FOG(o,vt);
				return o;
			}

			fixed4 SampleAtlas(float4 uv, float4 rc) {
				return (fixed4)tex2D(_ATLAS, frac(uv.xy)*rc.zw + rc.xy, ddx(uv*rc.zw + rc.xy).x, ddy(uv*rc.zw + rc.xy).y);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv2 = i.tex.zw;

				float4 encodedNormal = 1;
				/*if (i.tan.z < 1)
				{
					encodedNormal = tex2D(_NormalMap, uv2);
					//return float4(1, 0, 0, 1);
				}
				else if (i.tan.z < 2)
				{
					encodedNormal = tex2D(_NormalMap2, uv2);
					//return float4(0, 1, 0, 1);
				}
				else if (i.tan.z < 3)
				{
					encodedNormal = tex2D(_NormalMap3, uv2);
					//return float4(0, 0, 1, 1);
				}
				*/

				if (i.tan.z < 3)
				{

				}
				else if (i.tan.z < 4)
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

				/*if (i.tan.w > 0)
				{
					if (i.tan.w == 4)
					{
						return fixed4(1,0,0,1);
					}
					else if (i.tan.w == 5)
					{
						return fixed4(0,1,0,1);
					}
					else if (i.tan.w == 6)
					{
						return fixed4(0,0,1,1);
					}
					else if (i.tan.w == 7)
					{
						return fixed4(1,1,0,1);
					}
					else if (i.tan.w == 8)
					{
						return fixed4(1,0,1,1);
					}
					else
					{
						return fixed4(0,1,1,1);
					}
				}*/

				float tmpN = encodedNormal.x;
				tmpN = (tmpN - _LowValue) / (_HighValue - _LowValue);
				encodedNormal = float4(tmpN, tmpN, tmpN, 1);
				//return encodedNormal;

				float4 uv;
				uv.xy = i.tex.xy;
				uv.zw = float2(0, i.ext.w);
				int k = frac(i.ext.x) > 0.5f ? ceil(i.ext.x) : floor(i.ext.x);
				fixed4 col = (fixed4)SampleAtlas(uv,_ARRAY[k]); // sampling 1
				//return col;

				fixed3 withSpecular = fixed3(1, 1, 1) - (fixed3(1, 1, 1) - encodedNormal.xyz) / col.xyz;
				return fixed4(withSpecular, 1)*0.3+col*0.8;
			}
			ENDCG
		}
	}
}
