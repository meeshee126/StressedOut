using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//Michael Schmidt
public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    /// <summary>
    /// Controlls background music via slider
    /// </summary>
    public void SetMusic()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(slider.value) * 20);
    }

    /// <summary>
    /// Controls Sound effects via slider
    /// </summary>
    public void SetSFX()
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(slider.value) * 20);
    }
}
