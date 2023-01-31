using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloHouse : MonoBehaviour
{
    private Renderer[] m_Renderers = null;
    

    private void Awake()
    {
        
        m_Renderers = this.GetComponentsInChildren<Renderer>();
        SetRenderers(false);
    }

    public void SetRenderers(bool _value)
    {
        for (int i = 0; i < m_Renderers.Length; ++i)
        {
            m_Renderers[i].enabled = _value;
        }
    }
}
