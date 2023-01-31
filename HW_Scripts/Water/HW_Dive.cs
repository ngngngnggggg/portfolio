using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Dive : MonoBehaviour
{
    [SerializeField] private HW_Player _player;
    [SerializeField] private HW_Water _water;
    private BoxCollider boxCollider;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            if(Input.GetKey(KeyCode.LeftShift))
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("isDive");
            }
            else if (Input.GetKey(KeyCode.A))
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("isDive");
            }
            _player.GetComponent<HW_Player>().particle.Play();
            OffWaterWall();
            boxCollider.enabled = false;
            Invoke("OnCollider",2f);
            
        }
    }

    private void OnWaterWall()
    {
        GameObject.Find("WaterWall").SetActive(true);
    }
    
    private void OffWaterWall()
    {
        GameObject.Find("WaterWall").SetActive(false);
    }
    
    private void OffCollider()
    {
        boxCollider.enabled = false;
    }

    private void OnCollider()
    {
        boxCollider.enabled = true;
    }
}
