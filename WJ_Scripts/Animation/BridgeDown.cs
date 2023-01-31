using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDown : MonoBehaviour
{


    BridgeAnimation ani;



    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponentInParent<BridgeAnimation>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            ani.Down();
        }
    }
    

}
