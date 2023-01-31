using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_TruckLight : MonoBehaviour
{
   [SerializeField] private Light[] lights;
   
   private void Start()
   {
       //비활성화
         foreach (var light in lights)
         {
              light.enabled = false;
         }
   }

   private void OnTriggerEnter(Collider other)
   {
           //충돌한 오브젝트가 플레이어인지 확인
           if (other.CompareTag("Player"))
           {
                //활성화
                foreach (var light in lights)
                {
                     light.enabled = true;
                }

                StartCoroutine(LightBlink());
           }
   }

   private IEnumerator LightBlink()
    {
         //1분동안 깜빡이기
           for (int i = 0; i < 60; i++)
           {
                 foreach (var light in lights)
                 {
                       light.enabled = !light.enabled;
                 }
     
                 yield return new WaitForSeconds(0.5f);
           }
           //비활성화
              foreach (var light in lights)
              {
                  light.enabled = false;
              }
    }
}
