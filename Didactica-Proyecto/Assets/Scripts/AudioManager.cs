using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip sendSound;

    public AudioSource source;

    public static AudioManager audioManager;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

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
