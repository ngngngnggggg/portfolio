using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChg : MonoBehaviour
{
    [SerializeField] private HW_Player player;
    [SerializeField] private GameObject light;

    public Material skyOne;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && player.Getislaying)
        {
            light.SetActive(true);
            RenderSettings.skybox = skyOne;
            DynamicGI.UpdateEnvironment();
        }
    }
}
