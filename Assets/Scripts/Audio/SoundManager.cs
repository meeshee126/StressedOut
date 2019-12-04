using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    IEnumerator PlaySound()
    {
        audioSource.PlayOneShot(clip);

        yield return new WaitForSeconds(clip.length);

        Destroy(this.gameObject);
    }
}

