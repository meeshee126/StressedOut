using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt
public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    AudioClip newDaySFX;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //The hole Script will call when on an animation when a new day starts

    /// <summary>
    /// Locking mode
    /// </summary>
    public void Freeze()
    {
        // Frezze Player and Timer
        GameObject.Find("Player").GetComponent<Player>().characterRB.velocity = Vector2.zero;
        GameObject.Find("Player").GetComponent<Player>().enabled = false;
        GameObject.Find("GameManager").GetComponent<Timer>().enabled = false;
    }

    /// <summary>
    /// Unlatch mode
    /// </summary>
    public void Defreeze()
    {
        // Defreeze Player and Timer
        GameObject.Find("Player").GetComponent<Player>().enabled = true;
        GameObject.Find("GameManager").GetComponent<Timer>().enabled = true;
    }

    /// <summary>
    /// play sound when a new day starts
    /// </summary>
    public void NewDaySound()
    {
        audioSource.PlayOneShot(newDaySFX);
    }
}
