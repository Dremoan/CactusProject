Shader "Custom/WaterFade"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_Speed ("Speed" , float) = 1		
		_Distance ("Distance" , float) = 1
		_Amplitude ( "Amplitude" , float) = 1
		_Amount ("Amount" , Range(0,1) ) = 1 
		_DispTex ("Disp", 2D) = "white" {}
		
		_HeightMap ("WaveHeight",2D) = "white"
		

		

		
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				
				
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _DispTex;
			sampler2D _HeightMap;
			float4 _MainTex_ST;
			float4 _DispTex_ST;

			float _Speed;
			float _Amount;
			float _Amplitude;
			float _Distance;
			
			uniform half2 _Offset;

			v2f vert (appdata v)
			{
				v2f o;
				
				v.vertex.y += sin(_Time.y * _Speed + v.vertex.x * _Amplitude ) *_Distance * _Amount;
				v.vertex.x += sin(_Time.y * _Speed + v.vertex.y * _Amplitude ) *_Distance * _Amount;
				
				   
				
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
				

				
				
				return o;

				
				
			}

			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture

				fixed2 displacement1 = tex2D( _DispTex, i.uv.xy );	
				
				fixed2 heightDisp = tex2D( _HeightMap, i.uv.xy );
				
				float2 adjusted = i.uv.xy + (sin((displacement1.rg-0.5)/60)*2);			
				float alpha = heightDisp.rg;

				fixed4 col = tex2D(_MainTex, adjusted);
				
				col.a = (step(1,col.a)*(1 - i.uv.y));//*alpha;
				

				return col;
			}
			ENDCG
		}
	}
}
