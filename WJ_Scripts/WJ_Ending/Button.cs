using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private Door door;
    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door").GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(door.Open());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            door.Close();
        }
    }
    

}
