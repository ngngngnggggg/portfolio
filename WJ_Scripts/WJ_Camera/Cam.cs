using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering.PostProcessing;

public class Cam : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    BoxCollider bc;
    PostProcessingProfile ppf;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("Cam2").GetComponent<CinemachineVirtualCamera>();
        bc = GetComponent<BoxCollider>();
        cam = GetComponentInChildren<CinemachineVirtualCamera>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            cam.Priority = 10;
            
;        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            cam.Priority = 1;
        }
    }
}
