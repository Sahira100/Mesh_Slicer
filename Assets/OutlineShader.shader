 Shader "Unlit/OutlineShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", float) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Transparent"  "Queue"="Transparent" }

        //

        Pass
        {
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal:NORMAL;

            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz +=v.normal*_OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
               float4 col = tex2D(_MainTex, i.uv);
               return col*_OutlineColor;
            }
            ENDCG
        }

        //

        Tags { "RenderType"="Opaque" }

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
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
               float4 col = tex2D(_MainTex, i.uv);
               return col*_Color;

            }
            ENDCG
        }
    }
}
