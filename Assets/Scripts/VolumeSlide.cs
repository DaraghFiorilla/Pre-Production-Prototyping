using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlide : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if(!PlayerPrefs.HasKey("musicvolyme"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }
    
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicvolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
