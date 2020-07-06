// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/UIBlur" {
    Properties{
        _Color("Main Color", Color) = (1,1,1,1)
        _Size("Size", Range(0, 20)) = 1
        _Passes("Passes", Range(4, 512)) = 4
		_Intensity("Intensity", Range(1, 10)) = 1
    }

        Category{

            // We must be transparent, so other objects are drawn before this one.
            Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }


            SubShader {

                // Horizontal blur
                GrabPass {
                    Tags { "LightMode" = "Always" }
                }
                Pass {
                    Tags { "LightMode" = "Always" }

                    CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag
                    #pragma fragmentoption ARB_precision_hint_fastest
                    #include "UnityCG.cginc"

                    struct appdata_t {
                        float4 vertex : POSITION;
                        float2 texcoord: TEXCOORD0;
                    };

                    struct v2f {
                        float4 vertex : POSITION;
                        float4 uvgrab : TEXCOORD0;
                    };

                    v2f vert(appdata_t v) {
                        v2f o;
                        o.vertex = UnityObjectToClipPos(v.vertex);
                        #if UNITY_UV_STARTS_AT_TOP
                        float scale = -1.0;
                        #else
                        float scale = 1.0;
                        #endif
                        o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
                        o.uvgrab.zw = o.vertex.zw;
                        return o;
                    }

                    sampler2D _GrabTexture;
                    float4 _GrabTexture_TexelSize;
                    float _Size;
                    float _Passes;
					float _Intensity;

                    half4 frag(v2f i) : COLOR {
                        //                  half4 col = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
                        //                  return col;

                                            half4 sum = half4(0,0,0,0);
                                            float measurment = 0;
                                            #define GRABPIXEL(weight,kernelx) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x + _GrabTexture_TexelSize.x * kernelx*_Size, i.uvgrab.y, i.uvgrab.z, i.uvgrab.w))) * weight
                                            for (float j = 0; j < _Passes; j++) {
												float w = (_Passes - j) / _Passes * _Intensity;
                                                sum += GRABPIXEL(w, _Size / _Passes * j);
                                                sum += GRABPIXEL(w, -(_Size / _Passes * j));
                                                measurment += 2;
                                            }

                                            return sum / measurment;
                                        }
                                        ENDCG
                                    }
        // Vertical blur
        GrabPass {
            Tags { "LightMode" = "Always" }
        }
        Pass {
            Tags { "LightMode" = "Always" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord: TEXCOORD0;
            };

            struct v2f {
                float4 vertex : POSITION;
                float4 uvgrab : TEXCOORD0;
            };

            v2f vert(appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                #if UNITY_UV_STARTS_AT_TOP
                float scale = -1.0;
                #else
                float scale = 1.0;
                #endif
                o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y * scale) + o.vertex.w) * 0.5;
                o.uvgrab.zw = o.vertex.zw;
                return o;
            }

            sampler2D _GrabTexture;
            float4 _GrabTexture_TexelSize;
            float _Size;
            float _Passes;
			float _Intensity;

            half4 frag(v2f i) : COLOR {
                //                  half4 col = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
                //                  return col;

                                    half4 sum = half4(0,0,0,0);
                                    float measurment = 0;
                                    #define GRABPIXEL(weight,kernely) tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(float4(i.uvgrab.x, i.uvgrab.y + _GrabTexture_TexelSize.y * kernely*_Size, i.uvgrab.z, i.uvgrab.w))) * weight
                                    //G(X) = (1/(sqrt(2*PI*deviation*deviation))) * exp(-(x*x / (2*deviation*deviation)))

                                    for (float j = 0; j < _Passes; j++) {
										float w = (_Passes - j) / _Passes * _Intensity;
                                        sum += GRABPIXEL(w, _Size / _Passes * j);
                                        sum += GRABPIXEL(w, -(_Size / _Passes * j));
                                        measurment += 2;
                                    }

                                    return sum / measurment;
                                }
                                ENDCG
                            }
            }
        }
}