using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
   public GameObject SettingPanel;

    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SettingPanel.activeSelf)
                SettingPanel.gameObject.SetActive(true);

            else

                SettingPanel.SetActive(false);
        }
    }

    public void OnPauseClick()
    {
        // �Ͻ� ���� ���� ��۽�Ŵ
        isPaused = !isPaused;
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;

        var playerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = playerObj.GetComponents<MonoBehaviour>();
        foreach(var script in scripts)
        {
            script.enabled = !isPaused;
        }
       
    }

    public void GameExit()
    {
        //SceneManager.LoadScene("Opening");
        Application.Quit();
    }
}
