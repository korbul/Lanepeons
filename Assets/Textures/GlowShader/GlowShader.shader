Shader "Unlit/GlowShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_UvTex ("Texture", 2D) = "white" {}
		_MaskTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
		LOD 100

		Pass
		{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2: TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _UvTex;
			float4 _UvTex_ST;
			sampler2D _MaskTex;
			float4 _MaskTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.uv, _UvTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 uv = tex2D(_UvTex, i.uv );
				fixed4 col = tex2D(_MainTex, uv.xy * i.uv2 - fixed2(_Time.x * 0.5f,0));
				fixed4 mask = tex2D(_MaskTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				col.a = mask.r * col.r * 4;
				col.g = 1;
				col.rgb *= 1.5;
				return col;
			}
			ENDCG
		}
	}
}
