using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Person : MonoBehaviour
{
    private bool m_ladder, m_air;

    // Start is called before the first frame update
    void Start()
    {
        m_ladder = false;
        m_air = false;
    }

    private void FixedUpdate()
    {
        float speed =2f;

        if (m_ladder)
        {
            bool upKey = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
            bool downKey = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
            if (upKey)
            {
                this.transform.Translate(0, speed * Time.deltaTime, 0);
            }
            else if (downKey)
            {
                this.transform.Translate(0, -speed * Time.deltaTime, 0);
            }
        }
        else if (m_air)
        {
            bool upKey = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
            bool downKey = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
            if (upKey)
            {
                this.transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else if (downKey)
            {
                this.transform.Translate(0, 0, -speed * Time.deltaTime);
            }
        }
    }
    

    private void OnTriggerEnter(Collider Get)
    {
        if (Get.transform.tag == "Ladder=nottom")
        {
            if(!m_ladder)
            {
                m_ladder = true;
                this.transform.Translate(0, 10f, 0);
            }
        }
        else if (Get.transform.tag == "Ladder-air")
        {
            if(m_ladder)
            {
                m_ladder = false;
                m_air = true;
            }
        }
        else if(Get.transform.tag == "Ladder-top")
        {
            if (!m_ladder)
            {
                m_ladder = true;

                this.transform.Translate(0, -0.5f, 0);
            }
        }
        else if (Get.transform.tag == "Ladder-floor")
        {
            if(m_ladder)
            {
                m_ladder = false;
            }
        }
    }
    
}
