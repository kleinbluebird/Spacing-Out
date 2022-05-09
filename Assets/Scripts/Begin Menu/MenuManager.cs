using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    public CinemachineBrain mainCamera;
    public CinemachineVirtualCamera frame0_cam;
    public CinemachineVirtualCamera frame1_cam;

    public GameObject[] frame;
    public FadeInOut m_Fade;

    void Update()
    {
        if (Input.anyKeyDown && frame[0].activeInHierarchy)
        {
            frame[0].SetActive(false);
            frame[1].SetActive(true);
            frame1_cam.gameObject.SetActive(false);
            frame0_cam.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !frame[0].activeInHierarchy)
        {
            frame[1].SetActive(false);
            frame[0].SetActive(true);
            frame1_cam.gameObject.SetActive(false);
            frame0_cam.gameObject.SetActive(true);
        }
    }

    public void NewGame()
    {
        ES3.Save("playerPosition", new Vector3(-240f, -11f, 0f));
        ES3.Save("pass1", false);
        ES3.Save("pass2", false);
        ES3.Save("pass3", false);
        m_Fade.FadeToLevel(1);
    }
    public void ContinueGame()
    {
        m_Fade.FadeToLevel(1);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
