using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBall : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GameObject.Find("Ball").GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            rb.useGravity = true;
    }
}
