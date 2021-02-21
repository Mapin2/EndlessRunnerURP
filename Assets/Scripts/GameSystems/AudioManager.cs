using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    [HideInInspector] public static AudioManager Instance = null;

    public Sound[] sounds = null;
    public AudioMixerGroup[] audioMixerGroups = null;

    void Awake()
    {
        // Singleton instantiation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        // Setup of all sounds
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.outputAudioMixerGroup = audioMixerGroups[sound.mixerGroupId];
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    void Start()
    {
        // Start playing the main music
        Play("MenuTheme");
    }

    // Plays the sound passed as argument if exists
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, currentSound => currentSound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, currentSound => currentSound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sound.source.Stop();
    }

    public void Pause(string name)
    {
        Sound sound = Array.Find(sounds, currentSound => currentSound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        sound.source.Pause();
    }
}
