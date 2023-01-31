using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//해당 스크립트를 동작시키기 위해서 필수적인 컴포넌트 지정
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    public List<Transform> wayPoints = null;
   // public List<Transform> parkourPoint = null;
    public int nextIdx; //다음 순찰 지점의 배열 Index

    private Transform animalTr = null;
    private NavMeshAgent agent = null;
    private readonly float patrolSpeed = 1.5f;//순찰속도
    private readonly float traceSpeed = 4f;//추적속도
    private readonly float chaseSpeed = 5f;//추적속도
    private float damping = 1f; // 회전값 속도 조절 계수
    private bool _patrolling = false;


    public bool PATROLLING
    {
        get
        {
            return _patrolling;
        }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1f;
                MoveWayPoint();
            }
        }
    }

    private Vector3 _traceTarget;
    public Vector3 TRACETARGET //프로퍼티
    {
        get
        {
            return _traceTarget;
        }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;

            damping = 7f;
            //추적 대상 지정하는 함수 호출
            TraceTarget(_traceTarget);
        }
    }
    private Vector3 _chaseTarget;
    public Vector3 CHASETARGET //프로퍼티
    {
        get
        {
            return _chaseTarget;
        }
        set
        {
            _chaseTarget = value;
            agent.speed = chaseSpeed;

            damping = 7f;
            //추적 대상 지정하는 함수 호출
            TraceTarget(_chaseTarget);
        }
    }

    // NevMeshAgent 이동 속도에 대한 프로퍼티
    public float SPEED
    {
        get { return agent.velocity.magnitude; }
    }





    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //목적지에 도달할 수록 속도를 줄이는 기능을 끈다.
        //본게임에서 Enemy는 플레이어를 추적하므로
        //속도를 줄이지 않도록 한다.
        agent.autoBraking = false;
        agent.speed = patrolSpeed;

        animalTr = GetComponent<Transform>();
        agent.updateRotation = false;

        var group = GameObject.Find("WayPointGroup");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints); //자식오브젝트를 싹 다 땡겨옴
            wayPoints.RemoveAt(0);//부모 오브젝트도 같이 포함되기 떄문에 0번쨰 오브젝트도 삭제시켜준다.
            nextIdx = Random.Range(0, wayPoints.Count);
        }

        PATROLLING = true;
    }

    void MoveWayPoint()
    {
        if (agent.isPathStale)//bool 자료형 0,1 을 반환하는 자료형이다. 즉 계산중일떄는 참이기때문에 리턴하고 계산이 끝나면 아래 코드를 실행한다.
            return;
        agent.destination = wayPoints[nextIdx].position;

        agent.isStopped = false;
    }

    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;

        agent.destination = pos;
        agent.isStopped = false;

    }
    public void Stop()
    {
        //에이전트 움직임 멈추기
        agent.isStopped = true;
        //혹시나 움직일지 모르기 때문에 남은속도 0으로 변경
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }


    void Update()
    {
        //적이 이동중일때 실행됨
        if (agent.isStopped == false)
        {
            //NavMeshAgent가 진행해야될 방향을 구함
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            animalTr.rotation = Quaternion.Slerp(animalTr.rotation, rot, Time.deltaTime * damping);
        }

        if (!_patrolling)
            return;
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {

            nextIdx++;
            //nextIdx 값을 로테이션 시키기 위해서 사용
            //nextIdx = nextIdx % wayPoints.Count;//순회

            nextIdx = Random.Range(0, wayPoints.Count);
            MoveWayPoint();

        }

    }
}
