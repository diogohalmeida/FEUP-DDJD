using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField]
    private AudioMixer musicMixer;

    [SerializeField]
    private AudioMixer soundEffectsMixer;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider effectsSlider;

    float musicVolume = 1f;
    float effectsVolume = 1f;

    void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        effectsVolume = PlayerPrefs.GetFloat("effectsVolume");
        musicSlider.value = musicVolume;
        effectsSlider.value = effectsVolume;
    }


    public void SetMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("musicVolume", sliderValue);
    }

    public void SetEffectsVolume(float sliderValue)
    {
        soundEffectsMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("effectsVolume", sliderValue);
    }


}
