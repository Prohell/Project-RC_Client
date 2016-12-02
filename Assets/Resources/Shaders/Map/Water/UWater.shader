// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/UWater"
{
	Properties
	{
		_TexNoise("Texture", 2D) = "white" {}
		_TEXP("Noise", Vector) = (10.0, 1.0, 1.0, 1.0)

		_NormalMap("Normal map", 2D) = "white" {}
		_RefMap("Reflection Map", 2D) = "white" {}
		_RippleAmount("Ripple amount", Float) = 0.5
		
		_WaveMap("wave map", 2D) = "white" {}
		_NoiseMap("noise map", 2D) = "white" {}

		_WaveSpeed("WaveSpeed", float) = -12.64 //海浪速度
		//_WaveWidth("wave width",range(1,10)) = 2

		_Range("Range", float) = 0.37
		_WaveRange("WaveRange", float) = 0.3
		_NoiseRange("NoiseRange", float) = 6.43
		_WaveDelta("WaveDelta", float) = 2.43

	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue" = "Geometry-2" }
		LOD 100
		//Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			Stencil{
			Ref  2
			Comp always
			Pass replace}

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
				float2 uv2 : TEXCOORD1;
				float2 uv3 : TEXCOORD2;
			};

			struct v2f
			{
				//float2 uv : TEXCOORD0;
				float4 vertex      : SV_POSITION;
				float4 clr         : COLOR0;
				float4 tex         : TEXCOORD0;
				float4 tan         : TEXCOORD1;
				float4 ext         : TEXCOORD2;	

				half3  worldNormal : TEXCOORD3;
				half2  pos         : TEXCOORD4;
				half3  worldPos    : TEXCOORD5;
				float2 water       : TEXCOORD6;
				float2 hasfoam     : TEXCOORD7;
			};

			sampler2D _TexNoise;
			float4 _TexNoise_ST;
			sampler2D _ATLAS;
			float4 _ARRAY[8];

			float4 _TEXP;

			sampler2D _NormalMap;
			float4 _NormalMap_ST;
			sampler2D _RefMap;
			float4 _RefMap_ST;
			
			sampler2D _ReflectiveColor;
			float _RippleAmount;
			float _WaveSpeed;
		//	float _WaveWidth;



			sampler2D _WaveMap;
			sampler2D _NoiseMap;

			float _Range;
			float _WaveRange;
			float _NoiseRange;
			float _WaveDelta;



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
				
				float3 vn = mul(UNITY_MATRIX_M, v.normal.xyz);
				o.ext.xyz = vn;
				o.ext.w = o.vertex.z;


				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				float3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldPos    = worldPos;
				o.worldNormal = normalize(reflect(-worldViewDir, worldNormal));
				o.pos         = v.vertex.xy*0.06;
				o.water       = v.uv2.xy;
				o.hasfoam     = v.uv3.xy;
				return o;


			}

			fixed4 SampleAtlas(float4 uv, float4 rc) {
				//return (fixed4)tex2Dlod(_ATLAS, float4(frac(uv.xy)*rc.zw + rc.xy, 0, clamp(0,0,uv.w)));
				return (fixed4)tex2D(_ATLAS, frac(uv.xy)*rc.zw + rc.xy, ddx(uv*rc.zw + rc.xy).x, ddy(uv*rc.zw + rc.xy).y);
			}

			float4 desaturation(float4 c, float k)
			{
				float f = dot(c.xyz, float3(0.3, 0.59, 0.11));
				float3 cc = lerp(c.xyz, f.xxx, k);

				return float4(cc, c.w);
			}

			float3 calcNormal(float2 texcoord)
			{

				float2 p1 = texcoord+float2(0.484, 0.867);
				float2 p2 = texcoord+float2(0.685, 0.447);

				p1.y -= 0.01 * _Time.y;
				p2.y += 0.01 * _Time.y;

				p1 *= 2;
				p2 *= 2;

				float3 n1 = tex2D(_NormalMap, p1);
				float3 n2 = tex2D(_NormalMap, p2);

				float3 n = (n1 + n2)*0.5;
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
				
				
				float3 n = i.worldNormal;
				float3 v = normalize(i.worldPos.xyz - _WorldSpaceCameraPos.xyz);
				float3 norm = normalize(calcNormal(i.pos));
				
				float3 ref = normalize(mReflect(v, norm));
				float2 texCoord = ref.xy * 0.5 + 0.5;
				float4 color = tex2D(_RefMap, TRANSFORM_TEX(texCoord, _RefMap));
				color = desaturation(color,0.5);

				
				float shore = i.water.x;

				if (shore < _Range && i.hasfoam.x >0)
				{
					fixed4 noiseColor = tex2D(_NoiseMap, texCoord);
					fixed4 waveColor = tex2D(_WaveMap, float2(1 - min(_Range, shore) / _Range + _WaveRange*sin(_Time.x*_WaveSpeed + noiseColor.r*_NoiseRange), 1)*0.6);
					waveColor.rgb *= (1 - (sin(_Time.x*_WaveSpeed + noiseColor.r*_NoiseRange) + 1) / 2)*noiseColor.r;

					fixed4 waveColor2 = tex2D(_WaveMap, float2(1 - min(_Range, shore) / _Range + _WaveRange*sin(_Time.x*_WaveSpeed + _WaveDelta + noiseColor.r*_NoiseRange), 1)*0.6);
					waveColor2.rgb *= (1 - (sin(_Time.x*_WaveSpeed + _WaveDelta + noiseColor.r*_NoiseRange) + 1) / 2)*noiseColor.r;

					color = color + waveColor;// +waveColor2;
				}
			

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
				color = color*0.8;
				float4 terrainColor = cl[0] * i.clr.x + cl[1] * i.clr.y + cl[2] * i.clr.z + cl[3] * i.clr.w +
					cl[4] * i.tan.x + cl[5] * i.tan.y + cl[6] * i.tan.z + cl[7] * i.tan.w ;// color * i.clr.x*0.7 * (1 - alpha) + cl[7] * alpha;

				float terrainAlpha = (i.clr.x + i.clr.y + i.clr.z + i.clr.w + i.tan.x + i.tan.y + i.tan.z + i.tan.w);
				// 调这个值即可 范围0到正无穷
				float xxxxx = 1;

				float tempAlpha = pow(terrainAlpha, xxxxx);
				terrainColor *= tempAlpha;
				terrainAlpha *= tempAlpha;
				float4 waterColor = color*(1-terrainAlpha) + terrainColor;
				//waterColor.a = 0.6;
				return  waterColor;

			}
			ENDCG
		}
	}
}
