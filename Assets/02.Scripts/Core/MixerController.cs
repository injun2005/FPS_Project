using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;


    public void SetEffectSound(float sliderValue)
    {
        _audioMixer.SetFloat("EffectVolume", sliderValue);
    }
    public void SetMusicSound(float sliderValue)
    {
        _audioMixer.SetFloat("MusicVolume", sliderValue);
    }
}
