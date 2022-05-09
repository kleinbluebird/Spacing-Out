using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.Demos
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform player;
        Vector3 offset;
        private void Start()
        {
            offset = transform.position - player.position;
        }

        private void Update()
        {
            Vector3 targetPos = player.position + offset;
            targetPos.z = 387;
            transform.position = targetPos; 
        }
    }
}

