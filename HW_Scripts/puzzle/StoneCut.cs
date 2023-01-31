using UnityEngine;

public class StoneCut : MonoBehaviour
{
    [SerializeField] private GameObject stone;
    

    // ReSharper disable Unity.PerformanceAnalysis
    public void DestroyStone()
    {
        Invoke("InVokeStone", 3f);
    }

    void InVokeStone()
    {
        Destroy(gameObject);
        GameObject _stone = Instantiate(stone, transform.position, Quaternion.identity);
        Destroy(_stone, 3f);
    }
    
}
  