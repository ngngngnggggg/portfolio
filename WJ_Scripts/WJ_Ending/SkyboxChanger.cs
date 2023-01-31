using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] private WJ_Sound sound;
    public Material newSkybox;
    private Material currentSkybox;
    public GameObject ui;
    float time = 0f;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentSkybox = RenderSettings.skybox;
        
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.gameObject.name == "Player")
    //     {
    //         sound.StopBGM();
    //     }
    //     
    // }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            player.GetComponent<HW_Player>().enabled = false;
            time += Time.deltaTime;
            ui.SetActive(true);
             //
            if(5f < time)
            {
            RenderSettings.skybox = newSkybox;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RenderSettings.skybox = currentSkybox;
    }
}
