using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private bool canFallDown = false;
   private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Player") || _other.gameObject.CompareTag("Stone"))
        {
            Debug.Log("77777777");
            canFallDown = true;
        }
        else
        {
            canFallDown = false;
        }

    }

   public bool CanFallDown()
   {
       return canFallDown;
   }
}
