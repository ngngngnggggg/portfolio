using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOut : MonoBehaviour
{
    Animator animator;
    public bool IsOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetBool("IsOpen", true);
        }
    }
}

// 1. �ڰ� �Ͼ�� ���� ������ �ۿ� ������ �� ������Ʈ�� ���� �׸��� ���ٰ� �ݶ��̴��� �ε����� ����ȯ
// �ڰ� �Ͼ�� ���� ����
