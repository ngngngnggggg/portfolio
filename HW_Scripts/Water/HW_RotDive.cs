using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_RotDive : MonoBehaviour
{
    [SerializeField] private HW_Player player;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if(other.gameObject.tag == "Player" && player.transform.rotation.y <= -90 && player.transform.rotation.y <= 180)
        {
            Debug.Log("rotdive");
            player.GetComponent<Animator>().SetTrigger("isDive");
        }
        else
        {
            Debug.Log("return");
            return;
        }
    }
}
