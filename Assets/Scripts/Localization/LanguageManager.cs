using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager instance;

    public enum Language
    {
        English,
        Khmer
    } 

    public Language currentLanguage;

    public Font englishFont;
    public Font khmerFont;

    public Dropdown languageDropdown; 

    Dictionary<string, string> translations;

    void Awake()
    {
        instance = this;

        // Always start the game in English
        currentLanguage = Language.English;

        if (languageDropdown != null)
        {
            languageDropdown.value = 0;
        }

        LoadLanguage();
    }

    public void SetLanguage(int index)
    {
        currentLanguage = (Language)index;

        PlayerPrefs.SetInt("language", index);

        LoadLanguage();

        LanguageText[] texts = FindObjectsOfType<LanguageText>(true);

        foreach (LanguageText t in texts)
        {
            t.UpdateText();
        }

        foreach (AnimalInfoPanel panel in FindObjectsOfType<AnimalInfoPanel>(true))
        {
            panel.RefreshLanguage();
        }
    }

    void LoadLanguage()
    {
        string fileName = currentLanguage == Language.English ? "en" : "km";

        TextAsset jsonFile = Resources.Load<TextAsset>("Localization/" + fileName);

        if (jsonFile != null)
        {
            translations = JsonUtility.FromJson<LocalizationData>(jsonFile.text).ToDictionary();
        }
        else
        {
            Debug.LogError("Localization file not found: " + fileName);
        }
    }

    public string GetText(string key)
    {
        if (translations != null && translations.ContainsKey(key))
        {
            return translations[key];
        }

        return key;
    }

    public Font GetFont()
    {
        return currentLanguage == Language.English ? englishFont : khmerFont;
    }
}