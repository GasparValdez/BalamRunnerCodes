using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(AudioSource))]
public class AudioManager2 : MonoBehaviourPun
{
    public static AudioManager2 Instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! Más de un AudioManager en escena.");
        }



    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }
}
