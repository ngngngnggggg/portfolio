using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SB_Event : MonoBehaviour
{
    [SerializeField] private GameObject spawnPointGroup;
    private Transform[] spawnPoints;
    [SerializeField] private GameObject[] drops;

    private bool on = false;

    private void Start()
    {
        spawnPoints = spawnPointGroup.GetComponentsInChildren<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            on = true;
            StartCoroutine(Drop());
        }
    }

    IEnumerator Drop()
    {
        while (on)
        {
            GameObject drop = Instantiate(drops[Random.Range(0,drops.Length)], 
                        spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);

            Destroy(drop, 6f);
            yield return new WaitForSeconds(0.5f);

            yield return null;
        }
    }
}
