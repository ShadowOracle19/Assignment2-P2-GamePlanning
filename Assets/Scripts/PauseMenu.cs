using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanelUI;
    public GameObject settingPanelUI;
    public bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.isGamePaused = isPaused;
        if (!isPaused)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = true;
                PauseGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = false;
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanelUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanelUI.SetActive(false);
        settingPanelUI.SetActive(false);
        Time.timeScale = 1;
    }
}
