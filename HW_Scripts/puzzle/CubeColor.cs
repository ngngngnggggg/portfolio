using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    private MeshRenderer mr;
    public Material mat;
    public Material originMat;
    public bool success = false;
    [SerializeField] private ResetColor resetcolor;
    [SerializeField] private Material answerMat;
    private GameObject image;
    
    private void Start()
    {
        image = GameObject.Find("PuzzleText");
        mr = GetComponent<MeshRenderer>();
        originMat = mr.material;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(("Player")))
        {
            success = true;
            StartCoroutine(transform.GetChild(0).GetComponent<HW_ParticleDrop>().ParticleDrop());
            this.GetComponent<BoxCollider>().enabled = false;
            // mat = transform.GetChild(0).GetComponent<HW_ParticleDrop>().mat;
            // mr.material = mat;
        }
    }
    
    public void ChangeColor(Material _mat)
    {
        mat = _mat;
        if (answerMat == mat)
        {
            resetcolor.count++;
        }
        else
        {
            if (image != null)
            {
                image.SetActive(true);
                image.transform.GetChild(1).gameObject.SetActive(true);
                image.transform.GetChild(0).gameObject.SetActive(false);
            }

            resetcolor.count--;
            
        }
        mr.material = mat;
    }
    
    public void ResetColor()
    {
        if (image != null)
        {
            image.SetActive(false);
            image.transform.GetChild(1).gameObject.SetActive(false);
            image.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(image);
        }

        mr.material = originMat;
        mat = originMat;
        success = false;
        this.GetComponent<BoxCollider>().enabled = true;
        resetcolor.count = 0;
    }
}
