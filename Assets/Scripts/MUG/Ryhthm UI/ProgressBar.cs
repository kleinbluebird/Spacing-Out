using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{
    public GameObject playerGO;
    public GameObject finishGO;
    Image progressBar;
    float maxDistance;
    public FadeInOut Fade;

    void Start()
    {
        progressBar = GetComponent<Image>();
        maxDistance = finishGO.transform.position.x - 12f;
        progressBar.fillAmount = 0f;
    }

    void Update()
    {
        if(progressBar.fillAmount < 1){
            if(playerGO.transform.position.x<12f)
                progressBar.fillAmount = 0f;
            else
                progressBar.fillAmount = (playerGO.transform.position.x - 12f) / maxDistance;
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                ES3.Save("playerPosition", new Vector3(-205f, -11f, 0f));
                ES3.Save("pass1", true);
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                ES3.Save("playerPosition", new Vector3(-16f, -11f, 0f));
                ES3.Save("pass2", true);
            }
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                ES3.Save("playerPosition", new Vector3(210f, -11f, 0f));
                ES3.Save("pass3", true);
            }
            Fade.FadeToLevel(1);
        }
    }
}
