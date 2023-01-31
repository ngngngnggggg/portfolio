using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    public bool IsTransparent { get; private set; } = false;

    [SerializeField]
    private MeshRenderer[] renderers;
    private WaitForSeconds delay = new WaitForSeconds(0.001f);
    private WaitForSeconds resetDelay = new WaitForSeconds(0.005f);
    [SerializeField]
    private const float THRESHOLD_ALPHA = 0.25f;
    [SerializeField]
    private const float THRESHOLD_MAX_TIMER = 0.5f;
    [SerializeField]
    private bool isReseting = false;
    private float timer = 0f;
    [SerializeField]
    private Coroutine timeCheckCoroutine;
    [SerializeField]
    private Coroutine resetCoroutine;



    void Awake()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    public void BecomeTransparent()
    {
        if (IsTransparent)
        {
            timer = 0f;
            return;
        }

        // 리셋 중에 오브젝트가 캐릭터를 다시 가리는 경우 
        if (resetCoroutine != null && isReseting)
        {
            isReseting = false;
            IsTransparent = false;
            StopCoroutine(resetCoroutine);
        }
        SetMaterialTransparent();
        Debug.Log("Rendering Mode : Fade");
        IsTransparent = true;
        StartCoroutine(BecomeTransparentCoroutine());


    }

    private void SetMaterialTransparent()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                material.SetFloat("_Mode", 2f);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("Zwrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
            }
        }
    }


    private void SetMaterialOpaque()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("Zwrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
            }
        }
    }

    public void ResetOriginalTransparent()
    {
        SetMaterialOpaque();
        Debug.Log("Rendering Mode : Opaque");
        resetCoroutine = StartCoroutine(ResetOriginalTransparentCoroutine());
    }



    private IEnumerator BecomeTransparentCoroutine()
    {
        while (true)
        {
            bool isComplete = true;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.color.a > THRESHOLD_ALPHA)
                    isComplete = false;

                Color color = renderers[i].material.color;
                color.a -= Time.deltaTime;
                renderers[i].material.color = color;
            }

            if (isComplete)
            {
                CheckTimer();
                break;
            }
            yield return delay;
        }
    }

    private IEnumerator ResetOriginalTransparentCoroutine()
    {
        IsTransparent = false;

        while (true)
        {
            bool isComplete = true;

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.color.a > 1f)
                    isComplete = false;

                Color color = renderers[i].material.color;
                color.a += Time.deltaTime;
                renderers[i].material.color = color;
            }

            if (isComplete)
            {
                isReseting = false;
                break;
            }
        }

        yield return resetDelay;
    }


    public void CheckTimer()
    {
        if (timeCheckCoroutine != null)
            StopCoroutine(timeCheckCoroutine);
        timeCheckCoroutine = StartCoroutine(CheckTimerCouroutine());

    }

    private IEnumerator CheckTimerCouroutine()
    {
        timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;

            if (timer > THRESHOLD_MAX_TIMER)
            {
                isReseting = true;
                ResetOriginalTransparent();
                break;
            }
            yield return null;

        }
    }
}