using UnityEngine;
using UnityEngine.UI;

public class AnimalInfoPanel : MonoBehaviour
{
    public Image animalImage;
    public Text animalText;
    public AnimalDatabase database;

    public int englishFontSize = 40;
    public int khmerFontSize = 48;

    private string currentAnimalKey;

    public void ShowAnimal(string animalKey)
    {
        currentAnimalKey = animalKey;

        AnimalData data = database.animals.Find(a => a.animalKey == animalKey);

        if (data == null)
        {
            Debug.LogError("Animal not found: " + animalKey);
            return;
        }

        animalImage.sprite = data.image;
        animalText.text = LanguageManager.instance.GetText(data.textKey);
        animalText.font = LanguageManager.instance.GetFont();
        if (LanguageManager.instance.currentLanguage == LanguageManager.Language.Khmer)
        {
            animalText.fontSize = khmerFontSize;
        }
        else
        {
            animalText.fontSize = englishFontSize;
        }
    }

    public void RefreshLanguage()
    {
        if (!string.IsNullOrEmpty(currentAnimalKey))
            ShowAnimal(currentAnimalKey);
    }
}