﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace SceneExport{
	public partial class Exporter{
		[System.Serializable]
		public class JsonTexture{		
			public string name;
			public int id = -1;
			public string path;
			public string filterMode;
			public float mipMapBias = 0.0f;
			public int width = 0;
			public int height = 0;
			public string wrapMode;
			public bool isTex2D = false;
			public bool isRenderTarget = false;
			public bool alphaTransparency = false;
			public float anisoLevel = 0.0f;
			public string base64;
			public bool sRGB = true;
			public string textureType = "default";
			public bool normalMapFlag = false;

			public Texture textureRef = null;			
			public JsonTexture(Texture tex, Exporter exp){
				name = tex.name;
				id = exp.textures.findId(tex);
				var assetPath = AssetDatabase.GetAssetPath(tex);
				exp.registerResource(assetPath);
				path = assetPath;
				filterMode = tex.filterMode.ToString();
				width = tex.width;
				height = tex.height;
				wrapMode = tex.wrapMode.ToString();				
				var tex2D = tex as Texture2D;
				var rendTarget = tex as RenderTexture;
				isTex2D = tex2D != null;
				isRenderTarget = rendTarget != null;
				var importer = AssetImporter.GetAtPath(assetPath);
				var texImporter = (TextureImporter)importer;
				if (isTex2D){
					alphaTransparency = tex2D.alphaIsTransparency;
				}
				if (isRenderTarget){
					anisoLevel = rendTarget.anisoLevel;
				}
				if (texImporter){
					sRGB = texImporter.sRGBTexture;
					textureType = texImporter.textureType.ToString();
					normalMapFlag = (texImporter.textureType == TextureImporterType.NormalMap);
				}
				textureRef = tex;
			}
		}
	}
}