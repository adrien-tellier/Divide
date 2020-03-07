// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Fov"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_FirstColor("DeathZoneColor", Color) = (1, 0, 0, 1)
		_SecondColor("LifeZoneColor", Color) = (1, 1, 0, 1)
		_Distance("First Distance", Float) = 1
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}


		CGPROGRAM
		#pragma surface surf Lambert alpha
		//#pragma surface surf NoLighting alpha

		sampler2D _MainTex;
		half4 _distanceColor;
		float _Distance;
		half4 _FirstColor;
		half4 _SecondColor;

		struct Input
		{
			float2 uv_MainTex;
			float3 worldPos;
		};


		void surf(Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			float dist = distance(mul(unity_ObjectToWorld, float4(0.0, 0.0, 0.0, 1.0)), IN.worldPos);

			if (dist < _Distance)
				_distanceColor = _FirstColor;
			else
				_distanceColor = _SecondColor;
			
			o.Albedo = c.rgb * _distanceColor.rgb;
			o.Alpha = _distanceColor.a;
		}

	ENDCG
	}
}