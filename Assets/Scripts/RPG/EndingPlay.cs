using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlay : MonoBehaviour
{
    public Collider trigger;
    public GameObject collider;
    public GameObject endTrigger;
    public DialogueTrigger dialogue;

    private void OnTriggerEnter(Collider other) {
        if (ES3.Load<bool>("pass3") == true)
        {
            dialogue.TriggerDialogue();
            collider.SetActive(false);
            endTrigger.SetActive(true);
        }
        else
        {
            collider.SetActive(true);
            endTrigger.SetActive(false);
        }
    }
}
