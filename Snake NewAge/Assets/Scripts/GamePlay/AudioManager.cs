using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [HideInInspector] public static AudioManager instance;
    [HideInInspector] public float volume;
    public Slider volumeSlider;

    void Awake()
    {
        MakeInstance();
        SetVolumeLevel();
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    void SetVolumeLevel()
    {
        volumeSlider.value = volume;
    }

    private void Update()
    {
        float curVolume = volumeSlider.value;
        PlayerPrefs.SetFloat("SoundVolume",curVolume);
    }

}
