using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PhotoScoring : MonoBehaviour
{

    //This class is used for scoring a photo based on a render texture(ID Texture), 
    public RenderTexture idDataTexture;
    public CameraRoll cameraRoll;
    private Dictionary<Color32, int> colorScores = new Dictionary<Color32, int>();

    CharacterManager characterManager;

    [Header("Debug")]
    public List<int> pixelAmounts;
    public List<Color32> colors;

    public int maxCharactersInFrame = 3;

    //this method is called by a game event indicating that the required data is ready
    public void ScorePhoto(int photoIndex)
    {
        //to do this we need to write the render texture into a texture 2d
        Texture2D photoDataTex = new Texture2D(idDataTexture.width, idDataTexture.height, TextureFormat.RGBA32, false);
        RenderTexture.active = idDataTexture;
        Rect rect = new Rect(0, 0, idDataTexture.width, idDataTexture.height);
        photoDataTex.ReadPixels(rect, 0, 0);
        photoDataTex.Apply();

        //color scores is a dictionary that uses colorIDs as keys and pixels occupied by that Id as values
        //clear the dictionary and count the pixels in the texture into the dictionary by ID
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

        //black and clear are obstacles and background, we don't need these so dump them
        Color32 black = new Color32(0, 0, 0, 255);
        Color32 clear = new Color32(0, 0, 0, 0);
        RemoveByKey(black);
        RemoveByKey(clear);

        //make sure all of the colors that appear in the texture are in
        //the list of characters registered to the manager
        CompareToCharacterManager();

        //These lists are actually just here for debugging purposes
        colors = colorScores.Keys.ToList();
        pixelAmounts = colorScores.Values.ToList();

        cameraRoll.scores[photoIndex].capturedCharacterNames.Clear();
        cameraRoll.scores[photoIndex].capturedCharacterPercents.Clear();

        // 1) get the most represented characters in the texture up to the ammount of characters we allow the loop for
        // 2) Then write these values into the photoscore scriptable object so the ui can use it. 
        for (int i = 0; i < maxCharactersInFrame; i++)
        {
            // 1)
            Color32 mostRepresented = FindMostOfCharacter();
            if (!mostRepresented.Equals(clear))
            {
                // 2)
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

    //This function finds the color ID in the dictionary that has the highest value (most pixels in the data texture)
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

    //The character manager is a singleton that character subscribe themselves to. We need to
    //make sure that the character ids we are checking appear in the manager or remove them from the dictionary
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
