using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void ChangeBackgroundVolume(float sliderValue)
    {
        audioMixer.SetFloat("Background Volume", Mathf.Log10(sliderValue) * 20);
    }
    public void ChangeSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFX Volume", Mathf.Log10(sliderValue) * 20);
    }
    public void ChangeMasterVolume(float sliderValue)
    {
        audioMixer.SetFloat("Master Volume", Mathf.Log10(sliderValue) * 20);
    }
}
