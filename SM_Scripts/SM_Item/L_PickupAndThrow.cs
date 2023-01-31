using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_PickupAndThrow : MonoBehaviour
{
    public GameObject objectToPickup;
    public Transform handTransform;
    public float throwForce = 10.0f;

    private bool holdingObject = false;
    private GameObject pickedObject;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E) && pickedObject != null)
        //{
        //    if (!holdingObject)
        //    {
        //        Pickup();
        //    }
        //    else
        //    {
        //        Throw();
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!holdingObject)
            {
                Pickup();
            }
            else
            {
                Throw();
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Stone"))
    //    {
    //        pickedObject = other.gameObject;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Stone"))
    //    {
    //        pickedObject = null;
    //    }
    //}
    void Pickup()
    {
        objectToPickup.GetComponent<Rigidbody>().useGravity = false;
        objectToPickup.GetComponent<BoxCollider>().enabled = false;

        objectToPickup.transform.position = handTransform.position;
        objectToPickup.transform.parent = handTransform;
        holdingObject = true;
        
    }

    void Throw()
    {
        objectToPickup.GetComponent<Rigidbody>().useGravity = true;
        StartCoroutine(EnableCollider());
        objectToPickup.GetComponent<BoxCollider>().enabled = true;
        objectToPickup.transform.parent = null;
        objectToPickup.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
        holdingObject = false;
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(1);
        objectToPickup.GetComponent<Collider>().enabled = true;
    }
}
