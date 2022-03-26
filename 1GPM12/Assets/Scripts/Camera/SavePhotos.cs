using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SavePhotos : MonoBehaviour
{
    public CameraRoll cameraRoll;
    public LevelPhotos levelPhotos;

    public void Save()
    {
        Debug.Log("save");
        Rect rect = new Rect(0, 0, 256, 256);

        for (int i = 0; i < levelPhotos.textures.Length; i++)
        {
            RenderTexture.active = cameraRoll.photos[i];
            Texture2D tex = new Texture2D(256, 256);
            tex.ReadPixels(rect, 0, 0, false);
            tex.Apply();

            byte[] bytes = tex.EncodeToPNG();
            string path = AssetDatabase.GetAssetPath(levelPhotos.textures[i]);
            File.WriteAllBytes(path, bytes);
        }

        AssetDatabase.Refresh();
    }
}