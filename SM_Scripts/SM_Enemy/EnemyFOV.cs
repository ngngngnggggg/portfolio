using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    [Range(0, 360)]
    public float viewAngle = 80f; // �þ߰�
    public float viewRange = 10f; // �þ߹���

    [Header("�÷��̾� �Ǵܿ� �ʿ��� ����")]
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

        // �÷��̾� ���̾ �����ϹǷ� 1�� ��� = �÷��̾ ������ ������ ��
        //Debug.Log("colls" + colls.Length);
        if (colls.Length == 1)
        {
            Vector3 dir = (playerTr.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dir) < viewAngle * 0.5)
            {
                isTrace = true; // �����Ѵٸ� ���� ����
            }
        }
        return isTrace;

    }

    public bool isViewPlayer()
    {
        bool isView = false;
        RaycastHit hit; // �浹 ���� ���� ����
        Vector3 dir = (playerTr.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, dir, out hit, viewRange, playerLayer))
        {
            isView = hit.collider.CompareTag("Player");
        }
        return isView;
    }
   
}



