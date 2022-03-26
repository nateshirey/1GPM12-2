using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class CelShader_GUI : ShaderGUI
{
    MaterialEditor editor;
    MaterialProperty[] properties;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        this.editor = materialEditor;
        this.properties = properties;
        Main();
    }

    void Main()
    {
        GUILayout.Label("Textures", EditorStyles.boldLabel);

        Albedo();
        Ramp();
        Specular();
        Gloss();
    }

    void Albedo()
    {
        MaterialProperty tex = FindProperty("_MainTex", properties);
        GUIContent label = new GUIContent(tex.displayName, "Albedo");
        editor.TextureProperty(tex, label.text);

        MaterialProperty aTint = FindProperty("_AlphaTint", properties);
        MaterialProperty useAlpha = FindProperty("_UseAlpha", properties);
        Rect rect = new Rect(new Vector2(85, 95), new Vector2(20, 20));

        useAlpha.floatValue = EditorGUI.Toggle(rect, useAlpha.floatValue > 0.5f) ? 1f : 0f;

        //on
        if (useAlpha.floatValue > 0.5f)
        {
            aTint.colorValue = editor.ColorProperty(aTint, aTint.displayName);
        }
        else
        {
            GUILayout.Label(useAlpha.displayName);
        }
    }

    void Ramp()
    {
        MaterialProperty map = FindProperty("_Ramp", properties);
        Texture tex = map.textureValue;
        EditorGUI.BeginChangeCheck();
        GUIContent label = new GUIContent(map.displayName, "ColorRamp");
        editor.TexturePropertySingleLine(label, map,
                                            !map.textureValue ? FindProperty("_ColorRamp1", properties) : null,
                                            !map.textureValue ? FindProperty("_ColorRamp2", properties) : null);
        if (EditorGUI.EndChangeCheck())
        {
            if(tex != map.textureValue)
            {
                SetKeyword("_COLOR_RAMP", map.textureValue);
            }
        }
    }

    void Specular()
    {
        MaterialProperty color = FindProperty("_SpecularColor", properties);
        GUIContent label = new GUIContent(color.displayName, "Specular Color");
        editor.ColorProperty(color, color.displayName);
    }

    void Gloss()
    {
        MaterialProperty gloss = FindProperty("_Gloss", properties);
        editor.RangeProperty(gloss, "Gloss");
    }

    void SetKeyword(string keyword, bool state)
    {
        if (state)
        {
            foreach (Material m in editor.targets)
            {
                m.EnableKeyword(keyword);
            }
        }
        else
        {
            foreach (Material m in editor.targets)
            {
                m.DisableKeyword(keyword);
            }
        }
    }
}
