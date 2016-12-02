Shader "Cabal/Weapon" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_FlowTex("UV Texture", 2D) = "black" {}  //流光贴图
	_uvaddspeedX  ("uvaddspeedX",float) = 0                    //流光速度x
	_uvaddspeedY  ("uvaddspeedY",float) = 0.1  //y
	_UVColor ("UV Color ", Color) = (1, 1, 1, 1)
//	_UVAlpha("UV Alpha" , Float) = 25.0
 	}

SubShader {
	Tags {"Queue"="Transparent-9" "IgnoreProjector"="True" "RenderType"="Transparent"}
	//ZWrite Off
	Alphatest Greater 0.5
	Blend SrcAlpha OneMinusSrcAlpha 
	CGPROGRAM
    #pragma surface surf NoLight
          half4 LightingNoLight (SurfaceOutput s, half3 lightDir, half atten) {
          half4 c;
          c.rgb = s.Albedo;
          c.a = s.Alpha;
          return c;
      }
    fixed4 _Color;
    sampler2D _MainTex;
	sampler2D _FlowTex;
	half _uvaddspeedX;  
	half _uvaddspeedY;
//	half _UVAlpha;
	fixed4 _UVColor;  
	struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			float2 uv =IN.uv_MainTex; //计算流光UV
			uv.x/=2;
			uv.x+=_Time.y*_uvaddspeedX;
			uv.y+=_Time.y*_uvaddspeedY;
			
			float flow = tex2D (_FlowTex, uv); //liu guang liang du
			
			o.Albedo = (c.rgb*_Color )+ _UVColor.rgb*flow;
			//o.Emission = c.rgb + float3(flow,flow,flow);
//			fixed4 texColor = tex2D(_MainTex , IN.uv_MainTex) * _Color;
//			o.Albedo = texColor.rgb * flow.rgb * _UVColor.rgb ;
			o.Alpha = c.a;
//			o.Emission = texColor.rgb;
//			o.Alpha = texColor.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}