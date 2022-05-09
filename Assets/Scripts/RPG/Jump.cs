using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Collider trigger;
    public Animator jump;
    
    public FadeInOut m_Fade;
    private void OnTriggerEnter(Collider other) {
        if (ES3.Load<bool>("pass3") == true)
        {
            jump.SetTrigger("Jump");
            m_Fade.FadeToLevel(5);
        }
    }
}
