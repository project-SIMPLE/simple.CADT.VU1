using UnityEngine;

public class AnimalInteractable : MonoBehaviour
{
    public string animalKey;
    public AnimalInfoPanel panel;

    public void OnSelect()
    {
        panel.ShowAnimal(animalKey);
        panel.gameObject.SetActive(true);
    }
}