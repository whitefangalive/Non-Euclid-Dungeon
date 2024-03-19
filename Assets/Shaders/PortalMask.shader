Shader "Portals/PortalMask"
{
	Properties
	{
		_LeftEyeTexture("Left Eye Texture", 2D) = "white" {}
		_RightEyeTexture("Right Eye Texture", 2D) = "white" {}
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

				#pragma multi_compile __ STEREO_RENDER
				#pragma target 3.0

				sampler2D _LeftEyeTexture;
				sampler2D _RightEyeTexture;
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
					ZERO_INITIALIZE(v2f, o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert

					o.vertex = TransformObjectToHClip(v.vertex.xyz);
					o.screenPos = ComputeScreenPos(o.vertex);
					return o;
				}

				uniform sampler2D _MainTex;


				float4 frag(v2f i) : SV_Target
				{
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i); //Insert
					float2 uv = i.screenPos.xy / i.screenPos.w;
					float4 color;
					if (unity_StereoEyeIndex == 0)
						color = tex2D(_LeftEyeTexture, uv);
					else
						color = tex2D(_RightEyeTexture, uv);
					return color;
				}
			ENDHLSL
		}
	}
}
