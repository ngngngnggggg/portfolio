using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private GameManager gameMng;

    private MeshRenderer renderer;
    [SerializeField] private Material mat;

    private void Start()
    {
        renderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    //private void OnCollisionEnter(Collision coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    {
    //        gameMng.GameSave();
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player")  )
        {
            renderer.material = mat;
            Debug.Log("??");
            gameMng.GameSave();
        }
    }
}
