using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventos : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip audio1;
    public AudioClip audio2;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void achou()
    {
        print("achou");
        audio.PlayOneShot(audio1);
    }

    public void perdeu()
    {
        print("perdeu");
        audio.PlayOneShot(audio2);
    }
}
