using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [Range(0, 360)]
    public float viewAngle = 80f; // 시야각
    public float viewRange = 10f; // 시야범위

    [Header("플레이어 판단에 필요한 변수")]
    private Transform playerTr = null;
    private Transform animalTr = null;
    public LayerMask playerLayer;

    private int animalLayer;
    private int obstacleLayer;
    private int layerMask;

    private void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        obstacleLayer = LayerMask.NameToLayer("OBSTACLE");
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }
    public Vector3 CirclePoint(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private void Update()
    {
        //print("isVeiw" + isTracePlayer());
    }
    public bool isTracePlayer()
    {
        bool isTrace = false;
        Collider[] colls = Physics.OverlapSphere(transform.position, viewRange, playerLayer);

        // 플레이어 레이어를 검출하므로 1인 경우 = 플레이어가 범위에 들어왔을 때
        //Debug.Log("colls" + colls.Length);
        if (colls.Length == 1)
        {
            Vector3 dir = (playerTr.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dir) < viewAngle * 0.5)
            {
                isTrace = true; // 존재한다면 추적 가능
            }
        }
        return isTrace;

    }

    public bool isViewPlayer()
    {
        bool isView = false;
        RaycastHit hit; // 충돌 정보 저장 변수
        Vector3 dir = (playerTr.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, dir, out hit, viewRange, playerLayer))
        {
            isView = hit.collider.CompareTag("Player");
        }
        return isView;
    }
   
}



