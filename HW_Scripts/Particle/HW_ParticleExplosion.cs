using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW_ParticleExplosion : MonoBehaviour
{
    //파티클 프리펩 2개 선언
    public GameObject particlePrefab;
    public GameObject particlePrefab2;
    //파티클 소환 위치
    public Transform particleSpawnPoint;
    
    [SerializeField] private ResetColor resetColor;
    
    

  
    
    //ResetColor 스크립트의 OnTriggerExit 함수가 실행될 때, 파티클을 소환하는 함수
    public void ParticleSpawn()
    {
        Debug.Log("ParticleSpawn");
        StartCoroutine(ParticleSpawnSequence_Coroutine());
       // Invoke("CloneDestroy", 4f);
    }

    
    IEnumerator ParticleSpawnSequence_Coroutine()
    {
        //파티클 소환
        Instantiate(particlePrefab, particleSpawnPoint.position, particleSpawnPoint.rotation);
        yield return new WaitForSeconds(3.0f);
        Instantiate(particlePrefab2, particleSpawnPoint.position, particleSpawnPoint.rotation);
    }
    
    public void CloneDestroy()
    {
        //Destroy();
    }
    
    
}
