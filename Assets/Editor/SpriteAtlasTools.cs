using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;
using System.Collections.Generic;
using System.IO;

[ExecuteInEditMode]
public class SpritePackerTools
{
    static List<Sprite> spriteList = new List<Sprite>();
    [MenuItem("ProjectRC/Map Tools/ExportTerrainConfig", false, 1)]
    static void Pack()
    {
        GetSpriteList();
        if (!spriteList[0].packed)
        {
            Packer.RebuildAtlasCacheIfNeeded(EditorUserBuildSettings.activeBuildTarget);
        }
        SaveSubTextureInfo();
        SaveAltasTexture();
    }

    static void GetSpriteList()
    {
        // List<Sprite> spriteList = new List<Sprite>();
        if (spriteList == null)
        {
            spriteList = new List<Sprite>();
        }
        spriteList.Clear();
        DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "//Art//Map//Textures//Terrain");
        foreach (FileInfo jpgFile in rootDirInfo.GetFiles("*.jpg", SearchOption.AllDirectories))
        {
            string allPath = jpgFile.FullName;
            string assetPath = allPath.Substring(allPath.IndexOf("Assets"));
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

            spriteList.Add(sprite);
        }
    }
    static void SaveAltasTexture()
    {
        Texture2D altasTexture = SpriteUtility.GetSpriteTexture(spriteList[0], true);
        byte[] data = altasTexture.GetRawTextureData();
        string filename = Application.dataPath + "/Resources/" + MapPrefabDef.MapAtlas + ".bytes";

        FileStream file = new FileStream(filename, FileMode.Create);
        file.Write(data, 0, data.Length);
        file.Close();
    }
    static void SaveSubTextureInfo()
    {
        Texture2D altasTexture = SpriteUtility.GetSpriteTexture(spriteList[0], true);

        string filename = Application.dataPath + "/Resources/" + MapPrefabDef.MapTerrainSprData + ".txt";

        StreamWriter sw;
        FileInfo t = new FileInfo(filename);
        if (!t.Exists)
        {
            //如果此文件不存在则创建  
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在则打开  
            t.Delete();
            sw = t.CreateText();
        }

        byte[] altasdata = altasTexture.GetRawTextureData();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendFormat(" {0}", altasTexture.name);
        sb.AppendFormat(" {0}", altasTexture.width);
        sb.AppendFormat(" {0}", altasTexture.height);
        sb.AppendFormat(" {0}", (int)altasTexture.format);
        sb.AppendFormat(" {0}", altasdata.Length);
        sb.AppendFormat(" {0}", altasTexture.mipmapCount);
        sb.Remove(0, 1);
        sw.WriteLine(sb.ToString());
        List<Vector4> TArray = new List<Vector4>();
        for (int i = 0; i < spriteList.Count; i++)
        {
            Vector2[] data = SpriteUtility.GetSpriteUVs(spriteList[i], true);
            Vector2 ms = new Vector2(altasTexture.width, altasTexture.height);
            //左上
            Vector2 p0 = new Vector2(data[0].x * ms.x, data[0].y * ms.y);
            //右下
            Vector2 p1 = new Vector2(data[1].x * ms.x, data[1].y * ms.y);
            //右上
            Vector2 p2 = new Vector2(data[2].x * ms.x, data[2].y * ms.y);
            //左下
            Vector2 p3 = new Vector2(data[3].x * ms.x, data[3].y * ms.y);
            var sp = p3;
            var ss = new Vector2(p1.x - p3.x, p0.y - p3.y);
            Vector4 pos = new Vector4(sp.x / ms.x, sp.y / ms.y, ss.x / ms.x, ss.y / ms.y);
            sw.WriteLine(spriteList[i].name + " " + pos.x + " " + pos.y + " " + pos.z + " " + pos.w + "\n");
        }
        sw.Close();
        //销毁流  
        sw.Dispose();
        AssetDatabase.Refresh();
    }
}
