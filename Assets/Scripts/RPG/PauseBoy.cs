using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBoy : MonoBehaviour
{
    public static bool BoyIsPaused = false;
    public GameObject pauseUI;
    public FadeInOut b_Fade;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(BoyIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ForButton()
    {
        if(BoyIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        BoyIsPaused = false;
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        BoyIsPaused = true;
    }

    public void Leave()
    {
        Time.timeScale = 1f;
        b_Fade.FadeToLevel(0);
    }

    public void Level_One()
    {
        b_Fade.FadeToLevel(2);
    }
    public void Level_Two()
    {
        b_Fade.FadeToLevel(3);
    }
    public void Level_Three()
    {
        b_Fade.FadeToLevel(4);
    }
}
