using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer = null;

    private void Start()
    {
        // Set volume from player prefs, quality, fullscreen and resolution gets saved automatically
        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 0f));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0f));
        audioMixer.SetFloat("SfxVolume", PlayerPrefs.GetFloat("SfxVolume", 0f));
    }

    public void PlayGame()
    {
        AudioManager.Instance.Play("ConfirmButton");
        AudioManager.Instance.Stop("MenuTheme");
        SceneLoaderManager.Instance.FadeToLevel(1);
        AudioManager.Instance.Play("GameTheme");
    }

    public void Options()
    {
        AudioManager.Instance.Play("ConfirmButton");
        SceneLoaderManager.Instance.FadeToLevel(3);
    }

    public void Credits()
    {
        AudioManager.Instance.Play("ConfirmButton");
        SceneLoaderManager.Instance.FadeToLevel(4);
    }

    public void QuitGame()
    {
        AudioManager.Instance.Play("CancelButton");
        Application.Quit();
    }
}
