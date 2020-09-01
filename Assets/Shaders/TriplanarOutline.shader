Shader "Custom/Outlined/Triplanar"
{
	Properties
	{
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}

		_MapScale("Map Scale", Float) = 1

		_FirstOutlineColor("Outline color", Color) = (1,0,0,0.5)
		_FirstOutlineWidth("Outlines width", Range(0.0, 2.0)) = 0.15

		_SecondOutlineColor("Outline color", Color) = (0,0,1,1)
		_SecondOutlineWidth("Outlines width", Range(0.0, 2.0)) = 0.025

		_Angle("Switch shader on angle", Range(0.0, 180.0)) = 89
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata {
		float4 vertex : POSITION;
		float4 normal : NORMAL;
	};

	half4 _Color;
	sampler2D _MainTex;
	half _MapScale;

	uniform float4 _FirstOutlineColor;
	uniform float _FirstOutlineWidth;

	uniform float4 _SecondOutlineColor;
	uniform float _SecondOutlineWidth;
	uniform float _Angle;

	ENDCG

	SubShader
	{
		//First outline
		Pass{
		Tags{ "Queue" = "Geometry" }
		Cull Front
		CGPROGRAM

		struct v2f {
		float4 pos : SV_POSITION;
	};

#pragma vertex vert
#pragma fragment frag

	v2f vert(appdata v) {
		appdata original = v;

		float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
		//This shader consists of 2 ways of generating outline that are dynamically switched based on demiliter angle
		//If vertex normal is pointed away from object origin then custom outline generation is used (based on scaling along the origin-vertex vector)
		//Otherwise the old-school normal vector scaling is used
		//This way prevents weird artifacts from being created when using either of the methods
		if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _Angle) {
			v.vertex.xyz += normalize(v.normal.xyz) * _FirstOutlineWidth;
		}
		else {
			v.vertex.xyz += scaleDir * _FirstOutlineWidth;
		}

		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		return o;
	}

	half4 frag(v2f i) : COLOR{
		float4 color = _FirstOutlineColor;
		color.a = 1;
		return color;
	}
		ENDCG
	}

			Tags{ "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf Standard vertex:vert fullforwardshadows addshadow
			#pragma target 3.0

			struct Input
			{
				float3 localCoord;
				float3 localNormal;
			};

			void vert(inout appdata_full v, out Input data)
			{
				UNITY_INITIALIZE_OUTPUT(Input, data);
				data.localCoord = v.vertex.xyz;
				data.localNormal = v.normal.xyz;
			}	

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				// Blending factor of triplanar mapping
				float3 bf = normalize(abs(IN.localNormal));
				bf /= dot(bf, (float3)1);

				// Triplanar mapping
				float2 tx = IN.localCoord.yz * _MapScale;
				float2 ty = IN.localCoord.zx * _MapScale;
				float2 tz = IN.localCoord.xy * _MapScale;

				// Base color
				half4 cx = tex2D(_MainTex, tx) * bf.x;
				half4 cy = tex2D(_MainTex, ty) * bf.y;
				half4 cz = tex2D(_MainTex, tz) * bf.z;
				half4 color = (cx + cy + cz) * _Color;
				o.Albedo = color.rgb;
				o.Alpha = color.a;
			}
		ENDCG
	}
	Fallback "Diffuse"
}
