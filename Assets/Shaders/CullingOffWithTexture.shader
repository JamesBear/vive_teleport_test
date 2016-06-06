Shader "Unlit/CullingOffWithTexture" {
	//Copied from: http://answers.unity3d.com/questions/176487/materialtexture-on-the-inside-of-a-sphere.html
	//Merged with: http://docs.unity3d.com/Manual/SL-SurfaceShaderExamples.html

	Properties{
		_MainTex("Texture", 2D) = "white" {}
	}

	SubShader{

		Tags{ "RenderType" = "Opaque" }

		Cull Front

		CGPROGRAM

#pragma surface surf Lambert vertex:vert
	struct Input {
		float2 uv_MainTex;
		float4 color : COLOR;
	};
	sampler2D _MainTex;

	void vert(inout appdata_full v)
	{
		//v.normal.xyz = v.normal * -1;
	}

	void surf(Input IN, inout SurfaceOutput o) {
		//o.Albedo = 1;
		o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
	}

	ENDCG

	}

		Fallback "Diffuse"

}
