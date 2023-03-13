using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanelUI;
    public GameObject settingPanelUI;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGamePaused)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.isGamePaused = true;
                PauseGame();
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
