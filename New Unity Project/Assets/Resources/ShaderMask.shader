Shader "Custom/AlphaMask" {
   Properties 
    {
    _Color ("Main Color", Color) = (1,1,1,1)
	_Offset ("Offset", Range(-1,1)) = 0
    _MainTex2 ("Base (RGB) Trans (A)", 2D) = "black" {}
    _MaskTex ("Mask (A)", 2D) = "white" {}
    _Progress ("Progress", Range(0,1)) = 0.5
    }

    Category 
    {
        Lighting Off
        ZWrite Off
        Cull back
        Fog { Mode Off }
        Tags {"Queue"="Transparent" "IgnoreProjector"="True"}
        Blend SrcAlpha OneMinusSrcAlpha
        SubShader 
        {
            Pass 
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
             
                sampler2D _MainTex2;
                sampler2D _MaskTex;
                fixed4 _Color;
                float _Progress;
                float _Offset;
                struct appdata
                {
                    float4 vertex : POSITION;
                    float4 texcoord : TEXCOORD0;
                };
                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };
                v2f vert (appdata v)
                {
                    v2f o;
                    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                    o.uv = v.texcoord.xy;
                    return o;
                }
                half4 frag(v2f i) : COLOR
                {
                	float2 offset = float2(_Offset, 0);
                    fixed4 c = _Color * tex2D(_MainTex2, i.uv + offset);
                    //fixed ca = tex2D(_MaskTex, i.uv).a;
                   // c.a = ca;
                    return c;
                }
                ENDCG
            }
        }

        SubShader 
        {           
             AlphaTest LEqual [_Progress]  
              Pass  
              {  
                 SetTexture [_MaskTex] {combine texture}  
                 SetTexture [_MainTex2] {combine texture, previous}  
              }  
        }
        
    }
    Fallback "Transparent/VertexLit"
}