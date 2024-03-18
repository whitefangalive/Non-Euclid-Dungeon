Shader "Portals/PortalMask"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
			"Queue" = "Geometry"
			"RenderPipeline" = "UniversalPipeline"
		}

		HLSLINCLUDE
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		ENDHLSL

		Pass
		{
			Name "Mask"

			Stencil
			{
				Ref 1
				Pass replace
			}

			HLSLPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				struct appdata
				{
					float4 vertex : POSITION;

					UNITY_VERTEX_INPUT_INSTANCE_ID //Insert
				};

				struct v2f
				{
					float4 vertex : SV_POSITION;
					float4 screenPos : TEXCOORD0;

					UNITY_VERTEX_OUTPUT_STEREO //Insert
				};

				v2f vert(appdata v)
				{
					v2f o;

					UNITY_SETUP_INSTANCE_ID(v); //Insert
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert

					o.vertex = TransformObjectToHClip(v.vertex.xyz);
					o.screenPos = ComputeScreenPos(o.vertex);
					return o;
				}

				uniform sampler2D _MainTex;

				float4 frag(v2f i) : SV_Target
				{
					float2 uv = i.screenPos.xy / i.screenPos.w;
					float4 col = tex2D(_MainTex, uv);
					return col;
				}
			ENDHLSL
		}
	}
}
