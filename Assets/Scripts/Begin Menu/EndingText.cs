using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingText : MonoBehaviour
{
    public static EndingText instance { get; set; }
    public Vector3 destination;
    public bool movingstatus = false;
    public float speed=10f;
    private float distance;
    public FadeInOut m_Fade;
 
    void Start()
    {
        distance = Vector3.Distance(gameObject.transform.position, destination);
    }
    void Update()
    {
        if(movingstatus==true)
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, speed * Time.deltaTime);
        }
 
        if(gameObject.transform.position == destination)
        {
            movingstatus = false;
            m_Fade.FadeToLevel(0);
        }
    }
}
