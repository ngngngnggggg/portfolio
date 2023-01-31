using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Key : MonoBehaviour
{
    [SerializeField] private ParticleSystem keyparticle;
    [SerializeField] private GameObject key;
    public AudioSource keySound;
    public AudioClip keyClip;


    private void Start()
    {
        keyparticle.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            keyparticle.Stop();
            keySound.PlayOneShot(keyClip);
        }
    }
    
   

}
