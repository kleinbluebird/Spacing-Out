using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLoad : MonoBehaviour
{
    public GameObject player;
    public GameObject dragonfly1;
    public GameObject dragonfly2;
    public GameObject dragonfly3;
    public Animator animator2;


    void Start()
    {
        Load();
    }

    public void Load()
    {
        player.transform.position = ES3.Load<Vector3>("playerPosition");
        if (ES3.Load<bool>("pass1") == false)
        {
            dragonfly1.transform.position = new Vector3(-228.53f, -3.15f, -9.24f);
            dragonfly1.transform.rotation = Quaternion.Euler(new Vector3(18f, 180f, 0f));
        }
        else
        {
            dragonfly1.transform.position = new Vector3(-215.45f, -1.53f, -3.81f);
            dragonfly1.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        if (ES3.Load<bool>("pass2") == false)
        {
            dragonfly2.transform.position = new Vector3(4.33f, -6.78f, -4.08f);
            dragonfly2.transform.rotation = Quaternion.Euler(new Vector3(18f, 180f, 0f));
        }
        else
        {
            dragonfly2.transform.position = new Vector3(9.62f, -4.05f, -4.08f);
            dragonfly2.transform.rotation = Quaternion.Euler(new Vector3(18f, 13.97f, 0f));
            animator2.SetTrigger("Fly");

        }
        if (ES3.Load<bool>("pass3") == false)
        {
            dragonfly3.transform.position = new Vector3(218.5f, -2.13f, -16.85f);
            dragonfly3.transform.rotation = Quaternion.Euler(new Vector3(19.29f, 180f, 0f));
        }
        else
        {
            
        }
        
    }
}
