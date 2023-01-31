using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_LightRot : MonoBehaviour
{
    private void Update()
    {
        //X축으로 -45 ~ 45도 오브젝트를 회전
        
        transform.Rotate(Vector3.right * (Mathf.Sin(Time.time) * 15f * Time.deltaTime));
        
        
    }
}
