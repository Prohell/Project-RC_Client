// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/UHexGon_Opt2"
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

			v2f vert (appdata v)
			{
				v2f o;
				float4 vt = v.vertex;

				o.ext = int4(0,0,0,0);
				o.ext.x = v.normal.z;

				float4 vm = mul(UNITY_MATRIX_M, vt);
				o.vertex = mul(UNITY_MATRIX_VP, vm);
				o.tex.x = dot(mul(UNITY_MATRIX_M, float4(1,0,0,0)), vm);
				o.tex.y = dot(mul(UNITY_MATRIX_M, float4(0,1,0,0)), vm);
				o.tex.xy*=1.0f/_TEXP.x;
				o.tex.zw = float2(vt.z, o.vertex.z);
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
				float4 uv;
				uv.xy = i.tex.xy;
				uv.zw = float2(0, i.ext.w);
				int k = frac(i.ext.x) > 0.5f ? ceil(i.ext.x) : floor(i.ext.x);
				fixed4 col = (fixed4)SampleAtlas(uv,_ARRAY[k]); // sampling 1
				return col;
			}
			ENDCG
		}
	}
}
