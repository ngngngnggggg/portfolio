using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objdata : MonoBehaviour
{
    public int num;
    private SkinnedMeshRenderer mr;
    [SerializeField] private Material mat;
    Material originMat;

    private void Start()
    {
        mr = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        originMat = mr.material;
    }

    public void ChangeColor()
    {
        mr.material = mat;
    }

}
