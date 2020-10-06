Shader "Thermal Vision" {
	Properties {
		_MainTex       ("Main", 2D) = "white" {}
		_NoiseTex      ("Noise", 2D) = "white" {}
		_HeatLookupTex ("HeatLookup", 2D) = "white" {}
		_HotLight      ("Hot Light", Float) = 0.6
		_Rnd           ("Random", Vector) = (0.3, 0.7, 0, 0)
		_BlurAmount    ("Blur Amount", Float) = 0.5
		_Dimensions    ("Dimensions", Vector) = (0.3, 0.3, 0, 0)
	}
	CGINCLUDE
	#include "UnityCG.cginc"
	uniform sampler2D _MainTex, _NoiseTex, _HeatLookupTex;
	uniform float _HotLight, _BlurAmount;
	uniform float2 _Rnd, _Dimensions;

	float4 frag1 (v2f_img i) : SV_Target
	{
		float depth = Luminance(tex2D(_MainTex, i.uv).rgb);
		depth *= (depth * _HotLight);
		
		float heat = depth;

		float interference = -0.5 + tex2D(_NoiseTex, i.uv + float2(_Rnd.x, _Rnd.y));
		interference *= interference;
		interference *= 1 - heat;
		heat += interference;

		heat = max(0.005, min(0.995, heat));
		return tex2D(_HeatLookupTex, float2(heat, 0.0));
	}
	float4 frag2 (v2f_img i) : SV_Target
	{
		const float2 offsets[4] = 
		{
			-0.3,  0.4,
			-0.3, -0.4,
			0.3, -0.4,
			0.3,  0.4
		};
		float4 c = tex2D(_MainTex, i.uv);

		// glow
		float glow = 0.0113 * (2.0 - max(c.r, c.g));

		// blur
		for (int n = 0; n < 4; n++)
			c += tex2D(_MainTex, i.uv + _BlurAmount * glow * offsets[n]);
		c *= 0.25;
		
		// vignette
		float2 gradient = 0.5 - i.uv;
		gradient.x = gradient.x * (1 / _Dimensions.x);
		gradient.y = gradient.y * (1 / _Dimensions.y);
		float dist = length(gradient);
		c = lerp(c, float4(0, 0, 0, 1), dist);
		
		return c;
	}
	ENDCG
	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag1
			ENDCG
		}
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag2
			ENDCG
        }
    }
}
