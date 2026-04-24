using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        slider.value = savedVolume;
    }
}