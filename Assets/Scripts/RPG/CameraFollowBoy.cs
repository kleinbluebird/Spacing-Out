using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBoy : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    public float smoothSpeed = 0.15f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 pos = transform.position;
        pos.x = target.position.x;
        transform.position = pos;
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

}
