using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camera Roll", menuName = "Camera Stuff/Camera Roll", order = 51)]
public class CameraRoll : ScriptableObject
{
    public RenderTexture[] photos = new RenderTexture[16];
    public PhotoScore[] scores = new PhotoScore[16];
}
