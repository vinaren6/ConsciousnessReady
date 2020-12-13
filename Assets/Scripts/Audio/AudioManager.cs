using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioMixer audioMixer;

    public Sound[] sounds;

    public static AudioManager instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.outputAudioMixerGroup = audioMixer.outputAudioMixerGroup;
        }
    }

    private void Start()
    {
        Play("MusicGameplay");
    }

    public void Play(string name)
    {
        Sound soundClip = Array.Find(sounds, sound => sound.name == name);

        if (soundClip == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (soundClip.PlayOneShot)
        {
            soundClip.source.PlayOneShot(soundClip.source.clip, soundClip.volume);
        }
        else
        {
            soundClip.source.Play();
        }


    }
}
