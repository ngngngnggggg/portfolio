using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    
    //줄 탈때 손에 위치에 라인 랜더러 생성
    public Vector3 Gethandpos()
    {
        return transform.position;
    }
}
