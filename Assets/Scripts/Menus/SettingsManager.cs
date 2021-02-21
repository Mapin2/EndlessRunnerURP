using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer = null;
    Resolution[] resolutions = null;
    int currentResolutionIndex = 0;
    [SerializeField] TMP_Dropdown resolutionDropdown = null, qualityDropDown = null;
    [SerializeField] Slider masterVolumeSlider = null, musicVolumeSlider = null, sfxVolumeSlider = null;
    [SerializeField] Toggle fullScreen = null;

    void Awake()
    {
        // Update values on UI from player prefs

        // Resolution
        UpdateResolution();
        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
        // Quality
        int qualityIndex = PlayerPrefs.GetInt("QualitySettings", 0);
        qualityDropDown.value = qualityIndex;
        // FullScreen
        bool isFullScreen = System.Convert.ToBoolean(PlayerPrefs.GetString("FullScreen", "true"));
        fullScreen.isOn = isFullScreen;
        // Audio
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0f);
    }

    public void MainMenu()
    {
        AudioManager.Instance.Play("CancelButton");
        SceneLoaderManager.Instance.FadeToLevel(0);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", volume);
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualitySettings", qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetString("FullScreen", isFullScreen.ToString());

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    private void UpdateResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ResetPreferenesAndRecord()
    {
        PlayerPrefs.DeleteAll();
        AudioManager.Instance.Play("CancelButton");
        SceneLoaderManager.Instance.FadeToLevel(0);
    }
}
