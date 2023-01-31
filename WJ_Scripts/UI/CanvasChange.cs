using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasChange : MonoBehaviour
{
    Canvas cr;
    

    // Start is called before the first frame update
    void Start()
    {
        cr = this.gameObject.GetComponent<Canvas>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //cr.renderMode = RenderMode.ScreenSpaceCamera
    }
    public void Change()
    {
        cr.renderMode = RenderMode.ScreenSpaceCamera;
    }
    public void Change1()
    {
        cr.renderMode = RenderMode.ScreenSpaceOverlay;
    }

}