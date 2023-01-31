using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // 충돌하면 떨어지는 함정에 필요한 변수
    public float fallSpeed = 1.0f;
    [SerializeField] private Bridge bridge;
    private Rigidbody rb;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        rb.useGravity = false;
    }

    private void Update()
    {
        if (bridge.CanFallDown() == true)
        {
            rb.useGravity = true;
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
    }
}
