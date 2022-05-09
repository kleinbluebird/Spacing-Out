using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
    public Collider trigger;
    public Animator fog;
    public GameObject fogMonitor;
    public GameObject fogFly;

    private void OnTriggerEnter(Collider other) {
        if (ES3.Load<bool>("pass2") == false)
        {
            fogMonitor.SetActive(true);
        }
        else
        {
            fog.SetTrigger("FogOut");
            fogFly.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
