using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField]
    private AudioMixer musicMixer;

    [SerializeField]
    private AudioMixer soundEffectsMixer;


    public void SetMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetEffectsVolume(float sliderValue)
    {
        soundEffectsMixer.SetFloat("SoundEffectsVolume", Mathf.Log10(sliderValue) * 20);
    }


}
