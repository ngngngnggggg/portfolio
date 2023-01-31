using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private AudioClip[] clip;
    [SerializeField] private HW_Player Player;

    // 태그가 Player이면 문이 -90도로 부드럽게 회전한다.

    private void OnCollisionEnter(Collision coll)
    {
        {
            if (coll.gameObject.CompareTag("Player") && Player.hasKey)
            {
                doorSound.PlayOneShot(clip[0]);
                //3초동안 문이 열린다
                StartCoroutine(OpenDoor());
            }
            else
            {
                doorSound.volume = 1.5f;
                doorSound.PlayOneShot(clip[1]);
            }
        }
    }


    IEnumerator OpenDoor()
    {
        for (int i = 0; i < 90; i++)
        {
            door.transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(0.02f);
        }
    }
    



}
