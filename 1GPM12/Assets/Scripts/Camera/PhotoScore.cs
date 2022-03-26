using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Photo Score", menuName = "Photos/Photo Score", order = 51)]
public class PhotoScore : ScriptableObject
{
    public List<string> capturedCharacterNames;
    public List<float> capturedCharacterPercents;
}
