using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class L_Player : MonoBehaviour
{
    //�÷��̾� �̵� ����
    [SerializeField] private float speed = 2.5f;

    private float ropeTime;
    //�÷��̾� ���� ����
    [SerializeField] private float jumpPower = 7f;

    [SerializeField] private GameObject Stone;
    [SerializeField] private Transform Handpos;

    [SerializeField] private StoneCut[] stone;

    //�÷��̾� 3d������ٵ�
    private Rigidbody rigid;
    //�÷��̾� �ִϸ�����
    private Animator anim;
    //�÷��̾� ĸ�� �ݶ��̴�
    private CapsuleCollider cc;
   
    //������ �ߴ��� Ȯ���ϴ� ����
    [Header("������ �ϰ� �ִ��� Ȯ��")]
    [SerializeField]
    private bool canJump;

    [Header("�ٴڿ� �پ� �ִ��� Ȯ��")]
    [SerializeField]
    private bool isGround;

    [Header("�����̵��� �ϰ� �ִ��� Ȯ��")]
    [SerializeField]
    private bool isSlide;

    private int count = 1;

    [Header("���� �����ϴ� ���� �Ÿ�")]
    [SerializeField]
    private float range = 0.3f;

    [Header("��Ÿ�� �ӵ�")][SerializeField] private float climbspeed = 0.5f;
    [Header("���� Ÿ���� Ȯ��")][SerializeField] private bool isclimbing = false;
    [SerializeField] private bool isclimbingUp = false;

    [Header("���� Ÿ���� Ȯ��")][SerializeField] private bool isSideStep = false;
    [Header("������ �Ŵ޷ȴ��� Ȯ��")][SerializeField] private bool isRope = false;
    [Header("������ �������� Ȯ��")][SerializeField] private bool endRope = false;
    [Header("�������� Ȯ��")][SerializeField] private bool isdie = false;
    public bool IsRope
    {
        get { return isRope; }
    }

    [SerializeField] private Transform startPos;
    [SerializeField] private MoveRope rope;
    private LineRenderer lr;
    private Hand hand;
    private GameObject attachGo = null;

    //�÷��̾ ���� �����ϰ� �ϴ� ���� ��Ʈ
    private RaycastHit hit;


    [SerializeField] private GameManager gameMng;
    [SerializeField] private Material mat;
    //[SerializeField] private Material enemy1;
    //[SerializeField] private Material enemy2;
    //[SerializeField] private Material enemy3;
    public bool hasKey;
    public bool hasLight;
    public GameObject L_light;

    GameObject nearObject;

    private bool isSave;
    [SerializeField] private GameObject lamp;

    //public GameObject[] item;
    //public bool[] hasitem;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider>();
        lr = GetComponent<LineRenderer>();
        hand = GetComponentInChildren<Hand>();
        

    }

    private void Update()
    {
        if (isSave)
            StartCoroutine(Load());

        if (Move()) Run();
        Grab();
        Jump();
        //Rope();
        Climb();
        SideStep();

        //Debug.DrawRay(transform.position + (Vector3.up * 3f), transform.forward, Color.red);
        // switch (animState)
        // {
        //     case EAnim.Walk:
        //         if ( Move()) Run();
        //         break;
        //     case EAnim.Grab:
        //         Grab();
        //         break;
        // }
    }

    //�÷��̾� �̵� �Լ�
    // ReSharper disable Unity.PerformanceAnalysis
    private bool Move()
    {
        if (isclimbing || isdie || isSideStep) return false;


        //�÷��̾� �̵�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //������ �������� �÷��̾� ȸ��
        if (moveDir != Vector3.zero && !isclimbing)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
        else
            isSlide = false;

        speed = Input.GetKey(KeyCode.LeftShift) ? 7f : 2.5f;
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C) && !isSlide)
        {
            anim.SetTrigger("isSlide");
            //�ִϸ��̼� ���� �ϴ� ���ȿ��� �ݶ��̴��� Z������ 0.5��ŭ �ٿ��� �����̵� ȿ���� �ִ� �ڷ�ƾ �Լ�
            StartCoroutine(Slide());
        }

        //�÷��̾� �̵�   
        transform.Translate(moveDir.normalized * (speed * Time.deltaTime), Space.World);



        ChangeAnim(anim, moveDir, speed, canJump, hit);

        return h != 0;
    }

    //�ڷ�ƾ �����̵� �Լ�
    IEnumerator Slide()
    {
        isSlide = true;
        //cc.height = 0.5f;
        cc.direction = 2;
        yield return new WaitForSeconds(0.828f);
        //cc.height = 1.929797f;
        cc.direction = 1;
        isSlide = false;
    }


    //�÷��̾� �޸��� �Լ�
    private void Run()
    {
        //LShiftŰ�� ������ 3.0�� �ӵ��� �޸��� ���� 1.5�� �ӵ��� �ȱ�
        //speed = Input.GetKey(KeyCode.LeftShift) ? 3.0f : 1.5f;
    }

    private void Jump()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, 0.3f);
        canJump = Input.GetKeyDown(KeyCode.Space) && isGround;
        if (canJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }

    }

    //�÷��̾� ������Ʈ �׷� �Լ�
    // ReSharper disable Unity.PerformanceAnalysis
    private void Grab()
    {
        //�÷��̾ ������Ʈ�� �����ϸ�
        if (Physics.Raycast(transform.position, transform.forward, out hit, range) && Input.GetKey(KeyCode.E))
        {
            //���� �����ϸ� ���� �׷�
            if (hit.collider.CompareTag("Stone"))
            {
                //���� �׷��ϸ� ���� �÷��̾� �ڽ����� �����
                hit.collider.transform.SetParent(Handpos);
                //�ڽ��� �ݶ��̴��� ��Ȱ��ȭ
                hit.collider.GetComponent<Collider>().enabled = false;
                //�ڽ��� ������ٵ� ��Ȱ��ȭ
                hit.collider.GetComponent<Rigidbody>().isKinematic = true;
                //���� �÷��̾��� �ڽ����� ����� �÷��̾ ���� ������ �� �ְ�
                hit.collider.transform.localPosition = new Vector3(0, 0, 0);
                //isPick �ִϸ��̼��� ����
                anim.SetTrigger("isPick");
                //isPick �ִϸ��̼��� ������ �� �ٸ� �ִϸ��̼� ������ ���� ����
                anim.SetBool("isWalk", false);
                anim.SetBool("isRun", false);
                anim.SetBool("isJump", false);
                anim.SetBool("isIdle", false);


            }
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("1242314123123412344123412341234123123");
            //���� ������ ���� �θ� ��Ȱ��ȭ
            Stone.transform.SetParent(null);    
            //���� �ݶ��̴��� Ȱ��ȭ
            Stone.GetComponent<Collider>().enabled = true;
            //���� ������ٵ� Ȱ��ȭ
            Stone.GetComponent<Rigidbody>().isKinematic = false;
            //���� ������ٵ� ���� ����
            Stone.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
            //Throw �ִϸ��̼� ����
            anim.SetTrigger("isThrow");

        }

        if(Input.GetKeyDown(KeyCode.G))
           
         {
            Stone.transform.SetParent(null);
            Stone.GetComponent<Collider>().enabled = true;
            Stone.GetComponent<Rigidbody>().isKinematic = false;
            anim.SetTrigger("isPick");
        }
    }


    

    // ReSharper disable Unity.PerformanceAnalysis
    private void Climb()
    {

        if (!isclimbing && Input.GetKeyDown(KeyCode.C))
        {
            //Debug.Log("wallboolȮ��");
           
            if (Physics.Raycast(transform.position + (Vector3.up * 2f), transform.forward, out hit, range))
            {
                Debug.Log("wallboolȮ��");
                if (hit.transform.tag == "Wall")
                {
                    Debug.Log("wallboolȮ��2");
                    isclimbing = true;
                    isclimbingUp = true;
                }
            }
        }

        if (isclimbingUp)
        {
            if (Physics.Raycast(transform.position + (Vector3.up * 0.7f), transform.forward, out hit, range))
            {
                if (hit.transform.tag == "Wall")
                {
                    //Quaternion endrot = Quaternion.Euler(0f, 90f, 0f);

                    transform.position += transform.up * Time.deltaTime * climbspeed;
                    //transform.rotation = Quaternion.Lerp(transform.rotation, endrot, 1f);
                    //�ݶ��̴��� �߷��� ��Ȱ��ȭ �Ѵ�.
                    //cc.enabled = false;
                    rigid.useGravity = false;
                    //�ִϸ��̼� ����
                    anim.SetTrigger("isClimbing");
                }

            }
            else
            {
                isclimbingUp = false;
                // Debug.Log("123");
                StartCoroutine(ClimbCoroutine());
            }
        }

    }

    private IEnumerator ClimbCoroutine()
    {

        anim.SetBool("isClimbing", false);


        yield return new WaitForSeconds(0.5f);

        float t = 0f;
        Vector3 startpos = transform.position;
        Vector3 endpos = startpos + (Vector3.up * 0.1f) + (Vector3.right * 0.02f);
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        t = 0f;
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 0.2f) + (Vector3.right * 0.02f);
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        t = 0f;
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 0.3f) + (Vector3.right * 0.42f);
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        t = 0f;
        startpos = transform.position;
        endpos = startpos + (Vector3.up * 0.3f);
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos, endpos, t);
            yield return null;
        }

        cc.enabled = true;
        rigid.useGravity = true;

        isclimbing = false;
    }

    private void SideStep()
    {


        if (!isSideStep && Input.GetKeyDown(KeyCode.C))
        {

            if (Physics.Raycast(transform.position, -transform.up, out hit, range))
            {

                if (hit.transform.tag == "Bridge")
                {

                    isSideStep = true;
                    isSlide = true;
                }
            }
        }

        if (isSideStep)
        {
            if (Physics.Raycast(transform.position, -transform.up, out hit, range))
            {
                if (hit.transform.tag == "Bridge" && transform.rotation.y > 0)
                {
                    float sidespeed = 1f;
                    float t = 0f;


                    Quaternion startrot = transform.rotation;
                    Quaternion endrot = Quaternion.Euler(0f, 0f, 0f);

                    //������Ʈ�� x�������� �̵��Ѵ�
                    Vector3 startpos = transform.position;
                    Vector3 endpos = startpos + (Vector3.right * 0.5f);
                    anim.SetBool("isSideStep", true);
                    while (t < 1f)
                    {

                        t += Time.deltaTime * sidespeed;
                        transform.position = Vector3.Lerp(startpos, endpos, t);
                        transform.rotation = Quaternion.Lerp(startrot, endrot, t);
                        return;
                    }
                }
                else if (hit.transform.tag == "Bridge" && transform.rotation.y < 0)
                {
                    float sidespeed = 1f;
                    float t = 0f;
                    Quaternion startrot = transform.rotation;
                    Quaternion endrot = Quaternion.Euler(0f, 180f, 0f);

                    //������Ʈ�� x�������� �̵��Ѵ�
                    Vector3 startpos = transform.position;
                    Vector3 endpos = startpos + (Vector3.left * 0.5f);
                    anim.SetBool("isSideStep", true);
                    while (t < 1f)
                    {

                        t += Time.deltaTime * sidespeed;
                        transform.position = Vector3.Lerp(startpos, endpos, t);
                        transform.rotation = Quaternion.Lerp(startrot, endrot, t);
                        return;
                    }
                }
                else if (hit.transform.tag == "Ground" || hit.transform.tag != "Bridge")
                {
                    isSideStep = false;
                    isSlide = false;
                    anim.SetBool("isSideStep", false);

                    //StartCoroutine(SideStepCoroutine());
                }
            }

        }
    }



    //������ �Ŵ޸���
    private void Rope()
    {
        //������ �ϰ� �ְ� ���ڸ� ���� �ִϸ��̼��� ���� ���϶�
        if (isGround == false)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("CŰ �Է�");
                if (Physics.Raycast(transform.position, transform.up, out hit, range + 1f))
                {
                    Debug.Log("RayȮ��");
                    if (hit.transform.gameObject.CompareTag("Rope"))
                    {
                        Debug.Log("tag�� ����");
                        isRope = true;
                        //�ִϸ��̼� ����
                        anim.SetTrigger("isRopeS");
                        transform.SetParent(startPos);
                    }
                }
            }
        }

        if (isRope == true && rope.EndRope == false)
        {

            lr.SetPosition(0, hand.Gethandpos());
            lr.SetPosition(1, startPos.position + new Vector3(0f, 0f, 0f));

            Debug.Log("hit rope");
            anim.SetTrigger("isRopeS");
            rigid.useGravity = false;
            rigid.isKinematic = true; //isRope�� �ƴϸ� ���� �������



        }
        else if (rope.EndRope == true)
        {

            Debug.Log("endrope");
            //isRope = false;
            transform.SetParent((null));
            if (lr != null)
                Destroy(lr);
            rigid.useGravity = true;
            rigid.isKinematic = false;
            anim.SetTrigger("isRopeE");
            //StartCoroutine(RopeCoroutine());  
        }


    }


    private void ChangeAnim(Animator anim, Vector3 _moveDir, float _speed, bool _canJump, RaycastHit _hit)
    {
        // switch (animState)
        // {
        //     case EAnim.Walk:
        //         anim.SetBool("isWalk", _moveDir != Vector3.zero);
        //         break;
        // }

        //�÷��̾��� �ӵ��� 0���� Ŭ �� Walking �ִϸ��̼� ����
        //�÷��̾��� �ӵ��� 0�� �� Idle �ִϸ��̼� ����
        anim.SetBool("isWalk", _moveDir != Vector3.zero);

        //�÷��̾��� �ӵ��� 0���� ũ��, ���� ����Ʈ�� ������ �� isRun �ִϸ��̼� ����
        anim.SetBool("isRun", Input.GetKey(KeyCode.LeftShift) && _moveDir != Vector3.zero);

        //Player Jump animation start
        anim.SetBool("StandingJump", _canJump);

        //player Run Jump animation start
        anim.SetBool("RunningJump", _canJump);
        //Debug.DrawRay(transform.position, transform.forward, Color.red);





    }

    //���� ���� �� �ִϸ��̼��� ������ ������ �ִϸ��̼� ���� �Լ�
    private void StopAnim(Animator anim, string _animName)
    {
        //���� ���� �� �ִϸ��̼��� ������ ������ �ִϸ��̼� ����
        foreach (var _anim in anim.runtimeAnimatorController.animationClips)
        {
            if (_anim.name == _animName) continue;
            anim.ResetTrigger(_anim.name);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SavePoint"))
        {
            isSave = true;
            Debug.Log("??");
            gameMng.GameSave();
            Renderer renderer = other.GetComponentInChildren<Renderer>();
            renderer.material = mat;
        }
        if (other.CompareTag("Enemy") )
        {
            //if (other.GetComponent<Objdata>().num == 1 || numClear)
            //{
            //    numClear = true;
            //    if (other.GetComponent<Objdata>().num == 2 || numClear)
            //    {
            //        if (other.GetComponent<Objdata>().num == 3)
            //            Debug.Log("����");
            //    }
            //    else
            //        other.GetComponent<EnemyState2>().enabled = true;
            //}
            //else
            //    other.GetComponent<EnemyState2>().enabled = true;
           
                if (other.GetComponent<Objdata>().num == count)
                {
                    other.GetComponent<Objdata>().num = 0;
                    other.GetComponent<Objdata>().ChangeColor();
                    count++;


                if (count == 4)
                {
                    StartCoroutine(DestroyStone());
                    return;
                }

            }
                else if (other.GetComponent<Objdata>().num == 0)
                {
                    return;
                }
                else
                {
                    other.GetComponent<EnemyMove>().enabled = true;
                    other.GetComponent<EnemyState2>().enabled = true;
                }
            

        }
        if (other.gameObject.tag == "Light")
        {
            
                Destroy(other.gameObject);

            L_light.SetActive(true);
            hasLight = true;
        }

        if (other.gameObject.tag == "Key")
        {
            
            Destroy(other.gameObject);
            
            hasKey = true;
        }
        if (other.CompareTag("DeathZone"))
        {
            anim.SetBool("isDie", true);
            Debug.Log("����");
            isdie = true;
            StartCoroutine(DieCoroutine());
        }
    }
    // �±׿� ������ �״� �ִϸ��̼�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
            anim.SetBool("isDie", true);
            Debug.Log("����");
            isdie = true;
            StartCoroutine(DieCoroutine());
        }


    

    }
    IEnumerator DestroyStone()
    {
        yield return new WaitForSeconds(3f);
        foreach (var t in stone)
        {
            t.DestroyStone();
        }
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(3f);
        //gameMng.GameLoad();
        SceneManager.LoadScene("SangMin");
        
    }

    IEnumerator Load()
    {
        lamp.SetActive(true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isDie", false);
        yield return new WaitForSeconds(2.5f);

        isdie = false;
        isSave = false;
    }


    void Interation()
    {

    }









    //void Interation()
    //{
    //    if (Input.GetKeyDown(KeyCode.G) && nearObject != null)
    //    {
    //        if (nearObject.tag == "item")
    //        {
    //            Item item = nearObject.GetComponent<L_Item>();
    //            //Item item = gameObj.GetComponent<L_Item>();
    //            int weaponIndex = item.value;
    //            hasitem[weaponIndex] = true;

    //            Destroy(nearObject);
    //        }
    //    }
    //}
}
