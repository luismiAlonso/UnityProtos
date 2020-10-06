Shader "FX/ThermalDrome"
{
    Properties
	{
		_Matcap("Matcap", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_Strength("Tinting Strength", Range(-2,2)) = 0.4 
	}

	SubShader
	{		
		Tags
		{
			"Queue" = "Geometry"
			"RenderType" = "Transparent"
			"ThermalVision" = "ThermalColors"
		}

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float2 cap : TEXCOORD2;
			};

			sampler2D _Mask, _Matcap;
			float _Strength;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
							
				float3 vertexN = normalize(v.vertex);
					// get vector perpendicular to both vertex and normal
				float3 viewCross = cross(vertexN, v.normal);
				
				o.cap = viewCross.xy * 0.5 + 0.5;	
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// Mask
				fixed4 m = tex2D(_Mask, i.uv);
				
				// take out masked area
				i.cap *= (1 - (m.g* _Strength));
				// color texture over cap
				fixed4 color = tex2D(_Matcap, i.cap);
				return color;

			}

			ENDCG
		}
	}
}
