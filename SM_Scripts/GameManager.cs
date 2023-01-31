using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private Transform startPoint;

    
    private void Start()
    {
         GameLoad();
    }
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
        PlayerPrefs.Save(); 

        Debug.Log("�����");
    }
    public void GameLoad()
    {
        if(!PlayerPrefs.HasKey("PlayerX"))
        {
            ReStart();
            Debug.Log("리셋");
        }
        Debug.Log(PlayerPrefs.GetFloat("PlayerX"));

        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            PlayerPrefs.DeleteAll();
            return;
        }

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        player.transform.position = new Vector3(x, y, z);
    }

    public void ReStart()
    {
        PlayerPrefs.SetFloat("PlayerX", startPoint.position.x);
        PlayerPrefs.SetFloat("PlayerY", startPoint.position.y + 0.5f);
        PlayerPrefs.SetFloat("PlayerZ", startPoint.position.z);

        //SceneManager.LoadScene(3);
    }
}
     