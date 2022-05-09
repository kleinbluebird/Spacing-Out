using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForMenu
{
    public class BoyMove : MonoBehaviour
    {
        public float Speed = 5f;
        public Animator animator;
        public DialogueTrigger dialogue1;
        public Collider trigger1;
        public DialogueTrigger dialogue2;
        public Collider trigger2;
        public DialogueTrigger dialogue3;
        public Collider trigger3;
        public DialogueTrigger dialogue4;
        public Collider trigger4;
        
        public GameObject interact1;
        public GameObject interact2;
        public GameObject interact3;

        void Update()
        {
            if (VirtualInputManager.Instance.MoveRight && VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetBool("Move", false);
                return;
            }
            if(!VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetBool("Move", false);
            }
            if (VirtualInputManager.Instance.MoveRight)
            {
                this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                animator.SetBool("Move", true);
            }
            if (VirtualInputManager.Instance.MoveLeft)
            {
                this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                animator.SetBool("Move", true);
            }

            if (this.gameObject.transform.position.x > -230f && this.gameObject.transform.position.x < -200f)
            {
                interact1.SetActive(true);
            }
            else
            {
                interact1.SetActive(false);
            }
            if (this.gameObject.transform.position.x > -20f && this.gameObject.transform.position.x < 15f)
            {
                interact2.SetActive(true);
            }
            else
            {
                interact2.SetActive(false);
            }
            if (this.gameObject.transform.position.x > 208f)
            {
                interact3.SetActive(true);
            }
            else
            {
                interact3.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other == trigger1)
            {
                dialogue1.TriggerDialogue();
            }
            if (other == trigger2)
            {
                dialogue2.TriggerDialogue();
            }
            if (other == trigger3 && ES3.Load<bool>("pass2") == false)
            {
                dialogue3.TriggerDialogue();
            }
            if (other == trigger4 && ES3.Load<bool>("pass3") == false)
            {
                dialogue4.TriggerDialogue();
            }
        }

    }
}

