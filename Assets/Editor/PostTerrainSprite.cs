using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// 导入地形图片使用，自动设置图片的格式和Atlas名称
/// 
/// by TT
/// </summary>
public class PostTerrainSprite : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        // Only post process textures if they are in a folder
        // "Terrain" or a sub folder of it.
        if (assetPath.IndexOf("/HexTerrain/") == -1)
            return;
        string AtlasName = new DirectoryInfo(Path.GetDirectoryName(assetPath)).Name;
        TextureImporter textureImporter = assetImporter as TextureImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spritePackingTag = AtlasName;
        textureImporter.mipmapEnabled = true;
    }
}