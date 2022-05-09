using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SonicBloom.Koreo.Demos
{
    public class PlayerMovement : MonoBehaviour
    {
        bool running = true;
        public float speed = 5;
        public Rigidbody rb;
        float horizontalInput;
        public float horizontalMultiplier = 2;

        private void FixedUpdate() {
            if(!running) return;

            Vector3 forwardMove = new Vector3(1,0,0) * speed * Time.fixedDeltaTime;
            Vector3 horizontalMove = -transform.forward * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
            rb.MovePosition(rb.position + forwardMove);
        }
        
        private void Update()
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        public void EndGame()
        {
            running = false;
            Invoke("Restart", 2);
        }

        void Restart()
        {
            //Restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
