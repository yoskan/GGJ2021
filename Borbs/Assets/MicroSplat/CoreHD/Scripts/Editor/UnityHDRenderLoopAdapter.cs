using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text;
using JBooth.MicroSplat;
using System.Collections.Generic;
using System.Linq;

namespace JBooth.MicroSplat
{
   public class UnityHDRenderLoopAdapter : IRenderLoopAdapter
   {
      static TextAsset template;
      static TextAsset adapter;
      static TextAsset sharedInc;
      static TextAsset terrainBody;
      static TextAsset terrainBlendBody;
      static TextAsset terrainBlendCBuffer;
      static TextAsset sharedHD;
      static TextAsset properties;
      static TextAsset vertex;
      static TextAsset mainFunc;
      static TextAsset templateDecal;
      static TextAsset vertMesh;
      static TextAsset pass_decal;
      static TextAsset pass_depthonly;
      static TextAsset pass_gbuffer;
      static TextAsset pass_forward;
      static TextAsset pass_lighttransport;


      public string GetDisplayName()
      {
         return "Unity HD";
      }

      public string GetRenderLoopKeyword()
      {
         return "_MSRENDERLOOP_UNITYHD";
      }

      public int GetNumPasses() { return 1; }

      public void WriteShaderHeader(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler compiler, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {
         sb.AppendLine("   SubShader {");


         sb.Append("      Tags{\"RenderPipeline\"=\"HDRenderPipeline\" \"RenderType\" = \"HDLitShader\" \"Queue\" = \"Geometry+100\" ");


         if (features.Contains("_MAX4TEXTURES"))
         {
            sb.Append("\"SplatCount\" = \"4\"");
         }
         else if (features.Contains("_MAX8TEXTURES"))
         {
            sb.Append("\"SplatCount\" = \"8\"");
         }
         else if (features.Contains("_MAX12TEXTURES"))
         {
            sb.Append("\"SplatCount\" = \"12\"");
         }
         else if (features.Contains("_MAX20TEXTURES"))
         {
            sb.Append("\"SplatCount\" = \"20\"");
         }
         else if (features.Contains("_MAX24TEXTURES"))
         {
            sb.Append("\"SplatCount\" = \"24\"");
         }
         else if (features.Contains("_MAX28TEXTURES"))
         {
            sb.Append("\"SplatCount\" = \"28\"");
         }
         else if (features.Contains("_MAX32TEXTURES"))
         {
            sb.Append("\"SplatCount\" = \"32\"");
         }
         else
         {
            sb.Append ("\"SplatCount\" = \"16\"");
         }
         sb.AppendLine("}");


      }

      public bool UseReplaceMethods()
      {
         return true;
      }

      public void WritePassHeader(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler compiler, int pass, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {

      }


      public void WriteVertexFunction(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler compiler, int pass, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {

      }

      public void WriteFragmentFunction(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler compiler, int pass, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {


      }

      public void WritePerMaterialCBuffer(string[] functions, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {

      }


      public void WriteShaderFooter(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler compiler, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader, string baseName)
      {
         sb.AppendLine("      }");
         if (auxShader != null && !string.IsNullOrEmpty (auxShader.customEditor))
         {
            sb.AppendLine ("      CustomEditor \"" + auxShader.customEditor + "\"");
         }
         else if (auxShader != null)
         {

         }
         else if (baseName != null)
         {
            if (features.Contains ("_MICROTERRAIN"))
            {
               sb.AppendLine ("   Dependency \"BaseMapShader\" = \"" + baseName + "\"");
            }
            sb.AppendLine ("   CustomEditor \"MicroSplatShaderGUI\"");
         }
         sb.AppendLine ("   Fallback \"Nature/Terrain/Diffuse\"");
         sb.Append ("}");
      }

      public void Init(string[] paths)
      {
         for (int i = 0; i < paths.Length; ++i)
         {
            string p = paths[i];
            if (p.EndsWith("microsplat_terrain_unityhd_template.txt"))
            {
               template = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_terrain_unityhd_template_decal.txt"))
            {
               templateDecal = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_terrain_unityhd_adapter.txt"))
            {
               adapter = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_terrainblend_body.txt"))
            {
               terrainBlendBody = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_terrainblend_cbuffer.txt"))
            {
                terrainBlendCBuffer = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }

            if (p.EndsWith("microsplat_terrain_body.txt"))
            {
               terrainBody = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_shared.txt"))
            {
               sharedInc = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_terrain_unityhd_shared.txt"))
            {
               sharedHD = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_terrain_unityhd_properties.txt"))
            {
               properties = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_terrain_unityhd_vertex.txt"))
            {
               vertex = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_terrain_unityhd_mainfunc.txt"))
            {
               mainFunc = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_unityhd_vertmesh.txt"))
            {
               vertMesh = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_unityhd_pass_depthonly.txt"))
            {
               pass_depthonly = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_unityhd_pass_forward.txt"))
            {
               pass_forward = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_unityhd_pass_gbuffer.txt"))
            {
               pass_gbuffer = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_unityhd_pass_lighttransport.txt"))
            {
               pass_lighttransport = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }
            if (p.EndsWith("microsplat_unityhd_pass_decal.txt"))
            {
               pass_decal = AssetDatabase.LoadAssetAtPath<TextAsset>(p);
            }

         }
      }

      public void WriteProperties(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {
         sb.AppendLine(properties.text);
      }

      public void PostProcessShader(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler compiler, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {
         StringBuilder temp = new StringBuilder();
         compiler.WriteFeatures(features, temp);
         if (auxShader != null && auxShader.trigger == "_TERRAINBLENDING")
         {
            temp.AppendLine("      #define _SRPTERRAINBLEND 1");
         }

         StringBuilder cbuffer = new StringBuilder();
         compiler.WritePerMaterialCBuffer(features, cbuffer);
         if (auxShader != null && auxShader.trigger == "_TERRAINBLENDING")
         {
             cbuffer.AppendLine(terrainBlendCBuffer.text);
         }

         sb = sb.Replace("//MS_DEFINES", temp.ToString());

         sb = sb.Replace("//MS_ADAPTER", adapter.ToString());
         sb = sb.Replace("//MS_SHARED_INC", sharedInc.text);
         sb = sb.Replace("//MS_SHARED_HD", sharedHD.text);
         sb = sb.Replace("//MS_TERRAIN_BODY", terrainBody.text);
         sb = sb.Replace("//MS_VERTEXMOD", vertex.text);
         sb = sb.Replace("//MS_MAINFUNC", mainFunc.text);
         sb = sb.Replace("//MS_CBUFFER", cbuffer.ToString());
         sb = sb.Replace("//MS_PASS_DEPTHONLY", vertMesh.text + "\n" + pass_depthonly.text);
         sb = sb.Replace("//MS_PASS_GBUFFER", vertMesh.text + "\n" + pass_gbuffer.text);
         sb = sb.Replace("//MS_PASS_FORWARD", vertMesh.text + "\n" + pass_forward.text);
         sb = sb.Replace("//MS_PASS_LIGHTTRANSPORT", vertMesh.text + "\n" + pass_lighttransport.text);
         sb = sb.Replace("//MS_PASS_DECAL", vertMesh.text + "\n" + pass_decal.text);

         if (auxShader != null && auxShader.trigger == "_TERRAINBLENDING")
         {
            sb = sb.Replace("//MS_BLENDABLE", terrainBlendBody.text);
            sb = sb.Replace("Blend [_SrcBlend] [_DstBlend], [_AlphaSrcBlend] [_AlphaDstBlend]", "Blend SrcAlpha OneMinusSrcAlpha");
         }

         // extentions
         StringBuilder ext = new StringBuilder();
         compiler.WriteExtensions(features, ext);

         sb = sb.Replace("//MS_EXTENSIONS", ext.ToString());

         ext = new StringBuilder();
         foreach (var e in compiler.extensions)
         {
            e.WriteAfterVetrexFunctions(ext);
         }

         sb = sb.Replace("//MS_AFTERVERTEX", ext.ToString());

            // HD fixup
         sb = sb.Replace("fixed", "half");
         sb = sb.Replace("unity_ObjectToWorld", "GetObjectToWorldMatrix()");
         sb = sb.Replace("unity_WorldToObject", "GetWorldToObjectMatrix()");
         sb = sb.Replace("_ObjectToWorld", "GetObjectToWorldMatrix()");
         sb = sb.Replace("_WorldToObject", "GetWorldToObjectMatrix()");

         sb = sb.Replace("UNITY_MATRIX_M", "GetObjectToWorldMatrix()");
         sb = sb.Replace("UNITY_MATRIX_I_M", "GetWorldToObjectMatrix()");
         sb = sb.Replace("UNITY_MATRIX_VP", "GetWorldToHClipMatrix()");
         sb = sb.Replace("UNITY_MATRIX_V", "GetWorldToViewMatrix()");
         sb = sb.Replace("UNITY_MATRIX_P", "GetViewToHClipMatrix()");

         if (features.Contains("_USESPECULARWORKFLOW"))
         {
            sb = sb.Replace("// #define _MATERIAL_FEATURE_SPECULAR_COLOR 1", "#define _MATERIAL_FEATURE_SPECULAR_COLOR 1");
         }

         if (features.Contains("_TESSDISTANCE"))
         {
            sb = sb.Replace("#pragma vertex Vert", "#pragma hull hull\n#pragma domain domain\n#pragma vertex tessvert\n#pragma require tessellation tessHW\n");
            
         }


      }

      public void WriteSharedCode(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler compiler, int pass, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {
         if (auxShader != null && auxShader.trigger == "_TERRAINBLENDING")
         {
            sb.AppendLine(templateDecal.text);
         }
         else
         {
            sb.AppendLine(template.text);
         }
      }

      public void WriteTerrainBody(string[] features, StringBuilder sb, MicroSplatShaderGUI.MicroSplatCompiler compiler, int pass, MicroSplatShaderGUI.MicroSplatCompiler.AuxShader auxShader)
      {


      }


      public string GetVersion()
      {
         return "3.6";
      }
   }
}
