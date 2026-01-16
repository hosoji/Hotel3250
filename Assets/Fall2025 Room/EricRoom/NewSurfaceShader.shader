Shader "Custom/CoolWave"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Base Color", Color) = (1,1,1,1)
        _WaveColor ("Wave Color", Color) = (0,1,0,1)
        _Speed ("Wave Speed", Range(0,5)) = 1
        _Strength ("Wave Strength", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 150

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
        fixed4 _Color;
        fixed4 _WaveColor;
        float _Speed;
        float _Strength;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);

            // the waves moves in the sharder using sin 
            float wave = sin((IN.uv_MainTex.x + _Time.y * _Speed) * 6.28);

            // combines both wavecolor and wave.
            fixed3 finalColor = lerp(tex.rgb * _Color.rgb, _WaveColor.rgb, wave * _Strength);

            o.Albedo = finalColor; 
            o.Metallic = 0;
            o.Smoothness = 0.3;
            o.Alpha = 1;
        }
        ENDCG
    }
}
