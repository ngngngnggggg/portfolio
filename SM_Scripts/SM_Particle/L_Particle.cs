using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Particle : MonoBehaviour
{

    [SerializeField] private ParticleSystem particle;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            particle.Play();
        }
    }
}
