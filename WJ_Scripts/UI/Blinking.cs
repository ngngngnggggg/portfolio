using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    public GameObject Ruin;
    public GameObject dream;

    private LoveHouse m_LoveHouse = null;
    private SoloHouse m_SoloHouse = null;

    private bool m_IsEnd = false;

    [SerializeField] private WJ_Sound sound;
    
    private void Awake()
    {
        Ruin.SetActive(true);
        dream.SetActive(true);
        m_LoveHouse = dream.GetComponent<LoveHouse>();
        m_SoloHouse = Ruin.GetComponent<SoloHouse>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Image image =GetComponent<Image>();
        //StartCoroutine(blinking());
    }

    // Update is called once per frame
    float time = 0;
    //GetComponent<Image>().color;

    // Update is called once per frame
    void Update()
    {
        if (time < 8f)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, time / 5);
        }
        else if (time <9.0f)
        {
            //Ruin.SetActive(true);
            //dream.SetActive(false);
            //GetComponent<Image>().color = new Color(0, 0, 0, 0);
            //map.SetActive(true);
        }
        else if (time < 9.2f)
        {
            if (!m_IsEnd)
            {
                m_IsEnd = true;
                sound.PlayBGM2();
                End();
            } 

        }
        //Debug.Log("o"+ time);
        
        //else if (10f < time)
        //{
        //    GetComponent<Image>().color = new Color(0, 0, 0, 255f);
        //}
        time += Time.deltaTime;
    }
    //public void End()
    //{
    //    //time = 0f;
    //    if (time < 8f)
    //    {
    //        GetComponent<Image>().color = new Color(0, 0, 0, time / 5);
    //    }
    //    else if (9.0f < time)
    //    {
    //        map.SetActive(true);
    //        dream.SetActive(false);
    //        //GetComponent<Image>().color = new Color(0, 0, 0, 0);
    //        //map.SetActive(true);
    //    }
    //    else if (time < 9.2f)
    //    {
    //        GetComponent<Image>().color = new Color(0, 0, 0, 0);
    //    }
    //    else if (10f < time)
    //    {
    //        GetComponent<Image>().color = new Color(0, 0, 0, 255f);
    //    }
    //    time += Time.deltaTime;
    //}


    

    IEnumerator blinking()
    {
        Image image = GetComponent<Image>();
        Text text = GetComponentInChildren<Text>();
        bool on = true;
        int count = 0;
        while (count < 10 && on == true)
        {
            //Debug.Log("On" + on);
            //Debug.Log(count);
            //sound.PlayBGM2();
            image.color = Color.black;
            yield return new WaitForSeconds(0.05f);
            m_LoveHouse.SetRenderers(true);
            m_SoloHouse.SetRenderers(false);
            image.color = new Color(0, 0, 0, 0); // ���̱�

            yield return new WaitForSeconds(0.05f);

            image.color = Color.black;
            yield return new WaitForSeconds(0.05f);
            m_LoveHouse.SetRenderers(false);
            m_SoloHouse.SetRenderers(true);
            image.color = new Color(0, 0, 0, 0); // ���̱�
            yield return new WaitForSeconds(0.05f);
            
            count++;
            
            //if(count >= 9)
            //{
            //    image.color = Color.black;
            //    text.text = "Thank you for Play";

            //}
            //if (count >= 20)
            //{
            //    map.SetActive(true);

            //}
        }
        on= false;
        while(on == false)
        {

            //Debug.Log("off" + on);
            image.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(1f);
            image.color = Color.black;
            yield return new WaitForSeconds(0.05f);
            text.text = "Thank you for Play";
            yield return new WaitForSeconds(2f);
            Application.Quit();
            yield break;
        }
        
        

    }
    public void End()
    {
        StartCoroutine(blinking());
    }
}
