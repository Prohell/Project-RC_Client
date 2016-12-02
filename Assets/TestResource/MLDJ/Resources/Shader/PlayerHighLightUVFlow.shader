Shader "Cabal/Player/PlayerHighLightUVFlow" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Main Color", Color) = (0.88,0.88,0.88,1)

		_HighLightColor ("High Light", Color) = (0.6,0.6,0.6,1)
		_PowValue("Pow Value" , float) = 0
		_LightColor("Light Color" , Color) = (1,1,1,1)
		_LightDir("Light Direction" , Vector) = (0,0,0,0)

		_UVTex ("UV Texture", 2D) = "black" {}
		_UVAlpha("UV Alpha" , Float) = 25.0
		_UVColor ("UV Color ", Color) = (1, 1, 1, 1)
		_uvXSpeed ("X UV Speed", Float) = 0.0
		_uvYSpeed("Y UV Speed", Float) = 1.0

		_FlowColor ("Flow Color", Color) = (0.5, 0.5, 0.5, 1)
		_FlowSize("Flow Size", Float) = 12.0
		_FlowAlpha("Flow Alpha" , Range(0,1)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque""Queue" = "Transparent-9" }
		Blend SrcAlpha ONeMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf SimpleLambert noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nofog nometa noforwardadd

		sampler2D _MainTex;
		fixed4 _Color;

		fixed4 _HighLightColor;
		half _PowValue;
		fixed4 _LightColor;
		half4 _LightDir;

		sampler2D _UVTex;
		half _UVAlpha;
		fixed4 _UVColor;
		half _uvXSpeed;
		half _uvYSpeed;

		half4 _CurrentLightDirection;
		half4 _FlowColor;
		half _FlowSize;
		half _FlowAlpha;

		half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL = s.Specular;
			half3 temp = s.Albedo * (_HighLightColor.rgb + _LightColor.rgb) * (NdotL * pow(NdotL , _PowValue)  *1.5);
			temp += s.Albedo * min(_PowValue/10,0.3);

			half nl = max(0, dot(s.Normal, _CurrentLightDirection.xyz));			
			temp += pow(nl, _FlowSize) * _FlowColor.rgb * 4 * _FlowAlpha;

			half4 c;
			c.rgb = temp;
			c.a = s.Alpha;
	         
			return c;		
		}

		struct Input
		{
			half2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			float2 uv = IN.uv_MainTex;
			uv.x += _Time.x * _uvXSpeed;
			uv.y += _Time.x * _uvYSpeed;
			fixed4 flow = tex2D(_UVTex, uv);

     		fixed4 texColor = tex2D(_MainTex , IN.uv_MainTex) * _Color;
			o.Albedo = texColor.rgb + flow.rgb * _UVColor.rgb * _UVAlpha;
			o.Specular = max(dot(normalize(o.Normal) , normalize(_LightDir.xyz)),0.001);
			o.Emission = texColor.rgb;
			o.Alpha = texColor.a;
		}
		ENDCG
	}
}
