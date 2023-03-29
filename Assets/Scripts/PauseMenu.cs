using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanelUI;
    public GameObject settingPanelUI;
    public GameObject pauseButtonUI;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGamePaused)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        GameManager.Instance.isGamePaused = true;
        pausePanelUI.SetActive(true);
        pauseButtonUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        GameManager.Instance.isGamePaused = false;
        pausePanelUI.SetActive(false);
        settingPanelUI.SetActive(false);
        pauseButtonUI.SetActive(true);
        Time.timeScale = 1;
    }
}
