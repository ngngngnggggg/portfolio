using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller = null;

    public float speed = 5;//�̵��ӵ�
    public float gravity = -9.18f;//�߷� ���� ����
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;

    private Vector3 velocity;


    void Update()
    {
        //Debug.Log(controller.isGrounded);
        if (controller.isGrounded && velocity.y < 0)//���̰�, ���� ���Ҷ�  
        {
            velocity.y = -2f; //-2��ŭ ����
        }

        if (Input.GetKey("left shift") && controller.isGrounded) //shift ������ ����
        {
            speed = 7;
        }
        else //���� �ӵ�
        {
            speed = 5;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime); //�̵�

        if (Input.GetButtonDown("Jump") && controller.isGrounded)//���� �پ������� ����
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //�����ö� �ӵ�
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}