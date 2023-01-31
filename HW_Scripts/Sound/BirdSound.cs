using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    [SerializeField] private AudioSource birdSound;
    [SerializeField] private AudioClip birdClip;
    [SerializeField] private ParticleSystem birdParticles;
    [SerializeField] private GameObject bird;

    private void Start()
    {
        birdSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(bird != null)
            //파티클 비활성화
                StartCoroutine(DisableParticle());
        }
    }

    IEnumerator DisableParticle()
    {
        birdParticles.Play();
        birdSound.PlayOneShot(birdClip);
        yield return new WaitForSeconds(3.0f);
        DestroyScirpts();
        
    }

    //최적화
    private void DestroyScirpts()
    {
        Destroy(bird);
        Destroy(birdSound);
        Destroy(this);
    }

}
