using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public SonicBloom.Koreo.Demos.RhythmGameController gameController;
    public FadeInOut l_Fade;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameController.Resume();
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameController.Pause();
        GameIsPaused = true;
    }

    public void Restart()
    {
        GameIsPaused = false;
        gameController.Restart();
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        l_Fade.FadeToLevel(1);
    }
}
