using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;

    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetLevel()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log(slider.value) * 20);
    }

}
