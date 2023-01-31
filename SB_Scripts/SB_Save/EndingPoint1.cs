using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndingPoint1 : MonoBehaviour
{
    [SerializeField] private Image[] image;
    [SerializeField] private L_Sound sound;
       
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
           SceneManager.LoadScene("SooBin2");
       }
}
