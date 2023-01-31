using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoveHouse : MonoBehaviour
{
    private Renderer[] m_Renderers = null;
    public GameObject l_Light;

    private void Awake()
    {
        
        m_Renderers = this.GetComponentsInChildren<Renderer>();
    }

    public void SetRenderers(bool _value)
    {
        for(int i = 0; i < m_Renderers.Length; ++i)
        {
            m_Renderers[i].enabled = _value;
            l_Light.SetActive(_value);
        }
    }
}
