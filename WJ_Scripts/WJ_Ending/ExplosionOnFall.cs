using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionOnFall : MonoBehaviour
{
    public GameObject explosionPrefab; //폭발할 파티클 추가
   
    private bool hasExploded = false;
    private float time = 0f;
   

    private void Update()
    {
        time+= Time.deltaTime;
        if (!hasExploded && transform.position.y <= 2.037f)
        {
            hasExploded = true;
            Vector3 explosionPos = transform.position;
            explosionPos.y -= 2.0f; // 생성될 위치 수정
            Instantiate(explosionPrefab, explosionPos, Quaternion.identity);
            
            Destroy(this.gameObject,0.3f);
        }
        else if (time > 5.9f && transform.position.y <= 2.1f)
        {
            

            Destroy(this.gameObject);
        }
    }
}