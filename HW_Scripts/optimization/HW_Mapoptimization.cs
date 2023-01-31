using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Mapoptimization : MonoBehaviour
{
   //맵을 비활성화 할 변수
   public GameObject Map;
   
   //트리거 충돌 시 맵을 비활성화
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player")
      {
         Map.SetActive(false);
      }
   }

}
