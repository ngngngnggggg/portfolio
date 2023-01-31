using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//�ش� ��ũ��Ʈ�� ���۽�Ű�� ���ؼ� �ʼ����� ������Ʈ ����
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    public List<Transform> wayPoints = null;
   // public List<Transform> parkourPoint = null;
    public int nextIdx; //���� ���� ������ �迭 Index

    private Transform animalTr = null;
    private NavMeshAgent agent = null;
    private readonly float patrolSpeed = 1.5f;//�����ӵ�
    private readonly float traceSpeed = 4f;//�����ӵ�
    private readonly float chaseSpeed = 5f;//�����ӵ�
    private float damping = 1f; // ȸ���� �ӵ� ���� ���
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
    public Vector3 TRACETARGET //������Ƽ
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
            //���� ��� �����ϴ� �Լ� ȣ��
            TraceTarget(_traceTarget);
        }
    }
    private Vector3 _chaseTarget;
    public Vector3 CHASETARGET //������Ƽ
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
            //���� ��� �����ϴ� �Լ� ȣ��
            TraceTarget(_chaseTarget);
        }
    }

    // NevMeshAgent �̵� �ӵ��� ���� ������Ƽ
    public float SPEED
    {
        get { return agent.velocity.magnitude; }
    }





    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //�������� ������ ���� �ӵ��� ���̴� ����� ����.
        //�����ӿ��� Enemy�� �÷��̾ �����ϹǷ�
        //�ӵ��� ������ �ʵ��� �Ѵ�.
        agent.autoBraking = false;
        agent.speed = patrolSpeed;

        animalTr = GetComponent<Transform>();
        agent.updateRotation = false;

        var group = GameObject.Find("WayPointGroup");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints); //�ڽĿ�����Ʈ�� �� �� ���ܿ�
            wayPoints.RemoveAt(0);//�θ� ������Ʈ�� ���� ���ԵǱ� ������ 0���� ������Ʈ�� ���������ش�.
            nextIdx = Random.Range(0, wayPoints.Count);
        }

        PATROLLING = true;
    }

    void MoveWayPoint()
    {
        if (agent.isPathStale)//bool �ڷ��� 0,1 �� ��ȯ�ϴ� �ڷ����̴�. �� ������ϋ��� ���̱⶧���� �����ϰ� ����� ������ �Ʒ� �ڵ带 �����Ѵ�.
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
        //������Ʈ ������ ���߱�
        agent.isStopped = true;
        //Ȥ�ó� �������� �𸣱� ������ �����ӵ� 0���� ����
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }


    void Update()
    {
        //���� �̵����϶� �����
        if (agent.isStopped == false)
        {
            //NavMeshAgent�� �����ؾߵ� ������ ����
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            animalTr.rotation = Quaternion.Slerp(animalTr.rotation, rot, Time.deltaTime * damping);
        }

        if (!_patrolling)
            return;
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {

            nextIdx++;
            //nextIdx ���� �����̼� ��Ű�� ���ؼ� ���
            //nextIdx = nextIdx % wayPoints.Count;//��ȸ

            nextIdx = Random.Range(0, wayPoints.Count);
            MoveWayPoint();

        }

    }
}
