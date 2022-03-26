using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;

    public static CharacterManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        //singleton stuff
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        characterList.Clear();
        charactersDictionary.Clear();
    }

    private void Start()
    {
        //populate the characterID to data dictionary
        foreach (CharacterData charData in characterList)
        {
            AddCharacterToList(charData);
        }
    }

    //a list of meshes to draw in the command buffer.
    //populated using the character vertex color class
    private List<MeshRenderer> characterDataMeshes = new List<MeshRenderer>();
    public List<MeshRenderer> CharacterDataMeshes
    {
        get { return characterDataMeshes; }
    }

    //a list of character SO's used to analyze pictures.
    //also populated by the character vertex color class
    public List<CharacterData> characterList = new List<CharacterData>();
    private Dictionary<Color32, CharacterData> charactersDictionary = new Dictionary<Color32, CharacterData>();

    //methods used by objects to add themselves to the list of
    //meshes rendered in the command buffer for analysis later
    public void AddDataMesh(MeshRenderer mesh)
    {
        if (mesh != null)
        {
            characterDataMeshes.Add(mesh);
        }
    }
    public void RemoveDataMesh(MeshRenderer mesh)
    {
        if (mesh != null && characterDataMeshes.Contains(mesh))
        {
            characterDataMeshes.Remove(mesh);
        }
    }

    //methods for objects to add themselves to the character list
    //and by extension the dictionary used to analyze pictures
    public void AddCharacterToList(CharacterData data)
    {
        if(data != null && !characterList.Contains(data))
        {
            characterList.Add(data);
            if (!charactersDictionary.ContainsKey(data.characterID))
            {
                charactersDictionary.Add(data.characterID, data);
            }
        }
    }
    public void RemoveCharacterFromList(CharacterData data)
    {
        if (data != null && characterList.Contains(data))
        {
            characterList.Remove(data);
            if (charactersDictionary.ContainsValue(data))
            {
                charactersDictionary.Remove(data.characterID);
            }
        }
    }

    public bool CheckCharacterFromID(Color32 idColor)
    {
        if (charactersDictionary.ContainsKey(idColor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public CharacterData GetCharacterFromID(Color32 idColor)
    {
        if (charactersDictionary.ContainsKey(idColor))
        {
            return charactersDictionary[idColor];
        }
        else
        {
            return null;
        }
    }
}
