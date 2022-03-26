using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PhotoScoring : MonoBehaviour
{
    public RenderTexture idDataTexture;
    public CameraRoll cameraRoll;
    private Dictionary<Color32, int> colorScores = new Dictionary<Color32, int>();

    CharacterManager characterManager;

    [Header("Debug")]
    public List<int> pixelAmounts;
    public List<Color32> colors;

    public int maxCharactersInFrame = 3;

    public void ScorePhoto(int photoIndex)
    {
        Texture2D photoDataTex = new Texture2D(idDataTexture.width, idDataTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = idDataTexture;
        Rect rect = new Rect(0, 0, idDataTexture.width, idDataTexture.height);
        photoDataTex.ReadPixels(rect, 0, 0);
        photoDataTex.Apply();

        colorScores.Clear();
        int numPixels = photoDataTex.width * photoDataTex.height;
        for (int x = 0; x < photoDataTex.width; x++)
        {
            for (int y = 0; y < photoDataTex.height; y++)
            {
                Color32 col = photoDataTex.GetPixel(x, y);
                if (!colorScores.ContainsKey(col))
                {
                    colorScores.Add(col, 1);
                }
                else
                {
                    int val = colorScores[col] + 1;
                    colorScores[col] = val;
                }
            }
        }

        //black and clear are obstacles and background
        Color32 black = new Color32(0, 0, 0, 255);
        Color32 clear = new Color32(0, 0, 0, 0);
        RemoveByKey(black);
        RemoveByKey(clear);

        //make sure all of the colors appear in the texture are in
        //the list of character registered to the manager
        CompareToCharacterManager();

        pixelAmounts = colorScores.Values.ToList();
        colors = colorScores.Keys.ToList();

        cameraRoll.scores[photoIndex].capturedCharacterNames.Clear();
        cameraRoll.scores[photoIndex].capturedCharacterPercents.Clear();

        //get the most represented characters in the texture
        for (int i = 0; i < maxCharactersInFrame; i++)
        {
            Color32 mostRepresented = FindMostOfCharacter();
            if (!mostRepresented.Equals(clear))
            {
                string name = characterManager.GetCharacterFromID(mostRepresented).characterName;
                cameraRoll.scores[photoIndex].capturedCharacterNames.Add(name);
                float percent = (float)colorScores[mostRepresented] / (float)numPixels;
                cameraRoll.scores[photoIndex].capturedCharacterPercents.Add(percent);
            }

            if (colorScores.ContainsKey(mostRepresented))
            {
                colorScores.Remove(mostRepresented);
            }
        }
    }

    private Color32 FindMostOfCharacter()
    {
        Color32 maxColor = new Color32(0, 0, 0, 0);
        int maxVal = -1;
        foreach (Color32 col in colorScores.Keys)
        {
            if(colorScores[col] > maxVal)
            {
                maxColor = col;
                maxVal = colorScores[col];
            }
        }
        return maxColor;
    }

    private void CompareToCharacterManager()
    {
        characterManager = CharacterManager.Instance;
        foreach (Color32 col in colorScores.Keys)
        {
            if (!characterManager.CheckCharacterFromID(col))
            {
                colorScores.Remove(col);
            }
        }
    }

    private void RemoveByKey(Color32 key)
    {
        if (colorScores.ContainsKey(key))
        {
            colorScores.Remove(key);
        }
    }
}
