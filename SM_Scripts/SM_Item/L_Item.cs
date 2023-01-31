using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Item : MonoBehaviour
{


    [SerializeField] private GameObject Key;
    Rigidbody rigid;
    SphereCollider sphereCollider;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        Key.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.Rotate(Vector3.up * (10 * Time.deltaTime));
    }
    
 
    
}

