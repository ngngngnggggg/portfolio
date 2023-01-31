using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;

public class ResetColor : MonoBehaviour
{
    [SerializeField] private GameObject[] cubes;
    [SerializeField] private StoneCut[] stone;
    [SerializeField] private Cubepuzzle puzzle;
    [SerializeField] private HW_ParticleExplosion explosion;
    [SerializeField] private HW_PuzzleUI puzzleUI;
     
     
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    
    [SerializeField] private Image image;
    
    public int count = 0;
    private ParticleSystem ps;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(image != null)
                image.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (count == 4)
            {
                foreach (var t in stone)
                {
                    t.DestroyStone();
                    Invoke("EndPuzzle", 6f);
                }
                //audioSource.PlayOneShot(audioClip);
                    Invoke("ExplosionSound", 3f);
                explosion.GetComponent<HW_ParticleExplosion>().ParticleSpawn();
                return;
            }
           

            foreach(GameObject cube in cubes)
                cube.GetComponent<CubeColor>().ResetColor();
        }
    }


    private void ExplosionSound()
    {
        //OnTriggerExit 함수가 실행 될때 사운드 실행
        audioSource.PlayOneShot(audioClip);
    }

    private void EndPuzzle()
    {
        Destroy(GameObject.Find("Cubepuzzle"));
    }
}
