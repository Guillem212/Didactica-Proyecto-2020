using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip sendSound;

    public AudioSource source;

    public void PlayClick()
    {
        source.clip = clickSound;
        source.Play();
    }

    public void PlaySend()
    {
        source.clip = sendSound;
        source.Play();
    }
}
