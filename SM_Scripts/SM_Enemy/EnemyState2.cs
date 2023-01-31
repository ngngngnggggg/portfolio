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
    public float attackDist;//공격 사정거리

    public float traceDist;//추적 사정거리
    public bool isDie = false;//사망유무 판단
    public LayerMask playerLayer;
    public LayerMask EnemyLayer;



    private Transform playerTr = null;
    private EnemyMove moveAgent = null;
    private WaitForSeconds ws = null;
    private Animator animator = null;
    private EnemyFOV enemyFOV = null;
    private Transform animalTr;


    [Header("애니메이션 변수 관련")]
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
            //플레이어가 존재한다면 해당 오브젝트로 부터 Transform 추출
            playerTr = player.GetComponent<Transform>();
        }


        moveAgent = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
        enemyFOV = GetComponent<EnemyFOV>();
        ws = new WaitForSeconds(0.3f);
    }




    private void Update()
    {
        ////Chase 상태 관련
        //// Debug.Log("currentHp" + currentHp); //현재 체력 출력
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
        StartCoroutine(CheckState());//상태 체크 코루틴
        StartCoroutine(Action());//상태에 따른 행동 코루틴
    }

    IEnumerator CheckState()
    {
        // 다른 스크립트 초기화(Awake, Start)함수 돌 동안 대기
        yield return new WaitForSeconds(1f);

        while (!isDie)
        {
            if (state == State.DIE)//죽은 상태라면 
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
            //추적 반경 및 시야각에 있는지 판단 후 추적 
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
                    Debug.Log("공격");
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