using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(AudioSource))]

public class HW_FootStep : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClip;
    private AudioSource audioSource;
    [SerializeField] HW_Player player;
    [SerializeField] W_Player w_Player;
    [SerializeField] L_Player l_Player;

   private void Awake()
   {
         audioSource = GetComponent<AudioSource>();
   }

   
   //thats the animation event
   public void Step()
   {
       //볼륨은 0.46f로 고정
         audioSource.volume = 0.26f;
       if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1f))
       {
           if(hitInfo.transform.CompareTag(("Ground")))
           {
               audioSource.PlayOneShot(audioClip[12]);
           }
           else if (hitInfo.transform.CompareTag("Concrete"))
           {
               audioSource.PlayOneShot(audioClip[5]);
           }
           else if (hitInfo.transform.CompareTag("Untagged"))
           {
               audioSource.PlayOneShot(audioClip[0]);
           }
           else if (hitInfo.transform.CompareTag("Wood"))
           {
               audioSource.PlayOneShot(audioClip[13]);
           }
          
       }
   }
   
   private void RunStep()
   {
       audioSource.volume = 0.26f;
       if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1f))
       {
           if(hitInfo.transform.CompareTag(("Ground")))
           {
               audioSource.PlayOneShot(audioClip[12]);
           }
           else if (hitInfo.transform.CompareTag("Concrete"))
           {
               audioSource.PlayOneShot(audioClip[5]);
           }
           else if (hitInfo.transform.CompareTag("Untagged"))
           {
               audioSource.PlayOneShot(audioClip[1]);
           }
           else if (hitInfo.transform.CompareTag("Wood"))
           {
               audioSource.PlayOneShot(audioClip[13]);
           }

       }
   }

   private void StandingJumpSound()
   {
       audioSource.volume = 0.26f;
       if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1f))
       {
           if(hitInfo.transform.CompareTag(("Ground")))
           {
               audioSource.PlayOneShot(audioClip[12]);
           }
           else if (hitInfo.transform.CompareTag("Concrete"))
           {
               audioSource.PlayOneShot(audioClip[5]);
           }
           else if (hitInfo.transform.CompareTag("Untagged"))
           {
               audioSource.PlayOneShot(audioClip[2]);
           }
           else if (hitInfo.transform.CompareTag("Wood"))
           {
               audioSource.PlayOneShot(audioClip[13]);
           }

       }
   }
   
   private void RunJumpSound()
   {
       audioSource.volume = 0.2f;
       if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 1f))
       {
           if(hitInfo.transform.CompareTag(("Ground")))
           {
               audioSource.PlayOneShot(audioClip[12]);
           }
           else if (hitInfo.transform.CompareTag("Concrete"))
           {
               audioSource.PlayOneShot(audioClip[5]);
           }
           else if (hitInfo.transform.CompareTag("Untagged"))
           {
               audioSource.PlayOneShot(audioClip[3]);
           }
           else if (hitInfo.transform.CompareTag("Wood"))
           {
               audioSource.PlayOneShot(audioClip[13]);
           }

       }
   }
   
   private void SlidingSound()
   {
       audioSource.volume = 0.26f;
       audioSource.PlayOneShot(audioClip[4]);
   }

   private void RopeSound()
   {
       audioSource.PlayOneShot(audioClip[6]);
   }
   private void EndRopeSound()
   {
       
       audioSource.Stop();
   }
   
   //다이빙 사운드 , 물속 사운드
    private void DivingSound()
    {
        audioSource.PlayOneShot(audioClip[7]);
    }

    private void SwimmingIdleSound()
    {
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(audioClip[8]);
    }
    
    private void SwimmingSound()
    {
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(audioClip[9]);
    }

    private void FastSwimmingSound()
    {
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(audioClip[10]);
    }

    public void OutSwimmingSound()
    {
        if (player.isWater)
        {
            audioSource.PlayOneShot(audioClip[11]);
        }

    }

    


   


   private AudioClip GetRandomClip()
   {
       int index = Random.Range(0, audioClip.Length - 1);
       return audioClip[index];
   }
}
