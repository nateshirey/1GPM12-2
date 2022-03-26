using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVertexColor : MonoBehaviour
{
    public CharacterData characterData;
    private MeshRenderer rend;

    private void Awake()
    {
        //set vertex color
        if(TryGetComponent<MeshFilter>(out MeshFilter meshFilter))
        {
            Mesh mesh = meshFilter.mesh;
            Color32[] newColors = new Color32[mesh.colors32.Length];
            for (int i = 0; i < newColors.Length; i++)
            {
                newColors[i] = characterData.characterID;
            }

            mesh.colors32 = newColors;
        }
        if(TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            rend = meshRenderer;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (CharacterManager.Instance != null)
        {
            if(rend != null)
            {
                CharacterManager.Instance.AddDataMesh(rend);
            }
            if(characterData != null)
            {
                CharacterManager.Instance.AddCharacterToList(characterData);
            }
        }
    }

    private void OnEnable()
    {
        if (CharacterManager.Instance != null)
        {
            if (rend != null)
            {
                CharacterManager.Instance.AddDataMesh(rend);
            }
            if (characterData != null)
            {
                CharacterManager.Instance.AddCharacterToList(characterData);
            }
        }
    }

    private void OnDisable()
    {
        if (CharacterManager.Instance != null)
        {
            if (rend != null)
            {
                CharacterManager.Instance.RemoveDataMesh(rend);
            }
            if (characterData != null)
            {
                CharacterManager.Instance.RemoveCharacterFromList(characterData);
            }
        }
    }
}
