using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundControl : Singleton<SoundControl>
{
    [SerializeField] private AudioMixer m_audioMixer = default;

    [SerializeField] private Slider sfxSlider = default;
    [SerializeField] private Slider musicSlider = default;

    public void SetSoundLevels()
    {
        sfxSlider.value = PlayerController.Instance.sfxVol;
        musicSlider.value = PlayerController.Instance.musicVol;
    }

    public void SetSFXLevel(float sfxLevel)
    {
        m_audioMixer.SetFloat("sfxVol", sfxLevel);
        PlayerController.Instance.sfxVol = sfxLevel;
        SaveSystem.SavePlayerData(PlayerController.Instance);
    }
    public void SetMusicLevel(float musicLevel)
    {
        m_audioMixer.SetFloat("musicVol", musicLevel);
        PlayerController.Instance.musicVol = musicLevel;
        SaveSystem.SavePlayerData(PlayerController.Instance);
    }
}
