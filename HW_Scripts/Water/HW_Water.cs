using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HW_Water : MonoBehaviour
{
    [SerializeField] private HW_EventArrow[] eventArrow;
    [SerializeField] private HW_Player player;
    [SerializeField] private L_Item item;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private ParticleSystem waterParticles;
    [SerializeField] private AudioSource waterAudio;
    [SerializeField] private AudioClip waterClip;

    [SerializeField] private GameObject Map;
    
    [SerializeField] private float waterDrag; //물속 중력
    [SerializeField] private float originDrag; //물에서 나왔을 때 원래의 중력

    [SerializeField] private Color waterColor; //물 속의 색상
    [SerializeField] private float waterFogDensity; //물 속의 탁함 정도
    
    [SerializeField] private Color originColor; //물에서 나왔을 때 원래의 색상
    [SerializeField] private float originFogDensity; //물에서 나왔을 때 원래의 물밀도

    private BoxCollider boxCollider;

    private Animator anim;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        originColor = RenderSettings.fogColor;
        originFogDensity = RenderSettings.fogDensity;
        originDrag = 0;
        //시작시 자식 비활성화
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        waterAudio = GetComponent<AudioSource>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            
            Map.SetActive(false);
            Debug.Log("물에 들어감");
            soundManager.GetComponent<SoundManager>().HWStopSound();
            soundManager.GetComponent<SoundManager>().PlayWaterBGM();
            GetWater(other);
            //player에 상속되어있는 파티클 실행
            player.GetComponent<HW_Player>().particle.Play();
            other.GetComponent<HW_Player>().isWater = true;
            if (other.GetComponent<HW_Player>().isWater = true == true)
            {
                //자식 활성화
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            other.GetComponent<Animator>().SetTrigger("isWaterIdle");
            Debug.Log("Enter");
        }
    }

    //물속에선 isGround가 false가 되어야 하므로
    
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<HW_Player>().isGround = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("endSwim");
            item.GetComponent<L_Item>().gameObject.SetActive(true);
            if (player.hasKey == true)
            {
               Destroy(GameObject.Find("EventWaterKey"));
            }
            soundManager.GetComponent<SoundManager>().PlayBGM();
            waterAudio.PlayOneShot(waterClip);
            player.GetComponent<HW_Player>().particle.Stop();
            other.GetComponent<HW_Player>().isWater = false;
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<Animator>().SetTrigger("endSwim");
            GetOutWater(other);
            boxCollider.enabled = false;
            Invoke("OnCollider",5f);
            //자식 비활성화
            OffChildSetActive();
            Invoke("OnChildSetActive", 3f);

        }
    }

    public void OffChildSetActive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnChildSetActive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void OffCollider()
    {
        boxCollider.enabled = false;
    }
    private void OnCollider()
    {
        boxCollider.enabled = true;
    }
    
    private void GetWater(Collider _player)
    {
        
        _player.transform.GetComponent<Rigidbody>().drag = waterDrag;
        StartCoroutine(WaterChangeColor(_player.GameObject()));
    }
    
    
    //1초 후 색상 변경 코루틴 함수
private IEnumerator WaterChangeColor(GameObject _player)
    {
        Debug.Log("inCorointein");
        yield return new WaitForSeconds(1f);
        RenderSettings.fogColor = waterColor;
        RenderSettings.fogDensity = waterFogDensity;
        player.isDive = false;
        yield return new WaitForSeconds(0.5f);
        _player.GetComponent<Rigidbody>().useGravity = false;
    }
    
    
    
    private void GetOutWater(Collider _player)
    {
        
        _player.transform.GetComponent<Rigidbody>().drag = originDrag;
        _player.GetComponent<HW_Player>().isGround = true;
        
        RenderSettings.fogColor = originColor;
        RenderSettings.fogDensity = originFogDensity;
        
    }
}

