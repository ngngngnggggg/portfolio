using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_Moon : MonoBehaviour
{
    //달의 모든 transform을 고정 시킬 변수
    private Transform tr;
    //달이 따라다닐 타겟 변수
    public Transform targetTr;
    //달이 따라다닐 타겟의 위치 변수
    private Vector3 targetPos;
    //달의 속도는 플레이어의 속도와 동일하게 설정 할 변수
    public float speed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        //달의 transform을 고정 시킴
        tr = GetComponent<Transform>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //달이 따라다닐 타겟의 위치를 가져옴
        targetPos = targetTr.position;
        //달의 위치를 타겟의 위치로 이동시킴
        tr.position = targetPos;
    }
    
    
    //달이 따라다닐 타겟의 위치를 가져오는 함수
    public void GetTargetPos()
    {
        targetPos = targetTr.position;
    }
    
    //달의 위치를 타겟의 위치로 이동시키는 함수
    public void MoveToTargetPos()
    {
        tr.position = targetPos;
    }
    
}
