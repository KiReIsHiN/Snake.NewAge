using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider _slider;
    private float currentLevel;

    void Awake()
    {
        _slider = GetComponent<Slider>();

        currentLevel = PlayerPrefs.GetFloat("SoundVolume");
    }

    void Update()
    {
        SetSliderValue();
        SetAudioLevel();
    }


    void SetSliderValue()
    {
        currentLevel = _slider.value;
        PlayerPrefs.SetFloat("SoundVolume", currentLevel);
    }

    void SetAudioLevel()
    {
        AudioListener.volume = currentLevel;
    }

}
