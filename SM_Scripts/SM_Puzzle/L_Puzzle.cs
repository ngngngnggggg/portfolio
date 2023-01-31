using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Puzzle : MonoBehaviour
{

    private BoxCollider boxcollider;
    private GameObject gameObj;
    bool onHint = false;

    private void Start()
    {
        gameObj = GetComponent<GameObject>();
        boxcollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            transform.GetChild(1).gameObject.SetActive(true);

        }
        if (other.CompareTag("Stone"))
        {
            transform.GetChild(1).gameObject.SetActive(true);

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (other.CompareTag("Stone"))
        {
            transform.GetChild(1).gameObject.SetActive(false);

        }
    }

}
