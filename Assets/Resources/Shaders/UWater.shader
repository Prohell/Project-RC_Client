// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/UWater"
{
	Properties
	{
		_TexNoise("Texture", 2D) = "white" {}
		_TEXP("Noise", Vector) = (10.0, 1.0, 1.0, 1.0)

		_NormalMap("Normal map", 2D) = "white" {}
		[NoScaleOffset] _ReflectiveColor("Reflective color (RGB) fresnel (A) ", 2D) = "" {}
		//_ReflectSkyMap("reflectSkymap ", 2D) = "gray" {}

		//[HideInInspector] _ReflectionTex("Internal Reflection", 2D) = "" {}

		_Color1("Color1", Color) = (0.0824, 0.219,0.116,1)
		_Color2("Color2", Color) = (0.05, 0.171, 0.219, 1)
		_Alpha("Alpha", Range(0,1)) = 1
		_RefMap("Reflection Map", 2D) = "white" {}
		_RippleAmount("Ripple amount", Float) = 0.5
		_Speed("Speed", Vector) = (0.02, 0.02, 0.02, 0.02)
		_FresnelPower("Fresnel Power", Float) = 2
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
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

				half3 worldNormal : TEXCOORD3;

				half2 pos:TEXCOORD4;
			};

			sampler2D _TexNoise;
			float4 _TexNoise_ST;
			sampler2D _ATLAS;
			float4 _ARRAY[8];

			float4 _TEXP;

			sampler2D _NormalMap;
			sampler2D _ReflectiveColor;
			//sampler2D _ReflectionTex;
			//sampler2D _ReflectSkyMap;

			//sampler2D _NormalMap;
			sampler2D _RefMap;

			float4 _Color1;
			float4 _Color2;
			float4 _RefMap_ST;
			float4 _NormalMap_ST;
			float _RippleAmount;
			float4 _Speed;
			float _FresnelPower;
			float _Alpha;




			v2f vert (appdata v)
			{
				v2f o;
				float4 vt = v.vertex;
				float4 vm = mul(UNITY_MATRIX_M, vt);
				o.pos = v.vertex.xy*0.1;
				o.vertex = mul(UNITY_MATRIX_VP, vm);
				//o.tex.x = o.tex.x / o.tex.w;
				//o.tex.y = o.tex.y / o.tex.w;
				o.tex.x = dot(mul(UNITY_MATRIX_M, float4(1,0,0,0)), vm);
				o.tex.y = dot(mul(UNITY_MATRIX_M, float4(0,1,0,0)), vm);
				o.tex.xy*=1.0f/_TEXP.x;
				o.tex.zw = float2(vt.z, o.vertex.z);
				o.tan = v.tangent;
				o.clr = v.color;
				float3 vn = mul(UNITY_MATRIX_M, v.normal.xyz);
				o.ext.xyz = vn;
				o.ext.w = o.vertex.z;


				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldNormal = reflect(-worldViewDir, worldNormal);

				//o.idx = int(v.color.a)
				//UNITY_TRANSFER_FOG(o,vt);
				return o;


			}

			fixed4 SampleAtlas(float4 uv, float4 rc) {
				//return (fixed4)tex2Dlod(_ATLAS, float4(frac(uv.xy)*rc.zw + rc.xy, 0, clamp(0,0,uv.w)));
				return (fixed4)tex2D(_ATLAS, frac(uv.xy)*rc.zw + rc.xy, ddx(uv*rc.zw + rc.xy).x, ddy(uv*rc.zw + rc.xy).y);
			}
			float fresnel(float3 light, float3 normal, float R0, float p)
			{

				float cosAngle = 1 - saturate(dot(light, normal));

				float result = cosAngle * cosAngle;
				result = result * result;
				result = result * cosAngle;
				result = saturate(result * (1 - saturate(R0)) + R0);


				return pow(result, p);
			}

			float4 desaturation(float4 c, float k)
			{
				float f = dot(c.xyz, float3(0.3, 0.59, 0.11));
				float3 cc = lerp(c.xyz, f.xxx, k);

				return float4(cc, c.w);
			}

			float3 calcNormal(float3 worldNormal, float2 texcoord)
			{

				float2 p1 = texcoord + float2(0.484, 0.867);
				float2 p2 = texcoord + float2(0.685, 0.447);
				float2 p3 = texcoord + float2(0.0954, 0.04);
				float2 p4 = texcoord;

				p1.y -= _Speed.x * _Time.y;
				p2.y += _Speed.y * _Time.y;
				p3.x -= _Speed.z * _Time.y;
				p4.x -= _Speed.w * _Time.y;

				p1 *= 2;
				p2 *= 2;
				p3 *= 2;
				p4 *= 2;

				float3 n1 = tex2D(_NormalMap, p1);
				float3 n2 = tex2D(_NormalMap, p2);

				float3 n = n1 + n2;
				n = n * 2 - 2;

				n = lerp(n, float3(0, 1, 0), _RippleAmount);


				return n;
			}

			float3 mReflect(float3 eye, float3 norm)
			{
				return eye - 2 * norm * dot(eye, norm);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				
				float4 cc = 0;
				cc += tex2D(_NormalMap, i.tex.xy * 1.0 / _TexNoise_ST.xy + _TexNoise_ST.zw);
				cc += tex2D(_NormalMap, i.tex.xy * 0.1 / _TexNoise_ST.xy + _TexNoise_ST.zw);
				
				float4 uv;
				uv.xy = i.tex.xy;
				uv.zw = float2(0, i.ext.w);

				

				float3 n = normalize(i.worldNormal);
				float3 v = normalize(i.vertex.xyz - _WorldSpaceCameraPos.xyz);
				float3 norm = calcNormal(n, i.pos);
				float3 ref = normalize(mReflect(v, norm));

				float2 texCoord = ref.xz * 0.5 + 0.5;
				float4 c = tex2D(_RefMap, TRANSFORM_TEX(texCoord, _RefMap));

				c = desaturation(c, 0.7);
				float3 c1 = _Color1.rgb + c.rgb;
				float3 c2 = _Color2.rgb + c.rgb;
				float fres = fresnel(-v, n, 0.018, _FresnelPower);
				c.rgb = lerp(c1, c2, fres);
				float4 color = c;

				//return frac(i.ext.z);
				//return color;

				fixed4 cl[8]={
					//fixed4(0,0,0,1),
					(fixed4)SampleAtlas(uv,_ARRAY[0]),
					//fixed4(1,0,0,1),
					(fixed4)SampleAtlas(uv,_ARRAY[1]),
					//fixed4(0,1,0,1),
					(fixed4)SampleAtlas(uv,_ARRAY[2]),
					//fixed4(0,0,1,1),
					(fixed4)SampleAtlas(uv,_ARRAY[3]),
					//fixed4(0,1,1,1),
					(fixed4)SampleAtlas(uv,_ARRAY[4]),
					//fixed4(1,0,1,1),
					(fixed4)SampleAtlas(uv,_ARRAY[5]),
					//fixed4(1,1,0,1),
					(fixed4)SampleAtlas(uv,_ARRAY[6]),
					//fixed4(1,1,1,1),
					(fixed4)SampleAtlas(uv,_ARRAY[7]),
				};

				bool bs = i.clr.a > 0.0f;
				fixed4 col =
					color * i.clr.x*0.5 + cl[1] * i.clr.y + cl[2] * i.clr.z + cl[3] * i.clr.w +
					cl[4] * i.tan.x + cl[5] * i.tan.y + cl[6] * i.tan.z + cl[7] * i.tan.w;

				return col;//- float4(cc.xyz*.5, 0);
			}
			ENDCG
		}
	}
}
