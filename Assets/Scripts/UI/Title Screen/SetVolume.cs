using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public bool adjustMasterVolume;
    public bool adjustMusicVolume;
    public bool adjustSFXVolume;


    public AudioMixer audioMixer;

    public void Volume(float sliderValue)
    {
        if (adjustMasterVolume)
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        if (adjustMusicVolume)
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        if (adjustSFXVolume)
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
    }


}
