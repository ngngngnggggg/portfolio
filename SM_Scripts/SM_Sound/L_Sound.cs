using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Sound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClips;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        L_SoundPlay();
    }
    
    private void L_SoundPlay()
    {
        audioSource.PlayOneShot(audioClips);
    }
    
    private void L_SoundStop()
    {
        audioSource.Stop();
    }
}
