using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;

    private float musicVolume;

    private void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        volumeSlider.value = musicVolume;

        volumeSlider.onValueChanged.AddListener(VolumeChange);
    }

    private void VolumeChange(float musicVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }
}
