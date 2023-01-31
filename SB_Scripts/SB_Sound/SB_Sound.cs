using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_Sound : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip[] audioClips;
   
   private void Start()
   {
       audioSource = GetComponent<AudioSource>();
       BGMSound();
   }


   public void BGMSound()
   {
       audioSource.clip = audioClips[0];
         audioSource.Play();
   }
   public void BGMSound2()
   {
       audioSource.clip = audioClips[1];
       audioSource.Play();
   }
   
   public void BGMNext()
   {
       //0번째 오디오 클립 정지
       audioSource.Stop();
       audioSource.PlayOneShot(audioClips[1]);
   }
}
