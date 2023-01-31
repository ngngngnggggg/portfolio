using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyState2 : MonoBehaviour
{

    private bool isCanDie = false;
    private float maxHp = 100f;
    public bool isDamaged = false;
    public int currentHp;
    public Punch punch;

    public enum State
    {
        STAY,
        TRACE,
        ATTACK,
        DIE,
    }

    public State state = State.STAY;
    public float attackDist;//���� �����Ÿ�

    public float traceDist;//���� �����Ÿ�
    public bool isDie = false;//������� �Ǵ�
    public LayerMask playerLayer;
    public LayerMask EnemyLayer;



    private Transform playerTr = null;
    private EnemyMove moveAgent = null;
    private WaitForSeconds ws = null;
    private Animator animator = null;
    private EnemyFOV enemyFOV = null;
    private Transform animalTr;


    [Header("�ִϸ��̼� ���� ����")]
    private readonly int hashMove = Animator.StringToHash("IsMove");
    //private readonly int hashRun = Animator.StringToHash("IsRun");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashDie = Animator.StringToHash("IsDie");


    private void Awake()
    {
        //playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            //�÷��̾ �����Ѵٸ� �ش� ������Ʈ�� ���� Transform ����
            playerTr = player.GetComponent<Transform>();
        }


        moveAgent = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
        enemyFOV = GetComponent<EnemyFOV>();
        ws = new WaitForSeconds(0.3f);
    }




    private void Update()
    {
        ////Chase ���� ����
        //// Debug.Log("currentHp" + currentHp); //���� ü�� ���
        //if (isDamaged == true)
        //{
        //    chaseTime += Time.deltaTime;
        //}
        //if (chaseTime > 5f)
        //{
        //    isDamaged = false;
        //    chaseTime = 0f;
        //}
        //// Debug.Log("isdmaged" + damageTime);
    }
    private void OnEnable()
    {
        StartCoroutine(CheckState());//���� üũ �ڷ�ƾ
        StartCoroutine(Action());//���¿� ���� �ൿ �ڷ�ƾ
    }

    IEnumerator CheckState()
    {
        // �ٸ� ��ũ��Ʈ �ʱ�ȭ(Awake, Start)�Լ� �� ���� ���
        yield return new WaitForSeconds(1f);

        while (!isDie)
        {
            if (state == State.DIE)//���� ���¶�� 
            {
                yield break;
            }

            float dist = Vector3.Distance(playerTr.position, transform.position);

            if (dist <= attackDist)
            {
                if (enemyFOV.isViewPlayer())
                {
                    state = State.ATTACK;
                }
                else
                {
                    Debug.Log("111");
                    state = State.TRACE;
                }
            }
            //���� �ݰ� �� �þ߰��� �ִ��� �Ǵ� �� ���� 
            else if (enemyFOV.isTracePlayer())
            {
                Debug.Log("222");
                state = State.TRACE;
            }

            else
            {
                state = State.STAY;
            }

            if (currentHp <= 0f)
            {
                state = State.DIE;
                yield break;
            }
            yield return ws;
        }
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;

            switch (state)
            {
                case State.STAY:
                    //moveAgent.PATROLLING = true;
                    moveAgent.Stop();
                    punch.gameObject.SetActive(false);
                    break;

                case State.TRACE:
                    moveAgent.TRACETARGET = playerTr.position;
                    punch.gameObject.SetActive(false);
                    animator.SetBool(hashAttack, false);
                    animator.SetBool(hashDie, false);
                    animator.SetBool(hashMove, true);
                    break;



                case State.ATTACK:
                    if ((this.transform.position - playerTr.position).magnitude < attackDist)
                        moveAgent.Stop();
                    Debug.Log("����");
                    punch.gameObject.SetActive(true);
                    animator.SetBool(hashDie, false);
                    animator.SetBool(hashMove, false);
                    animator.SetBool(hashAttack, true);
                    break;

    

                case State.DIE:
                    this.gameObject.tag = "Untagged";
                    isDie = true;
                    moveAgent.Stop();
                    punch.gameObject.SetActive(false);
                    animator.SetBool(hashAttack, false);
                    animator.SetBool(hashMove, false);
                    animator.SetBool(hashDie, true);
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    Destroy(gameObject, 11f);
                    break;

            }
        }
    }


}