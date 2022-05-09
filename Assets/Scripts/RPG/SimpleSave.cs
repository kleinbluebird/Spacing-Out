using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSave : MonoBehaviour
{
    public GameObject player;

    void Start()
    {

    }
    public void Save()
    {
        ES3.Save("playerPosition", player.transform.position);
        bool tmp;
        tmp = ES3.Load<bool>("pass1");
        ES3.Save("pass1", tmp);
        tmp = ES3.Load<bool>("pass2");
        ES3.Save("pass2", tmp);
        tmp = ES3.Load<bool>("pass3");
        ES3.Save("pass3", tmp);
    }
}
