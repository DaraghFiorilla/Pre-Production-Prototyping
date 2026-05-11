using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{

    private enum VolumeType
    {
        Master,
        Music,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.Music:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.sfxVolume;
                break;
            default:
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                AudioManager.instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.Music:
                AudioManager.instance.musicVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.sfxVolume = volumeSlider.value;
                break;
            default:
                break;
        }
    }
}
