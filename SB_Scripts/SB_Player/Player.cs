using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10.0f;

    public float rotateSpeed = 10.0f;

    public float jumpForce = 10.0f;          // �����ϴ� ��

    private bool isGround = true;           // ĳ���Ͱ� ���� �ִ��� Ȯ���� ����

    private bool isTree = false;

    [SerializeField]
    private GameObject attachGo = null;
    [SerializeField] private Transform handTr = null;



    Rigidbody body;                         // ������Ʈ���� RigidBody�� �޾ƿ� ����

    float h, v;

    // ����Ƽ ����� ���ÿ� �ѹ� ����Ǵ� �Լ�
    void Start()
    {
        body = GetComponent<Rigidbody>();   // GetComponent�� Ȱ���Ͽ� body�� �ش� ������Ʈ�� Rigidbody�� �־��ش�.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // �θ�-�ڽ� ���� �����(��������)
            //attachGo.transform.parent = transform;
            attachGo.transform.SetParent(handTr);
            attachGo.transform.localPosition = Vector3.zero;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            // �и�
            attachGo.transform.parent = null;
        }
    }

    // �̵� ���� �Լ��� © ���� Update���� FixedUpdate�� �� ȿ���� ���ٰ� �Ѵ�. �׷��� ����ߴ�.
    void FixedUpdate()
    {
        Move();
        Jump();

        if(isTree)
        {
            float ver = Input.GetAxis("Vertcel");
            body.velocity = new Vector2(body.velocity.x, ver * Speed);
            Debug.Log("Tree");
        }
    }

    // ���� FixedUpdate�� �ִ� ���� ���� ���� ���� ���� Move�� �ű�
    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        if (!(h == 0 && v == 0))
        {
            transform.position += dir * Speed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }

    void Jump()
    {
        // �����̽��ٸ� ������(�Ǵ� ������ ������), �׸��� ĳ���Ͱ� ���� �ִٸ�
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            // body�� ���� ���Ѵ�(AddForce)
            // AddForce(����, ���� ��� ���� ���ΰ�)
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // ������ ���������Ƿ� isGround�� false�� �ٲ�
            isGround = false;
        }
    }

    // �浹 �Լ�
    void OnCollisionEnter(Collision collision)
    {
        // �ε��� ��ü�� �±װ� "Ground"���
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGround�� true�� ����
            isGround = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Tree"))
        {
            isTree = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Tree"))
        {
            isTree = false;
        }
    }
}