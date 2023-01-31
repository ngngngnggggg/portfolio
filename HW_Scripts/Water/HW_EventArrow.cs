using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_EventArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    // 색상 변수
    private Color color;
    //Emission 변수
    private Material material;
    //색상변경 속도
    private float speed = 1f;
    //색상변경 최소값
    private float min = 0.5f;
    //색상변경 최대값
    private float max = 1f;
    
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
        //시작할 때 흰색으로
        color = Color.white;
        //Emission을 받아옴
        material = arrow.GetComponent<Renderer>().material;
    }
    
    // Update is called once per frame
    private void Update()
    {
        //색상을 흰색에서 파란색으로 변경
        color = Color.Lerp(Color.white, Color.blue, Mathf.PingPong(Time.time * speed, 1));
        //색상을 Emission에 적용
        material.SetColor("_EmissionColor", color);
        
    }
    
    
}
