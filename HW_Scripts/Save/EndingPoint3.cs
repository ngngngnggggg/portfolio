using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingPoint3 : MonoBehaviour
{
    [SerializeField] private Image[] image;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine((NextScene()));
        }
    }
    
    IEnumerator NextScene()
    {
        image[0].enabled = true; 
        image[1].enabled = true; 
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("SangMin");
    }
}
