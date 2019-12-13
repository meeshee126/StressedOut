using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael Schmidt
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlaySound());
    }

    /// <summary>
    /// After instantiaded this GameObject the audio will play and after destroying his self
    /// </summary>
    /// <returns></returns>
    IEnumerator PlaySound()
    {
        audioSource.PlayOneShot(clip);

        yield return new WaitForSeconds(clip.length);

        Destroy(this.gameObject);
    }
}

