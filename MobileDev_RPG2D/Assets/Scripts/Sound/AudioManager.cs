using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer m_audioMixer = default;
    [SerializeField] private string musicGroup = default;
    [SerializeField] private string sfxGroup = default;
    public Sound[] music;
    public Sound[] sfx;

    private void Awake()
    {
        foreach (Sound clip in music)
        {
            clip.source = gameObject.AddComponent<AudioSource>();
            clip.source.outputAudioMixerGroup = m_audioMixer.FindMatchingGroups(musicGroup)[0];

            clip.source.name = clip.name;
            clip.source.clip = clip.sound;
            clip.source.volume = clip.volume;
            clip.source.loop = clip.loop;
        }
        foreach (Sound clip in sfx)
        {
            clip.source = gameObject.AddComponent<AudioSource>();
            clip.source.outputAudioMixerGroup = m_audioMixer.FindMatchingGroups(sfxGroup)[0];

            clip.source.name = clip.name;
            clip.source.clip = clip.sound;
            clip.source.volume = clip.volume;
        }
    }

    public void PlaySound(Sound[] soundArray, string soundName)
    {
        Sound clip = Array.Find(soundArray, sound => sound.name == soundName);
        if (clip == null)
        {
            Debug.LogWarning("Sound " + soundName + " Does not exist");
            return;
        }
        clip.source.Play();
    }
    public void StopSound(Sound[] soundArray, string soundName)
    {
        Sound clip = Array.Find(soundArray, sound => sound.name == soundName);
        if (clip == null)
        {
            Debug.LogWarning("Sound " + soundName + " Does not exist");
            return;
        }
        clip.source.Stop();
    }



}

public class AudioGroups
{
    public const string Music = "Music";
    public const string SFX = "SFX";
}

