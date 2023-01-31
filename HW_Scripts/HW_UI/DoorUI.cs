using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorUI : MonoBehaviour
{
    private Text text;
    [SerializeField] private GameObject door;
    [SerializeField] private HW_Player player;
    [SerializeField] private HW_EventArrow[] eventArrow;


    private void Awake()
    {
        text = GetComponent<Text>();
        door.gameObject.SetActive(false);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //부모만 활성화
            door.gameObject.SetActive(true);
            
            door.transform.GetChild(1).gameObject.SetActive(false);
            Invoke("NextText", 2f);
            player.enabled = false;
            player.anim.SetBool("isRun", false);
            player.anim.SetBool("isWalk", false);
            player.anim.SetBool("isJump", false);

            //이벤트 화살표 활성화
            for (int i = 0; i < eventArrow.Length; i++)
            {
                eventArrow[i].gameObject.SetActive(true);
            }
            //플레이어 다시 활성화
            Invoke("PlayerActive", 4f);
        }
        
    }

    public void NextText()
    {
        door.transform.GetChild(0).gameObject.SetActive(false);
        door.transform.GetChild(1).gameObject.SetActive(true);
        Invoke("DeleteDoor", 2f);
    }
    
    public void DeleteDoor()
    {
        Destroy(door);
    }
    
    public void PlayerActive()
    {
        player.enabled = true;
    }
}
