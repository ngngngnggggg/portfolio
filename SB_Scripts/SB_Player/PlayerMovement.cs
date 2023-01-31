using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller = null;

    public float speed = 5;//이동속도
    public float gravity = -9.18f;//중력 구현 변수
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;

    private Vector3 velocity;


    void Update()
    {
        //Debug.Log(controller.isGrounded);
        if (controller.isGrounded && velocity.y < 0)//땅이고, 점프 안할때  
        {
            velocity.y = -2f; //-2만큼 감소
        }

        if (Input.GetKey("left shift") && controller.isGrounded) //shift 누르면 가속
        {
            speed = 7;
        }
        else //평상시 속도
        {
            speed = 5;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); //이동

        if (Input.GetButtonDown("Jump") && controller.isGrounded)//땅에 붙어있을때 점프
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //내려올때 속도
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}