/**************************************
说明：
1. MainTex : 水面贴图
2. Bump ： 水面法线贴图
3. Cube ： 天空盒
4. LightDirection ： 光线的方向
5. LightColor ： 光的颜色， 另外水的透明度可由LightColor的alpha值设置
6. WaveDirection ： 水流的速度（大小和方向）,xy是波1，zw是波2
7. Reflect ： 水面的反射度，值越大，天空盒越不明显
8. Fog Color： 雾的颜色
9. Fog Near：雾的范围
10. Fog Far： 雾的范围

修改，针对部分安卓机（如小米3）显示异常进行调整，在保证原显示效果的基础上，
将计算uv值的过程放入vertex函数中，修改雾颜色计算方法
**************************************/

Shader "Water/Water_wave" {
	Properties {
	    _Shininess ("Shininess", Range (0.03, 1)) = 0.078125
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_Bump ("Normalmap", 2D) = "bump" {}
		_Cube("Environment Map", Cube) = ""{}
		_LightDirection("Light Direction", vector) = (1,1,1,1)
		_LightColor("Light Color",Color)=(1,1,1,1)
		_WaveDirection("Wave Direction", vector) =(1,0,0,0)
		_Reflect("reflect",range(0,1))=0.5
		_FogColor("Fog Color", Color) = (1,1,1,1)
		_FogNear("Fog Near", float) =1
		_FogFar("Fog Far",float) = 300
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent+1"}
		Blend SrcAlpha OneMinusSrcAlpha
		LOD 200
		Pass {
		    Name "FORWARD"
			Tags { "LightMode" = "ForwardBase" }		
			ZTest LEqual
			ZWrite Off
			Cull Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag		
			#include "UnityCG.cginc"
					
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _Bump;
			samplerCUBE _Cube;
			half _Shininess;
			float3 _LightDirection;
			float4 _LightColor;
			fixed _Reflect;
			fixed _FogNear;
			fixed _FogFar;
			fixed4 _FogColor;
			fixed4 _WaveDirection;
			
			struct v2f {
			    fixed4 pos : SV_POSITION;
				fixed4 pack0 : TEXCOORD0;
				fixed2 pack1 : TEXCOORD1;
				fixed3 lightDir : TEXCOORD2;
				fixed4 viewDir : TEXCOORD3;
		   };
		   	struct vertexInput{
			    fixed4 vertex : POSITION;
				fixed4 tangent : TANGENT;
				fixed3 normal : NORMAL;
				fixed2 texcoord : TEXCOORD0;
			};
			v2f vert(vertexInput v){
			    v2f o = (v2f)0;
				TANGENT_SPACE_ROTATION;				
				o.viewDir.xyz = mul(rotation,WorldSpaceViewDir(v.vertex));	
				fixed  depth = length(mul (UNITY_MATRIX_MV, v.vertex).xyz);//获取到相机的距离			
				o.viewDir.w = saturate((_FogFar-depth)/(_FogFar-_FogNear));//计算fog factor
				o.lightDir = mul(rotation,_LightDirection);
				o.pack1.xy =TRANSFORM_TEX(v.texcoord, _MainTex);
				//扰动
				o.pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex)+frac(_Time.x*0.01*_WaveDirection.xy);  
				o.pack0.zw = TRANSFORM_TEX(v.texcoord,_MainTex)+frac(_Time.x*0.01*_WaveDirection.zw)+0.3;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);//获取位置
				return o;
			}

			fixed4 frag(v2f IN):COLOR
			{
				fixed3 bump1 = tex2D(_Bump, IN.pack0.xy)*2-1;
				fixed3 bump2 = tex2D(_Bump, IN.pack0.zw)*2-1;
				fixed3 bump = normalize((bump1+bump2)*0.5);//叠加扰动

				fixed diff = max(0,dot(bump,IN.lightDir));//漫反射

				fixed3 h = normalize(IN.lightDir+IN.viewDir.xyz);
				fixed nh = max(0,dot(bump,h));//半角向量
				fixed spec = pow(nh,_Shininess*200);//高光

			    fixed4 baseColor = tex2D(_MainTex, (IN.pack1.xy+bump.xy));//贴图颜色
				fixed4 envColor =texCUBE(_Cube, reflect(IN.viewDir.xyz,bump));//环境色
				fixed4 specColor = _LightColor*spec;
				fixed fogStrength = 1-IN.viewDir.w;

				fixed4 finalColor;
				finalColor = _LightColor*(baseColor*(_Reflect)+envColor*(1-_Reflect))+specColor;//+_LightColor.rgb*spec;
				finalColor = lerp(finalColor, _FogColor, fogStrength);
				finalColor.a = _LightColor.a;
				return finalColor;
			}
ENDCG
		}
	} 
	FallBack "Diffuse"
}
