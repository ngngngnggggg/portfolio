using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HW_PuzzleUI : MonoBehaviour
{
    public GameObject image;
    
    private void Awake()
    {
        
        image.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (image != null)
            {
                image.gameObject.SetActive(true);
                image.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

}
