using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyState : MonoBehaviour
{

    private bool isCanDie = false;
    private float maxHp = 100f;
    public bool isDamaged = false;
    public int currentHp;

    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE,
    }

    public State state = State.PATROL;
    public float attackDist = 15f;//���� �����Ÿ�

    public float traceDist = 200f;//���� �����Ÿ�
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
    private readonly int hashRun = Animator.StringToHash("IsRun");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashDie = Animator.StringToHash("IsDie");
    private readonly int hashHowl = Animator.StringToHash("IsHowl");


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
        //animator = GetComponent<Animator>();
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
                    state = State.TRACE;
                }
            }

            //���� �ݰ� �� �þ߰��� �ִ��� �Ǵ� �� ���� 
            else if (enemyFOV.isTracePlayer())
            {
                state = State.TRACE;
            }

            else
            {
                state = State.PATROL;
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
                case State.PATROL:
                    moveAgent.PATROLLING = true;
                    //animator.SetBool(hashRun, false);
                    //animator.SetBool(hashMove, true);
                    break;

                case State.TRACE:
                    moveAgent.TRACETARGET = playerTr.position;
                    //animator.SetBool(hashMove, false);
                    //animator.SetBool(hashRun, true);
                    break;


                case State.ATTACK:
                    if ((this.transform.position - playerTr.position).magnitude < attackDist)
                        moveAgent.Stop();
                    Debug.Log("����");
                    //animator.SetBool(hashMove, false);
                    //animator.SetBool(hashRun, false);
                    //animator.SetTrigger(hashAttack);
                    break;



                case State.DIE:
                    this.gameObject.tag = "Untagged";
                    isDie = true;
                    moveAgent.Stop();
                    //animator.SetBool(hashRun, false);
                    //animator.SetBool(hashMove, false);
                    //animator.SetBool(hashDie, true);
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<Rigidbody>().isKinematic = true;
                    Destroy(gameObject, 11f);
                    break;

            }
        }
    }

   
}