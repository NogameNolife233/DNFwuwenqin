// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.35 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.35;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33005,y:32712,varname:node_3138,prsc:2|alpha-9455-OUT,refract-4618-OUT;n:type:ShaderForge.SFN_Tex2d,id:4456,x:32129,y:32665,ptovrint:False,ptlb:NormalMap,ptin:_NormalMap,varname:node_4456,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1fcfe9d793612da46bd174df7395d4bf,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Multiply,id:4618,x:32828,y:33012,cmnt:Refraction,varname:node_4618,prsc:2|A-9878-OUT,B-5421-OUT;n:type:ShaderForge.SFN_Slider,id:1396,x:31904,y:33082,ptovrint:False,ptlb:RefractionScale,ptin:_RefractionScale,varname:node_1396,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_ComponentMask,id:9878,x:32618,y:32845,varname:node_9878,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3547-OUT;n:type:ShaderForge.SFN_Vector1,id:9455,x:32828,y:32870,varname:node_9455,prsc:2,v1:0;n:type:ShaderForge.SFN_VertexColor,id:7479,x:32034,y:33167,varname:node_7479,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9740,x:32264,y:33230,varname:node_9740,prsc:2|A-1396-OUT,B-7479-A;n:type:ShaderForge.SFN_SwitchProperty,id:5421,x:32471,y:33210,ptovrint:False,ptlb:ParticleControl,ptin:_ParticleControl,varname:node_5421,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1396-OUT,B-9740-OUT;n:type:ShaderForge.SFN_Lerp,id:3547,x:32374,y:32646,varname:node_3547,prsc:2|A-1452-OUT,B-4456-RGB,T-9100-RGB;n:type:ShaderForge.SFN_Vector3,id:1452,x:32129,y:32507,varname:node_1452,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Tex2d,id:9100,x:32129,y:32877,ptovrint:False,ptlb:MaskMap,ptin:_MaskMap,varname:node_9100,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:4456-9100-1396-5421;pass:END;sub:END;*/

Shader "GG/GGFX_SpaceDistort_01" {
    Properties {
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _MaskMap ("MaskMap", 2D) = "white" {}
        _RefractionScale ("RefractionScale", Range(0, 2)) = 1
        [MaterialToggle] _ParticleControl ("ParticleControl", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform float _RefractionScale;
            uniform fixed _ParticleControl;
            uniform sampler2D _MaskMap; uniform float4 _MaskMap_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(i.uv0, _NormalMap)));
                float4 _MaskMap_var = tex2D(_MaskMap,TRANSFORM_TEX(i.uv0, _MaskMap));
                float2 node_9878 = lerp(float3(0,0,1),_NormalMap_var.rgb,_MaskMap_var.rgb).rg;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (node_9878*lerp( _RefractionScale, (_RefractionScale*i.vertexColor.a), _ParticleControl ));
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                return fixed4(lerp(sceneColor.rgb, finalColor,0.0),1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
