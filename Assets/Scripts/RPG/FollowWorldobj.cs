using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorldobj : MonoBehaviour
{
    [SerializeField]
    GameObject worldPos;
    [SerializeField]
    RectTransform rectTrans;
    public Vector2 offset;

    void Update () {
        Vector2 screenPos=Camera.main.WorldToScreenPoint(worldPos.transform.position);
        rectTrans.position = screenPos + offset;
    }
}
