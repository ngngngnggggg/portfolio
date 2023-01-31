using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
   AudioSource audioSource;
   public AudioClip[] audioClips;
   [SerializeField]private HW_Player player;
   
   
   void Start()
   {
       audioSource = GetComponent<AudioSource>();
       PlayBGM();
   }
   
   //게임을 시작하면 0번째 브금 실행
    public void PlayBGM()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    
    //isWater이면 1번째 브금 실행
    public void PlayWaterBGM()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
    
    public void HWStopSound()
    {
        audioSource.Stop();
    }
   
    
    
    
}
