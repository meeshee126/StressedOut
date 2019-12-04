﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMusic()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(slider.value) * 20);
    }

    public void SetSFX()
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(slider.value) * 20);
    }
}
