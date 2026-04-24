using UnityEngine;
using UnityEngine.UI;

public class LanguageText : MonoBehaviour
{
    public string key;
    public int englishFontSize = 40;
    public int khmerFontSize = 48;
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (text == null || LanguageManager.instance == null)
            return;
        text.text = LanguageManager.instance.GetText(key);
        text.font = LanguageManager.instance.GetFont();

        if (LanguageManager.instance.currentLanguage == LanguageManager.Language.Khmer)
        {
            text.fontSize = khmerFontSize;
        }
        else
        {
            text.fontSize = englishFontSize;
        }
    }
}