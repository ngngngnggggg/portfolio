using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HW_ParticleDrop : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    Vector3 originalPos;
    [SerializeField] private CubeColor color;
    [SerializeField] private Material mat;
    
  

    private Rigidbody rb;

    //시작시 초기화, 리지드바디 생성
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            StartCoroutine(ParticleDrop());
        }
    }
    //3초후 파티클 사라지는 코루틴 함수
    // ReSharper disable Unity.PerformanceAnalysis
    public IEnumerator ParticleDrop()
    {
        rb.velocity = Vector3.zero;
            ps.gameObject.SetActive(true);
            ps.Play();
           

        if (color.success)
        {
            color.ChangeColor(mat);
        }
        transform.position = originalPos;
        
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        ps.gameObject.SetActive(false);
    }
}