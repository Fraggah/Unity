Shader "Custom/2DGlowEffect"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1, 1, 0, 1)
        _GlowIntensity ("Glow Intensity", Float) = 1
        _GlowWidth ("Glow Width", Float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _GlowColor;
            float _GlowIntensity;
            float _GlowWidth;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 texColor = tex2D(_MainTex, i.uv);

                // Calcular el contorno basado en el borde del sprite
                float edge = smoothstep(0.5 - _GlowWidth, 0.5, texColor.a);
                float glow = edge * _GlowIntensity;

                // Combinar el color base del sprite con el efecto de glow
                float4 glowColor = _GlowColor * glow;
                return texColor + glowColor;
            }
            ENDCG
        }
    }
}
