using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SavePhotos : MonoBehaviour
{
    public CameraRoll cameraRoll;
    public LevelPhotos levelPhotos;

    //this function is called by a save event.
    // 1) it takes the list of render textures that we just captured
    // 2) then translates them to Texture2d's
    // 3) then we can save them on the disc
    public void Save()
    {
        Debug.Log("save");
        Rect rect = new Rect(0, 0, 256, 256);

        for (int i = 0; i < levelPhotos.textures.Length; i++)
        {
            // 1)
            RenderTexture.active = cameraRoll.photos[i];
            // 2)
            Texture2D tex = new Texture2D(256, 256);
            tex.ReadPixels(rect, 0, 0, false);
            tex.Apply();
            // 3)
            byte[] bytes = tex.EncodeToPNG();
            string path = AssetDatabase.GetAssetPath(levelPhotos.textures[i]);
            File.WriteAllBytes(path, bytes);
        }

        AssetDatabase.Refresh();
    }
}